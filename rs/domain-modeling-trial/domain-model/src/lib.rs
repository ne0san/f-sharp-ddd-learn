use rust_decimal::prelude::*;

#[derive(Debug, Clone)]
struct Undefined();

#[derive(Debug, Clone)]
struct Address(String);
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

#[derive(Debug, Clone)]
struct EMailAddress(Address);
impl SimpleType<Address> for EMailAddress {
    fn create(v: Address) -> Result<Self, String> {
        if v.0.contains('@') {
            Ok(Self(v))
        } else {
            Err("EMailAddress must contains '@'".to_string())
        }
    }
    fn value(&self) -> &Address {
        &self.0
    }
}

#[derive(Debug, Clone)]
struct CustomerInfo {
    customer_id: CustomerId,
    customer_name: CustomerName,
}

#[derive(Debug, Clone)]
struct BothContactInfo {
    email_address: EMailAddress,
    address: Address,
}

#[derive(Debug, Clone)]
enum ContactMethod {
    EmailOnly(EMailAddress),
    AddressOnly(Address),
    EmailAndAddress(BothContactInfo),
}

#[derive(Debug, Clone)]
struct UnvalidatedOrderLine {
    product_id: String,
    quantity: Decimal,
    contact_method: ContactMethod,
}

#[derive(Debug, Clone)]
struct UnvalidatedOrder {
    customer_id: String,
    order_lines: Vec<UnvalidatedOrderLine>,
}
#[derive(Debug, Clone)]
struct UnvalidatedAddress(String);

#[derive(Debug, Clone)]
struct ValidatedArress(String);

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
