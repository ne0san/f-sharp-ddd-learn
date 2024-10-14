type ResultBuilder() =
    member this.Return(x) = Ok x
    member this.Bind(x, f) = Result.bind f x

let result = ResultBuilder()


let prepend firstR restR =
    match firstR, restR with
    | Ok first, Ok rest -> Ok(first :: rest)
    | Error err1, Ok _ -> Error err1
    | Ok _, Error err2 -> Error err2
    | Error err1, Error _ -> Error err1

module Result =
    let sequence aListOfResults =
        let initialValue = Ok []
        List.foldBack prepend aListOfResults initialValue

type IntOrError = Result<int, string>

let listOfSuccess: IntOrError list = [ Ok 1; Ok 2 ]
let successResult = Result.sequence listOfSuccess

printfn "%A" successResult

// errorの場合、最初のerrorが返却される
let listOfError: IntOrError list = [ Ok 1; Error "error1"; Error "error2" ]
let errorResult = Result.sequence listOfError
printfn "%A" errorResult
