open System

type ValidationError =
    { FieldName: string
      ErrorDescription: string }

// 9-1
module Domain =
    type OrderId = private OrderId of string
    module OrderId =
        let create str =
            // Result型で、ドメインで扱う例外を明示的にする
            // どのようなエラーを返却するかも定義する
            if String.IsNullOrEmpty(str) then
                Error { 
                    FieldName = "OrderID"
                    ErrorDescription = "OrderId must not be null or empty"
                }
            elif str.Length > 50 then
                Error { 
                    FieldName = "OrderID"
                    ErrorDescription = "OrderId must not be more than 50 chars"
                }
            else
                Ok (OrderId str)
        let value (OrderId str) = str

open Domain

// モデリング段階で定義途中の型をUndefinedとする
type Undefined = exn

type AsyncResult<'success, 'failure> = Async<Result<'success, 'failure>>

type UnvalidatedAddress = UnvalidatedAddress of string
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


type CheckAddress = CheckAddress of Address

type PlacedOrderError = ValidationError of ValidationError list
      
type AddressValidationError = AddressValidationError of string

// ------------- 型定義ここまで

// ワークフローのうち、外部に依存しているステップを定義
type CheckProductCodeExists = string -> Result<ProductCode, ValidationError>

// ワークフローのうち、外部に依存しているステップを定義
type CheckAddressExists =
    UnvalidatedAddress -> AsyncResult<CheckAddress, AddressValidationError>

type ValidateOrder = 
    CheckProductCodeExists  // 依存関係(関数で表現) コンポジションルートでDIする
        -> CheckAddressExists  // 依存している関数(関数で表現) コンポジションルートでDIする
        -> UnvalidatedOrder 
        -> AsyncResult<ValidatedOrder, ValidationError list>

