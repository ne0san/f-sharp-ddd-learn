#r "nuget: FSharp.Data.SqlClient"
open FSharp.Data

type ResultBuilder() =
    member this.Return(x) = Ok x
    member this.Bind(x, f) = Result.bind f x

let result = ResultBuilder()


[<Literal>]
// let CompileTimeConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=AdventureWorks2012;Integrated Security=True"
let CompileTimeConnectionString = @"Data Source=.\mydatabase.db;"

type ReadOneCustomer =
    SqlCommandProvider<"""
    SELECT customer_id, name, birthdate
    FROM customer
    WHERE customer_id = @customer_id
    """, CompileTimeConnectionString>

// シリアライズの例と同様にtoDomain関数を定義する

let toDomain (dbRecord: ReadOneCustomer.Record) : Result<Customer, _> =
    result {
        let! customerId = CustomerId dbRecord.customer_id
        let! name = Name dbRecord.name
        let! birthdate = Birthdate dbRecord.birthdate

        return
            { CustomerId = customerId
              Name = name
              Birthdate = birthdate }
    }

type DatabaseError = DatabaseError of string

let convertSingleDbRecord tableName idValue records toDomain =
    match records with
    | [] ->
        let msg = sprintf "Not found. Table= %s Id= %A" tableName idValue
        Error msg
    | [ dbRecord ] -> dbRecord |> toDomain |> Ok
    | _ ->
        let msg = sprintf "Multiple records found. Table= %s Id= %A" tableName idValue
        raise (DatabaseError msg)

let readOneCustomer (productionConnection: SqlConnection) (CustomerId customerId) =
    use cmd = new ReadOneCustomer(productionConnection)
    let tableName = "customer"
    let records = cmd.Execute(customerId = customerId) |> Seq.toList
    convertSingleDbRecord tableName customerId records toDomain
