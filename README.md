# プログラミング言語「ちょくだい」

## はじめに
この言語は、「ちょく」と「だい」で構成されるプログラミング言語です。「ちょくだい」とは、AtCoderの社長であり、競技プログラマーである高橋直大さんのハンドルネームであるchokudaiのことです。名前を使用させてくださった高橋直大さんに感謝申し上げます。

## 言語仕様

### 使われる文字及び文字列
この言語では、「ちょく」、「だい」、「tourist」及び空白(全角スペースである)、改行が用いられる。これ以外の文字及び文字列は使用しないでください(できないとは言ってない)。また、この言語の予約語は、全て「ちょく」と「だい」のみで構成され、「tourist」は、ユーザーが定義する関数名と変数名にのみ使用できる。空白と改行は文字列を区切る際に用いられる。

### ソースコードの文字コード
この言語のソースコードの文字コードはUnicodeでなければならない。その他の文字コードの場合の動作は保証されていない。

### トークン
トークンは、識別子、キーワード、演算子、区切り文字、又はリテラルを表す。

#### 区切り文字
区切り文字は、トークンを分割するための文字で、空白と改行が区切り文字として使用される。

#### 識別子
識別子は、変数や型に対し名前を付けるものである。識別子は、「ちょく」、「だい」及び「tourist」から構成される。但し、一部の識別子は、キーワード又は演算子として定義されているため、使用することができない。また、同じソースファイル内ですでに使われた識別子を使うことはできない。

#### キーワード
キーワードは、別記の通りである(キーワード一覧.csvを参照のこと)。

#### 整数リテラル
整数リテラルは、整数を表すものである。2進数で書かれる。「ちょく」を1、「だい」を0として用いる。整数リテラルは、「ちょく」と空白の後に符号を表す「ちょく」と「だい」、「ちょく」と「だい」で表された2進数を書くことで表される。符号は、「ちょく」が負の数、「だい」が正の数を表す。例えば、+10を表す場合は「ちょく　だいちょくだいちょくだい」と書く。但し0を「ちょく　だい」または「ちょく　ちょく」と表してはならない。

#### 文字リテラル
文字リテラルは、文字を表す。Unicodeを用いて表す。「だい」と空白の後に、表したい文字のコードを書くことで表される。文字コードは、整数と同じく2進数で表される。例えば、「あ」を表す場合は「だい　だいだいちょくちょくだいだいだいだいだいちょくだいだいだいだいちょくだい」と書く。但し、文字コードについて、初めの0を表す「だい」を省略してもよい。例えば、「あ」を「だい　ちょくちょくだいだいだいだいだいちょくだいだいだいだいちょくだい」と表してもよい。

#### 文字列リテラル
文字列リテラルは、文字列を表す。文字列は、文字の集まりとして表す。「だいだい」と空白の後に、文字数を表す整数リテラル、文字列を構成する文字の文字リテラルを空白区切りで書くことで表される。例えば、「高橋直大」を表す文字列は「だいだい　ちょく　だいちょくだいだい　だい　ちょくだいだいちょくちょくだいちょくだいちょくちょくだいちょくちょくだいだいだい　だい　だいちょくちょくだいちょくだいちょくだいだいちょくだいだいちょくだいちょくちょく　だい　だいちょくちょくちょくだいちょくちょくだいちょくちょくちょくちょくだいちょくだいだい　だい　だいちょくだいちょくちょくだいだいちょくだいだいちょくだいだいちょくちょくちょく」と表す。

#### リスト
リストはデータの集まりである。整数のリスト、文字のリスト、文字列のリストだけではなく、整数リストのリスト、文字列リストのリストのリストなど、任意のリストを作ることができる。また、リストの要素は互いに型が異なっていてもよい。ただし、文字のリストと文字列は同等ではない。「ちょくだい　(要素の数)　(要素)」のように記述すればよい。リストにできる操作については「リスト操作」の項を参照すること。

#### deque(両端キュー)
「だいだいちょくだいだい」と記述することで、dequeを生成できる。変数に格納しないと、dequeの操作ができないことに注意すること。dequeにできる操作については「dequeの操作」の項を参照すること。

