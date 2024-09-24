// UnitQuantity の例
type UnitQuantity = private UnitQuantity of int // プライベートコンストラクタ

module UnitQuantity =
    // スマートコンストラクタという
    let create qty =
        if qty < 1 then
            Error "UnitQuantity can not be negative"
        else if qty > 100 then
            Error "UnitQuantity can not be more than 100"
        else
            Ok(UnitQuantity qty)
    // メソッドやん！！って思ったけどモジュール自体に定義しているものなので関数です
    let value (UnitQuantity qty) = qty
// privateだとパターンマッチでラップされたデータを取り出すのにコンストラクタがつかえなくなる
// 取り出す用のvalue関数を作って解決

// 共通コードを持つヘルパーモジュールを別途定義して繰り返しを省略できる
// 参照 Common.SimpleTypes.fs
