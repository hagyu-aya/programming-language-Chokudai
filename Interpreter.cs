using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chokudai
{
    class Interpreter
    {

        List<string> commands;
        public Interpreter(List<string> _commands)
        {
            commands = _commands;
        }

        int ToInt(string s)
        {
            int ret = 0;
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
            return ret;
        }

        char ToChar(string s)
        {
            int ret = 0;
            int i = 0;
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
            return (char)ret;
        }

        string ToStr(ref int now_line)
        {
            // 文字数読み込み
            int len = 0; // 文字列の長さ
            if (commands[now_line] == "ちょく") // 定数の場合
            {
                now_line++;
                len = ToInt(commands[now_line]);
                now_line++;
            }
            else
            {
                // TODO: 変数の場合
            }

            // 文字読み込み
            string s = "";
            for (int i = 0; i < len; ++i)
            {
                if (commands[now_line] == "だい")
                {
                    now_line++;
                    char ch = ToChar(commands[now_line]);
                    s += ch;
                    now_line++;
                }
                else
                {
                    // TODO: 変数の場合
                }
            }
            return s;
        }

        public void Run()
        {
            int now_line = 0;
            while (now_line < commands.Count)
            {
                string command = commands[now_line];
                if (command[0] == 'ち')
                {
                    if (command == "ちょく") // 数字を表す
                    {
                        now_line++;
                        int val = ToInt(commands[now_line]);
                        Console.Write(val);
                        now_line++;
                    }
                }
                else
                {
                    if (command == "だい") // 文字を表す
                    {
                        now_line++;
                        char ch = ToChar(commands[now_line]);
                        Console.Write(ch);
                        now_line++;
                    }
                    else if (command == "だいだい") // 文字列を表す
                    {
                        now_line++;
                        string s = ToStr(ref now_line);
                        Console.Write(s);
                    }

                }
            }
        }
    }
}
