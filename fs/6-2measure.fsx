[<Measure>]
type kg

[<Measure>]
type m

// 使うとき

let fiveKilos = 5.0<kg>
let fiveMeters = 5.0<m>

// ちなみにSI単位系はMicrosoft.FSharp.Data.UnitSystems.SIで定義されている

// 測定単位の互換性を強制し、一致したい場合エラーを返す

// fiveKilos = fiveMeters // コンパイルエラー
let listOfWights =
    [ fiveKilos
      // fiveMeters // コンパイルエラー
      ]

// キログラム量がキロであることを強制する
type KilogramQuantity = KilogramQuantity of decimal<kg>
