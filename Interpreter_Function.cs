using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chokudai
{
    partial class Interpreter
    {
		// ユーザー定義関数とプリミティブな関数を同等に扱うためのもの
		abstract class Function
        {
            public int arg_num { get; protected set; }
            public abstract dynamic Run(dynamic[] args);
        }

		// ユーザー定義関数
		class UserDefinedFunction : Function
        {
            Interpreter interpreter; // 親er
            List<string> commands; // 命令
            Dictionary<string, dynamic> vars; // 変数名->値

			public UserDefinedFunction(Interpreter _interpreter, List<string> _commands)
            {
                interpreter = _interpreter;
                commands = _commands;

                int now_index = 1;
                arg_num = GetInt(ref now_index);
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
                int len = GetVal(ref now_index); // 文字列の長さ

                // 文字読み込み
                string s = "";
                for (int i = 0; i < len; ++i)
                {
                    char ch = GetVal(ref now_index);
                    s += ch;
                }
                return s;
            }
            List<int> GetIntList(ref int now_index)
            {
                now_index++;

                // 要素数読み込み
                int len = GetVal(ref now_index);

                // 要素読み込み
                var list = new List<int>(len);
                for (int i = 0; i < len; ++i)
                {
                    list[i] = GetVal(ref now_index);
                }
                return list;
            }
            List<string> GetStrList(ref int now_index)
            {
                now_index++;

                // 要素数読み込み
                int len = GetVal(ref now_index);

                // 要素読み込み
                var list = new List<string>();

                for (int i = 0; i < len; ++i)
                {
                    list.Add(GetVal(ref now_index));
                }
                return list;
            }
            dynamic GetVal(ref int now_index)
            {
                string command = commands[now_index];
                if (command == "ちょく")
                {
                    return GetInt(ref now_index);
                }
                else if (command == "ちょくだい")
                {
                    return GetIntList(ref now_index);
                }
                else if (command == "だい")
                {
                    return GetChar(ref now_index);
                }
                else if (command == "だいだい")
                {
                    return GetStr(ref now_index);
                }
                else if (command == "だいだいだい")
                {
                    return GetStrList(ref now_index);
                }
				else if(command == "だいちょくちょく") // リストの要素数を求める
                {
                    ++now_index;
                    var list = GetVal(ref now_index);
                    if (list is string) return list.Length;
                    else return list.Count;
                }
				else if(command == "だいちょくだい") // ランダムアクセス
                {
                    ++now_index;
                    var list = GetVal(ref now_index);
                    int index = GetVal(ref now_index);
                    return list[index];
                }
                else if (interpreter.funcs.ContainsKey(command)) // 関数呼び出し
                {
                    return CallFunc(command, ref now_index);
                }
                else return vars[commands[now_index++]];
            }

			// 関数呼び出し
			dynamic CallFunc(string name, ref int now_index)
            {
                var function = interpreter.funcs[name];
                int arg_num2 = function.arg_num;
                dynamic[] args2 = new dynamic[arg_num2];
                now_index++;
                for (int i = 0; i < arg_num2; ++i)
                {
                    args2[i] = GetVal(ref now_index);
                }
                return function.Run(args2);
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

            // リストに要素を追加
            // index: 要素を追加する場所(デフォルトで末尾)
            void ListInsert(string name, dynamic val, int index)
            {
                var list = vars[name];
                if (index == -1) list.Add(val);
                else list.Insert(index, val);
            }

            // リストから要素を削除
            // indexはListInsertに同じ
            void ListDelete(string name, int index = -1)
            {
                var list = vars[name];
                list.RemoveAt((index < 0 ? list.Count - 1 : index));
            }

            public override dynamic Run(dynamic[] args)
            {
                vars = new Dictionary<string, dynamic>();
                int now_index = 3;

                // 引数を変数リストに格納
                for (int i = 0; i < arg_num; ++i)
                {
                    vars.Add(commands[now_index], args[i]);
                    now_index++;
                }

                string command = commands[now_index];
                while (command != "ちょくちょくだい")
                {
                    command = commands[now_index];
                    now_index++;
                    if (command == "ちょくちょく") // 変数の定義or代入
                    {
                        string name = commands[now_index];
                        now_index++;
                        SetVar(name, GetVal(ref now_index));
                    }
                    else if (command == "ちょくだいちょく") // 数字の入力の受け取り
                    {
                        string name = commands[now_index];
                        SetVar(name, int.Parse(interpreter.ReadStr()));
                        now_index++;
                    }
                    else if (command == "ちょくだいだいだい") // 文字列の入力の受け取り
                    {
                        string name = commands[now_index];
                        SetVar(name, interpreter.ReadStr());
                        now_index++;
                    }
                    else if (command == "ちょくだいだいだいだい") // 一行丸々受け取り
                    {
                        string name = commands[now_index];
                        SetVar(name, Console.ReadLine());
                        now_index++;
                    }
                    else if (command == "だいちょく") // 標準出力
                    {
                        Console.Write(GetVal(ref now_index));
                    }
					else if (command == "だいちょくちょくちょく") // リストへの要素の追加
                    {
                        string name = commands[now_index++];
                        dynamic element = GetVal(ref now_index);
                        int index = GetVal(ref now_index);
                        ListInsert(name, element, index);
                    }
					else if (command == "だいちょくちょくだい") // リストの要素の削除
                    {
                        string name = commands[now_index++];
                        int index = GetVal(ref now_index);
                        ListDelete(name, index);
                    }
                    else if (command == "ちょくだいだい") // return
                    {
                        return GetVal(ref now_index);
                    }
                    else if (command == "ちょくちょくだい") // 関数の終了
                    {
                        return null;
                    }
                    else if (interpreter.funcs.ContainsKey(command)) // 関数呼び出し
                    {
                        CallFunc(command, ref now_index);
                    }
                    else if (vars.ContainsKey(command)) // 変数参照
                    {
                        GetVal(ref now_index);
                    }
                    else
                    {
                        throw new Exception($"no function or variable named {command} found.\n current index is {now_index - 1}.");
                    }
                }
                return null;
            }
        }

		// プリミティブな関数
		class PrimitiveFunction : Function
        {
            Func<dynamic[], dynamic> func;

            public PrimitiveFunction(int _arg_num, Func<dynamic[], dynamic> _func)
            {
                arg_num = _arg_num;
                func = _func;
            }

            public override dynamic Run(dynamic[] args)
            {
                return func(args);
            }
        }
    }
}
