# 概要

「関数型ドメインモデリング」の学習内容をアウトプットするスクラップ
https://amzn.asia/d/i3VaTIU

# Todo: 書籍のコード repo を探す

https://github.com/swlaschin/DomainModelingMadeFunctional

# 前提条件

私の学習前は以下の状態である

- F#は軽く触ったことがある程度
- ドメイン駆動設計は名前とざっくりした概要を知っている程度
- 設計に関する体系的学習はしたことがない
- 実務において詳細設計以下のフェーズは多く担当

# 本スクラップの書き方

内容を適当に要約して書いている
理解が足りていない部分は「?」をつけたりする

# 第 1 章 ドメイン駆動設計の紹介

## DDD とは

ソフトウェア開発を入出力(要件 → 成果物)で例えると
「ゴミが入るとゴミが出てくる」。
コミュニケーションとドメイン知識に焦点を当てた設計により、
ゴミが入る部分を最小限にする。

1 章では以下を説明

- DDD の原則
- ドメインにどう適用できるか

ただしすべての開発に適しているわけではない。

## 1-1 モデルを共有することの重要性

実際にリリースされるのは開発者の理解
→ ドメインエキスパートのものではない

仕様書や要件定義書で問題のすべてを把握しようとすると、
開発者(解決手段を作る者)とドメインエキスパート(問題を理解している者)の間の距離が空く

### ドメインエキスパートとは

問題を最も理解している人

## ドメイン駆動設計の目標

以下がすべて同じモデルを共有すること

- ドメインエキスパート
- 開発チーム
- ステークホルダ
- ソースコード
  これにより翻訳の必要性がなくなる

### 共有モデルを作るガイドライン

- データ構造ではなくビジネスイベントやワークフローに着目
- ドメインをより小さなサブドメインに分割
- 各サブドメインのモデルを解決空間に作成する
- PJ 関係者全員共有で、コードのあらゆる場所で使用される共通言語を開発

## 1-2 ビジネスイベントによるドメインの理解

典型的なビジネスプロセスはデータ変換の連続

### 「ドメインイベント」とは

モデル化したいビジネスプロセスの出発点
何らかの作業を開始するきっかけとなる外部からのトリガーのようなもの?

過去形で書く。きっかけのイベント自体は不変の事実

### イベントストーミングによるドメインの探索

ドメイン内のイベントを発見する方法のうち DDD のアプローチに特に適しているもの
ビジネスイベントとそれに関連するワークフローを発見する

#### やり方

ドメインの様々な部分を理解している人(質問がある人、答えられる人)を招集、ファシリテータつきのワークショップを開催

1. ビジネスイベントを付箋に書き出し壁に貼る
2. 他の人がその出来事をきっかけとしたビジネスワークフローをまとめたメモを貼る
3. メモを時系列に整理
4. 参加者全員、知っていることは壁に貼り、知らないことは質問

### ドメインを探索：受注システム

どこからでもいいのでビジネスイベントを壁に貼り出す。
作業のきっかけをビジネスイベントとする。

#### ビジネスの共通モデル

参加者全員でビジネスに対する共通認識をもつ
チーム間の対立構造を無くす

#### 全チームの把握

各チーム間における相互関係の抜け漏れを防ぐ
自チームのイベントにのみ着目すると他チーム部分の抜けが気づけるような感じ

#### 要件抜け漏れなど発見

抜けてるイベントとかに気づける
明確な答えが無い場合は議論のきっかけにする。

#### チーム間連携

あるチームのアウトプットが別のチームのインプットかもしれない
その依存関係そのものを明らかにする

#### レポーティング

過去に起こったことを理解する。
読み取られるだけのモデルがあるはず

### イベントを端まで広げる

イベントの連鎖をシステム境界まで広げる
これにより不足要件を見つけられる

必ずしもすべてシステム化する必要はない。ビジネスプロセスへの理解を優先

### コマンドの文書化

コマンド成功
↓
ワークフロー開始
↓
対応するドメインイベント作成

この方法でビジネスプロセスをモデル化する。

プロセスを入力と出力を持つパイプラインとする考えは関数型とマッチする。
関数型はある値を渡したら出力となる値が返却される

#### コマンドとは

ドメインイベントを引き起こそうとした依頼のことを DDD ではコマンドと表現
常に現在形

「A 社に発注する」
このコマンドに対するドメインイベントは
「発注された」

※イベント → コマンドの流れがわかってないので次はそこから

## 1-3 ドメインをサブドメインに分割

受注をより小さなサブドメインに分割する例

- 受注
- 発送
- 請求

### ドメインとは

首尾一貫した知識領域

プログラマはある意味ドメインエキスパート
「Web プログラミング」は「プログラミング一般」のサブドメインといえる
さらに特定言語や分野などもドメインの一種といえる

ドメイン同士をベン図にすると領域に重複部分がある

受注を分割したサブドメインでも同様のことがいえる。

## 1-4 境界付けられたコンテキストを利用した解決手段の作成

重複部分があるドメインを設計プロセスで境界付ける

問題空間
→ ドメイン

をモデル化

解決空間
→ 境界付けられたコンテキスト

### 境界付けられたコンテキストとは

いわゆるサブシステムのようなもの
境界を意識づけるためにこのように呼ばれる

解決手段における専門的知識を表す

問題空間のドメインが解決空間のコンテキストと常に 1:1 とは限らない
複数のドメインを解決するサブシステムがあるかもしれない

### コンテキストを正しく区別する

### コンテキストマップ作成

コンテキストを定義した後は設計の詳細に巻き込まれないように IF を定義
これをコンテキストマップという

### 最も重要なコンテキストに焦点を当てる

ドメイン同士の重要度は異なる
ビジネス上の優位性があるものなど(企業の強みとか)
コアではないドメインは支援ドメインという
企業固有でないものは汎用ドメインという

優先順位をつけて並列実装するべきではない

## 1-5 ユビキタス言語の創造

ドメインエキスパートとコードが同じモデルを使う必要がある
「注文」に対応して「Order」が必要
ドメインエキスパートの中に含まれていないもの(「OrderHelper」等)は設計に含めない

### ユビキタス言語

チーム全員が共有する概念と語彙のセット

PJ 内あらゆる場所で使われるべき
もちろんコード上でも

この言語構築はチーム全員で協力して行う
さらにこれは随時更新されることが前提

# 第 2 章 ドメインの理解

特定のワークフローについての掘り下げのステップ

- トリガーになるもの
- 必要なデータ
- 関連付けが要る境界付けられたコンテキストは他にあるか

ドメインエキスパートについて注意深くヒアリングする必要がある

## 2-1 ドメインエキスパートへのインタビュー

コマンド/イベントごと
一つのワークフローに焦点を当てた短いインタビューを何度も行う

最初は WF の i/o にのみ焦点を当てる

先入観を持たずにどのようなニーズがあるかを聞き取る

### 非機能要件の理解

WF の文脈や規模についても聞いてみる

### 残りの WF の理解

I/O を整理

WF のアウトプットは WF が生成するイベント

また、その WF でどのような副作用が起こるかも整理する

## DB 設計について

DB は一旦おいておいて設計するのがいい
ユーザはデータ永続化方法については考えていないしユビキタス言語にも含まれていない

ドメインそのもののモデル化に集中する

これを永続化非依存という

## クラス駆動について

クラス設計したとき、実在しない基底クラスを作ってしまうことがある。
当然これはドメインエキスパートには理解できない代物

## ドメインの文書化

要件をどうやって記録すべきか

WF とデータ構造を文書化

これをドメインエキスパートに見せて一緒に作業できるレベルで簡単に構造化

コードについても同様にできるかは後に

## WF を深堀

i/o が分かったところで受注の WF を具体的に理解

- クライアントの利益として何が優先事項になるか把握
- 実はコンテキスト外のサードパーティのサービスを使用していた
- 「場合による」が出てきたら複雑になる

## 複雑さをドメインモデルで表現

深堀して得た情報をテキストベースで表現

### 制約条件の表現

### ライフサイクルを表現

データモデルとして、検証済みのものとそうでないものを別定義するなど、

未検証の注文には未検証の住所があり、
検証済みの注文には検証済みの住所があるなど

### WF ステップを具体化

WF をより小さなステップに細分化

サブステップごとに入出力と依存関係を定義
どんな入力が来て、どんな出力がされうるか
例
入力：未検証の注文
出力：検証済みの注文 or 検証エラー
依存関係：外部サービス

# 三章 関数型アーキテクチャ

ドメインの理解を関数型プログラミングの原則に基づいたアーキテクチャに落とし込む

システムへの理解は引き続き行う
モデルを同実装するかを大まかなアイデアを持っておく

ソフトウェアアーキテクチャ自身もドメインなので、ユビキタス言語で

## 「C4」アプローチ

- 「システムコンテキスト」 -> システム全体を表す最上位の概念　複数のコンテナから成る
- 「コンテナ」-> デプロイ可能な単位 多数のコンポーネントから成る
- 「コンポーネント」コード構造で主要な構成要素 多数のクラスから成る
- 「クラス」(関数型では「モジュール」)低レベルメソッドや関数の集合
  各概念間の境界を定義し、変更コストを最小限に抑える

## 自律的ソフトウェアコンポーネントとしての境界付けられたコンテキスト

コンテキストは自律的サブシステムで、明確に定義された境界を持つ

これでも選択肢多数

- 単一モノリシックなデプロイ
- 境界付けられたコンテキスト単位でのデプロイ -> サービス指向アーキ
- ワークフローを独立してデプロイ可能なコンテナ単位にする -> マイクロサービス
  ただしこの時点では特定のアプローチにとらわれなくていい
  コンテキストが独立・自律的である限り

ドメインの理解が深まるにつれて協会は変化する

最初はモノリスにして、必要に応じて疎結合コンテナにリファクタするなど

## 境界付けられたコンテキストのコミュニケーション

イベントを発行することでコンテキスト同士のコミュニケーションを行う

真に自律的コンポーネントにするには疎結合にする

具体的なメカニズムはアーキテクチャによる

## コンテキスト間のデータ転送

### DTO

イベントは下流コンポーネントが処理するためのデータをすべて含む
データが大きすぎる場合は共有ストレージの位置を示すなど
コンテキスト間のデータ転送に使うデータオブジェトのことを言う。

境界付けられたコンテキスト内で定義されたオブジェクト(ドメインオブジェクト)とは異なる

### 信頼の境界線と検証

コンテキストの外側にあるものは信頼されておらず無効の可能性がある

#### ゲート

ワークフローの最初と最後に追加
ドメインと外の仲介

入力ゲートは入力値が制約を満たすか常に確認

出力ゲートはコンテキストの外にプライベートな情報が漏れないようにする

## コンテキスト間の契約

### 共有カーネル関係

共通のドメイン設計を持つ

DTO 定義を変更するときは他のコンテキストの所有者と協議する

### 顧客/供給者

下流が上流に提供してほしい契約を定義

### 順応者

下流は上流のコンテキストが提供する契約を受け入れる

### 腐敗防止層

外部システムとの連携時に IF がまったく一致しない場合、コンテキスト内で変換する必要がある
↓
外部適応しようとして内部ドメインモデルが「腐敗」

コンテキスト間疎結合の追加レベルとして置くもの

## コンテキストでのワークフロー

コンテキスト内ではドメインイベントの使用を避ける
隠れた依存関係が発生するため

## コンテキスト内のコード構造

レイヤー化アプローチではコードを各層に分ける
この時ワークフローの動作を変更するとすべてのレイヤーに手を入れる必要がある

### オニオンアーキテクチャ

ドメインコードを中心に置き、各レイヤーが内部のレイヤーにのみ依存する構造

ヘキサゴナルアーキテクチャやクリーンアーキテクチャなど類似したアプローチがある。
依存が内側に向くようにするために DI またはそれに類する機能を使う

### IO を端に追いやる

副作用を WF の開始時と終了時にする
WF 内部では DB にアクセスできない

