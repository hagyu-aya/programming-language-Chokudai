# プログラミング言語「ちょくだい」

## はじめに
この言語は、「ちょく」と「だい」で構成されるプログラミング言語です。ネタです。許して。

## 言語仕様

### 使われる文字及び文字列
この言語では、「ちょく」、「だい」、「tourist」及び空白(全角スペースである)、改行が用いられる。これ以外の文字及び文字列は使用しないでください(できないとは言ってない)。また、この言語の予約語は、全て「ちょく」と「だい」のみで構成され、「tourist」は、ユーザーが定義する関数名と変数名にのみ使用できる。空白と改行は文字列を区切る際に用いられる。

### ソースコードの文字コード
この言語のソースコードの文字コードはUnicodeでなければならない。その他の文字コードの場合の動作は保証されていない。

### トークン
トークンは、識別子、キーワード、演算子、区切り文字、又はリテラルを表す。

#### 区切り文字
区切り文字は、トークンを分割するための文字で、空白が区切り文字として使用される。
また、ステートメントの終端には改行を用いる。

#### 識別子
識別子は、変数や型に対し名前を付けるものである。識別子は、「ちょく」、「だい」及び「tourist」から構成される。但し、一部の識別子は、キーワード又は演算子として定義されているため、使用することができない。また、同じソースファイル内ですでに使われた識別子を使うことはできない。

#### キーワード
キーワードは、別記の通りである(註: まだ書かれていない)。

#### 整数リテラル
整数リテラルは、整数を表すものである。2進数で書かれる。「ちょく」を1、「だい」を0として用いる。整数リテラルは、「ちょく」と空白の後に符号を表す「ちょく」と「だい」、「ちょく」と「だい」で表された2進数を書くことで表される。符号は、「ちょく」が負の数、「だい」が正の数を表す。例えば、+10を表す場合は「ちょく　だいちょくだいちょくだい」と書く。

#### 整数リスト
整数リストは、整数の集まりを表す。「ちょくだい」と空白の後に、要素の数を表す整数リテラル、要素を表す整数リテラルを空白区切りで書くことで表される。例えば、{8, 1, 0} のような整数リストは、「ちょくだい　ちょく　だいちょくちょく　ちょく　だいちょくだいだいだい　ちょく　だいちょく　ちょく　だいだい」と表す。

#### 文字リテラル
文字リテラルは、文字を表す。Unicodeを用いて表す。「だい」と空白の後に、表したい文字のコードを書くことで表される。文字コードは、整数と同じく2進数で表される。例えば、「あ」を表す場合は「だい　だいだいちょくちょくだいだいだいだいだいちょくだいだいだいだいちょくだい」と書く。但し、文字コードについて、初めの0を表す「だい」を省略してもよい。例えば、「あ」を「だい　ちょくちょくだいだいだいだいだいちょくだいだいだいだいちょくだい」と表してもよい。

#### 文字列リテラル
文字列リテラルは、文字列を表す。文字列は、文字の集まりとして表す。「だいだい」と空白の後に、文字数を表す整数リテラル、文字列を構成する文字の文字リテラルを空白区切りで書くことで表される。例えば、「高橋直大」を表す文字列は「だいだい　ちょく　だいちょくだいだい　だい　ちょくだいだいちょくちょくだいちょくだいちょくちょくだいちょくちょくだいだいだい　だい　だいちょくちょくだいちょくだいちょくだいだいちょくだいだいちょくだいちょくちょく　だい　だいちょくちょくちょくだいちょくちょくだいちょくちょくちょくちょくだいちょくだいだい　だい　だいちょくだいちょくちょくだいだいちょくだいだいちょくだいだいちょくちょくちょく」と表す。

#### 文字列リスト
文字列リストは、文字列の集まりを表す。　「だいだいだい」と空白の後に、文字列の数を表す整数リテラル、要素となる文字列を空白区切りで書くことで表される。

