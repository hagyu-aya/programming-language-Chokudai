using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chokudai
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("an argument is needed");
                return;
            }
            var sr = new StreamReader(args[0]);
            var commands = new List<string>();
            while (sr.Peek() > -1)
            {
                var tmp = sr.ReadLine().Split('　');
                foreach (var s in tmp)
                {
                    if(s != "") commands.Add(s);
                }
            }

            Interpreter interpreter = new Interpreter(commands);
            interpreter.Run();
        }
    }
}