# 4 章 型の理解

人間にしか理解できない方をいかにコードに落とし込むか

## 関数の理解

### 型シグネチャ

`int -> int`
int を入れたら int が返却されることを示す

```fsharp
let add1 x = x + 1 // int -> int
let add x y = x + y // int -> int -> int
```

F#はほとんどの場合コンパイラが型推論する

### ジェネリック型

f#では `'a`のようにシングルクォートがついたものはジェネリック型

## 型と関数

### 型とは

関数の入手出力として使用可能な値の集合に与えられた名前

### 値

入出力に使えるもの
不変なので変数ではない

### オブジェクト

データ構造と動作を含めたもの

## 型の合成

```fsharp
// レコード型
// フィールド3つが含まれる
// AND型
type Cube = {
    Depth: int
    Height: int
    Width: int
}

// 判別共用体
// DepthかHeightかWidthでタグ付けされたintがある
// OR型
type Figure =
    | Depth of int
    | Height of int
    | Width of int
```

### 単純型

判別共用体(選択型)で一つしかない型を定義することがドメインモデリングではよくある

```fsharp
type Length =
    | Length of int
```

ラッパーを簡単に作れるので

### 代数的型システム

すべての複合型がより小さな型を AND か OR で合成してできているもの

## F#の型を使う

```fsharp
// レコードを作成
let cube = {Depth= 5; Height= 2; Width=5}

// パターンマッチでレコードをバラす
let {Depth=depth; Height=height; Width=width} = cube

printfn "%d, %d, %d" depth height width
```

```fsharp
// 選択型のケースラベルのいずれかをコンストラクタ関数として使用する
let fd = Depth 4
let fh = Height 34
let fw = Width -489

// パターンマッチで選択型をバラす
let printFigureValue v =
    match v with
    | Depth d ->
        printfn "depth %d" d
    | Height h ->
        printfn "height %d" h
    | Width w ->
        printfn "width %d" w

printFigureValue fd
printFigureValue fh
printFigureValue fw
```

## 型の合成によるドメインモデル構築

型の定義をしたのち、これら型に関する関数を作成(メソッドではない)

## 省略可能な値、エラー、コレクションのモデリング

### 任意の値

Option 型を使う

### エラー

Result 型を使う

### 値が存在しないこと

unit を使う(関数型では関数は何かしらの値を返却しなければならないので void はない)
型シグネチャで unit があるということは副作用があることを強く示す

### コレクション

いくつかある

- list
- array
- ResizeArray
- seq
- set
- map
  ドメインモデリングでは常に list をした方が良い

## ファイルやプロジェクトでの型の整理

ドメイン型を一つにまとめる
依存する関数はそのファイルのうしろにまとめる

# 5 章 型によるドメインモデリング

共有されたドメインモデルがコードにも反映されていなければならない

これができていればドメインモデルをコードに翻訳する必要が無い

理想的には他の非開発者がコードレビューや設計確認できるのが望ましい

この強力な型システムで設計と実装がずれない

## 単純な値

ドメインエキスパートは int や string ではなく、OrderId 等のドメインの概念で型を考える

おなじ int であらわされるものでも互換性があるわけではないことを明確にする

単純型を作る

```fs
type CustomerId = CustomerId of int
//   ^型名         ^ケースラベル
let customerId = CustomerId 42
```

単純型ではケースラベルと型名を同一にする
ケース名がコンストラクタになるから

同じ int でも別の単純型にすることで混同しないようになる
分解するにはケースラベルでパターンマッチ

```fs
let (CustomerId innerValue) = customerId
printfn "%d" innerValue
```

関数定義で直接分解することはある

```fs
let processCustomerId (CustomerId innerValue) =
    printfn "%d" innerValue
```

### 単純型のパフォーマンス問題を回避

単純型を使用することは
メモリ使用率と効率性の犠牲がある

改善例

- 単純型ではなく型エイリアスを使う -> 型の安全性は下がる

```fs
type UnitQuantity = int
```

- 参照型ではなく値型(構造体)を使う

```fs
[<Struct>]
type UnitQuantity = UnitQuantity of int
```

- コレクション全体を一つの型とする

```fs
type UnitQyantities = UnitQyantities of int[]
```

## 複雑なデータ

Order が下記のようになっている場合

```
data Order =
    CustomerInfo
    AND ShippingAddress
    AND BillingAddress
    AND list of IrderLines
    AND AmountToBill
```

↓

```fs
type Order = {
    CustomerInfo : CustomerInfo
    ShippingAddress : ShippingAddress
    BillingAddress : BillingAddress
    OrderLines : OrderLine list
    AmountToBill : AmountToBill
}
```

この時、複雑な型で使用される型の実態が不明

ドメインエキスパートに協力してもらうことで実現した方が良い
ShippingAddress と BillingAddress を別物として扱う場合は論理的に分離するなど

### 未知の型のモデリング

モデリングしたい型の明確な型が不明な場合がある

未定義の型を表現したい場合、例外型の exn を Undefined とエイリアスする

```fs
type Undefined = exn

type CustomerInfo = Undefined
type ShippingAddress = Undefined
type BillingAddress = Undefined
type OrderLines = Undefined
type AmountToBill = Undefined

type Order = {
    CustomerInfo : CustomerInfo
    ShippingAddress : ShippingAddress
    BillingAddress : BillingAddress
    OrderLines : OrderLine list
    AmountToBill : AmountToBill
}
```

### 選択型によるモデリング

```
data ProductCode =
    WidgetCode
    OR GizmoCode
```

```fs
type ProductCode =
    | WidgetCode of WidgetCode
    | GizmoCode of GizmoCode
```

ここまでで名詞をモデリングできる

## ワークフローのモデリング

動詞をモデリングする

```fs
type ValidateOrder = UnvalidatedOrder -> ValidatedOrder
```

### 複雑な入出力

#### 出力

A と B の両方を出力する場合、そのようにするレコード型を作成

```fs
type out = {
    A : A
    B : B
}
```

どちらかを出力する場合は、選択型にする

```fs
type out =
    | A of A
    | B of B
```

#### 入力

入力に選択肢がある場合、選択型にする

すべて必須であるとき、複数のアプローチになる

- 別のパラメータとする

```fs
// Bが入力ではなく依存関係である場合はこのアプローチ
// DIと同等の機能を使える
type WorkFlow = A -> B -> Out
```

- 入力用のレコード型

```fs
// AとBそれぞれの入力が常に必要である場合
// タプルでもできるが、名前付きの型の方が良い
type Input = {
    A : A
    B : B
}
type WorkFlow = Input -> Out
```

### 関数のシグネチャでエフェクトを文書化

#### エフェクトとは

関数が主な入出力以外に行うことを説明する時に使う言葉

例えば、検証する関数で検証失敗が発生する場合は Result 型
非同期処理をする場合 Async 型を返却するなど

```fs
type ValidateOrder =
    UnvalidatedOrder -> Async<Result<ValidatedOrder, ValiudatioinError list>>
```

さらに型エイリアスで見た目を整える

```fs
type ValidationResponse<'a> = Async<Result<'a, ValiudatioinError list>>
type ValidateOrder = UnvalidatedOrder -> ValidationResponse<ValidatedOrder>
```

## アイデンティティの考察

永続的な ID を持つかどうかでデータ型を分類

### エンティティ

永続的なアイデンティティを持つオブジェクト

### 値オブジェクト

永続的なアイデンティティを持たないオブジェクト

例
WidgetCode のインスタンスが"W123"同士である場合、等価とみなす

```fs
type WidgetCode = WidgetCode of string
let widgetCode1 = WidgetCode "W123"
let widgetCode2 = WidgetCode "W123"
printfn "%b" (widgetCode1 = widgetCode2) // true
```

単純型でなくとも当てはまる

```fs
[<Struct>]
type FirstName = FirstName of string
[<Struct>]
type LastName = LastName of string
[<Struct>]
type PersonalName = {
    FirstName : FirstName
    LastName : LastName
}
let name1 = {
    FirstName = FirstName "test";
    LastName = LastName "test2"
}
let name2 = {
    FirstName = FirstName "test";
    LastName = LastName "test2"
}
printfn "%b" (name1 = name2) // true
```

### 値オブジェクトの透過性の実装

F#はデフォルトでフィールドベースの透過性を実装してくれる。しゅごい！

選択型なら同じケースで内容が同じ値であるときに等しくなる

## 5-7 アイデンティティ

### エンティティとは

構成要素が変わっても固有のアイデンティティを持つものであることをモデル化することがよくある
このようなもの

ビジネス上では多くの場合、何らかの文書

ライフサイクルがあり、ビジネスプロセスである状態から別の状態に変換される

#### 何をエンティティとするか

文脈次第

端末製造過程で固有のシリアルが与えられる場合、
その端末はエンティティである

販売時はシリアルは関係ない
この場合は値オブジェクト

### エンティティの識別子

他の値がどんな変更があっても安定している必要がある
ID のような一意のキーを与える必要がある

このキーは実際のドメイン自体によって提供される場合がある
無ければ UUID など自前で作る必要がある

```fs
type UserId = UserId of string

type User = {
    UserId: UserId
    // あとは省略
}
// 他のパラメータが変わってもUserIdが不変
```

### データ定義への識別子の追加

モデルの内側に識別子を追加するのが一般的

外側にすると型が複数に分散する

#### 外側で表現する時

```fs
type UnpaidInvoiceInfo = UnpaidInvoiceInfo of string
type PaidInvoiceInfo = PaidInvoiceInfo of string

type InvoiceInfo =
    | Unpaid of UnpaidInvoiceInfo
    | Paid of PaidInvoiceInfo

type InvoiceId = InvoiceId of string
type Invoice = {
    InvoiceId: InvoiceId
    InvoiceInfo: InvoiceInfo
}
let invoice = {
    InvoiceId = InvoiceId "testID";
    InvoiceInfo = UnpaidInvoiceInfo "string" |> Unpaid;
}
// 中身に対してパターンマッチでばらさないといけない
match invoice.InvoiceInfo with
    | Unpaid _unoaidInvoice ->
        printfn "unpaid %A" invoice.InvoiceId
    | Paid _paidInvoice ->
        printfn "paid %A" invoice.InvoiceId
```

#### 内側で表現する時

```fs
type UnpaidInvoice = {
    InvoiceId : InvoiceId //内側にあるID
    // その他各パラメータ
}

type PaidInvoice = {
    InvoiceId : InvoiceId
    // その他各パラメータ
}

type Invoice =
    | Unpaid of UnpaidInvoice
    | Paid of PaidInvoice
```

この時、パターンマッチで ID も含めてすべてのデータを一度で見れる

```fs
match invoice with
    | Unpaid unoaidInvoice ->
        printfn "unpaid %A" unpaidInvoice.InvoiceId
    | Paid paidInvoice ->
        printfn "paid %A" paidInvoice.InvoiceId
```

### エンティティに対する等価性の実装

同一のエンティティであるか確認するために
識別子だけ比較したい

以下を行う

1. Equals をオーバーライド
2. GetHashCode をオーバーライド
3. CustomEquality と NoComparison 属性を追加して、デフォルトの動作を変えたことをコンパイラに伝える

```fs
/// 内部で表現するパターン
[<Struct>]
type UserId = UserId of string

[<Struct>]
type EMailAddress = EMailAddress of string

// 比較をオーバーライドする
// オブジェクト指向の構文なのでここでしか使わない
[<CustomEquality; NoComparison>]
type User =
    { UserId: UserId
      EMailAddress: EMailAddress }

    override this.Equals(obj) =
        match obj with
        | :? User as u -> this.UserId = u.UserId
        | _ -> false
    override this.GetHashCode() = hash this.UserId


let userId = UserId "testID"

let user1 =
    { UserId = userId
      EMailAddress = EMailAddress "testAddress" }

let user2 =
    { UserId = userId
      EMailAddress = EMailAddress "testAddress22" }

printfn "%b" (user1 = user2) // true

```

