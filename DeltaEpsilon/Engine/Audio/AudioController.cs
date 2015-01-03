using DeltaEpsilon.Engine.Utils;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine
{
    class AudioController
    {
        public AudioController()
        {
            Instance = this;
        }

        Dictionary<string, SoundBuffer> buffers = new Dictionary<string, SoundBuffer>();
        List<Sound> sounds = new List<Sound>();

        public SoundBuffer GetSoundBuffer(string filename)
        {
            buffers[filename] = new SoundBuffer(FS.CreateStream(filename));
            return buffers[filename];
        }

        public Sound GenerateSound(string filename)
        {
            Sound sound = new Sound(GetSoundBuffer(filename));
            sounds.Add(sound);
            return sound;
        }

        public Sound GenerateSound(System.IO.Stream stream)
        {
            buffers["" + stream.GetHashCode()] = new SoundBuffer(stream);
            Sound sound = new Sound(buffers["" + stream.GetHashCode()]);
            sounds.Add(sound);
            return sound;
        }

        public Music GenerateMusic(string filename)
        {
            return new Music(FS.CreateStream(filename));
        }

        public void PlaySound(string filename, float volume = 20f, float pitch = 1f)
        {
            Sound sound = GenerateSound(filename);
            sound.Volume = volume;
            sound.Pitch = pitch;
            sound.Play();
        }

        public void PlaySound(System.IO.Stream stream, float volume = 20f, float pitch = 1f)
        {
            Sound sound = GenerateSound(stream);
            sound.Volume = volume;
            sound.Pitch = pitch;
            sound.Play();
        }

        long nextUpdate = 0;

        public void Update()
        {
            if (Time.Millis < nextUpdate) return;
            nextUpdate = Time.Millis + 500;
            for (int i = 0; i < sounds.Count; i++)
            {
                if (sounds[i].Status == SoundStatus.Stopped)
                {
                    sounds[i].Dispose();
                    sounds.RemoveAt(i);
                    i--;
                }
            }
        }

        public static AudioController Instance { get; private set; }
    }
}
