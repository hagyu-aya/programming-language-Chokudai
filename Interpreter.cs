using System;
using System.Collections.Generic;

namespace Chokudai
{
    partial class Interpreter
    {
        List<string> commands; // 読み込んだ命令
        Dictionary<string, dynamic> vars; // 変数名->値
        Dictionary<string, Function> funcs; // 関数名->(index(関数名を指す), 引数の数)
        Queue<string> input_que; // 受け取った入力を格納

        public Interpreter(List<string> _commands)
        {
            commands = _commands;
            vars = new Dictionary<string, dynamic>();
            funcs = new Dictionary<string, Function>();
            input_que = new Queue<string>();
            
        }

        // 関数を定義する
        void DefFunc(string name, List<string> commands)
        {
            var udf = new UserDefinedFunction(this, commands);
            funcs.Add(name, udf);
        }


        // 空白で区切られた文字の読み込み
        string ReadStr()
        {
            if (input_que.Count == 0)
            {
                var inputs = Console.ReadLine().Split();
                foreach (string s in inputs)
                {
                    input_que.Enqueue(s);
                }
            }
            return input_que.Dequeue();
        }
        
        public void Run()
        { 
            int now_index = 0;
            while (now_index < commands.Count)
            {
                string command = commands[now_index];
                now_index++;
                
                if(command == "ちょくちょくちょく")
                {
                    int func_begin = now_index;
                    string func_name = commands[now_index];
                    while (now_index < commands.Count && commands[now_index] != "ちょくちょくだい") now_index++;
                    int func_end = now_index;
                    var func_commands = commands.GetRange(func_begin, func_end - func_begin + 1);
                    DefFunc(func_name, func_commands);
                    
                }
            }
            funcs["ちょくだいちょくだい"].Run(null);
        }
    }
}