ただし、 `=` の動作をしれっと変更しているので、`NoEquality`属性で`=`を無効化する手もある

この場合、識別子で比較する形にすればいい。

この場合はオブジェクトレベルの透過性の意味をあいまいにしない

透過性テストで複数のフィールドが使われる場合、key プロパティを使う手もある

### 不変性とアイデンティティ

関数型では値はデフォルトで不変

- 値オブジェクト
  名前の一部だけ変えた場合、それは新しいものとみなす
- エンティティ
  関連データは時間とともに変化する
  不変のデータ構造でこの変化を表すには、エンティティのコピーを作成する

一部のフィールドだけ変更してレコードのコピーを作る例

```fs
let initialPerson = {PersonOd = PersonId 42; Name="Joseph"}
let ipdatedPerson = {initialPerson with Name = "Joe"}
```

可変のデータ構造を使う欠点

- 副作用が生じる

```fs
type UpdateName = Person -> Name -> unit
// PersonのNameを変更する関数を書く場合、出力がunitになる
// これはどう変化したか傍からみてわかりづらい。副作用があるかもしれない
```

↓

```fs
type UpdateName = Person -> Name -> Person
// 新しいPersonを生成するようにすると問題が生じない
```

## 5-8 集約

Order が OrderLine を含む場合の前提

OrderLine が変更されたとき、それが属する Order は変更されたといえるか
↓
YES

OrderLine の変更だけではなく、変更後の Order も生成しなければならない

```fs
let changeOrderLineQuantity = order orderLineId newPrice =
  // orderLineIdで対象行を検索
  let orderLine = order.OrderLines |> findOrderLine OrderLineId

  // 新しい明細行を作成
  let newOrderLine = {orderLine with Price = newPrice}

  // 新しい明細を作成
  let newOrderLines =
    order.OrderLines |> replacceOrderLine orderLineId newOrderLine

  let newOrder = {OrderLine with OrderLines = newOrderLines}

  newOrder
```

あるエンティティを変えるだけでもそれを内包する高レベルのエンティティごと生成する

### 集約とは

エンティティのコレクションを有するエンティティ

### 集約ルートとは

トップレベルのエンティティ

### 集約による整合性と不変条件の担保

整合性の境界として機能する

集約のある部分が変更されると、整合性担保のために他の箇所も変更する
注文量を変更したらトップレベルの合計金額も変更が要る、等

整合性を維持する方法を知っているコンポーネントが集約ルート

### 集約の参照

Customer を Order に追加しようとする

この場合、Customer を変更しても Order に変更が波及する
↓
Order に Customer の参照を格納する
つまり CustomerId のみを格納する

Customer と Order は独立した集約
↓
集約が永続化の基本単位でもある

データベースからオブジェクトをロードまたはセーブしたい場合は集約全体をロードまたはセーブするべき

各 DB トランザクションは単一の集約を扱うべき
複数の集約を含んだり集約の境界を超えてはいけない

#### 集約の役割

- トップレベルのエンティティが「ルート」として機能する単一のユニットとして扱えるドメインオブジェクトのこと
- 集約内のオブジェクトに対するすべての変更は集約内のすべてのデータが同時に正しく更新されることを保障する集約の境界として機能
- 集約は永続化・トランザクション・データ転送におけるアトミックな(全て実行 or 何も実行されない)処理単位

ドメインエキスパートだけが集約の関係と境界を理解できる

## 5-9 全てを組み合わせる

境界付けられたコンテキストを表現するため、FS の namespace に突っ込んでほかのコンテキストと区別

参照
`fs/5-9combination.fsx`

### 型はドキュメントの代わりになるか

開発者以外がレビューできるか

F#の型として文書化されているが、AND や OR の記法のドキュメントとほぼ同じ

C#や Java よりは明らかに読みやすい

## 5-10 まとめ

ユビキタス言語で string や int 等を使用しない
Manager や Handler のような型も使っていない

整合性確保のために集約という単位を設定

ちなみに型定義がコンパイル可能であるのでドメイン定義と常に同期したアプリケーションコードにできる。
やったね

何ならドメインエキスパートと一緒に作業することすらできる。

問題点

- 単純型の制約
- 集約の整合性の確保
- 注文の状態変化のモデル化

# 第六章 ドメインの完全性と整合性

信頼できるデータだけを含む境界付けられたコンテキストだけを常に含むのが目標
防御的コーディングが要らなくなる

## 完全性とは

データが正しいビジネスルールに従っていることを意味する
↓ のような

- UnitQuantity は 1~1000 としたとき、コード上でそれが常に真であることを当てにできるのか
- 注文には明細が常に 1 行以上
- 発想部門に送る前に発送先住所の検証をする

## 整合性とは

ドメインモデルの異なる部分が事実に基づいて一致していることを意味する
↓ のような

- 注文の合計は個々の行の合計と一致
- 注文確定時、対応する請求が作成される
- 注文時にクーポン使用時、該当クーポンを使用済みとし、再使用できなくする

## 単純な値の整合性

制約を満たさない限り値を作成できないようにする
↓
一度作成できたら値が不変なので再チェック不要

### スマートコンストラクタアプローチ

関数型の世界で、コンストラクタを private にして別の関数を用意して、無効な値は拒否する

UnitQuantity の例

参照
`fs/6-1tryal.fsx`

## 6-2 測定単位

測定単位を使う方法として、数値にカスタムの単位を付与できる

## 6-3 型システムによる不変条件の強制

何が起きても真である条件のこと
一つの注文に少なくとも一つの明細が存在しなければならない、みたいな

```fs
type NonEmptyList<'a> = {
    First: 'a
    Rest: 'a list
}
```

add や remove もいるが、FSharpx.Collections のような、サードパーティのライブラリを使うこともできる

```fs
type Order ={
    // 省略
    OrderLines : NonEmptyList<OrderLine>
}
```

最低一つの明細がある制約が自動的に適用

## 6-4 ビジネスルールを型システムで表現

顧客メールが検証済みと未検証が混在する時

- 検証メールは未検証のアドレスにのみ送信すべきである
- pass リセットは検証済みメールにのみ送信すべき

```fs
type CustomerEmail = {
    EmailAddress : EmailAddress
    IsVerified : bool
}
```

↑ の状態ではいつどのような時にフラグが使用されるかが不明確

検証済みと未検証を別のものとしてモデル化する

```fs
type CustomerEmail =
    | Unverified of EmailAddress
    | Verified of EmailAddress
```

これでも Verified を作ろうとして未検証を渡してしまう可能性を否定できない

```fs
type CustomerEmail =
    | Unverified of EmailAddress
    | Verified of VerifiedEmailAddress
```

さらに VerifiedEmailAddress にプライベートコンストラクタを定義
検証サービスだけがそのコンストラクタを作成できるようにする

これらのルールを型で表現することで、テストコードではなくコンパイルで検証できる

パスワードリセットの WF には Verified を入力として指定できる
Unverified が渡される危険性がなくなる。すごい！！

- 顧客は電子メールか郵便アドレスを持つ必要がある

両方必須でも両方 option でもなく

これはメール or 住所 or(メール and 住所)という選択型で表現

```fs
type BothContactMethods = {
    Email : EmailContactInfo
    Address : PostalContactInfo
}

type ContactInfo =
    | EmailOnly of EmailContactInfo
    | AddressOnly of PostalContactInfo
    | EmailAndAddr of BothContactMethods

type CustomerInfo = {
    Name: Name
    ContactInfo: ContactInfo
}
```

### 不正な状態がドメインで表現されないようにすること

検証サービスでは Unvalidated を受け取り、option Validated を返却する

```fs
type AddressValidationService =
    UnvalidatedAddress -> ValidatedAddress option
```

発送先の検証が要るルールを適用するために異なる方を作成し、ValidatedOrder が ValidatedAddress を含むことを要求

```fs
type UnvalidatedOrder = {
    // 省略
    ShippingAddress : UnvalidatedAddress
    //
}
```

```fs
type ValidatedOrder = {
    //
    ShippingAddress : ValidatedAddress
    //
}
```

## 6-5 整合性

整合性要件について

- 注文の合計金額は個々の行の合計と同じでなければならない
- 注文が確定すると対応する請求書が作成され中ればならない
- 注文時にクーポン使用時、それを再使用不能にする

### 整合性とは

ビジネスの用語
直ちに合計に反映されるべきか否かは状況次第

なるべく整合性を必要としない設計にしたい
PO が要求する整合性のレベルが現実的でない場合がある
->後から整合性をとっても別に問題ない場合がある

注文の内部的

### 一つの集約内での整合性

簡単な方法は、都度計算するようにする
つまり変更されるたび新しいものを生成するようにする

### 異なるコンテキスト間の整合性

- 注文が確定すると、対応する請求書が作成されなければならない 注文あり・請求書なしはデータ矛盾

請求書発行は受注ドメインではなく請求ドメインの一部

未確定の注文
↓
確定した注文&請求書 (請求書は異なるドメイン)

2 フェーズコミットという手もあるが、ぶっちゃけすべてのプロセスが足並みをそろえる必要はない
かわりにメッセージで非同期に処理を行う
うまくいかない場合がまれにあるが、すべてを同期するコストより安い

例
請求書の作成をすぐに要求するのではなく、請求ドメインにメッセージ or イベントを作成

メッセージが失われ、請求書が作成されなかった場合は?
以下の三択

- 何もしない。リスクが小さい場合は許容?
- メッセージが失われたことを検出して再送信する 照合プロセスが行う
- 前のアクションを元に戻す or エラーを修正する補償アクション
  - 注文をキャンセルする

これらのケースではコンテキスト間の厳密な調整は要らない
2 か 3 を実施するのは整合性が求められる場合。ただ、ほとんどの場合は遅れて整合性ができる、結果整合性

例えば商品価格が変わったとき、全体の変更をすぐに反映するのはコストが高い
↓
かわりに価格が変わったというイベントを発行して結果整合性を担保する

### 同じコンテキストの集約間の整合性

状況により、結果整合性か同一トランザクションか

一般的には単一トランザクションで単一の集約のみを更新する、というガイドラインが有効

ただし、WF が単一トランザクションと考えられる場合、影響を受けるすべてのエンティティをトランザクションに含める価値があるかもしれない

口座間送金など

この場合、手続き自体が固有の識別子を持っている場合がある
それを一つの集約で表現する手もある

```fs
type MoneyTransfer = {
    Id: MoneyTransferId
    ToAccount : AccountId
    FromAccount : AccountId
    Amount: Money
}
```

集約の目的が一つでも必要ならば新しく定義する

### 同一のデータに作用する複数の集約

Amount 集約と MoneyTransfer 集約があり、
どちらも赤いウントの残高に作用、どちらもマイナスにならないようにする必要がある

NonNegativeMoney 型でモデル化できる。
もしくは検証関数を共有する

## 6-6 まとめ

信頼性担保

単純型 - スマートコンストラクタ
複雑型 - 不完全な状態を表現不能にする

単一の集約でない場合は結果整合性で設計

# 第 7 章 パイプラインによる WF モデリング

ドメインエキスパートが見れる WF のモデリング

WF が一連のサブステップで構成されている

ビジネスプロセスを表すパイプラインを作成し、
それをさらに小さな「パイプ」の週 g プ体として構築

パイプは一つのものだけ変換し、それらを結合して大きなパイプにする

これを「変換指向プログラミング」ということがある

関数型プログラミングの原則にのっとって、
各ステップはステートレスで副作用が無いように設計

## 7-1 WF の入力

WF の入力は常にドメインオブジェクトでなければならない
(入力はデシリアライズ済みとする)

```fs
type UnvalidatedOrder = {
    OrderId : string
    CustomerInfo : UnvalidatedCustomerInfo
    ShippingAddress : UnvaliudatedAddress
    // 以下略
}
```

### 入力としてのコマンド

WF はそれを開始するコマンドと関連する
ある意味で WF の本当の入力はコマンドといえる

