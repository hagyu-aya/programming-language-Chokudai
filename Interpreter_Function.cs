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
            Dictionary<int, Tuple<int, int>> ifs; // ifのindex->(else ifのindex, 終了場所のindex)
            Dictionary<int, int> whiles; // whileのindex->終了場所のindex

            public UserDefinedFunction(Interpreter _interpreter, List<string> _commands)
            {
                interpreter = _interpreter;
                commands = _commands;
                ifs = new Dictionary<int, Tuple<int, int>>();
                whiles = new Dictionary<int, int>();

                int now_index = 1;
                arg_num = GetInt(ref now_index);

                now_index += arg_num + 1;

                var if_stack = new Stack<Queue<int>>();
                var while_stack = new Stack<int>();

                while (now_index < commands.Count)
                {
                    string command = commands[now_index];
                    if (command == "だいだいだいだい" && commands[now_index - 1] != "ちょく" && commands[now_index - 1] != "だい") // if
                    {
                        if_stack.Push(new Queue<int>());
                        if_stack.Peek().Enqueue(now_index);
                    }
                    else if (command == "だいだいだいちょく" && commands[now_index - 1] != "ちょく" && commands[now_index - 1] != "だい") // else if
                    {
                        if_stack.Peek().Enqueue(now_index);
                    }
                    else if (command == "だいだいちょくだい" && commands[now_index - 1] != "ちょく" && commands[now_index - 1] != "だい") // end if
                    {
                        int end = now_index;
                        var que = if_stack.Peek();
                        int fst = que.Dequeue();
                        while (que.Count != 0)
                        {
                            int snd = que.Dequeue();
                            ifs.Add(fst, new Tuple<int, int>(snd, end));
                            fst = snd;
                        }
                        ifs.Add(fst, new Tuple<int, int>(end, end));
                    }
                    else if (command == "だいだいちょくちょく" && commands[now_index - 1] != "ちょく" && commands[now_index - 1] != "だい") // while
                    {
                        while_stack.Push(now_index);
                    }
                    else if (command == "だいちょくだいちょく" && commands[now_index - 1] != "ちょく" && commands[now_index - 1] != "だい") // end while
                    {
                        int begin = while_stack.Pop();
                        int end = now_index;
                        whiles.Add(begin, end);
                    }
                    now_index++;
                }
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
            List<dynamic> GetList(ref int now_index)
            {
                now_index++;

                int len = GetVal(ref now_index);

                var list = new List<dynamic>();
                for (int i = 0; i < len; ++i)
                {
                    list.Add(GetVal(ref now_index));
                }
                return list;
            }

            // 任意の値を取得する　通常はこれを使う
            dynamic GetVal(ref int now_index)
            {
                string command = commands[now_index];
                if (command == "ちょく")
                {
                    return GetInt(ref now_index);
                }
                else if (command == "ちょくだい")
                {
                    return GetList(ref now_index);
                }
                else if (command == "だい")
                {
                    return GetChar(ref now_index);
                }
                else if (command == "だいだい")
                {
                    return GetStr(ref now_index);
                }
                else if (command == "だいだいちょくだいだい") // deque構築
                {
                    now_index++;
                    return new Deque<dynamic>();
                }
                else if(command == "だいちょくだいちょくだい") // priority_queue構築
                {
                    now_index++;
                    return new PriorityQueue<long>();
                }
                else if (command == "だいちょくちょく") // リストの要素数を求める
                {
                    ++now_index;
                    var list = GetVal(ref now_index);
                    int rank = GetVal(ref now_index);
                    for (int i = 0; i < rank; ++i)
                    {
                        list = list[GetVal(ref now_index)];
                    }
                    if (list is string) return list.Length;
                    return list.Count;
                }
                else if (command == "だいちょくだい") // ランダムアクセス
                {
                    ++now_index;
                    var list = GetVal(ref now_index);
                    int rank = GetVal(ref now_index);
                    for (int i = 0; i < rank; ++i)
                    {
                        list = list[GetVal(ref now_index)];
                    }
                    return list;
                }
                else if (command == "ちょくだいだいちょく") // 論理演算
                {
                    ++now_index;
                    string s = commands[now_index++];
                    int[] res = new int[4];
                    int t = 0;
                    for (int i = 0; i < 4; ++i)
                    {
                        if (s[t] == 'ち')
                        {
                            res[i] = 1;
                            t += 3;
                        }
                        else if (s[t] == 'だ')
                        {
                            res[i] = 0;
                            t += 2;
                        }
                    }
                    int a = Interpreter.BoolToInt(!EqualToZero(GetVal(ref now_index)));
                    int b = Interpreter.BoolToInt(!EqualToZero(GetVal(ref now_index)));
                    return res[a * 2 + b];
                }
                else if (command == "だいだいちょくだいちょく") // deq pop_front
                {
                    var deq = vars[commands[++now_index]];
                    now_index++;
                    return deq.PopFront();
                }
                else if (command == "だいだいちょくちょくだい") // deq pop_back
                {
                    var deq = vars[commands[++now_index]];
                    now_index++;
                    return deq.PopBack();
                }
                else if(command == "だいちょくだいちょくちょく") // pq pop
                {
                    var pq = vars[commands[++now_index]];
                    now_index++;
                    return pq.Pop();
                }
                else if(command == "だいちょくちょくだいちょく") // pq count
                {
                    var pq = vars[commands[++now_index]];
                    now_index++;
                    return pq.count;
                }
                else if(command == "だいちょくだいだいちょく") // deq count
                {
                    var deq = vars[commands[++now_index]];
                    now_index++;
                    return deq.count;
                }
                else if (interpreter.funcs.ContainsKey(command)) // 関数呼び出し
                {
                    now_index++;
                    return CallFunc(command, ref now_index);
                }
                else return vars[commands[now_index++]];
            }

            // 0と空文字列、空リストを0と見なす
            bool EqualToZero(dynamic val)
            {
                if (val is int || val is char) return val == 0;
                if (val is string) return val == "";
                return val.Count == 0;
            }

            int Comparison<T>(List<T> x, List<T> y) where T : IComparable
            {
                for (int i = 0; i < x.Count && i < y.Count; ++i)
                {
                    if (x[i].CompareTo(y[i]) > 0) return 1;
                    if (x[i].CompareTo(y[i]) > 0) return -1;
                }
                if (x.Count > y.Count) return 1;
                if (x.Count < y.Count) return -1;
                return 0;
            }

            // 関数呼び出し
            dynamic CallFunc(string name, ref int now_index)
            {
                var function = interpreter.funcs[name];
                int arg_num2 = function.arg_num;
                dynamic[] args2 = new dynamic[arg_num2];
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
            void ListInsert(string name, int[] index, dynamic val)
            {
                var list = vars[name];
                int rank = index.Length;
                for (int i = 0; i < rank - 1; ++i)
                {
                    list = list[index[i]];
                }
                int id = index[rank - 1];
                if (id == -1) id = list.Count;
                list.Insert(id, val);
            }

            // リストの要素に値を代入
            void SetListElement(string name, int[] index, dynamic val)
            {
                var list = vars[name];
                int rank = index.Length;
                for (int i = 0; i < rank; ++i)
                {
                    list = list[index[i]];
                }
                list = val;
            }

            // リストから要素を削除
            // indexはListInsertに同じ
            void ListDelete(string name, int[] index)
            {
                var list = vars[name];
                int rank = index.Length;
                for (int i = 0; i < rank - 1; ++i)
                {
                    list = list[index[i]];
                }
                int id = index[rank - 1];
                list.RemoveAt((id < 0 ? list.Count - 1 : id));
            }

            public override dynamic Run(dynamic[] args)
            {
                vars = new Dictionary<string, dynamic>();
                var while_stack = new Stack<int>();
                var if_stack = new Stack<int>();
                int now_index = 3;

                // 引数を変数リストに格納
                for (int i = 0; i < arg_num; ++i)
                {
                    vars.Add(commands[now_index], args[i]);
                    now_index++;
                }

                while (true)
                {
                    string command = commands[now_index];
                    if (command == "ちょくちょくだい" && !(commands[now_index - 1] == "ちょく" || commands[now_index - 1] == "だい")) break;
                    now_index++;
                    if (command == "ちょくちょく") // 変数の定義or代入
                    {
                        string name = commands[now_index];
                        now_index++;
                        SetVar(name, GetVal(ref now_index));
                    }
                    else if (command == "だいだいだいだい") // if始まり
                    {
                        int begin = now_index - 1;
                        if (EqualToZero(GetVal(ref now_index))) now_index = ifs[begin].Item1;
                        else if_stack.Push(begin);
                    }
                    else if (command == "だいだいだいちょく") // else if
                    {
                        int begin = now_index - 1;
                        if (if_stack.Count == 0 || ifs[if_stack.Peek()].Item1 != begin)
                        {
                            if (EqualToZero(GetVal(ref now_index))) now_index = ifs[begin].Item1;
                            else if_stack.Push(begin);
                        }
                        else
                        {
                            now_index = ifs[if_stack.Pop()].Item2 + 1;
                        }
                    }
                    else if (command == "だいだいちょくだい") // if文終わり
                    {
                        if_stack.Pop();
                    }
                    else if (command == "だいだいちょくちょく") // while文始まり
                    {
                        int begin = now_index - 1;
                        if (EqualToZero(GetVal(ref now_index))) now_index = whiles[begin] + 1;
                        else while_stack.Push(begin);
                    }
                    else if (command == "だいちょくだいだい") // break
                    {
                        now_index = whiles[while_stack.Pop()];
                    }
                    else if (command == "だいちょくだいちょく") // while文終わり
                    {
                        now_index = while_stack.Pop();
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
                    else if (command == "だいだいだい")
                    {
                        string name = commands[now_index++];
                        int rank = GetVal(ref now_index);
                        var list = vars[name];
                        for (int i = 0; i < rank - 1; ++i)
                        {
                            list = list[GetVal(ref now_index)];
                        }
                        int id = GetVal(ref now_index);
                        var val = GetVal(ref now_index);
                        list[id] = val;
                    }
                    else if (command == "だいちょくちょくちょく") // リストへの要素の追加
                    {
                        string name = commands[now_index++];
                        int rank = GetVal(ref now_index);
                        int[] index = new int[rank];
                        for (int i = 0; i < rank; ++i)
                        {
                            index[i] = GetVal(ref now_index);
                        }
                        dynamic element = GetVal(ref now_index);
                        ListInsert(name, index, element);
                    }
                    else if (command == "だいちょくちょくだい") // リストの要素の削除
                    {
                        string name = commands[now_index++];
                        int rank = GetVal(ref now_index);
                        int[] index = new int[rank];
                        for (int i = 0; i < rank; ++i)
                        {
                            index[i] = GetVal(ref now_index);
                        }
                        ListDelete(name, index);
                    }
                    else if (command == "だいだいだいちょくだい") // 昇順ソート
                    {
                        string name = commands[now_index++];
                        var list = vars[name];
                        list.Sort();
                    }
                    else if (command == "だいだいだいちょくちょく") //降順ソート
                    {
                        string name = commands[now_index++];
                        var list = vars[name];

                        list.Sort();
                        list.Reverse();
                    }
                    else if (command == "だいだいちょくだいちょく") // deq pop_front
                    {
                        var deq = vars[commands[now_index++]];
                        deq.PopFront();
                    }
                    else if (command == "だいだいちょくちょくだい") // deq pop_back
                    {
                        var deq = vars[commands[now_index++]];
                        deq.PopBack();
                    }
                    else if (command == "だいだいちょくちょくちょく") // deq push_front
                    {
                        var deq = vars[commands[now_index++]];
                        deq.PushFront(GetVal(ref now_index));
                    }
                    else if (command == "だいちょくだいだいだい") // deq push_back
                    {
                        var deq = vars[commands[now_index++]];
                        deq.PushBack(GetVal(ref now_index));
                    }
                    else if(command == "だいちょくだいちょくちょく") // pq pop
                    {
                        var pq = vars[commands[now_index++]];
                        pq.Pop();
                    }
                    else if(command == "だいちょくちょくだいだい") // pq push
                    {
                        var pq = vars[commands[now_index++]];
                        pq.Push(GetVal(ref now_index));
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
