open System

type ServiceInfo = { Name: string; Endpoint: Uri }

type RemoteServiceError =
    { Service: ServiceInfo
      Exception: Exception }

// Resultを取り扱う関数群
module Result =

    // A -> Result<T,E> である関数を Result<A,E> -> Result<T,E> に変換する
    // ->引数がOkであった場合、中身をfに渡し、結果をそのまま返却
    // ->引数がErrorであった場合、そのErrorをそのまま返却
    let bind f aResult =
        match aResult with
        | Ok success -> f success
        | Error failure -> Error failure

    // A -> B である関数を Result<A,E> -> Result<B,E> に変換する
    // ->引数がOkであった場合、中身をfに渡し、結果をOkでラップする
    // ->引数がErrorであった場合、そのErrorをそのまま返却
    let map f aResult =
        match aResult with
        | Ok success -> Ok(f success)
        | Error failure -> Error failure

    // ResultがErrorであるとき、別のError型に変換する
    // ->引数がOkであった場合、中身をOkで再ラップして返却
    // ->引数がErrorであった場合、中身をfに渡し、結果をErrorでラップして返却 この時のerrorConstructorは別のError用型のコンストラクタ
    let mapError errorConstructor aResult =
        match aResult with
        | Ok success -> Ok success
        | Error failure -> Error(errorConstructor failure)

    // ドメインとしてキャッチする必要がある例外をResultに変換する
    let serviceExceptionAdaptor serviceInfo serviceFn x =
        try
            Ok(serviceFn x)
        with // ここのケースはドメインとして取り扱う例外のみをキャッチする
        | :? TimeoutException as ex ->
            Error
                { Service = serviceInfo
                  Exception = ex }
        | :? Exception as ex ->
            Error
                { Service = serviceInfo
                  Exception = ex }

    // なにも返却しない関数をつなぐために変換する
    let tee f x =
        f x
        x
// --------------------------------------------
// 各ステップの合成
// --------------------------------------------
// 単純型定義
type Apple = Apple of string
type Bananas = Bananas of string
type Cherries = Cherries of string
type Lemons = Lemons of string

// 各ステップの関数とエラー型定義
type FunctionA = Apple -> Result<Bananas, AppleError>
and AppleError = AppleError of string
type FunctionB = Bananas -> Result<Cherries, BananaError>
and BananaError = BananaError of string
type FunctionC = Cherries -> Lemons

// ワークフロー全体のエラー型定義
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

// FunctionAのErrorをFruitErrorに変換
let functionAWithFruitError = functionA >> Result.mapError AppleErrorCase
// FunctionBのErrorをFruitErrorに変換
let functionBWithFruitError = functionB >> Result.mapError BananaErrorCase
// functionBWithFruitErrorの引数をResultに変換
let functionBWithFruitErrorBound = Result.bind functionBWithFruitError
// ABを結合
let functionAB = functionAWithFruitError >> functionBWithFruitErrorBound

printfn "%A" (functionAB (Apple "Banana"))
printfn "%A" (functionAB (Apple "Apple"))
printfn "%A" (functionAB (Apple "d"))

// FunctionCの引数と戻り値をResultに変換
let functionCMapped = Result.map functionC

// ABCを結合
let functionABC =
    functionAWithFruitError >> functionBWithFruitErrorBound >> functionCMapped

printfn "%A" (functionABC (Apple "Banana"))
printfn "%A" (functionABC (Apple "Apple"))
printfn "%A" (functionABC (Apple "d"))
printfn "----"



// --------------------------------------------
// 例外をResultに変換
// --------------------------------------------
type Address = Address of string
type UnvalidatedAddress = UnvalidatedAddress of Address
type ValidatedAddress = ValidatedAddress of Address

let serviceInfo =
    { Name = "Service"
      Endpoint = Uri("http://localhost") }

let checkAddressExists (UnvalidatedAddress address) =
    if address = Address "123 Main St" then
        ValidatedAddress address
    else
        failwith "Address not found"

let checkAddressExistsR address =
    Result.serviceExceptionAdaptor serviceInfo checkAddressExists address


printfn "%A" (checkAddressExistsR (UnvalidatedAddress(Address "123 Main St")))
printfn "%A" (checkAddressExistsR (UnvalidatedAddress(Address "123 Sub St")))
printfn "----"

// --------------------------------------------
// 何も返さない関数を変換
// --------------------------------------------
let doNoting _n = ()

let logError msg = printfn "Error: %A" msg
let logInfo msg = printfn "Info: %A" msg

// OKのとき、unitを返す関数に渡す、という変換
let adaptDeadEnd f = Result.tee f |> Result.map
let logErrorPiped = adaptDeadEnd logError
let checkAddressRAndErrorLogOnlyOk = checkAddressExistsR >> logErrorPiped

checkAddressRAndErrorLogOnlyOk (UnvalidatedAddress(Address "123 Main St"))
checkAddressRAndErrorLogOnlyOk (UnvalidatedAddress(Address "123 Sub St")) // 何も出力されない Errorの時にはlogErrorには渡されない
printfn "----"

logErrorPiped (Ok(ValidatedAddress(Address "123 Main St")))
printfn "----"
let logInfoPiped = Result.tee logInfo
let checkAddressRAndErrorLog = checkAddressExistsR >> logInfoPiped
checkAddressRAndErrorLog (UnvalidatedAddress(Address "123 Main St"))
checkAddressRAndErrorLog (UnvalidatedAddress(Address "123 Sub St"))
