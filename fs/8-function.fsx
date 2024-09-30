let evalWith5ThenAdd2 fn = fn 5 + 2
let add1 x = x + 1
printfn "%d" (evalWith5ThenAdd2 add1) // 8
let square x = x * x
printfn "%d" (evalWith5ThenAdd2 square) // 27
