// 値オブジェクト

type WidgetCode = WidgetCode of string
let widgetCode1 = WidgetCode "W123"
let widgetCode2 = WidgetCode "W123"
printfn "%b" (widgetCode1 = widgetCode2) // true

[<Struct>]
type FirstName = FirstName of string
[<Struct>]
type LastName = LastName of string
[<Struct>]
type PersonalName = {
    FirstName : FirstName
    LastName : LastName
}
let name1 = {
    FirstName = FirstName "test";
    LastName = LastName "test2"
}
let name2 = {
    FirstName = FirstName "test";
    LastName = LastName "test2"
}
printfn "%b" (name1 = name2) // true




// エンティティ
[<Struct>]
type UserId = UserId of string
[<Struct>]
type EMailAddress = EMailAddress of string

[<CustomEquality>; NoComparison]
type User = {
    UserId: UserId
    EMailAddress: EMailAddress
}
with
    override this.Equals(obj) =
        match obj with
        | :? User as u -> this.UserId = c.UserId
        | _ -> false
    override this.GetHashCode() =
        hash this.UserId