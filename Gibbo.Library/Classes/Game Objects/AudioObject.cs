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
using System.ComponentModel;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

#if WINDOWS
using NAudio.Wave;
#endif

using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// Audio Game Object
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract]
    public class AudioObject : GameObject
    {
        #region fields
#if WINDOWS
        [NonSerialized]
        private IWavePlayer waveOutDevice;

        [NonSerialized]
        private LoopStream mainOutputStream;
#endif
        [DataMember]
        private string filePath = string.Empty;
        [DataMember]
        private bool loop = false;
        [DataMember]
        private bool isPlaying = false;
        [DataMember]
        private bool playOnStart = false;
        [DataMember]
        private float volume = 1.0f;

        [NonSerialized]
        private string lastLoadedPath = string.Empty;

        #endregion

        #region properties

        /// <summary>
        /// The relative path to the audio file
        /// </summary>
#if WINDOWS
        [EditorAttribute(typeof(ContentBrowserEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Audio Properties")]
        [DisplayName("File Path"), Description("The relative path to the audio file")]
#endif
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        /// <summary>
        /// Determine if the audio will loop in the end
        /// </summary>
#if WINDOWS
        [Category("Audio Properties")]
        [DisplayName("Loop"), Description("Determine if the audio will loop in the end")]
#endif
        public bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }

        /// <summary>
        /// 
        /// </summary>
#if WINDOWS
        [Category("Audio Properties")]
        [DisplayName("Volume"), Description("Audio Volume")]
#endif
        public float Volume
        {
            get { return volume; }
            set
            {
                volume = value;

                if (volume > 1) volume = 1;
                else if (volume < 0) volume = 0;

                if (IsPlaying)
                {
                    mainOutputStream.Volume = volume;
                }
            }
        }

        /// <summary>
        /// The audio position of the current track
        /// </summary>
#if WINDOWS
        [Category("Audio Properties")]
        [DisplayName("Position"), Description("The audio position of the current track")]
        [Browsable(false)]
#endif
        public long Position
        {
            get
            {
                if (mainOutputStream != null)
                    return mainOutputStream.Position;
                else
                    return -1;
            }
            set
            {
                if (mainOutputStream != null)
                    mainOutputStream.Position = value;
            }
        }

        /// <summary>
        /// Determine if the audio is playing
        /// </summary>
#if WINDOWS
        [Category("Audio Properties")]
        [DisplayName("Is Playing"), Description("Determine if the audio is playing")]
        [Browsable(false)]
#endif
        public bool IsPlaying
        {
            get { return (waveOutDevice.PlaybackState == PlaybackState.Playing ? true : false); }
        }

        /// <summary>
        /// Determine if the audio will automatically play when a scene is loaded
        /// </summary>
#if WINDOWS
        [Category("Audio Properties")]
        [DisplayName("Play On Start"), Description("Determine if the audio will automatically play when a scene is loaded")]
#endif
        public bool PlayOnStart
        {
            get { return playOnStart; }
            set { playOnStart = value; }
        }

#if WINDOWS
        [Category("Object Properties")]
        [DisplayName("Disabled"), Description("Determines if the object is disabled or not")]
#endif
        public override bool Disabled
        {
            get { return disabled; }
            set
            {
                disabled = value;
                if (disabled) this.Stop();
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
#if WINDOWS
            waveOutDevice = new DirectSoundOut(50);
#endif
            if (playOnStart)
            {
                Play();
            }
        }

        /// <summary>
        /// Plays the selected audio file
        /// </summary>
        public void Play()
        {
            if (this.Disabled) return;

            string _filePath = SceneManager.GameProject.ProjectPath + "//" + filePath;

            if (SceneManager.IsEditor) return;

#if WINDOWS
            if (_filePath != lastLoadedPath || waveOutDevice == null)
            {

                if (!File.Exists(filePath)) return;

                WaveChannel32 stream = Audio.CreateInputStream(_filePath, volume, false);

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

                    lastLoadedPath = _filePath;
                }
            }
            else
            {
                waveOutDevice.Play();
            }
#endif
        }

        /// <summary>
        /// Pauses the selected audio file
        /// </summary>
        public void Pause()
        {
            if (waveOutDevice != null) waveOutDevice.Pause();
        }

        /// <summary>
        /// Stops the selected audio file
        /// </summary>
        public void Stop()
        {
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
                mainOutputStream.Position = 0;
            }
        }

        #endregion
    }
}
