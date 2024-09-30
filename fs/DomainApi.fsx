open System
type Undefined = exn
type AsyncResult<'success, 'failure> = Async<Result<'success, 'failure>>

// ------------------
// 入力
// ------------------

type UnvalidatedOrder = {
    OrderId : string
    CutomerInfo: UnvalidatedCustomer
    ShippingAddress : UnvalidatedAddress
}
and UnvalidatedCustomer = {
    Name : string
    Email : string
}
and UnvalidatedAddress = UnvalidatedAddress of string

// ------------------
// 入力コマンド
// ------------------

type Command<'data> = {
    Data : 'data
    Timestamp : DateTime
    UserId : string
}

type PlaceOrderCommand = Command<UnvalidatedOrder>



// ------------------
// 以下はアウトプットとWF本体の定義
// ------------------

// ------------------
// パブリックAPI
// ------------------
// 受注確定WFの成功出力
type OrderPlaced = OrderPlaced of Undefined
type BillableOrderPlaced = BilliableOrderPlaced of Undefined
type OrderAcknowledgementSent = OrderAcknowledgementSent of Undefined

type PlacedOrderEvent =
    | OrderPlaced of OrderPlaced
    | BillableOrderPlaced of BillableOrderPlaced
    | AcknowledgementSent of OrderAcknowledgementSent

type PlaceOrderError = PlaceOrderError of Undefined

type PlaceOrderWorkflow = 
    PlaceOrderCommand -> AsyncResult<PlacedOrderEvent list, PlaceOrderError>

