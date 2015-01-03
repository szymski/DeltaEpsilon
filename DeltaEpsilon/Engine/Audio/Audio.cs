using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine
{
    public class Audio
    {

        public virtual void _PlaySound(string filename, float volume = 20f, float pitch = 1f) => AudioController.Instance.PlaySound(filename, volume, pitch);
        public virtual void _PlaySound(Stream stream, float volume = 20f, float pitch = 1f) => AudioController.Instance.PlaySound(stream, volume, pitch);

        public static void PlaySound(string filename, float volume = 20f, float pitch = 1f) => Instance._PlaySound(filename, volume, pitch);
        public static void PlaySound(Stream stream, float volume = 20f, float pitch = 1f) => Instance._PlaySound(stream, volume, pitch);

        public static Audio Instance { get; set; }
    }
}
