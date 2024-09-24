#load "SimpleValue.fsx"
open SimpleValue

// お試し
// コンパイルエラー
// let unitQty = UnitQuantity 1
let unitQtyResult = UnitQuantity.create 1

match unitQtyResult with
| Error msg -> printfn "Error: %s" msg
| Ok unitQty ->
    printfn "UnitQuantity: %A" unitQty
    let innerValue = UnitQuantity.value unitQty
    printfn "InnerValue: %d" innerValue
