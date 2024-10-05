struct Apple(String);

struct Person {
    name: String,
    age: u32,
    weight: f32,
    height: f32,
}

fn main() {
    let app = Apple("Apple".to_string());

    let Apple(str) = app;
    println!("{}", str);

    let person = Person {
        name: "Alice".to_string(),
        age: 30,
        weight: 60.0,
        height: 1.6,
    };

    let Person {
        name,
        age,
        weight: weighting,
        ..
    } = person;
    println!("Name: {}, Age: {}, Weight: {}", name, age, weighting);
}