このコマンドに WF がリクエストを処理する情報が含まれてなければならない
上記の UnvalidatedOrder みたいな

さらにだえ r がコマンドを作成したかなどのメタデータを記録・監査したいので、
コマンド型は ↓ になる

```fs
type PlaceOrder = {
    OrderForm : UnvalidatedOrder
    Timestamp: DateTime
    UserId: string
    // etc
}
```

### ジェネリクスによる共通構造の共有

コマンド情報を共通構造としたとき、共通部品をジェネリクスにして使いまわせる

```fs
type Command<'data> = {
    Data : 'data
    T
    Timestamp: DateTime
    UserId: string
    // etc
}
// あとは↓だけでWF固有コマンドを作れる
type PlaceOrder = Command<UnvalidatedOrder>
```

### 複数のコマンドを一つの型にまとめる

単一の境界付けられたコンテキストすべてのコマンドが同じ入力で送信されることもある

コンテキストでそれぞれ別の WF に紐づく

対応のため、これらをすべて含む選択型を使う

以下の三つのコマンドが同一の入力 IF の場合

- 注文確定
- 注文変更
- 注文キャンセル

```fs
type OrderTakingCommand =
    | Place of PlaceOrder
    | Change of ChangeOrder
    | Cancel of CancelOrder
```

この選択型は DTO にマッピング
入力チャンネル上でシリアライズ/デシリアライズされる
コンテキストの端に「ルーティング」or「ディスパッチ」ステージを足すだけ

## 7-2 状態の集合による注文のモデリング

Order は単なる静的な書類ではなく実際にはさまざまな状態に遷移する

これらをどうモデル化するか

各状態に対して新しい型を作成すること
そしてとりうる状態のうち一つ選択するトップの型を作成

```fs
type ValidatedOrder = {
    // 省略
}
type PricedOrder ={
    // 省略
}
type Order =
    | UnvalidatedOrder of UnvalidatedOrder
    | ValidatedOrder of ValidatedOrder
    | PricedOrder of PricedOrder
```

### 要件の変網に伴う新しい状態型の追加

このとき、既存コードを壊さずに新しい状態を追加できる

選択型として RefundedOrder とかを追加するのは容易

## 7-3 ステートマシン

典型的なモデルでは、ドキュメントやレコードは複数の状態を持つ

ある状態から別の状態へのパス(遷移)はなんらかのコマンドで引き起こされる

### ステートマシンを使う理由

- それぞれの状態で受け付ける処理を変えられる
- 全ての状態が明示的になる
- 起こりうる状態を全て考慮に入れて設計するようになる

### F#でシンプルなステートマシンの実装

各状態に対応する独自の方を作成する。

コマンドハンドラーはステートマシン全体(選択型)を引数に取り、新しいバージョンを返却する関数(更新後の選択型)

ShippingCart 型(空 or アイテムあり or 支払い済みのいずれかの選択型)を引数に取る addItem 関数 -> 返却値は ShippingCart 型

などなど

## 7-4 型を使った WF 各ステップのモデリング

### 検証ステップ

依存関係は関数として扱う

関数の型シグネチャは、後で実装する必要のある IF になる

ex 製品コードの存在を確認するには、カタログを参照する関数

ChsckProductCodeExists 型を定義できる

```fs
type CheckProductExists =
    ProductCode -> bool
```

ex 住所を検証する関数

```fs
type CheckedAddress = CheckedAddress of UnvalidatedAddress
type AddressValidationError = AddressValidationError of string
type CheckAddressExists =
    UnvalidatedAddress -> Result<CheckedAddress,AddressValidationError>
```

依存関係が整理されたところで、二つの依存関係を持つ関数として定義できる

```fs
type ValidateOrder =
    CheckProductCodeExists // 依存する関数
    -> CheckAddressExists // 依存する関数
    -> UnvalidatedOrder // 引数
    -> Result<ValidatedOrder,ValidationError> // 依存関係のどこかでResultが返されると、それを処理するトップレベルでもResultを返却する必要がある
```

### 価格計算ステップ

価格計算

入力は検証済み注文
出力は計算済み注文
依存関係はプロダクトコード確認

```fs
type GetProductProce =
    ProductCode -> Price
```

```fs
type PricedOrder =
    GetProductPrice //依存関係
    -> ValidatedOrder //入力
    -> PricedOrder //出力
```

### 注文確認

確認書のモデル

```fs
type HtmlString =
    HtmlString of string
```

```fs
type OrderAcknowledgement = {
    EmailAddress : EmailAddress
    Letter : HtmlString
}
```

Letter の内容について

```fs
type CreateOrderAcknowledgementLetter =
PricedOrder -> HtmlString
```

一旦具体的な実装は考えずに設計に集中

確認書を送信する部分は副作用

```fs
type SendOrderAcknowledgement =
    OrderAcknowledgement -> unit //送信されたかどうか不明
```

```fs
type SendResult = Send | NotSend
type SendOrderAcknowledgement =
    OrderAcknowledgement -> SendResult
```

最終的な出力が OrderAcknowledgementSent

```fs
type = OrderAcknowledgementSent = {
    OrderId: OrderId
    EmailAddress :EmailAddress
}
```

```fs
type AcknowledgeOrder =
    CreateOrderAcknowledgeLetter // 依存関係
    -> SendOrderAcknowledgement //依存関係
    -> PricedOrder // 入力
    -> OrderAcknowledgementSent option
```

### 返すイベントをつくる

実際に返却するイベントを作るとき、複数のイベントを返却することがある
これをイベントのリストで返却
リストは選択型にする

```fs
type PlacedOrderEvent =
    | OrderPlaced of OrderPlaced
    | BillableOrderPlaced of BillableOrderPlaced
    | AcknowledgementSent of AcknowledgementSent

// WFの最終ステップでこれらのリストを返却
type CreateEvents =
    PricedOrder -> PlaceOrderEvent list
```

## 7-5 エフェクトの文書化

全ての依存関係を再検討し、エフェクトを明示する必要があるかを再確認

### 検証ステップでのエフェクト

ドメイン内にないリモートなサービスの場合、
Async と Result を持つ形にする

```fs
type AsyncResult =<'success, 'failure> = Async<Result<'success, 'failure>>
```

実装がサードパーティに依存するようであれば、それを信頼する必要がある(or その問題を回避)

Async エフェクトも、それを含むコード全体に影響する

```fs
type ValidateOrder =
    CheckProductCodeExists // 依存する関数
    -> CheckAddressExists // 依存する関数
    -> UnvalidatedOrder // 引数
    -> Result<ValidatedOrder,ValidationError>
```

↓

```fs
type ValidateOrder =
    CheckProductCodeExists // 依存する関数
    -> CheckAddressExists // 依存する関数
    -> UnvalidatedOrder // 引数
    -> AsyncResult<ValidatedOrder,ValidationError list>
```

### 価格設定ステップでのエフェクト

価格計算は Async にはならないけどエラーになる可能性はある
よって Result 型になる

### 各員ステップのエフェクト

SendOrderAcknowledgement は I/O することが分かっているが、
エラーになっても気にせずに成功パスに進める
この時、Async にする

## 7-6 ステップから WF を合成する

```fs
type ValidateOrder =
    UnvalidatedOrder -> AsyncResult<ValidatedOrder, ValidationError list>

type PriceOrder =
    ValidatedOrder -> Result<PricedOrder, PricingError>

type AcknowledgeOrder =
    PricedOrder -> Async<OrderAcknowledgement option>

type CreateEvents =
    PricedOrder -> PlaceOrderEvent list
```

入出力が一致していないので合成できない
入出力の型を調整して互換性を持たせ、関数合成する

## 7-7 依存関係は設計の一部か

プロセスがどう仕事をするか、どのようなシステムと連携する必要があるのか

- パブリック API の依存関係は呼び出し元から隠す
- 内部で使用される関数は依存関係を明示
  ↓
- WF 内部の各ステップでは依存関係を明示すべき
- トップレベルの WF 関数の依存関係は呼び出すもとが知る必要がない

## 7-8 パイプラインの完成形

パブリック API の型を書く

`fs/DomainApi.fsx`

### 内部ステップ

内部ステップで使用する型は実装用の別ファイルで定義

`PlaceOrderWorkflow.fsx`

## 7-9 長時間稼働する WF

例えば外部サービス連携に時間を食うとしたら
外部サービス連携前に状態をストレージに保存する必要がある
サービス終了を知らせ宇メッセージを持ち＜ストレージから状態を再読み込み
通常の非同期呼び出しに比べて思い作業になる

元の WF をイベントをきっかけにした小さい塊に分割する
この場合ステートマシンの状態が永続化される

## 7-10 まとめ

これくらい型を定義することで、ドメインを伝達できるコードができる

## 7-11 次に

実際の実装に移る

# 第 8 章 関数の理解

## 8-1 関数

関数が非常に重要であるかの様にプログラミングする

プログラムがあるパーツの塊だとする

- オブジェクト指向ではこれらのパーツはクラスやオブジェクトになる
- 関数型では関数になる

プログラムの一部をパラメータ化する必要やコンポーネント間の結合を軽減する必要がある時

- オブジェクト指向では、IF や DI
- 関数型では関数でパラメータ化する

コンポーネント間でコードを再利用したい時

- オブジェクト指向なら継承や Decorator
- 関数では再利用可能なコードを全て関数にまとめて、関数合成を組み合わせる

## 8-2 「もの」としての関数

関数型プログラミングのパラダイムにおいて、関数はそれ自体が「もの」
関数自体も入出力にできる

### F#で関数を「もの」として扱う

関数はものなので、リストにまとめられる

let キーワードは一般的に値に名前を割り当てるのに使われる

### 入力としての関数

関数を入力パラメータとして受け取る例

```fs
let evalWith5ThenAdd2 fn = fn 5 + 2
let add1 x = x + 1
printfn "%d" (evalWith5ThenAdd2 add1) // 8
let square x = x * x
printfn "%d" (evalWith5ThenAdd2 square) // 27
```

### 出力としての関数

カリー化 よくわかっているので省略

どんな他パラメータの関数も、単一パラメータの関数の連なりにできる これをカリー化という

F#ではデフォルトで全ての関数がカリー化

### 部分適用

カリー化された関数に一つだけ引数渡したら、そのパラメータだけ組み込まれたカリー化された関数が返却される

## 8-3 全域関数

数学では関数はとりうる各入力に対してそれぞれ一つの出力に結びついている

この様な関数を全域関数という

例えば 12 を入力値で割る関数を作るとき、
0 が入力されたら未定義動作とかになる。

0 がそもそも渡されない様にするか、option か result にするか

NonZeroInteger を作成してそれを渡せる様にするなど

これにより型シグネチャが嘘つかない様になる

## 8-4 関数合成

A -> B
と
B -> C を合成して
A -> C という関数を作れる

### F#での関数合成

パイピング

|>演算子でパイピングする

```fs
let add1 x = x + 1
let square x = x * x
let add1ThenSquare x = x |> add1 |> square
printfn "%d" (add1ThenSquare 5) // 36
```

### 関数からアプリ全体を構築する

低レベル処理をパイプラインで繋ぎ合わせてワークフローをつくる

最後にこれら WF を入力に応じて特定の WF を選択して呼び出すコントローラ/ディスパッチャを作成することで構築できる

これが関数型アプリケーションを構築する方法

### 関数合成する上での課題

合成したい関数同士の型が一致していない場合

一般的には両サイドの最小公倍数にする

つまり両者を包含する型にする

`fs/8-4unmatchFunctions.fsx`

# 第九章 パイプラインの合成

WF は一覧のドキュメントの変換、すなわちパイプラインと考えられる

```fs
let placeOrder unvalidatedOrder =
    unvalidatedOrder
    |> validateOrder
    |> priceOrder
    |> acknowledgeOrder
    |> createEvents
```

この WF を実装するには個々のステップを作成し、それらを組み合わせるという二段階がある

パイプラインないの各ステップを独立した関数として実装する
その際ステートレスで副作用がない様にする

