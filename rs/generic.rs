fn are_equal<T: std::cmp::PartialEq>(a: T, b: T) -> bool {
    a == b
}

fn main() {
    println!("{}", are_equal(2, 3));
    println!("{}", are_equal(2, 2));
}
