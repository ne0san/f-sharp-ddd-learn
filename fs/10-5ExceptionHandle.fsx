open System
type Address = Address of string
type UnvalidatedAddress = UnvalidatedAddress of Address
type ValidatedAddress = ValidatedAddress of Address

// エラーを起こしたサービスを追跡
type ServiceInfo = { Name: string; Endpoint: Uri }

type RemoteServiceError =
    { Service: ServiceInfo
      Exception: Exception }

// この関数をベースにアダプタを必要に応じて作成
let serviceExceptionAdaptor serviceInfo serviceFn x =
    try
        Ok(serviceFn x)
    with
    | :? TimeoutException as ex ->
        Error
            { Service = serviceInfo
              Exception = ex }
    | :? Exception as ex ->
        Error
            { Service = serviceInfo
              Exception = ex }

let serviceInfo =
    { Name = "Service"
      Endpoint = Uri("http://localhost") }

let checkAddressExists (UnvalidatedAddress address) =
    if address = Address "123 Main St" then
        ValidatedAddress address
    else
        failwith "Address not found"

let checkAddressExistsR address =
    serviceExceptionAdaptor serviceInfo checkAddressExists address


printfn "%A" (checkAddressExistsR (UnvalidatedAddress(Address "123 Main St")))
printfn "%A" (checkAddressExistsR (UnvalidatedAddress(Address "123 Sub St")))