次にこれら小さな関数を大きな関数に合成する

大体の場合は型が一致しないので各ステップの入出力の型を一致させて関数合成できる様にする

## 9-1 単純型を扱う

ワークフロー自体のステップを実装する前にまずは OrderId や ProductCode などの単純型を実装する

各単純型ごとに二つ関数を実装する

- create 関数
- value 関数

ヘルパー関数は単純型と同じファイルにおいて適用する型と同じ名前のモジュールを使用する

```fs
module Domain =
    type orderId = private OrderId of string
    module OrderId =
        let create str =
            if String.IsNullOrEmpty(str) then
                failwith "OrderId must not be null or empty"
            elif str.Length > 50 then
                failwith "OrderId must not be more than 50 chars"
            else
                OrderId str
        let value (OrderId str) = str
```

## 9-2 関数の型から実装を導く

WF の各ステップを表す関数専用の型を定義した
これらのコードが型定義に準拠していることを確実にするには

最も簡単な手法は通常の方法で関数を定義し、後で使用するときに間違っていれば型チェックエラーが出ると信じること

もしくは関数が特定の型を実素巣していることを明確にしたい場合は他のスタイルも使える

関数はその型をアノテーションとして指定した値とし、関数の本体をラムダ式とする形式でも定義可能

```fs
type MuFunctionSignature = Param1 -> Param2 -> Result
let myFunc: MuFunctionSignature =
    fun param1 param2 ->
```

## 9-3 検証ステップの実装

```fs
type CheckAddressExists =
    UnvalidatedAddress -> AsyncResult<CheckAddress, AddressValidationError>


type ValidateOrder =
    CheckProductCodeExists
    -> CheckAddressExists
    ->UnvalidateOrder
    ->AsyncResult<ValidatedOrder, ValidationError list>
```

ただし、本章ではエフェクトを排するので AsuncResult を除く

```fs
type CheckAddressExists =
    UnvalidatedAddress -> CheckAddress

type ValidateOrder =
    CheckProductCodeExists -> CheckAddressExists -> UnvalidatedOrder ->ValidatedOrder

let validateOrder : ValidateOrder =
    fun checkProductCodeExists checkAddressExists unvalidatedOrder ->
        let orderId =
            unvalidatedOrder.OrderId |> OrderId.create
        let customerInfo =
            unvalidatedOrder.CustomerInfo |> toCutomerInfo
        let shippingAddress = unvalidatedOrder.ShippingAddress |> toAddress
        {
            OrderId = orderId
            CustomerInfo = customerInfo
            ShippingAddress = shippingAddress
            //等々
        }

// まだ定義居ていないヘルパー関数を使用している
// 検証されていない型からドメイン型を構築する
//
```

### 検証されたチェック済み住所の作成

toAddress は少し複雑

- 生のプリミティブ型をドメイン型オブジェクトに変換
- 外部サービスでアドレス存在チェック

### 明細行の作成

明細行の作成

- 一つの UnvalidatedOrderLine を ValidatedOrderLine に変換
  - これを toValidateOrderLine
- ↑ を List.map で全体に適用
- ↑ を用いて ValidateOrder 使用する

toOrderQuantity について

```fs
let toOrderQuantity productCode quantity =
    match productCode with
    | Widget _ ->
        quantity
        |> int
        |> UnitQuantity.create
        |> OrderQuantity.Unit
    | Gizmo _ ->
        qunatity
        |>KillogramQuantity.create
        |> OrderQuantity.Kilogram
```

toProductCode について

```fs
let toProductCode (checkProductCodeExists: CheckProductCodeExists) productCode =
    productCode
    |> ProductCode.create
    |>checkProductCodeExists
    // bool
```

パイプラインは productCode を返したいのだが、bool が返却されてしまう

解決法は ↓

### 関数アダプターの作成

bool を返す述語とチェックする値をパラメータ t ぽする実装

```fs
let convertToPassthru checkProductCodeExists productCode =
    if checkProductCodeExists productCode then
        productCode
    else
        failwith "Invalid Product Code"
```

パイプラインで使えるパススルー関数に変換する汎用アダプターになった(コンパイラがジェネリクスと判断した)

```fs
let predicateToPassthru errorMsg f x =
    // 部分適用でエラーメッセージを組み込めるようにerrorMsgを最初のパラメータにしている
    if f x then
        x
    else
        failwith errorMsg
```

部分適用でエラーメッセージを組み込めるように errorMsg を最初のパラメータにしている
このようなパススルー関数を作るのは関数型プログラミングでは非常に一般的

## 9-4 残りのステップの実装

実装のスケッチをする際、`todo!()`や`failwith ""`等で実装されてないことを示すと便利

### 確認ステップ実装

持ち上げ

Option を list に変換するなどができる

## 9-5 パイプラインのステップを一つに合成する

入力が複数ある関数とのつなぎをするために、
あらかじめ依存関係を部分適用して関数入出力を合わせてパイプライン化する。

関数がうまく合わない場合、一時的アダプターを書くか、命令型コードに切り替える

パイプラインほどきれいではないが理解と保守がしやすい

## 9-6 依存関係の注入

関数型では依存関係が暗黙的になるのはよろしくない

依存関係を明示的パラメータとするのが関数型

- リーダーモナド
- フリーモナド
- トップレベルの関数に渡し、さらに内部の関数に渡すを繰り返す

```fs
let toAddress checkAddressExists unvalidatedAddress =
//
let toProductCode checkProductCodeExists productCode =
//
```

↑ はどちらも依存関係が明示的

toValidatedOrderLine は toProductCode を使うので、
toValidateOrderLine も checkProductCodeExists もパラメータが必要

さらに一つ上のレベルの validateOrder 関数は toAddress と toValidateOrderLine を両方使うので、
それらに必要なサービスを両方とも追加パラメータとして渡す

↑ をトップレベルまで繰り返す
このトップレベルをオブジェクト指向ではコンポジションルートという

WF 関数である placeOrder をコンポジションルートにするべきか->否
サービスをセットアップするには一般的に構成情報へのアクセスが必要になるから

placeOrder ワークフロー自体も必要なサービスをパラメータとしていけとれるようにした方が良い

すべての依存関係を差し替えられるので簡単にテストできるという利点がある

### コンポジションルートをどこにすべきか?

実際はアプリケーションのエントリーポイントに近いものにするべき

- main 関数
- web とかなら OnStartup/Application_Start ハンドラー

### 依存関係が多すぎる場合

-> 関数が行うことが多すぎることを疑う
もっと小さな単位に分割できないか
難しい場合は依存関係を一つのレコードにまとめることで、見た目上小さくできる

子関数の依存関係が複雑な場合

アドレスチェックで、エンドポイント情報と認証情報が必要とする

```fs
let chechAddressExists endPoint Credentials =
```

この上の階層すべてで endPoint と Credentials を渡すのか??checkAddressExists しか使わないのに??
↓
否
↓

- 低レベル関数をトップレベル関数の外側で設定し、依存が組み込まれた checkAddressExists を注入する

## 9-7 依存関係のテスト

依存関係を渡す利点->コア機能のテストが非常に簡単になる

モックライブラリが無くてもフェイク関数を用意するだけで依存関係を提供できる

checkProductCodeExists の成否で検証全体の成否が変わるテストをしたい

```fs
open NUnit.Framework

[<Test>]
let ``正常系_製品存在時、検証成功``() =
    let checkAddress address =
        CheckAddress address
    let CheckProductCodeExists productCode =
        true
        // スタブを簡単に定義できちゃうぞ♡
```

サービスの障害で例外が投げられる様にしたが、これは避けたい

関数型プログラミングの原理をテストに使うことで実用的なメリットが得られる

- ステートレスで純粋関数
- 依存関係が明示的
- 副作用は関数自体ではなくパラメータで渡される関数でカプセル化される

## 9-8 組み立てられたパイプライン

1. 特定の WF を実装する全てのコードを同じモジュールにぶち込む。モジュール名を WF に基づく名前にする
2. ファイルの一番上に型定義を打ち込む
3. その下に各ステップの実装
4. その下に各ステップを組み立て、メインの WF 関数を作る

WF のパブリックな型は API モジュールなどん別の場所で定義されるので、内部ステージを表現する型だけでいい
パブリックなやつはモジュール序盤で open するなどしておく

## 9-9 まとめ

ドキュメント観点では例外を扱うのは最悪
次回で result に戻して色々やってみる

# 第 10 書 エラーの扱い

製品コードの形式が違っていたりなどのエラーについて
Result 型を使う

## 10-1 Result 型を使ってエラーを明示する

関数型ではできるだけ物事を明示的にすることを重視している

エラーも同様

関数の型シグネチャについて、エラーがない様に見えてうんこ

望ましいのは全域関数で起こりうる全ての結果が返り値として明示的に文書化されていることが望ましい

```fs
type CheckAddressExists =
    UnvalidatedAddress -> CheckedAddress
```

↓

```fs
type CheckAddressExisits =
    UnvalidatedAddress -> Result<CheckedAddress, AddressValidationError>
and AddressValidationError =
    | InvalidFormat of string
    | AddressNotFound of string
```

## 10-2 ドメインエラーを扱う

この様な型でも全てのエラーの処理はできないし望ましくもない

エラーを分類して処理する一貫的アプローチを検討

- ドメインエラー
  - ビジネスプロセスとして予想されるエラー。ドメイン設計に含まれる場合がある
- パニック
  - 処理不可能エラー　ゼロ除算、メモリ不足など
- インフラエラー
  - ネットワークタイムアウトや認証失敗など アーキに含まれるがビジネスプロセスではない

ドメインエラーなのかそうではないのかはっきりしない場合はドメインエキスパートに聞く

エラーの種類ごとに異なる実装が必要

ドメインエラーはドメインエキスパートと議論して型システムで文書化されるべき
パニックは WF を放棄し例外を発生させる main などで捕捉する
インフラストラクチャエラーは状況によってどちらでも適切
マイクロサービスなら例外でいいが、モノリシックならエラー処理を明示したほうがいいかも

    ドメインエラーと同じ様に扱うことでドメインエキスパートに対応を相談せざるを得なくなるかもしれない

### 型によるドメインエラーのモデリング

ドメインをモデリングする場合はドメインの型で作成した
エラーも同様

エラーについて選択型を用意し、対応が必要なエラーの種類ごとに個別ケースを用意する

```fs
type PlaceorderError =
    | ValidationError of string
    | ProductOutStock of ProductCode
    | RemoteServerError of RemoteServerError
```

選択型で列挙することで、どんなエラーが起こりうるかが明示的になる
変更も容易で、安全

一般的にはアプリケーションの開発する上でエラーケースが浮かび、それをど名ネラーとして扱うかを決定することになる

### エラー処理はコードの見た目を悪くする

いちいちパターンマッチが必要になるので、見た目が悪い

## 10-3 Result を生成する関数の連鎖

A -> Result<B,E>
この次の関数のシグネチャは
B -> Result<C,E>

鉄道にちなんで「スイッチ関数」もしくは一般的に「モナディック関数」と呼ぶこともある

### アダプターブロックの実装

簡単で
す！！！！！！！

B -> Result<C,E>の関数が
Result<B,E> -> Result<C,E>になり、E が渡されたとき E を返す様なアダプタを実装する
これを bind という

```fs
// ラムダ式の例
let bind switchFn =
    fun twoTrackInput ->
        match twoTrackInput with
        | Ok success -> switchFn success
        | Error failure -> Error failure
// もっとわかりやすい例
let bind switchFn twoTrackInput =
    match twoTrackInput with
    | Ok success -> switchFn success
    | Error failure -> Error failure
```

```
-------|-----------|----Ok
Ok     | \ switchFn|
--------|----------|----Error
Error   ↑ここでinのErrorとSwitchFnがErrorだった時が同じパスになる
```

`fs/10-3ErrorHandle.fsx`

逆に単一トラックの関数を二つのトラックの関数に変換するアダプタも便利
これを map という

