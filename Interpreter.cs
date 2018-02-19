using System;
using System.Collections.Generic;

namespace Chokudai
{
    class Interpreter
    {
        List<string> commands;
        Dictionary<string, dynamic> vars;
        Queue<string> input_que;

        public Interpreter(List<string> _commands)
        {
            commands = _commands;
            vars = new Dictionary<string, dynamic>();
            input_que = new Queue<string>();
        }

        // Getxxのnow_indexは終了時、xxを表すコマンドの直後のindexに変更される
        int GetInt(ref int now_index)
        {
            int ret = 0;
            now_index++;
            string s = commands[now_index];
            int i = (s[0] == 'ち' ? 3 : 2);
            while (i < s.Length)
            {
                ret *= 2;
                if (s[i] == 'ち')
                {
                    ret++;
                    i += 3;
                }
                else
                {
                    i += 2;
                }
            }
            if (s[0] == 'ち') ret *= -1;
            now_index++;
            return ret;
        }
        char GetChar(ref int now_index)
        {
            int ret = 0;
            int i = 0;
            now_index++;
            string s = commands[now_index];
            while (i < s.Length)
            {
                ret *= 2;
                if (s[i] == 'ち')
                {
                    ret++;
                    i += 3;
                }
                else
                {
                    i += 2;
                }
            }
            now_index++;
            return (char)ret;
        }
        string GetStr(ref int now_index)
        {
            now_index++;

            // 文字数読み込み
            int len = GetInt(ref now_index); // 文字列の長さ

            // 文字読み込み
            string s = "";
            for (int i = 0; i < len; ++i)
            {
                char ch = GetChar(ref now_index);
                s += ch;
            }
            return s;
        }
        dynamic GetVal(ref int now_index)
        {
            string command = commands[now_index];
            if (command == "ちょく")
            {
                return GetInt(ref now_index);
            }
            else if (command == "だい")
            {
                return GetChar(ref now_index);
            }
            else if (command == "だいだい")
            {
                return GetStr(ref now_index);
            }
            else return vars[commands[now_index++]];
        }

        // 変数の定義or代入
        void SetVar(string name, dynamic val)
        {
            // すでに宣言されていたら代入
            if (vars.ContainsKey(name))
            {
                vars[name] = val;
            }
            // 未定義なら変数を定義
            else
            {
                vars.Add(name, val);
            }
        }

        // 関数呼び出し
        dynamic CallFunc(string name)
        {
            throw new NotImplementedException("thrown by CallFunc");
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
                
                if (command[0] == 'ち')
                {
                    if(command == "ちょくちょく") // 変数の定義or代入
                    {
                        string name = commands[now_index + 1];
                        now_index += 2;
                        SetVar(name, GetVal(ref now_index));
                    }
                    else if(command == "ちょくだいちょく") // 数字の入力の受け取り
                    {
                        string name = commands[now_index + 1];
                        SetVar(name, int.Parse(ReadStr()));
                        now_index += 2;
                    }
                    else if(command == "ちょくだいだいだい") // 文字列の入力の受け取り
                    {
                        string name = commands[now_index + 1];
                        SetVar(name, ReadStr());
                        now_index += 2;
                    }
                    else if(command == "ちょくだいだいだいだい") // 一行丸々受け取り
                    {
                        string name = commands[now_index + 1];
                        SetVar(name, Console.ReadLine());
                        now_index += 2;
                    }
                }
                if (command[0] == 'だ')
                {
                    if(command == "だいちょく") // 出力
                    {
                        now_index++;
                        Console.Write(GetVal(ref now_index));
                    }
                }
                
            }
        }
    }
}
