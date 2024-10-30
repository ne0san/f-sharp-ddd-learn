open System

// 9-1
module Domain =
    type OrderId = private OrderId of string

    module OrderId =
        let create str =
            if String.IsNullOrEmpty(str) then
                // エフェクトを避ける為に例外を投げる
                // 別にResult型を使ってもいい
                Error "OrderId must not be null or empty"
            elif str.Length > 50 then
                Error "OrderId must not be more than 50 chars"
            else
                Ok (OrderId str)

        let value (OrderId str) = str


open Domain

type Undefined = exn

type AsyncResult<'success, 'failure> = Async<Result<'success, 'failure>>

type ValidationError =
    { FieldName: string
      ErrorDescription: string }

type UnvalidatedAddress = UnvalidatedAddress of string
and OrderId = Undefined
and CustomerInfo = Undefined
and Address = Address of string

type ValidatedOrderLine = ValidatedOrderLine of Undefined
and UnvalidatedCustomer = { Name: string; Email: string }

type UnvalidatedOrder =
    { OrderId: string
      CutomerInfo: UnvalidatedCustomer
      ShippingAddress: UnvalidatedAddress }

type ValidatedOrder =
    { OrderId: OrderId
      CustomerInfo: CustomerInfo
      ShippingAddress: Address
      BillingAddress: Address
      OrderLine: ValidatedOrderLine list }

type ProductCode =
    | WidgetCode of WidgetCode
    | GizmoCode of GizmoCode
and WidgetCode = WidgetCode of string
and GizmoCode = GizmoCode of string

type CheckProductCodeExists = string -> Result<ProductCode, ValidationError>

type CheckAddress = CheckAddress of Address

type PlacedOrderError = ValidationError of ValidationError list
      
type AddressValidationError = AddressValidationError of string

// ------------- 型定義ここまで

type CheckAddressExists =
    UnvalidatedAddress -> AsyncResult<CheckAddress, AddressValidationError>

type ValidateOrder = 
    CheckProductCodeExists -> CheckAddressExists -> UnvalidatedOrder ->AsyncResult<ValidatedOrder, ValidationError list>