- 入力は 2 トラックの関数と 2 トラックの値
- 入力成功時、1 トラックの関数に渡す
- 入力が失敗だった時は関数をバイパス

```fs
let map f aResult =
    match aResult with
    | Ok success -> Ok (f success)
    | Error failure -> Error failure
```

```
-------|----------|----Ok
Ok     |  f       |fの結果をOkでラップする
-----------------------Error
Error
```

### Result 関数の整理

これらアダプタ関数は同じ名前のモジュールにぶち込むのが標準的

Result.fs などに入れる

### 合成と型チェック

「スイッチ」関数を「2 トラック」関数にへ変換することで関数の「形」をそろえることに注力

あるステップの出力が次のステップの入力と一致する限り型はトラックに沿って変更できる

### 共通のエラー型に変換する

パイプライン内のすべての関数は同じエラー型を持つ必要がある
->エラー型を変更して互換性を持たせる

失敗トラックの値に作用する関数をつくる
mapError という

```fs
let mapError f aResult =
    match aResult with
    | Ok success -> Ok success
    | Error failure -> Error (f failure)
```

```fs
type FunctionA = Apple -> Result<Bananas,AppleError>
type FunctionB = Bananas -> Result<Cherries, BananaError>

type FruitError =
| AppleErrorCase of AppleError
| BananaErrorCase of BananaError

let functionA : FunctionA =
// 省略

let functionAWithFruitError input =
    input
    |> functionA
    |> Result.mapError AppleErrorCase
```

## 10-4 パイプラインでの bind と map の使用

↓ このファイル見ればすべてわかる。
`fs/10-3ErrorHandle.fsx`

## 10-5 他の種類の関数を 2 トラックモデルに適合させる

これまで 1 トラックとスイッチというふたつの関数型を見た。
それ以外のパターン

- 例外を投げる関数
- 何も返さない行き止まり関数

### 例外の処理

我々の管理外のライブラリとかサービスで例外が起きたら

トップレベル以外では補足の必要はないが、ドメインの一部として扱いたい場合

例外を返す関数を、Result に変換するアダプターをつくる

```fs
// エラーを起こしたサービスを追跡
type ServiceInfo = {
    Name : string
    Endpoint: Uri
}

type RemoteServiceError = {
    Service : ServiceInfo
    Exception : System.Exception
}
```

サービス情報をもとのサービス関数をアダプターブロックに渡す

全てをキャッチする必要はなく、ドメインに関するものだけキャッチする

```fs
let serviceExceptionAdaptor serviceInfo serviceFn x =
    try
        Ok (serviceFn x)
    with
    |:? TimeoutException as ex ->
        Error {Service=serviceInfo; Exception:ex}
    |:? AuthorizationException as ex ->
        Error {Service=serviceInfo; Exception:ex}
```

`fs/10-5ExceptionHandle.fsx`

### 行き止まり関数の処理

出力がない関数

この関数はほとんど IO がある
これを 2 トラックモデルで動作させるには別のアダプタブロックが要る

いわゆるパススルー的な

これを Tee という

```fs
let doNoting _n =
    ()
let tee f x =
    f x
    x
```

## 10-6 コンピュテーション式

これまでは単純なエラー処理を扱った

Result を生成する関数を bind で連鎖して、2 トラックモデルにした。

実際は条件分岐やループと組み合わせたり、深掘りネストする Result を扱う場合があるかも

こういう場合「コンピュテーション式」という形で救済措置がある

bind の煩わしい部分を裏に隠せる

### コンピュテーション式とは

特別な形の式ブロック

Result ように result というコンピュテーション式を作ってみる
必要なのは ↓ の二つ

- bind
- return

実装は省略
Result.fs みて

2 トラックの場合

```fs
let placeOrder unvalidatedOrder =
    validateOrderAdapted
    >> Result.bind priceOrderAdapted
    >> Result.map acknowledgeOrder
    >> Result.map createEvents
```

コンピュテーション式の場合、validateOrder や priceOrder の出力が Result でないかのように扱える

```fs
let placeOrder unvalidatedOrder =
    result {
        let! validatedOrder = validateOrder unvalidatedOrder |> Result.mapError PlaceOrderError.Validation
        let! pricedOrder = priceOrder validatedOrder |> Result.mapError PlaceOrderError.Pricing
        let acknowledgementOption = acknowledgeOrder pricedOrder
        let events = createEvents pricedOrder acknowledgementOption
        return events
    }
```

- result というコンピュテーション式は result で始まり、ブロックになる
- `let!`は結果を自動的にアンラップしている
- エラー型は全体で一致する必要があるので、Result.mapError で共通型にする
- return でブロック全体の値を示す

Result がないように見える

### コンピュテーション式の合成

validateOrder と priceOrder が result コンピュテーション式で定義されているとする

大きな Result 式で扱える
これを placeOrder で使用できる

placeOrder はさらに大きな result 式で使える

### Result で注文を検証する

result コンピュテーション式を使って、エラー処理ロジックを隠す

ただし各 Result の型は合わせる必要がある

```fs
type ResultBuilder() =
    member this.Return(x) = Ok x
    member this.Bind(x,f) = Result.bind f x
let result = ResultBuilder
```

### Result のリストを扱う

toValidateOrderLine を list.map で各行に対して適用していたが、
これが Result になると Result のリストになってしまう
なので、Result のリストではなくリストの Result が必要

list をループし、一つでも失敗したら全体が Error になり、すべて成功なら成功値リストになる

#### 実装時のコツ

F#ではリストがリンクリスト

一つのアイテムを含む Result を、アイテムのリストを含む Result に前置する

関数型プログラミングでは、cons 演算子としても知られる

prepend アクションの新しいバージョンが必要

```fs
let prepend firstR restR =
    match firstR, restR with
    | Ok first, Ok rest -> Ok (first::rest)
    | Error err1, Ok _ -> Error err1
    | Ok _, Error err2 -> Error err2
    | Error err1, Error _ -> Error err1
```

Result<'a>と Result<'a list>で Result<'a list>に統合する

ただし、これは error の場合最初のエラーになる。
実際はすべてのエラーを保存したい。
これをアプリカティブという

## 10-7 モナド等

プログラミングパターンの一つ

### モナドって??

モナディックな関数を直列につなげることができるものを指す

以下三つの要素を持つ言葉

- データ構造
- 関連するいくつかの関数
- 関数がどのように動作しなければならないかについてのいくつかのルール

モナドであるためには、データ型には return と bind という関連する関数が必要

- return(pure)は、通常の値をモナディック型に帰る関数
- bind(flatMap)は、モナディック関数を連鎖させられる関数

これらの関数がどのように動作すべきかのルールはモナド則という

#### モナディックって??

「通常」の値を受け取り、ある種の「拡張」された値を返す関数のこと

拡張された値とは Result 型でラップされたものと言える

モナディック関数とは、Result を生成する「スイッチ関数」の仲間

int list とか int async みたいに
ある型にある性質 or 構造を付加するもの
で、何らかの性質が付加されたものを共通化するのがモナド

#### モナド則って??

構造や性質が付加された中の値を維持する規則

これらの関数がどのモナドでも同じようにふるまう

```cs
// Unit関数、元の型に対して構造や性質を付加
// 12に対して配列という構造が付加されているだけ
// 12という中身はある
Unit(12) == [12]
Unit("ABC") == ["ABC"]
//Taskの場合
Unit(12) == Task.FromResult(12)
Unit("ABC") == Task.FromResult("ABC")


// Map関数「「元になる型」を引数にとる関数」を引数にとり、
// 「構造や性質」内部で関数を実行する。
// 戻り値は元と同じ「構造や性質」の中に入る

```

### アプリカティブを使って並列に合成する

モナディックな値を並列に組み合わせることが可能
検証の必要があれば、最初のエラーだけを残すだけでなく、
すべてのエラーを結合するためにアプリカティブなアプローチを使うことになる

詳しくは解説がない

[fsharpforfunandprofit.com]

## 10-8 非同期エフェクトの追加

当初の設計ではエラーエフェクトだけではなく、非同期エフェクトもある

AsyncResult 型に合わせて asyncResult コンピュテーション式を定義

使い方はほぼ同じ

```fs
let validateOrder: ValidateOrder =
    fun checkProductCode Exists checkAddressExists unvalidatedOrder ->
        asyncResult {
            let! orderId =
                unvalidatedOrder.OrderId
                |> OrderId.create
                |> Result.mapError ValidationError
                |> AsyncResult.ofResult // ResultをAsyncResultにする
            let! customerInfo =
                unvalidatedOrder.CustomerInfo
                |> toCustomerInfo
                |> AsyncResult.ofResult
            let! checkedShippingAddress =
                unvalidatedOrder.ShippingAddress
                |> toCheckedAddress checkAddressExists
            let! shippingAddress =
                checkedShippingAddress
                |> toAddress
                |> AsyncResult.ofResult
            let! billingAddress = // 省略
            let! lines =
            ///// みたいな感じ
        }
```

result 式を asyncResult に置き換え&全ての要素を AsyncResult にする

住所の検証を二つに分けた
エフェクトをすべて元に戻すと CheckAddresExisits が AsyncResult を返すため

エラー型が不適切
結果を処理するための toCheckedAddress 関数を作成、サービス固有のエラーをドメインのエラーにマッピング

## 10-9 まとめ

エラー処理や非同期エフェクトに対する型安全なアプローチを取り入れたパイプラインの改訂版実装が完了

この手間をかけることでパイプラインコンポーネントが問題なく動作するという信頼が得られる

これからはドメインと外界とのインタラクションの実装に取り組む

データのシリアライズ/デシリアライズや、状態を DB に永続化するなど

# 第 11 章 シリアライズ

入力をコマンド、出力をイベントとして扱うが、コマンドはどこから来て、イベントはどこへ行く?
->境界付けられたコンテキストの外のインフラストラクチャ
-> メッセージキューや Web リクエスト等

ここら辺のインフラは固有のドメインを理解していないので、ドメインモデルの型をインフラが理解できる形式(json,xml,protobuf 等)に変換

外部のインフラストラクチャと連携する際に重要な側面の一つがドメインモデルの型をシリアライズ・デシリアライズが容易なものに変換する能力

シリアライズ可能な型を設計し、ドメインオブジェクトをこれら中間型との間で変換する方法

## 11-1 永続化とシリアライズ

永続化
->作成元のプロセスよりも長く続く状態を意味する

シリアライズ
->ドメイン固有の表現から別の永続化が容易な表現に変換するプロセス

## 11-2 シリアライズのための設計

ドメインオブジェクトは複雑で深くネストされているので、シリアライザはうまくあつかえない
ドメインオブジェクトをシリアライズ専用の型(DTO)に変換、その DTO をシリアライズ

デシリアライズはその逆

デシリアライズはできるだけクリーンであることが望ましい
->DTO へのデシリアライズは基礎のデータが壊れてなければ常に成功されるべき
->ドメイン固有の検証は、DTO からドメインオブジェクトの変換で行われるべき

## 11-3 シリアライズコードとワークフローの連携

シリアライズプロセスは WF とパイプラインにもう一つコンポーネントを追加するだけ

デシリアライズ->ワークフロー->シリアライズ

```fs
type MyInputType = //...
type MyOutputType = //...

type Workflow = MyInputType -> MyOutputType

type JsonString = string
type MyInputDto = //...

type DeserializeInputDto = JsonString -> MyInputDto
type InputDtoToDomain = MyInputDto -> MyInputType

type MyOutputDto = //...

type OutputDtoFromDomain = MyOutputType -> MyOutputDto
type SerializeOutputDto = MyOutputDto -> JsonString
```

インフラに公開されるのは最終的な
デシリアライズ->ドメインオブジェクト変換->WF->DTO 変換->シリアライズ
というパイプライン
実際はエラーや非同期などの処理も必要

### 境界付けられたコンテキス間の契約としての DTO

