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
        [DisplayName("Volume"), Description("Audio Volume")]
#endif
        public float Volume
        {
            get { return volume; }
            set { volume = value; }
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
            get { return isPlaying; }
            set { isPlaying = value; }
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
            string _filePath = SceneManager.GameProject.ProjectPath + "//" + filePath;

            if (SceneManager.IsEditor) return;
#if WINDOWS
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
            }
#endif
        }

        #endregion
    }
}
