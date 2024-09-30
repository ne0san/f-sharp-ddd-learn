#load "DomainApi.fsx"
open DomainApi

// ------------------
// 注文のライフサイクル
// ------------------

//検証済みの状態

type ValidatedOrderLine = ValidatedOrderLine of Undefined
type ValidatedOrder = {
    OrderId : OrderId 
    CustomerInfo : CustomerInfo
    ShippingAddress : Address
    BillingAddress : Address
    OrderLine : ValidatedOrderLine list
}
and OrderId = Undefined
and CustomerInfo = Undefined
and Address = Address of string

// 価格計算済みの状態
type PricedOrderLine = PricedOrderLine of Undefined
type PricedOrder = PricedOrder of Undefined

// 全状態の結合
type Order = 
    | Unvalidated of UnvalidatedOrder
    | Validated of ValidatedOrder
    | Priced of PricedOrder

// ------------------
// 内部ステップの定義
// ------------------

type ProductCode =
    | WidgetCode of WidgetCode
    | GizmoCode of GizmoCode
and WidgetCode = WidgetCode of string
and GizmoCode = GizmoCode of string

// ------ 注文の検証 ------

// 注文の検証が使用するサービス
type CheckedAddress = CheckedAddress of UnvalidatedAddress
type CheckProductCodeExists =
    ProductCode -> bool
type AddressValidationError = AddressValidationError of string
type CheckAddressExists =
    UnvalidatedAddress -> Result<CheckedAddress,AddressValidationError>

type ValidateOrder =
    CheckProductCodeExists -> CheckAddressExists -> UnvalidatedOrder -> AsyncResult<ValidatedOrder,ValidationError list>
and ValidationError = ValidationError of string

// ------ 注文の価格計算 ------
// 注文の価格計算が使用するサービス

type GetProductPrice = 
    ProductCode -> Price
and Price = Price of int

type PricingError = PricingError of string

type PriceOrder = 
    GetProductPrice -> ValidatedOrder -> Result<PricedOrder, PricingError>

// etc