WF が受け取るコマンドは、ほかの境界付けられたコンテキストの出力によって trigger
WF が発するイベントは他のコンテキストの入力

これらのイベントとコマンドでこちらの境界付けられたコンテキストが従うべき契約の形が決まる
イベントやシリアライズされたフォーマットである DTO を変更するときは慎重に
つまりフォーマットは完全にコントロールすべきであり、ライブラリ任せは NG

## 11-4 シリアライズの全体例

ドメインオブジェクトを Json にしたり json からデシリアライズする例

```fs
module Domain =
    // こいつらに直接シリアライズできない
    type String50 = String50 of string
    type Birthdate = Birthdate of DateTime

    type Person = {
        First: String50
        Last: String50
        Birthdate: Birthdate
    }
    // 対応するDTO型を作成し、すべてのフィールドがプリミティブになるようにする
module Dto =
    type Person = {
        First: string
        Last: string
        Birthdate: DateTime
    }

module Person =
    let fromDomain (person:Domain.Person) :Dto.Person =
        let first = person.First |> String50.value
        let last = person.Last |> String50.value
        let birthdate = person.Birthdate |> Birthdate.value
        {First = first; Last = last; Birthdate = birthdate}
    // 実装
    let toDomain (person:Dto.Person): Result<Domain.Person, string> =
        result {
            let! first = dto.First |> String50.create "First"
            //String50.createは、エラーメッセージ作成のためstringをパラメータに持つ
            let! last = dto.Last |> String50.create "Last"
            let! birthdate = dto.Birthdate |> Birthdate.create
            return {First = first; Last = last; Birthdate = birthdate}
        }
    // 実装
```

エラー処理のため result コンピュテーション式を使う

### Json シリアライザーのマッピング

サードパーティライブラリを使うのが楽

API は関数型に親和性があるとは限らないので、Result にラップするかも

```fs
// jsonモジュール内でラップ
module Json =
    open Newtonsoft.Json
    let serialize obj =
        JsonConvert.SerializeObject obj
    let deserialize<'a> str =
        try
            JsonConvert.DeserializeObject<'a> str
            |> Result.Ok
        with
        | ex -> Result.Error ex
```

### シリアライズパイプラインの全体

DTO からドメインへの変換とシリアライズ関数を使うことで、ドメイン型の Person レコードを JSON 文字列に変換可能

`fs/11-4serialize.fsx`参照

シリアライズは result なしのため簡単だが、デシリアライズの場合は共通のエラーに変換するようにする

result 式で隠ぺいする

実際のアプリケーションではメッセージをログに記録したうえで呼び出し元にエラーを返すこともできる
アポリカティブでエラーを並列に合成する

エラー処理せずに、パニックにする手もある
パイプラインがどう処理したいか次第

### ほかのシリアライザーとの連携

PersonDto 型に属性を追加する必要があるかもしれない

シリアライズ型をドメインから分離することで、ドメイン型が汚染されることを防ぐ(変な属性とか)

### 複数バージョンのシリアライズ型を使う

DTO は境界付けられたコンテキスト間の契約として機能する

つまり複数バージョンの DTO をサポートする必要があるかもしれない

## 11-5 ドメイン型を DTO に変換する方法

ドメイン型は複雑
DTO 型はプリミティブ型のみの単純構造である必要がある
複数のドメイン型が与えられたとき、DTO はどうすべきか

### 単一ケース共用体

```fs
type ProductCode = ProductCode of string
```

この場合、対応する型は単なる string 型

### オプション型

None が null
null 許容にする

### レコード型

```fs
/// ドメイン型
type OrderLienId = OrderLineId of int
type OrderLineQty = OrderLineQty of int
type OrderLien = {
    OrderLineId : OrderLineId
    ProductCode : ProductCode
    Quantity : OrderLineQty option
    Description : string option
}

/// 対応するDTO型
type OrderLineDto = {
    OrderLineId : int
    ProductCode : string
    Quantity : Nullable<int>
    Description : string
}
```

### コレクション

シーケンス、リスト、セットなどはシリアライズフォーマットでサポートされている配列に変換すべき

```fs

type Order = {
    ///
    Lines : OrderLine list
}

type OrderDto = {
    ///
    Lines : OrderLineDto[]
}
```

マップとかの複雑なコレクションの場合、シリアライズフォーマットによってアプローチが異なる

### 列挙体として扱える判別共用体

ドメイン型には多くの場合、すべてのケースが異なる名前で余分なデータを持っていない共用体がある
それらは.NET の列挙で表現可能

```fs
type Color =
    | Red
    | Green
    | Blue

type ColorDto =
    | Red = 1
    | Green = 2
    | Blue = 3
```

デシリアライズは定義されてないものの値の場合の処理を必ず行う

もしくは列挙として扱える共用体を文字列としてシリアライズし、ケースの名前を値として使うこともできる

この方法では名前を変更することによる問題に注意が必要

### タプル

シリアライズでサポートされているとは加賀儀らないので、
専用のレコードで表現する

### 選択型

選択されている型を示す「タグ」と、各ケースに対応するデータを含むレコードとして表現可能

特定のケースが DTO 変換されると、それ以外のフィールドは null(リストの場合は空)になると

```fs
type Name = {
    First : String50
    Last : String50
}
type Example =
    | A
    | B of int
    | C of string list
    | D of Name

// シリアライズ可能なDTO型

type NameDto = {
    First : string
    Last : string
}

type ExampleDto = {
    Tag : string // "A" "B" "C" "D"
    BData:Nullable<int>
    CData:string[]
    DData:NameDto
}

// シリアライズは、選択されたケースを変換し、それ以外をnullにする
// デシリアライズする場合、「タグ」でパターンマッチ
// デシリアライズ前に該当データがnullではないことを必ず確認する

```

### レコード型や選択型のマップを使ったデシリアライズ

複合型の別のシリアライズ方法として、すべてをキーバリューマップとしてシリアライズする手もある

全ての DTO は同じように.NET の型である IDictionary<string, obj>として実装される

```fs
let nameDtoFromDomain (name:Name) : IDictionary<string:ojb> =
    let first = name.First |> String50.value :> obj
    let last = name.Last |> String50.value :> obj
    [
        ("First", first)
        ("Last", last)
    ]
    |> dict
```

ここではキーと値のペアから辞書を作成する

これを JSON シリアライズすると
あたかも別の NameDto 型をシリアライズしたかのような出力になる

`:>` で明示的に obj にキャストする必要がある
キーの値が選択型のどれかによって異なる

### ジェネリクスによる共通構造の共有

多くの場合、ドメイン方はジェネリック

# 第 12 章 永続化

ドメインモデルを設計する際に「永続化非依存」を考慮する

DDD で営ぞ k 儒家を扱う一般的ガイドライン

- 永続化を端に追いやる
- コマンド(更新)とクエリ(読み取り)を分離
- 境界付けられたコンテキストは、それぞれ独自のデータストアを持つ必要がある

## 12-1 永続化を端に追いやる

理想的にはすべての関数を「純粋」にしたい
ワークフローの設計では IO や永続化に関するロジック一切用いないことを目指す

- ビジネスロジック含むドメインの中心部分
- IO 含む「端」の部分

混同する例
請求書に支払いをするためのロジック

- DB から請求書をロード
- 支払いを適用
- 請求書が支払われたとき、DB で支払われた状態にし、イベントを発行する
- 支払いが完全でない場合、DB で半端に支払われた状態にする

関数が純粋でないのでテストが難しい

純粋なビジネスロジックを applyPayment 関数に切り出す。

ドメインの外->|->IO->純粋関数->IO|->ドメインの外

IO をする関数について単独でテストしたい場合、
呼び出すすべての IO アクションを表す関数パラメータを追加するだけで OK

```fs
let payInvoice
    loadUnpaidInvoiceFromDatabase //依存関係
    markAsFullyPaidInDb // 依存関係
    updateInvoiceInDb // 依存関係
    payInvoiceCommand =
    // この関数でIO ドメイン IOという処理をする
```

このような IO をする合成関数は、コンポジションルートに置くべき

### クエリに基づく意思決定

純粋関数の中で IO が挟まり、その結果に基づいて判断が必要となる場合

解決例

ドメインの外->|->IO->純粋関数->IO->純粋関数->IO|->ドメインの外

この場合ドメインと外部の境界が混在し、複雑化する

↓ の形でワークフローを複数に分割するという手がある
ドメインの外->|->IO->純粋関数->IO|->ドメインの外

### リポジトリパターンは?

リポジトリパターンが関数型に紐づくか微妙だが、別に紐づけなくてもいい

リポジトリパターンは、可変性を前提としたオブジェクト指向で、永続化を隠す便利な方法

ただしすべてを関数としてモデル化して永続化を端に追いやるとリポジトリパターン入らなくなる

## 12-2 コマンドとクエリの分離

CQS という原則

関数型アプローチでは全てのオブジェクトは不変である設計

ストレージも不変であるとする、つまり変更するために新しいもので置き換える

```fs
type InsertData = DataStoreState -> Data -> NewDataStoreState
type ReadData = DataStoreState -> Query -> Data
type UpdateData = DataStoreState -> Data -> NewDataStoreState
type DeleteData = DataStoreState -> Key -> NewDataStoreState
```

コマンドとクエリは分離
コマンドは mutation
クエリは query

- データを返す関数は副作用を持つべきでない
- 副作用を持つ関数はデータを返すべきではない。つまり Unit を返却する

```fs

type InsertData =  Data -> Unit
type ReadData =  Query -> Data
type UpdateData =  Data -> Unit
type DeleteData =  Key -> Unit
```

実際は Async と Result が含むかもなので、DB 用の Result 型を用意

### コマンドクエリ分離責務 (CQRS)

書き込みと読み込みに同じオブジェクトを再利用したくなることがある

```fs
type SaveCustomer = Customer -> DbResult<Unit>
type LoadCustomer = CustomerId -> DbResult<Customer>
```

いくつかの理由により、同じ型の物を読み書きに利用するのはよくない

- クエリが返すデータはデータ書き込み時に必要なものとは異なることがある
  - 新しいレコードを作成するときには生成された ID やバージョンなどのフィールドはないが、取得時には返却される
  - 非正規化されたデータや計算値を取得できるが、書き込み時は使えない
    それぞれのデータ型を一つの用途に合わせて設計したほうがいい
- クエリとコマンドが独立して進化する傾向があるため、結合すべきではない

ドメインモデリングの観点から、クエリとコマンドはほとんどの場合異なる

クエリ型とコマンド型を分離することで、それぞれのモジュールを分けることが自然にできる
真の意味で疎結合になる

```fs
type SaveCustomer = ReadModel.Customer -> DbResult<Unit>
type LoadCustomer = CustomerId -> DbResult<ReadModel.Customer>
```

### CQRS とデータベース分離

データストアを二種類準備する
書き込み最適化(インデックスなし、トランザクションあり)と、クエリ最適化(非正規化、大量インデックス)

論理的に分離するだけで、物理的に二つにする必要はない

書き込みはテーブル、読み取りはビューにするなど

物理的分離が必要な場合、データコピープロセスの実装が要る

結果整合性があるかどうかが重要
ドメインの状態によって判断

### イベントソーシング

CQRS としばしば関連
イベントソーシングアプローチでは、現在の状態は単一のオブジェクトで永続化されるのではなく、
状態に変化が起こるたびに、変化を表すイベントが永続化される
->これにより、バージョン管理システムのように各変更がキャプチャされる

## 12-3 境界付けられたコンテキストはデータストレージを所有しなければならない

- 独自のデータストレージと関連スキーマを所有して、ほかの関連付けられたコンテキストと調整することなく、いつでもそれらを変更できる
- 他のシステムが、境界付けられたコンテキストが所有するデータに直接アクセスできない。何らかのコピー or パブリック API で

### 分離の実態

ドメインのニーズによる

全てのデータストアが完全分離か、一つのデータストアかという例もある

### 複数ドメインのデータを扱う

