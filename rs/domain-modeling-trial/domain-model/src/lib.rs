struct Undefined();
trait SimpleType<T>: Sized {
    fn create(v: T) -> Result<Self, String>;
    fn value(&self) -> &T;
}

#[derive(Debug, Clone)]
struct CustomerId(String);
impl SimpleType<String> for CustomerId {
    fn create(v: String) -> Result<Self, String> {
        if v.starts_with("C") {
            Ok(Self(v))
        } else {
            Err("CustomerId must be start with 'C'".to_string())
        }
    }
    fn value(&self) -> &String {
        &self.0
    }
}

#[derive(Debug, Clone)]
struct CustomerName(String);
impl SimpleType<String> for CustomerName {
    fn create(v: String) -> Result<Self, String> {
        if v.len() < 100 {
            Ok(Self(v))
        } else {
            Err("CustomerName length must be letter than 100".to_string())
        }
    }
    fn value(&self) -> &String {
        &self.0
    }
}

struct CustomerInfo {
    customer_id: CustomerId,
    customer_name: CustomerName,
}
struct UnvalidatedOrder {
    customer_id: String,
}

pub fn add(left: usize, right: usize) -> usize {
    left + right
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn it_works() {
        let result = add(2, 2);
        assert_eq!(result, 4);
    }
}