#### priority queue(優先度付きキュー)
「だいちょくだいちょくだい」と記述することで、priority queueを生成できる。追加する要素の型は統一されていなければならず、その型は、整数型、文字型、文字列型でなければならない。変数に格納しないと操作できないことに注意すること。priority queueにできる操作については「priority queueの操作」の項を参照すること。

#### 真偽値の扱い
真偽値は整数を用いて表現される。0は偽を表し、0以外は真を表す。

### 型
型は、変数の持つ値の性質を表す。型には、整数型、文字型、文字列型、リスト型、deque型、priority queue型が存在する。

### 関数

#### 関数の定義
関数は、関数外でのみ定義できる。「ちょくちょくちょく　(関数名)　(引数の個数)　(引数の名前)」のように記述することで、関数を定義できる。2つ以上の引数を取りたい場合は、引数の名前を空白区切りで取りたい数だけ書けばよい。関数の定義を終了するには最後に「ちょくちょくだい」と書く必要がある。ないと実行時にエラーが発生する。いくつかの関数はあらかじめ定義されている。

#### 関数のスコープ
任意の関数は、それよりあとに記述されている関数を実行することができる。例えば、1行目~5行目で定義されている関数f内で、6行目以降で定義されている関数gを実行できる。

#### 関数の終了
関数の定義の最後に「ちょくちょくだい」を一つだけ書く必要がある。

#### 戻り値
関数は値を返すことができる。「ちょくだいだい　(返り値)」と書くと、関数は値を返して終了する。「ちょくだいだい」を通過せずに「ちょくちょくだい」を通った場合、戻り値を返さずに関数は終了する。

#### エントリーポイント
「ちょくだいちょくだい」関数はプログラムの実行時に自動的に呼ばれる。プログラム内にこの名前の関数が存在しない場合、実行時にエラーが生じる。また、この関数は引数を受け取ることができない。

#### プリセット関数
あらかじめ定義されている関数は、別記のキーワード一覧を参照のこと。

### 変数

#### 変数の定義
変数は、関数内でのみ定義できる。「ちょくちょく　(変数名)　(初期値)」のように記述することで、変数を宣言できる。

#### 変数のスコープ
別の関数内の変数を参照することはできない。また、同じ関数内でも、後の行で定義された関数を参照できない。

#### 再代入
全ての変数は再代入を許されている。変数を定義する場合と同様に、「ちょくちょく　(変数名)　(再代入する値)」と記述すればよい。但し、リストの要素の変更は「だいだいだい」を使用すること(使い方は「リスト操作」内の「要素の変更」を参照すること)。この言語は動的型付けであるため、文字列の入っている変数に、整数を再代入してもエラーは生じない。

### 値の扱いについて
値は基本的に関数に渡すものであって、関数に評価されない値を直接書いてはいけない。そのようなことに対応するのが面倒だからである。但し、値を返す関数について、値を返す以外に重要な操作を伴うものについては直接書いてよいものがある(dequeやpriority queueのpop等)。

### 分岐
場合に応じて別々の処理をさせることができる。

だいだいだいだい　(条件式1)
　(処理1)
だいだいだいちょく　(条件式2)
　(処理2)
だいだいだいちょく　(条件式3)
　(処理3)
だいだいちょくだい

のように書けばよい。なお、「だいだいだいちょく」は任意の個数書くことができる。「だいだいだいだい」の後の条件式が真ならば処理1が実行され、その後「だいだいちょくだい」に飛ぶ。偽なら次の「だいだいだいちょく」に飛び、その条件式を評価する。真なら処理を実行し、その後「だいだいちょくだい」に飛ぶ。「だいだいだいちょく」が存在しない場合は「だいだいちょくだい」に飛ぶ。

### 反復
処理を反復させることができる。

だいだいちょくちょく　(条件式)
　(処理)
だいちょくだいちょく

のように書けばよい。条件式が真の場合は処理が実行され、再び評価される。条件式が偽の場合は「だいちょくだいちょく」に飛ぶ。また、処理の中で「だいちょくちょくだい」を通過したとき、無条件で「だいちょくだいちょく」に飛ぶ。

### 算術に関する標準関数
算術に関する関数はすべて2値関数である。

#### 加法
「ちょくちょくちょくちょく　(数)　(数)」と記述することで、2数を足した結果が返ってくる。

