
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

// 単純型
// 一つしかない判別共用体
// type Length = Length of int と書くこともできる
// ラッパーを簡単に作れるのでドメインモデリングではよくある
type Length =
    | Length of int



// レコード型の値を作成
let cube = {Depth= 5; Height= 2; Width=5}

// パターンマッチでレコードをバラす
let {Depth=depth; Height=height; Width=width} = cube

printfn "%d, %d, %d" depth height width


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



type CustomerId = CustomerId of int
//   ^型名         ^ケースラベル
let customerId = CustomerId 42


let (CustomerId innerValue) = customerId
printfn "%d" innerValue



let processCustomerId (CustomerId innerValue) = 
    printfn "%d" innerValue
