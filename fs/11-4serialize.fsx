#r "nuget: Newtonsoft.Json, 12.0.3"
open System


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



type ResultBuilder() =
    member this.Return(x) = Ok x
    member this.Bind(x, f) = Result.bind f x

let result = ResultBuilder()

/////////////////////////////////////////////
// ドメインモデル
/////////////////////////////////////////////

module Domain =
    // こいつらに直接シリアライズできない
    type String50 = String50 of string
    type Birthdate = Birthdate of DateTime

    type Person =
        { First: String50
          Last: String50
          Birthdate: Birthdate }

module String50 =
    open Domain

    let create (fieldName: string) (str: string) =
        if String.IsNullOrEmpty(str) then
            Result.Error $"{fieldName} must not be null or empty"
        elif str.Length > 50 then
            Result.Error $"{fieldName} must not be more than 50 chars"
        else
            Result.Ok(String50 str)

    let value (String50 str) = str

module Birthdate =
    open Domain

    let create (date: DateTime) =
        if date > DateTime.Now then
            Result.Error "Birthdate must not be in the future"
        else
            Result.Ok(Birthdate date)

    let value (Birthdate date) = date


/////////////////////////////////////////////
// DTO型
/////////////////////////////////////////////

module Dto =
    type Person =
        { First: string
          Last: string
          Birthdate: DateTime }

    module Person =
        let fromDomain (person: Domain.Person) : Person =
            let first = person.First |> String50.value
            let last = person.Last |> String50.value
            let birthdate = person.Birthdate |> Birthdate.value

            { First = first
              Last = last
              Birthdate = birthdate }

        let toDomain (person: Person) : Result<Domain.Person, string> =
            result {
                let! first = person.First |> String50.create "First"
                //String50.createは、エラーメッセージ作成のためstringをパラメータに持つ
                let! last = person.Last |> String50.create "Last"
                let! birthdate = person.Birthdate |> Birthdate.create

                return
                    { First = first
                      Last = last
                      Birthdate = birthdate }
            }

module Json =
    open Newtonsoft.Json
    let serialize obj = JsonConvert.SerializeObject obj

    let deserialize<'a> str =
        try
            JsonConvert.DeserializeObject<'a> str |> Result.Ok
        with ex ->
            Result.Error ex

let jsonFromDomain = Dto.Person.fromDomain >> Json.serialize

/////////////////////////////////////////////
// シリアライズ実装例
/////////////////////////////////////////////

open Domain

let person =
    { First = String50 "Alice"
      Last = String50 "Adams"
      Birthdate = Birthdate(DateTime(1980, 1, 1)) }

person |> jsonFromDomain |> printfn "%A"



/////////////////////////////////////////////
// デシリアライズ実装例
/////////////////////////////////////////////

type DtoError =
    | ValidationError of string
    | DeserializationException of exn

let jsonToDomain jsonString : Result<Domain.Person, DtoError> =
    result {
        let! deserializedValue =
            jsonString
            |> Json.deserialize<Dto.Person>
            |> Result.mapError DeserializationException

        let! domainValue = deserializedValue |> Dto.Person.toDomain |> Result.mapError ValidationError
        return domainValue
    }

let jsonPerson =
    """
{
    "First": "Alice",
    "Last": "Adams",
    "Birthdate": "1980-01-01T00:00:00"
}
"""

jsonToDomain jsonPerson |> printfn "%A"


let jsonPersonWithErrors =
    """
{
    "First": "",
    "Last": "Adams",
    "Birthdate": "1980-01-01T00:00:00"
}
"""

jsonToDomain jsonPersonWithErrors |> printfn "%A"
