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
type PersonalName =
    { FirstName: FirstName
      LastName: LastName }

let name1 =
    { FirstName = FirstName "test"
      LastName = LastName "test2" }

let name2 =
    { FirstName = FirstName "test"
      LastName = LastName "test2" }

printfn "%b" (name1 = name2) // true




// エンティティ
/// 外部で表現するパターン
// 仮でstringにしてる
type UnpaidInvoiceInfo = UnpaidInvoiceInfo of string
type PaidInvoiceInfo = PaidInvoiceInfo of string

type InvoiceInfo =
    | Unpaid of UnpaidInvoiceInfo
    | Paid of PaidInvoiceInfo

type InvoiceId = InvoiceId of string

type Invoice =
    { InvoiceId: InvoiceId
      InvoiceInfo: InvoiceInfo }

let invoice =
    { InvoiceId = InvoiceId "testID"
      InvoiceInfo = UnpaidInvoiceInfo "string" |> Unpaid }
// 中身に対してパターンマッチでばらさないといけない
match invoice.InvoiceInfo with
| Unpaid _unpaidInvoice -> printfn "unpaid %A" invoice.InvoiceId
| Paid _paidInvoice -> printfn "paid %A" invoice.InvoiceId


/// 内部で表現するパターン
[<Struct>]
type UserId = UserId of string

[<Struct>]
type EMailAddress = EMailAddress of string

// 比較をオーバーライドする
// オブジェクト指向の構文なのでここでしか使わない
[<CustomEquality; NoComparison>]
type User =
    { UserId: UserId
      EMailAddress: EMailAddress }

    override this.Equals(obj) =
        match obj with
        | :? User as u -> this.UserId = u.UserId
        | _ -> false

    override this.GetHashCode() = hash this.UserId


let userId = UserId "testID"

let user1 =
    { UserId = userId
      EMailAddress = EMailAddress "testAddress" }

let user2 =
    { UserId = userId
      EMailAddress = EMailAddress "testAddress22" }

printfn "%b" (user1 = user2) // true

// 比較を無効にする
[<NoEquality; NoComparison>]
type User2 =
    { UserId: UserId
      EMailAddress2: EMailAddress }

let userId2 = UserId "testID"

let user12 =
    { UserId = userId
      EMailAddress2 = EMailAddress "testAddress" }

let user22 =
    { UserId = userId
      EMailAddress2 = EMailAddress "testAddress22" }
// printfn "%b" (user1 = user2) // 動かない


type IdentityId = IdentityId of string
// 複数のフィールドをkeyとする「値オブジェクト」
[<NoEquality; NoComparison>]
type User3 =
    { UserId: UserId
      EMailAddress: EMailAddress
      IdentityId: IdentityId }

    member this.Key = (this.UserId, this.IdentityId)

let identityId = IdentityId "testIdentityId"
let identityId2 = IdentityId "testIdenffftityId"

let user33 =
    { UserId = userId
      EMailAddress = EMailAddress "testAddress"
      IdentityId = identityId }

let user34 =
    { UserId = userId
      EMailAddress = EMailAddress "tetetete"
      IdentityId = identityId }

let user3ww4 =
    { UserId = userId
      EMailAddress = EMailAddress "tetetete"
      IdentityId = identityId2 }

printfn "%b" (user33.Key = user34.Key) // true
printfn "%b" (user33.Key = user3ww4.Key) // false



// エンティティの変化を表す例
type PersonId = PersonId of int
type Person = { PersonId: PersonId; Name: string }

let initialPerson =
    { PersonId = PersonId 42
      Name = "Joseph" }

let updatedPerson = { initialPerson with Name = "Joe" }

printfn "%A\n%A" initialPerson updatedPerson

type Name = string
// 本来なら type Name = Name of string とする
type UpdateName = Person -> Name -> Person
let updateName person name = { person with Name = name }

let updatedPerson2 = updateName initialPerson "Jon Doe"

printfn "%A" updatedPerson2

let InitialPersonUpdate = updateName initialPerson

let updatedPerson3 = InitialPersonUpdate "Jane Doe"

printfn "%A" updatedPerson3
