namespace OrderTaking.Domain

// 型の定義
type WidgetCode = WidgetCode of string
// 制約 先頭がW + 数字4桁
type GizmoCode = GizmoCode of string
// 制約 先頭がG + 数字3桁

type ProductCode =
    | Widget of WidgetCode
    | Gizmo of GizmoCode

//注文数量関係
type UnitQuantity = UnitQuantity of int
type KilogramQuantity = KilogramQuantity of decimal

type OrderQuantity =
    | UnitQuantity of UnitQuantity
    | KilogramQuantity of KilogramQuantity


// ここまでは値オブジェクト
// 注文はエンティティなのでIDを使う
// IDの型が分からんけど一旦Undefinedにする
type Undefined = exn

type OrderId = Undefined
type OrderLineId = Undefined
type CustomerId = Undefined


// 注文とその構成要素の概要を定義

type CustomerInfo = Undefined
type ShippingAddress = Undefined
type BillingAddress = Undefined
type Price = Undefined
type BillingAmount = Undefined

type Order =
    { Id: OrderId
      CustomerId: CustomerId
      ShippingAddress: ShippingAddress
      BillingAddress: BillingAddress
      OrderLines: OrderLine list
      AmountToBill: BillingAmount }

and OrderLine =
    { Id: OrderLineId
      OrderId: OrderId
      ProductCode: ProductCode
      OrderQuantity: OrderQuantity
      Price: Price }
// andキーワードで前方参照を許可

// 最後にワークフローの定義
// 未検証の注文は注文書から直接作成されるのでプリミティブな値しか持たない
type UnvalidatedOrder =
    { OrderId: string
      CustomerInfo: Undefined
      ShippingAddress: Undefined }

// ワークフロー出力に二つの型が必要
// ワークフロー成功時のイベント型

type PlaceOrderEvents =
    { AcknowledgementSent: Undefined
      OrderPlaced: Undefined
      BillableOrderPlaced: Undefined }

// ワークフロー失敗時のエラー型

type PlacedOrderError = ValidationError of ValidationError list
// | 他、あり得るエラー
and ValidationError =
    { FieldName: string
      ErrorDescription: string }

// 注文確定のためのワークフローを表すトップレベルの関数
type PlacedOrder = UnvalidatedOrder -> Result<PlaceOrderEvents, PlacedOrderError>


// ワークフローのモデルはまだ完成していない
// 注文が検証され、価格が計算されるといったWF内部の状態変化をどうモデル化するか