### 型
型は、変数の持つ値の性質を表す。型には、整数型(ちょく)、整数リスト型(ちょくだい)、文字型(だい)、文字列型(だいだい)、文字列リスト型(だいだいだい)が存在する。

### 関数

#### 関数の定義
関数は、関数外でのみ定義できる。「ちょくちょくちょく　(関数名)　(引数の個数)　(引数の名前)」のように記述することで、関数を定義できる。2つ以上の引数を取りたい場合は、引数の名前を空白区切りで取りたい数だけ書けばよい。関数の定義を終了するには最後に「ちょくちょくだい」と書く必要がある。ないと実行時にエラーが発生する。いくつかの関数はあらかじめ定義されている。

#### 関数のスコープ
任意の関数は、それよりあとに記述されている関数を実行することができる。例えば、1行目~5行目で定義されている関数f内で、6行目以降で定義されている関数gを実行できる。

#### 戻り値
関数は値を返すことができる。「ちょくだいだい　(返り値)」と書くと、関数は値を返して終了する。「ちょくだいだい」より先に「ちょくちょくだい」を実行した場合、返り値は存在しない。

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
全ての変数は再代入を許されている。変数を定義する場合と同様に、「ちょくちょく　(変数名)　(再代入する値)」と記述すればよい。この言語は動的型付けであるため、文字列の入っている変数に、整数を再代入してもエラーは生じない。

### 標準出力
「だいちょく　(出力する値)」と書くことで、値を出力できる。値は整数型、文字型、文字列型のものを指定できる。

### 標準入力
標準入力に関する命令は、「ちょくだいちょく」、「ちょくだいだいだい」、「ちょくだいだいだいだい」の3つである。

#### 整数の受け取り(ちょくだいちょく)
「ちょくだいちょく」は整数を受け取るものである。「ちょくだいちょく　(受け取る変数)」と書くことで、半角スペースや改行で区切られた整数を1つ受け取り、指定された変数に代入できます。

#### 文字の受け取り(ちょくだいだいだい)
「ちょくだいだいだい」は文字列を受け取るものである。「ちょくだいだいだい　(受け取る変数)」と書くことで、半角スペースや改行で区切られた文字列を1つ受け取り、指定された変数に代入できます。空白も含めて受け取りたい場合は、標準入力を1行受け取る「ちょくだいだいだいだい」を使用してください。

#### 文字列の受け取り(ちょくだいだいだいだい)
「ちょくだいだいだいだい」は文字列を受け取るものである。「ちょくだいだいだいだい　(受け取る変数)」と書くことで、入力を1行分受け取り、指定された変数に代入できます。

### リスト操作
整数リスト、文字列(=文字リスト)、文字列リストなどのリストについて、要素の追加や削除などのいくつかの操作ができる。

#### 要素数の取得
「だいちょくちょく　(リスト)」と記述することで、リストの要素数を取得できる。

#### 要素の取得
リストのi番目の要素が欲しいとき、「だいちょくだい　(リスト)　i」と記述することで取得できる。ただし、iは0-indexedである。すなわち、一番初めの要素が欲しいとき、iを0に相当する整数とすればよい。また、iに-1を指定することで末尾の要素(全部でn個の要素があるとしたとき、n-1番目の要素)を取得できる。

#### 要素の追加
リストのi番目の要素の前にaを挿入したい場合は、「だいちょくちょくちょく　(リスト)　a　i」と記述すればよい。aは新たなリストでi番目の要素となり、それ以降の要素は一つ後ろにずれる。iに-1を指定すると末尾に追加される。また、引数のリストは変数でなければならない(リストのリテラルに対して挿入することは無意味であり、実装するに値しない)。

#### 要素の削除
リストのi番目の要素を削除したい場合は、「だいちょくちょくだい　(リスト)　i」と記述すればよい。i番目以降の要素は一つ前にずれる。iに-1を指定すると末尾の要素が削除される。また、引数のリストは変数でなければならない(リストのリテラルに対して削除することは無意味であり、実装するに値しない)。