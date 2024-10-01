open System

// 9-1
module Domain =
    type orderId = private OrderId of string

    module OrderId =
        let create str =
            if String.IsNullOrEmpty(str) then
                // エフェクトを避ける為に例外を投げる
                // 別にResult型を使ってもいい
                failwith "OrderId must not be null or empty"
            elif str.Length > 50 then
                failwith "OrderId must not be more than 50 chars"
            else
                OrderId str

        let value (OrderId str) = str
