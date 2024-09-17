// Eコマースの支払いを追跡する例

// 単純型から
// プリミティブに意味を与える

// 小切手番号
type CheckNumber = CheckNumber of int
// カード番号
type CardNumber = CardNumber of string

// 低レベル型をいくつか構築
type CardType =
    Visa | Mastercard

type CreditCardInfo = {
    CardType: CardType
    CardNumber: CardNumber
}

type PaymentMethod =
    | Cash
    | Check of CheckNumber
    | Card of CreditCardInfo

type PaymentAmount = PaymentAmount of decimal
type Currency = EUR | USD

type Payment = {
    Amount: PaymentAmount
    Currency: Currency
    Method: PaymentMethod
}