#### 減法
「ちょくちょくちょくだい　(数1)　(数2)」と記述することで、数1から数2を引いた結果が返ってくる。

#### 乗法
「ちょくちょくだいちょく　(数)　(数)」と記述することで、2数を掛けた結果が返ってくる。

#### 除法
「ちょくちょくだいだい　(数1)　(数2)」と記述することで、数1を数2で割った結果が返ってくる。

#### 剰余
「ちょくちょくだいだいだい　(数1)　(数2)」と記述することで、数1を数2で割った余りが返ってくる。数1が負数の場合は結果も負数であり、正数の場合は結果は正数である。

### 論理演算
「ちょくだいだいちょく　(演算内容)　(数1)　(数2)」と記述することで、論理演算ができる。演算内容は、数1と数2がどのようなときにどのような値を返すかを指定するもので、「ちょく」と「だい」によってあらわされる。一つ目から順に、数1が偽かつ数2が偽、数1が偽かつ数2が真、数1が真かつ数2が偽、数1が真かつ数2が真の場合の結果を表す。「ちょく」が真、「だい」が偽を表す。

### 比較演算
数字の大小関係を調べるものである。不等号は大なりのみ用意してある。

#### 等号
「だいだいだいだいちょく　(値1)　(値2)」と書くと、値1と値2が等しいときに真(1)を、等しくないときに偽(0)を返す。

#### 不等号(大なり)
「だいだいだいだいだい　(値1)　(値2)」と書くと、値1>値2が成立したときに真(1)を、等しくないときに偽(0)を返す。

### 標準出力
「だいちょく　(出力する値)」と書くことで、値を出力できる。値は整数型、文字型、文字列型のものを指定できる。

### 標準入力
標準入力に関する命令は、「ちょくだいちょく」、「ちょくだいだいだい」、「ちょくだいだいだいだい」の3つである。

#### 整数の受け取り(ちょくだいちょく)
「ちょくだいちょく」は整数を受け取るものである。「ちょくだいちょく　(受け取る変数)」と書くことで、半角スペースや改行で区切られた整数を1つ受け取り、指定された変数に代入できます。但し、何もない行を挟む場合は後述の「ちょくだいだいだいだい」を使うこと。

#### 文字の受け取り(ちょくだいだいだい)
「ちょくだいだいだい」は文字列を受け取るものである。「ちょくだいだいだい　(受け取る変数)」と書くことで、半角スペースや改行で区切られた文字列を1つ受け取り、指定された変数に代入できる。空白も含めて受け取りたい場合は、標準入力を1行受け取る「ちょくだいだいだいだい」を使用すること。

#### 文字列の受け取り(ちょくだいだいだいだい)
「ちょくだいだいだいだい」は文字列を受け取るものである。「ちょくだいだいだいだい　(受け取る変数)」と書くことで、入力を1行分受け取り、指定された変数に代入できる。

### リスト操作
整数リスト、文字列(=文字リスト)、文字列リストなどのリストについて、要素の追加や削除などのいくつかの操作ができる。リストのindexはすべて0から始まる。

#### 階層
リストのリストの要素を簡潔に記すために、階層という概念を用意した。(他言語でいうところの、[]の数である。(e.g. list[0][0]は2階層である。))リストそのものをn階層としたとき、リストの中の要素はn+1階層である。生のリストは0階層である。要するに、n次配列の要素はn階層なのである。

例えば、
ちょくちょく　list　ちょくだい　ちょく　だいちょくだい　ちょくだい　だいちょくだい　ちょく　だいちょく　ちょく　だいちょくだい　ちょくだい　ちょく　だいちょくだい　ちょく　だいちょくちょく　ちょく　だいちょくだいだい
としたとき、listの中身は[[1, 2], [3, 4]]のようになっている。listそのものは0階層であり、list内のリストは1階層、list内のリスト内の要素は2階層である。

リストに関する多くの命令について、階層を書く必要がある。

