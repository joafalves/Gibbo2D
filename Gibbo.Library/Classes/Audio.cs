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

using NAudio.Wave;
using System.IO;
using System.Security.Cryptography;


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

        static byte[] ConvertNonSeekableStreamToByteArray(Stream NonSeekableStream)
        {
            MemoryStream ms = new MemoryStream();
            byte[] buffer = new byte[1024];
            int bytes;
            while ((bytes = NonSeekableStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, bytes);
            }
            byte[] output = ms.ToArray();
            return output;
        }
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

        FileInfo aAudioPath = new FileInfo(fileName);
        string encryFilename = fileName + ".encry";
        FileInfo aEncryptedAudio = new FileInfo(encryFilename);

        // The file exists?
        if (!aAudioPath.Exists && !aEncryptedAudio.Exists) return null;

        if (aEncryptedAudio.Exists) // !SceneManager.IsEditor
        {
            //Through the des decryption
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //Read through the documents flow
            FileStream fs = File.OpenRead(encryFilename);

            //Get file binary characters
            byte[] inputByteArray = new byte[fs.Length];

            //Reading the stream file
            fs.Read(inputByteArray, 0, (int)fs.Length);

            //Close the stream
            fs.Close();

            // Secret Key Retrieval
            // Open the file.
            string projectPath = System.IO.Path.Combine(SceneManager.GameProject.ProjectPath, "Data.dat");

            FileStream fStream = new FileStream(projectPath, FileMode.Open);

            // Read from the stream and decrypt the data.
            byte[] decryptedArray = Encryption.DecryptDataFromStreamWithoutEntropy(DataProtectionScope.CurrentUser, fStream, 230);

            fStream.Close();

            //A key array
            byte[] keyByteArray = decryptedArray;

            //Define hash variables
            SHA1 ha = new SHA1Managed();

            //Calculation of the specified byte group designated area hash value
            byte[] hb = ha.ComputeHash(keyByteArray);

            //The encryption key array
            byte[] sKey = new byte[8];

            //Encryption variables
            byte[] sIV = new byte[8];

            for (int i = 0; i < 8; i++)
                sKey[i] = hb[i];

            for (int i = 8; i < 16; i++)
                sIV[i - 8] = hb[i];

            //Access to the encryption key
            des.Key = sKey;

            //Encryption variables
            des.IV = sIV;

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);

            cs.FlushFinalBlock();

            //fs = File.OpenWrite(fileName + ".decrypt.mp3");

            //foreach (byte b in ms.ToArray())
            //{

            //    fs.WriteByte(b);

            //}

            //fs.Close();

            // Read Decrypted file
            //FileStream ffs = new FileStream(fileName + ".decrypt.mp3", FileMode.Open, FileAccess.Read);

            Stream myStream = new MemoryStream(ms.ToArray());

            if (fileName.ToLower().EndsWith(".wav"))
            {
                stream = new WaveFileReader(myStream);
            }
            else if (fileName.ToLower().EndsWith(".mp3"))
            {
                stream = new Mp3FileReader(myStream);
            }
            else
            {
                Console.WriteLine("Audio format not supported");
                return null;
            }
            
            //fs.Close();
            //cs.Close();
            //ms.Close();
        }
        else
        {
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
        }

        stream = new WaveFormatConversionStream(new WaveFormat(44100, 1), stream);
        offset = new WaveOffsetStream(stream);
        channel = new WaveChannel32(offset);
        channel.Volume = volume;

        if(addToBuffer)
            mixer.AddInputStream(channel);

        return channel;
    }
        #endregion
    }


}