レポーティングやアナリティクスの場合
複数のコンテキストからデータにアクセスする必要がある
↓
レポーティングと BI を別ドメインとして扱い、ほかの境界付けられたコンテキストが所有するデータをレポーティング用に設計された別システムにコピー
実装に手間がかかるが、保守しやすい

もしくは
従来の RTL で、BI システムにデータをコピー
実装が容易だが、保守の手間が増える

## 12-4 ドキュメント DB を扱う

簡単
DTO に変換してストレージにぶち込むだけ

## 12-5 RDB を扱う

関数型プログラミングの原理で開発されたデータモデルは、RDB と親和性が高い
なぜなら、データと動作が混在しないので、保存や取得が直接的になる

### いい点

RDB のテーブルが関数モデルのレコードのコレクションに対応している
(select where) -> (map filter)

シリアライズ手法で、テーブルに直接マッピングできるレコード型を設計する戦略をとる

### 悪い点

プリミティブ型しかない
ドメイン型をアンラップしないといけない
選択型はさらにめんどい

### 選択型をテーブルにマッピング

- 全て同じテーブル
- 各ケースを別テーブルに

各ケースで共通点が少なければ別テーブルでいいが、そうでなければ共通テーブルでもいい

#### 全て同じテーブルの場合

ケースによっては使わないというパラメータは null 許容が要る

#### 各ケースを別テーブルに

各ケースに対応する子テーブルを二つ用意

主キーは同じ値

```sql
create table contact_info(
    contact_id int not null,
    is_email bool not null,
    is_phone bool not null,
    constraint pk_contact_info primary key (contact_id)
)

create table contact_email(
    contact_id int not null,
    email_address varcher(100) not null,
    constraint pk_contact_email primary key (contact_id)
)
create table contact_phone(
    contact_id int not null,
    phone_number varcher(100) not null,
    constraint pk_contact_phone primary key (contact_id)
)
```

### ネストした型をテーブルにマッピング

- 内部型が独自のアイデンティティを持つ DDD エンティティの場合
  - 別テーブルに
- 値オブジェクトの場合
  - 親データにインラインで
  - どの値オブジェクトの列かを明示した名前にする

### RDB からの読み取り

F#では ORM を使うことは少なく、生 SQL コマンドを直接扱う

最も便利な方法は F#の SQL 型プロバイダを使う

FSharp.Data.SqlClient 型プロバイダを使用する

一般的になランタイムライブラリではなく、型プロバイダを使用することで、SQL 型プロバイダがコンパイル時に SQL クエリや SQL コマンドに応じた型を作成

SQL がおかしければコンパイルエラー
正しければ SQL の出力と完全一致するレコード型

CustomerId で Customer を一つ取得する特

まず、コンパイル時に使用する接続文字を定義

`fs/12-5rdb.fsx`

BirthDate は null 許容なので、dbRecord.Birthdate を option 型にしている

```fs
let bindOption f xOpt =
    match xOpt with
    | Some x -> f x |> Result.map Some
    | None -> Ok None
```

カスタムの toDomain を書く
不正なデータが絶対にないという場合は、result のエラーを例外に変換するヘルパーを使っていい

いずれにしても toDomain があれば、DB から読み取り、その結果をドメインとして返すコードを書ける

レコードなし or レコードあり or レコード複数すべてについて処理をしていること

後でぬるりが起きるよりも事前に明示的に判断することがいい

```fs
let convertSingleDbRecord tableName idValue records toDomain =
    match records with
    | [] ->
        let msg = sprintf "Not found. Table= %s Id= %A" tableName idValue
        Error msg
    | [ dbRecord ] -> dbRecord |> toDomain |> Ok
    | _ ->
        let msg = sprintf "Multiple records found. Table= %s Id= %A" tableName idValue
        raise (DatabaseError msg)

let readOneCustomer (productionConnection: SqlConnection) (CustomerId customerId) =
    use cmd = new ReadOneCustomer(productionConnection)
    let tableName = "customer"
    let records = cmd.Execute(customerId = customerId) |> Seq.toList
    convertSingleDbRecord tableName customerId records toDomain
```

### RDB から選択型を読み取る

基本的には同じだが少し複雑

単一のレコードで扱う場合、フラグでどのケースを作成するか判断

ORM を使うべきでは?と思うが、ネストした選択型の処理ができない

### RDB への書き込み

ドメインオブジェクトを DTO に変換して、挿入 or 更新コマンドを実行
SQL 型プロバイダですべてのテーブルに型を設定

writeContact 関数を定義して DTO に変換後、insert

もしくは手書き SQL で insert

## 12-6 トランザクション

多くの場合、まとめて全 or 無の保存が必要なものがある

```fs
let connection = new SqlConnection()
markAsFullyPaidAndPaymentCompleted connection paymentId invoiceId
```

異なるサービスとコミュニケーションをとり、サービスをまたぐトランザクションを行う方法がない場合もある
-> この場合、不具合検出プロセスや、失敗時に補填トランザクションを使用する

↓ 例

```fs
markAsFullyPaid connection invoiceId

let result = markPaymentCompleted connection paymentId

match result with
|Error err ->
unmarkAsFullyPaid connection invoideId
|Ok _ -> ...
```

## 12-7 まとめ

- クエリとコマンドの分離
- IO を端に追いやる
- コンテキストが自身専用のデータストアを持つ
- RDB 操作の仕組み

# 13 章 設計を進化させ、きれいに保つ

要件が変わるにつれてモデルが混とんとする。

ドメイン駆動設計は一度だけではない
開発者、DE、各ステークホルダが継続的に協力することを意味する

要件が変わったら最初にドメインモデルを見直す

- WF に新しいステップを追加
- WF の入力を変更
- 主要なドメイン型を変更、システムへの波及を確認
- WF 全体をビジネスルールに適合するように変更

## 13-1 送料の追加

送料を請求する際、特別な計算方法を用いたいという要件
↓
必要な物事
米国内への発想、米国内でも遠隔州かそうでないかで計算を分けるときなど、独特の条件でいくつも分離する

### アクティブパターンによるビジネスロジックの簡素化

ドメインに置ける「分類」の考え方を実際の価格設定ロジックから分離する

F#にはアクティブパターンという機能がある
条件分岐を名前付く選択しのセットに変えて、パターンマッチを可能にする

```fs
let (|UsLocalState|UsRoteState|International|) address =
    if address.Country = "US" then
        match address.State with
        |"CA" | "OR" | "AZ" | "NV" ->
            UsLocalState
        | _ ->
            UsRemoteState
    else
        International

let calculateShippingCost validatedOrder =
    match validatedOrder.ShippingAddress with
    | UsLocalState -> 5.0
    | UsRemoteState -> 10.0
    | International -> 20.0
```

#### アクティブパターンとは

### ワークフローに新しいステージを追加する

上記の送料計算を使用する

validate -> price -> acknowledge
↓
validate -> price -> AddShippingInfo -> acknowledge

追加情報を把握するために新しい型がいくつか必要になる

新しい方と既存型の合成にするか、既存型に新しい情報を含めてしまうか
前者にはいくつか利点がある

- 次のステップに必要な型が変わらない
- 既存型に追加する場合、初期化の検討が必要

送料は注文にどのように保存されるべきか

- トップレベルのフィールドか
- 新しい型の明細にしてしまうか
  後者の場合、ヘッダーのフィールドを含めるための特別なロジックが不要
  明細の合計から注文合計を計算できる
  ただし、送料を複数作成してしまう可能性がある、印刷時の順序を気を付ける

### パイプラインにステージを追加するその他の理由

ステージが他のステージから隔離されていて、要求された型に適合して入れば、
追加や削除を安全にできる

- 運用上の透明性を高めるためのステージを追加すれば、パイプラインの内部で何が起きているか簡単に確認できる
- 認可チェックするステージを追加すれば、権限が足りないときに残りのパイプラインをスキップできる
- 設定や入力からのコンテキストに基づいて、コンポジションルートでステージを動的に追加・削除もできる

## 13-2 VIP顧客への対応を追加

VIPは送料無料や夜間配達などができる

絶対に避けるべきなのは、ドメイン内のビジネスルールの出力をモデル化する(送料無料フラグを追加するなど)
↓
ビジネスルールへの入力(「顧客がVIPである」)を保存し、その入力に応じてビジネスルールを動作させる


VIPステータスは、Webサイトへのログインと関連づいていると仮定

既存型にパラメータを追加する方は、他の顧客状態と影響がありそう

```fs
type CustomerInfo ={
    ...
    IsVip : bool
    ...
}

type CustomerStatus =
|Normal of CustomerInfo
| Vip of CustomerInfo
type Order = {
    ...
    CustomerStatus:CustomerStatus
    ...
}
```

最善は折衷案
顧客情報とは独立した「VIP」か否かを表す情報を追加
他のステータスが必要になったらその度に追加する

```fs
type VipStatus =
|Normal
|Vip

type CustomerInfo ={
    ...
    VipStatus:VipStatus
    ...
}
```

### WFに新しい入力を追加する

VipStatus<VIPステータス>フィールドを新設する前提で進める

VipStatusはどこから取得するのか
↓
UnvalidatedCustomerInfoから
↓
DTOから


つまりUnvalidatedCustomerInfoとDTO両方にフィールドを追加
どちらも単なる文字列で、値が無いことはnullで表す

```fs
module Domain =
    type UnvalidatedCustomerInfo = {
        ...
        VipStatus: string
    }
module Dto =
    type CustomerInfo = {
        ...
        Vipstatus : string
    }
```

そしてようやくUnvalidatedCustomerInfoのstatusフィールドを他のすべてのフィールドと共に使用して、ValudatedCustomerInfoを構築可能になる

```fs
let validatecustomerInfo unvalidatedCustomerInfo =
    result {
        ...
        let! VipStatus = VipStatus.create unvalidatedCustomerinfo.VipStatus
        let customerInfo : CustomerInfo ={
            ...
            VipStatus = VipStatus
        }
        return customerInfo
    }
```

### 送料無料ルールをWFに追加

新しいセグメントを表す型を定義
```fs
type FreevipShipping = ProcedOrderWithShippingMethod -> ProcedOrderWithShippingMethod
```
これをWFに挿入しておしまい

## 13-3 プロモーションコードのサポート追加

注文時にプロモーションコードを提示されたら割引する

- 注文時に任意のプロモーションコードを入力させる
- 入力されたコードが存在する時、特定の製品が別の価格で提供
- 注文はプロモーションコード割引が適用されたことを明示

### ドメインモデルにプロモーションコードを追加

新しいプロモーションコードのフィールド
```fs
type PromotionCode = PromotionCode of string
type ValidatedOrder = {
    ...
    PromotionCode : PromotionCode option
}
```
この時点でコンパイルエラー

UnvalidatedOrderとDTOも同様に追加
DTOではnullが欠損値であることを示すため、optionではなくただのstring二する

### 価格計算ロジック変更

プロモーションコード有無で計算処理を分ける

既存モデル↓
```fs
type GetProductPrice = ProductCode -> Price
```

プロモーションコードに基づいて異なるGetProductPrice関数を提供する

- プロモーションコードがあるとき専用のGetProductPrice関数
- 無ければ既存のGetProductPrice関数

そこでファクトリー関数を使う
```fs
type GetPricingFunction = PromotionCode option -> GetProductPrice
```
↓optionだと不明確なので自己文書化できるように新しい型を作る
```fs
type ProcingMethod =
| Standard
| Promotion of PromotionCode
```
論理的には同等だが、ドメインモデルで分かりやすくする

ValidatedOrder型は次のようになる
```fs
type ValidatedOrder ={
    ... //他は同じ
    PricingMethod : PricingMethod
}
```
GetPricingFunctionは、次のように

```fs
type GetProcingFunction = PricingMethod -> GetProductPrice
```
さらにこれを新しい依存関係として注入

ここでコンパイルエラーが出るけれどそれを解消していく

### GetPricingFunctionの実装

```fs
type GetStandardPriceTable =
    

```