#### 要素数の取得
「だいちょくちょく　(リスト)　(階層)　(数字)」と記述することで、リストの要素数を取得できる。階層には、要素数を求めたいリストの階層を数字で書くこと。数字は階層の分だけ書く必要があり、i番目にはi階層のリストのindexを書く。
例えば、
ちょくちょく　list　ちょくだい　ちょく　だいちょくだい　ちょくだい　ちょく　だいちょくちょく　ちょく　だいだい　ちょく　だいちょく　ちょく　だいちょくだい　ちょくだい　ちょく　だいちょくだい　ちょく　だいちょくちょく　ちょく　だいちょくだいだい
としたとき、listの中身は[[0, 1, 2], [3, 4]]であり、「だいちょくちょく　list　ちょく　だいちょく　ちょく　だいだい」と書くと、list内の0番目のリストである[0, 1, 2]の大きさである3が返ってくる。

#### 要素の取得
リストの要素が欲しいとき、「だいちょくだい　(リスト)　(階層)　(数字)」と記述することで取得できる。ただし、階層は要素の階層である。
例えば、
ちょくちょく　list　ちょくだい　ちょく　だいちょくだい　ちょくだい　ちょく　だいちょくちょく　ちょく　だいだい　ちょく　だいちょく　ちょく　だいちょくだい　ちょくだい　ちょく　だいちょくだい　ちょく　だいちょくちょく　ちょく　だいちょくだいだい
としたとき、listの中身は[[0, 1, 2], [3, 4]]であり、「だいちょくだい　list　ちょく　だいちょくだい　ちょく　だいちょく　ちょく　だいちょく」と書くと、1番目のリスト[3, 4]の1番目の要素4が返ってくる。

#### 要素への代入
「だいだいだい　(リスト)　(階層)　(数字)　(値)」と記述することで、指定したリストの指定した場所に値を代入できる。指定に仕方は上と同様である。　

#### 要素の追加
リストの要素の前にaを挿入したい場合は、「だいちょくちょくちょく　(リスト)　(階層)　(数字)　a」と記述すればよい。数字の一番最後に書かれた数字をiとして、aは新たなリストでi番目の要素となり、それ以降の要素は一つ後ろにずれるまた、引数に指定するリストは変数でなければならない(リストのリテラルに対して挿入することは無意味であり、実装するに値しない)。

#### 要素の削除
リストの要素を削除したい場合は、「だいちょくちょくだい　(リスト)　(階層)　(数字)」と記述すればよい。最後の数字をiとして、i番目以降の要素は一つ前にずれる。iに-1を指定すると末尾の要素が削除される。また、引数に指定するリストは変数でなければならない(リストのリテラルに対して削除することは無意味であり、実装するに値しない)。

#### ソート
リストが整数リスト、文字リスト、文字列リストのいずれかの場合にのみリストをソートできる。「だいだいだいちょくだい　(リスト名)」と書くことで昇順ソート、「だいだいだいちょくちょく　(リスト名)」と書くことで降順ソートができる。階層ですか？知らない子ですね。

### deque
dequeは双方向キューである。

#### 構築
「だいだいちょくだいだい」と記述することで生成できる。変数に格納して使用すること。

#### 要素の追加
「だいだいちょくちょくちょく　(dequeの名前)　(値)」と記述することで先頭に、「だいちょくだいだいだい　(dequeの名前)　(値)」と記述することで末尾に要素を追加できる。

#### 要素の削除
「だいだいちょくだいちょく　(dequeの名前)」と記述することで先頭の、「だいだいちょくちょくだい　(dequeの名前)」と記述することで末尾の要素を削除できる。返り値は削除された要素である。

#### 要素数
「だいちょくだいだいちょく　(dequeの名前)」と書くことで、要素の数が分かる。

### priority queue
優先度付きキューである。最大のものから順にpopされる。(最小のものからpopしたい場合、マイナスをつけるなり要素をいじるなりで対応すること)

#### 構築
「だいちょくだいちょくだい」と書くことで生成できる。変数に格納して使用すること。

#### 要素の追加
「だいちょくちょくだいだい　(名前)　(値)」と書くことで、値を追加できる。ただし、値は整数型、文字型、文字列型のいずれかでなければならず、また、全ての要素は同じ型でなければならない。

#### 要素の削除
「だいちょくだいちょくちょく　(名前)」と書くことで、最大の要素を削除できる。

#### 要素数
「だいちょくちょくだいちょく　(名前)」と書くことで、要素数が分かる。