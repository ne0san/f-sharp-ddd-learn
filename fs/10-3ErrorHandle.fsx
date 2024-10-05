module Result =
    let bind f aResult =
        match aResult with
        | Ok success -> f success
        | Error failure -> Error failure

    let map f aResult =
        match aResult with
        | Ok success -> Ok(f success)
        | Error failure -> Error failure

    let mapError f aResult =
        match aResult with
        | Ok success -> Ok success
        | Error failure -> Error(f failure)

type Apple = Apple of string
type Bananas = Bananas of string
type Cherries = Cherries of string
type Lemons = Lemons of string
type AppleError = AppleError of string
type BananaError = BananaError of string

type FunctionA = Apple -> Result<Bananas, AppleError>
type FunctionB = Bananas -> Result<Cherries, BananaError>
type FunctionC = Cherries -> Lemons

type FruitError =
    | AppleErrorCase of AppleError
    | BananaErrorCase of BananaError

let functionA: FunctionA =
    fun apple ->
        if apple = Apple "Apple" then Ok(Bananas "Apple")
        elif apple = Apple "Banana" then Ok(Bananas "Bananas")
        else Error(AppleError "AppleError")

let functionB: FunctionB =
    fun bananas ->
        if bananas = Bananas "Bananas" then
            Ok(Cherries "Cherries")
        else
            Error(BananaError "BananaError")

let functionC: FunctionC = fun _cherries -> Lemons "Lemons"

let functionAWithFruitError input =
    input |> functionA |> Result.mapError AppleErrorCase

let functionBWithFruitError input =
    input |> functionB |> Result.mapError BananaErrorCase

let functionBWithFruitErrorBound = Result.bind functionBWithFruitError

let functionAB = functionAWithFruitError >> functionBWithFruitErrorBound

printfn "%A" (functionAB (Apple "Banana"))
printfn "%A" (functionAB (Apple "Apple"))
printfn "%A" (functionAB (Apple "d"))

let functionCMapped = Result.map functionC

let functionABC =
    functionAWithFruitError >> functionBWithFruitErrorBound >> functionCMapped

printfn "%A" (functionABC (Apple "Banana"))
printfn "%A" (functionABC (Apple "Apple"))
printfn "%A" (functionABC (Apple "d"))
