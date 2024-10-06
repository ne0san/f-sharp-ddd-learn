type Apple = Apple of string

type Person =
    { Name: string
      Age: int
      Weight: float
      Height: float }

let app = Apple "Apple"

let (Apple str) = app

printfn "%s" str

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
