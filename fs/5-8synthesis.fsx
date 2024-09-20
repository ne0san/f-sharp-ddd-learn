type OrderId = OrderId of int
type OrderLineId = OrderLineId of int

type OrderLine =
    { OrderLineId: OrderLineId
      ProductId: int // 便宜上、int
      Quantity: int } // 便宜上、int

type Order =
    { OrderId: OrderId
      OrderLines: OrderLine list }

let order1 =
    { OrderId = OrderId 1
      OrderLines =
        [ { OrderLineId = OrderLineId 1
            ProductId = 1
            Quantity = 2 } ] }


