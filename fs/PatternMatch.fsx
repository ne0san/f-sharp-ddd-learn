type Apple = Apple of string
let app = Apple "Apple"

let str = app |> fun (Apple a) -> a
let (Apple str2) = app

let str3 =
    match app with
    | Apple a -> a


type Person =
    { Name: string
      Age: int
      Weight: float
      Height: float }

let person =
    { Name = "Alice"
      Age = 30
      Weight = 60.0
      Height = 160.0 }

let { Name = name
      Age = age
      Weight = weighting } =
    person

printfn "Name: %s, Age: %d, Weight: %f" name age weighting
