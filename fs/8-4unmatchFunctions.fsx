let add1 x = x + 1

let printOption x =
    match x with
    | Some v -> printfn "Some %A" v
    | None -> printfn "None"

5 |> add1 |> Some |> printOption // Some 6


let twelveDividedBy n = if n = 0 then None else Some(12 / n)
let printInt x = printfn "%d" x

// 4 |> twelveDividedBy |> printInt // Some 3

let add1ThenPrint = add1 >> printInt

add1ThenPrint 5 // 6
