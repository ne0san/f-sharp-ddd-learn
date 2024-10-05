#r "nuget: NUnit, 3.12.0"

open NUnit.Framework

[<Test>]
let ``正常系_製品存在時、検証成功``() =
    let checkAddress address =
        CheckAddress address
    let CheckProductCodeExists productCode =
        true
        // スタブを簡単に定義できちゃうぞ♡
    // let unvalidatedOrder =
    //     { OrderId = "test"
    //       CutomerInfo = { Name = "test"; Email = "test" }
    //       ShippingAddress = UnvalidatedAddress "test" }
    // let result = validateOrder checkProductCodeExists checkAddressExists
