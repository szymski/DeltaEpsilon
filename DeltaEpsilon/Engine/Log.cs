using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DeltaEpsilon.Engine
{
    public static class Log
    {
        public static List<string> all = new List<string>();

        public static void ClearLog() => File.WriteAllText("log.txt", "");

        public static void Print(object obj)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(obj);
            all.Add(obj.ToString());
            File.AppendAllText("log.txt", "<\{DateTime.Now.ToString()}> \{obj}\n");
        }

        public static void PrintError(object obj)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + obj);
            File.AppendAllText("log.txt", "<\{DateTime.Now.ToString()}> ERROR! \{obj.ToString()}\n");
        }
    }
}
