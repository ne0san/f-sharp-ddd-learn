let add1 x = x + 1
let square x = x * x
let add1ThenSquare x = x |> add1 |> square
printfn "%d" (add1ThenSquare 5) // 36

let squareThenAdd1 = square >> add1
let quartic = square >> square
