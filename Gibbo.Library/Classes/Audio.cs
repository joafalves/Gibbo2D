#region Copyrights
/*
Gibbo2D - Copyright - 2013 Gibbo2D Team
Founders - Joao Alves <joao.cpp.sca@gmail.com> and Luis Fernandes <luisapidcloud@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. 
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if WINDOWS
using NAudio.Wave;
#endif

namespace Gibbo.Library
{
    // enum playbackstate? playing stopped paused
    #if WINDOWS
    class MappedWaveChannel
    {
        public WaveChannel32 WaveChannel { get; set; }
        public long LastPosition { get; set; }
    }
    #endif

    /// <summary>
    /// Class to play Audio
    /// </summary>
    /// 
    public static class Audio
    {
        #region fields

        private static bool initialized = false;
        private static int bufferCount = 0;

#if WINDOWS
        private static IWavePlayer waveOutDevice;
        private static LoopStream mainOutputStream;
        private static WaveMixerStream32 mixer;
        private static Dictionary<int, MappedWaveChannel> buffer = new Dictionary<int, MappedWaveChannel>();
#endif

        #endregion

        #region methods

        static Audio()
        {
            if (!initialized)
            {
#if WINDOWS
                try
                {
                    mixer = new WaveMixerStream32();
                    mixer.AutoStop = false;

                    waveOutDevice = new DirectSoundOut(50);
                    waveOutDevice.Init(mixer);

                    initialized = true;
                }
                catch (Exception driverCreateException)
                {
                    Console.WriteLine(String.Format("{0}", driverCreateException.Message));
                    return;
                }
#endif
            }
        }

        internal static void Initialize()
        {
            
        }

        /// <summary>
        /// Clears the Audio Buffer
        /// </summary>
        public static void ClearBuffer()
        {
#if WINDOWS
            buffer.Clear();
#endif
            bufferCount = 0;
        }

        /// <summary>
        /// Unloads audio wave
        /// </summary>
        public static void Unload()
        {
#if WINDOWS
            if (waveOutDevice != null)
            {
                waveOutDevice.Dispose();
            }

            if (mainOutputStream != null)
            {
                mainOutputStream.Dispose();
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        public static int LoadSoundToBuffer(string filepath, float volume)
        {
#if WINDOWS
            WaveChannel32 channel = CreateInputStream(filepath, volume, true);

            if (channel == null) return -1;

            buffer[bufferCount] = new MappedWaveChannel() { WaveChannel = channel, LastPosition = 0 };

            bufferCount++;

            return (bufferCount - 1);
#elif WINRT
            return -1;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="loop"></param>
        public static void PlayFromBuffer(int key)
        {
#if WINDOWS
            if (buffer.ContainsKey(key))
            {
                PlayAudioFromBuffer(buffer[key]);
            }
#endif
        }

#if WINDOWS
        internal static void PlayAudioFromBuffer(MappedWaveChannel param)
        {
            param.WaveChannel.Position = param.LastPosition;
            //param.WaveChannel.Position = 0;

            if (!(waveOutDevice.PlaybackState == PlaybackState.Playing))
                waveOutDevice.Play();
        }
#endif

        public static void StopFromBuffer(int key)
        {
#if WINDOWS
            if (buffer.ContainsKey(key))
            {
                StopAudioFromBuffer(buffer[key]);
            }
#endif
        }

#if WINDOWS
        internal static void StopAudioFromBuffer(MappedWaveChannel param)
        {
            param.WaveChannel.Position = param.WaveChannel.Length;
        }
#endif

        public static void PauseFromBuffer(int key)
        {
#if WINDOWS
            if (buffer.ContainsKey(key))
            {
                buffer[key].LastPosition = buffer[key].WaveChannel.Position;
                StopFromBuffer(key);
            }
#endif
        }

        /// <summary>
        /// Play a .mp3 or .wav audio file
        /// </summary>
        /// <param name="filePath">The relative path to the audio file</param>
        /// <param name="loop">Determine if the sound should loop</param>
        public static void PlayAudio(string filePath, bool loop) // testa isto  <- pelos scripts
        {
            if (!initialized) return;

            // Add the project path
            filePath = SceneManager.GameProject.ProjectPath + "//" + filePath;

#if WINDOWS
            WaveChannel32 stream = CreateInputStream(filePath, 1, false);
            
            if (stream != null)
            {
                mainOutputStream = new LoopStream(stream);
                mainOutputStream.EnableLooping = loop;

                try
                {
                    waveOutDevice.Init(mainOutputStream);
                }
                catch (Exception initException)
                {
                    Console.WriteLine(String.Format("{0}", initException.Message), "Error Initializing Output");
                    return;
                }

                waveOutDevice.Play();
            }
#endif
        }

        /// <summary>
        /// Play a .mp3 or .wav audio file
        /// </summary>
        /// <param name="filePath">The relative path to the audio file</param>
        public static void PlayAudio(string filePath)
        {
            PlayAudio(filePath, false);
        }

#if WINDOWS
        /// <summary>
        /// Creates a wave stream from a selected file
        /// </summary>
        /// <param name="fileName">The file path to the audio file (.mp3 or .wav)</param>
        /// <param name="volume">The default desired volume</param>
        /// <param name="addToBuffer">Determines if the audio file is added to the buffer</param>
        /// <returns></returns>
    public static WaveChannel32 CreateInputStream(string fileName, float volume, bool addToBuffer)
    {
        WaveStream stream;
        WaveOffsetStream offset;
        WaveChannel32 channel;

        if (fileName.ToLower().EndsWith(".wav"))
        {
            stream = new WaveFileReader(fileName);
        }
        else if (fileName.ToLower().EndsWith(".mp3"))
        {
            stream = new Mp3FileReader(fileName);
        }
        else
        {
            Console.WriteLine("Audio format not supported");
            return null;
        }

        stream = new WaveFormatConversionStream(new WaveFormat(44100, 1), stream);
        offset = new WaveOffsetStream(stream);
        channel = new WaveChannel32(offset);
        channel.Volume = volume;

        if(addToBuffer)
            mixer.AddInputStream(channel);

        return channel;
    }
#endif

        #endregion
    }
}
