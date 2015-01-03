using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Security.Cryptography;
using System.Windows.Forms;

namespace DeltaEpsilon.Engine
{
    public class FS
    {
        /*
            DEP - DeltaEpsilon Package

            Header:
            4 bytes - DEP0
            4 bytes (int) No. of Files

            File Info Block:
            4 bytes (int) Offset in File
            4 bytes (int) Length
            2 bytes (short) Name Length
            x bytes (string) Name
        */

        struct FileInfo
        {
            public string name;
            public int offset;
            public int length;
            public string package;
        }

        static byte[] DecryptBytes(byte[] bytes, int length, byte[] key, byte[] iv)
        {
            var provider = new TripleDESCryptoServiceProvider();

            MemoryStream stream = new MemoryStream(bytes);
            CryptoStream crypto = new CryptoStream(stream, provider.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            BinaryReader reader = new BinaryReader(crypto);

            return reader.ReadBytes(length);
        }

        static byte[] iv = new byte[] { 0x22, 0x59, 0xF6, 0x59, 0x07, 0xE8, 0xC7, 0x38 };
        static byte[] key = new byte[] { 0x93, 0x28, 0x7F, 0x78, 0x4E, 0x16, 0x34, 0x1F, 0x78, 0xAD, 0xE6, 0xE5, 0x14, 0xB3, 0xD2, 0xB7, 0x5F, 0x04, 0x25, 0x82, 0x18, 0xCF, 0x7E, 0x0A };

        List<FileInfo> files = new List<FileInfo>();

        public static byte[] ReadAllBytes(string filename)
        {
            if (File.Exists("data/" + filename))
            {
                return File.ReadAllBytes("data/" + filename);
            }
            foreach (FileInfo info in Instance.files)
            {
                if (info.name == filename)
                {
                    FileStream stream = File.OpenRead(info.package);
                    stream.Position = info.offset;
                    byte[] bytes = new byte[info.length];
                    stream.Read(bytes, 0, info.length);
                    stream.Close();
                    return DecryptBytes(bytes, info.length, key, iv);
                }
            }
            MessageBox.Show("Can not find " + filename + " in any package!", "DeltaEpsilon", MessageBoxButtons.OK, MessageBoxIcon.Error);
            throw new FileNotFoundException("Can not find " + filename + " in any package!");
        }

        public static string ReadAllText(string filename)
        {
            if (File.Exists("data/" + filename))
            {
                return File.ReadAllText("data/" + filename);
            }
            foreach (FileInfo info in Instance.files)
            {
                if (info.name == filename)
                {
                    FileStream stream = File.OpenRead(info.package);
                    stream.Position = info.offset;
                    byte[] bytes = new byte[info.length];
                    stream.Read(bytes, 0, info.length);
                    stream.Close();
                    return Encoding.ASCII.GetString(DecryptBytes(bytes, info.length, key, iv));
                }
            }
            MessageBox.Show("Can not find " + filename + " in any package!", "DeltaEpsilon", MessageBoxButtons.OK, MessageBoxIcon.Error);
            throw new FileNotFoundException("Can not find " + filename + " in any package!");
        }

        public static Stream CreateStream(string filename)
        {
            if (File.Exists("data/" + filename))
            {
                return File.OpenRead("data/" + filename);
            }
            foreach (FileInfo info in Instance.files)
            {
                if (info.name == filename)
                {
                    FileStream stream = File.OpenRead(info.package);
                    stream.Position = info.offset;
                    byte[] bytes = new byte[info.length];
                    stream.Read(bytes, 0, info.length);
                    stream.Close();
                    return new MemoryStream(DecryptBytes(bytes, info.length, key, iv));
                }
            }
            MessageBox.Show("Can not find " + filename + " in any package!", "DeltaEpsilon", MessageBoxButtons.OK, MessageBoxIcon.Error);
            throw new FileNotFoundException("Can not find " + filename + " in any package!");
        }

        public static FS Instance = new FS();

        public FS()
        {
            if (!Directory.Exists("packages")) return;
            foreach (string filename in Directory.GetFiles("packages"))
            {
                FileStream stream = File.OpenRead(filename);
                byte[] bytes = new byte[4];
                stream.Position = 4;
                stream.Read(bytes, 0, 4);
                int filesCount = BitConverter.ToInt32(bytes, 0);
                for (int i = 0; i < filesCount; i++)
                {
                    bytes = new byte[4];
                    stream.Read(bytes, 0, 4);
                    int offset = BitConverter.ToInt32(bytes, 0);
                    bytes = new byte[4];
                    stream.Read(bytes, 0, 4);
                    int length = BitConverter.ToInt32(bytes, 0);
                    bytes = new byte[2];
                    stream.Read(bytes, 0, 2);
                    short strLen = BitConverter.ToInt16(bytes, 0);
                    bytes = new byte[strLen];
                    stream.Read(bytes, 0, strLen);
                    string str = Encoding.ASCII.GetString(bytes);
                    files.Add(new FileInfo { name = str, offset = offset, length = length, package = filename });
                }
                stream.Close();
            }
        }
    }
}
