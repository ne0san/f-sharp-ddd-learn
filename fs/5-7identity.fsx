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
/// 外部で表現するパターン
// 仮でstringにしてる
type UnpaidInvoiceInfo = UnpaidInvoiceInfo of string
type PaidInvoiceInfo = PaidInvoiceInfo of string

type InvoiceInfo =
    | Unpaid of UnpaidInvoiceInfo
    | Paid of PaidInvoiceInfo

type InvoiceId = InvoiceId of string
type Invoice = {
    InvoiceId: InvoiceId
    InvoiceInfo: InvoiceInfo
}
let invoice = {
    InvoiceId = InvoiceId "testID";
    InvoiceInfo = UnpaidInvoiceInfo "string" |> Unpaid;
}
// 中身に対してパターンマッチでばらさないといけない
match invoice.InvoiceInfo with
    | Unpaid _unpaidInvoice ->
        printfn "unpaid %A" invoice.InvoiceId
    | Paid _paidInvoice ->
        printfn "paid %A" invoice.InvoiceId


/// 内部で表現するパターン
[<Struct>]
type UserId = UserId of string
[<Struct>]
type EMailAddress = EMailAddress of string

// 比較をオーバーライドする
[<CustomEquality; NoComparison>]
type User = {
    UserId: UserId
    EMailAddress: EMailAddress
}
with
    override this.Equals(obj) =
        match obj with
        | :? User as u -> this.UserId = u.UserId
        | _ -> false
    override this.GetHashCode() =
        hash this.UserId