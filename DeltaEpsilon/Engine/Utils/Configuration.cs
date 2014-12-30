using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DeltaEpsilon.Engine.Utils
{
    public class Configuration
    {
        Dictionary<string, object> keys = new Dictionary<string, object>();

        public object this[string key]
        {
            get
            {
                return keys[key];
            }
            set
            {
                if (!keys.ContainsKey(key))
                    keys.Add(key, value);
                else
                    keys[key] = value;
            }
        }

        public bool HasKey(string key)
        {
            return keys.ContainsKey(key);
        }

        public object GetOrDefault(string key, object def)
        {
            if (!HasKey(key))
                this[key] = def;
            return this[key];
        }

        public T GetOrDefault<T>(string key, T def)
        {
            if (!HasKey(key))
                this[key] = def;
            return (T)this[key];
        }

        public static Configuration Load(string content)
        {
            Configuration configuration = new Configuration();
            foreach (string line in content.Split('\n'))
            {
                if (line.Replace(" ", "").Replace("\t", "").IndexOf("//") == 0) continue;
                string[] split = line.Split(new string[] { "=" }, 2, StringSplitOptions.None);
                if (split.Length != 2) continue;

                object obj = null;

                int i = 0;
                float f = 0;

                if (int.TryParse(split[1].Replace(" ", "").Replace("\t", ""), out i)) { obj = i; }
                else if (float.TryParse(split[1].Replace(" ", "").Replace("\t", "").Replace('.', ','), out f)) { obj = f; }
                else if (split[1].Replace(" ", "").Replace("\t", "") == "true") { obj = true; }
                else if (split[1].Replace(" ", "").Replace("\t", "") == "false") { obj = false; }
                else
                {
                    if (split[1].IndexOf('"') != -1 && split[1].IndexOf('"') != split[1].LastIndexOf('"'))
                    {
                        obj = split[1].Substring(split[1].IndexOf('"') + 1, split[1].LastIndexOf('"') - split[1].IndexOf('"') - 1);
                    }
                }

                if (obj != null)
                {
                    configuration.keys.Add(split[0].Replace(" ", "").Replace("\t", ""), obj);
                }
            }
            return configuration;
        }

        public static Configuration LoadFromFile(string filename, bool createIfNotFound = false)
        {
            if (createIfNotFound && !File.Exists(filename)) File.WriteAllText(filename, "");
            return Load(File.ReadAllText(filename));
        }

        public string Save()
        {
            string lines = "";
            foreach (string s in keys.Keys)
            {
                if (keys[s] is string)
                    lines += s + " = \"\{keys[s].ToString()}\"\n";
                else
                    lines += s + " = \{keys[s].ToString().ToLower()}\n";
            }
            return lines;
        }

        public void SaveToFile(string filename)
        {
            File.WriteAllText(filename, Save());
        }
    }
}
