use proc_macro::TokenStream;
use quote::quote;
use rust_decimal::prelude::*;
use syn::{parse_macro_input, DeriveInput, Lit, Meta, MetaNameValue};

#[proc_macro_derive(SimpleType, attributes(validate, error_msg))]
pub fn derive_simple_type(input: TokenStream) -> TokenStream {
    let input = parse_macro_input!(input as DeriveInput);
    let name = &input.ident;
    let mut validator = None;
    let mut error_msg = None;

    for attr in input.attrs {
        if let Ok(Meta::NameValue(MetaNameValue { path, lit, .. })) = attr.parse_args() {
            if path.is_ident("validate") {
                if let Lit::Str(lit_str) = lit {
                    validator = Some(lit_str.value());
                }
            } else if path.is_ident("error_msg") {
                if let Lit::Str(lit_str) = lit {
                    error_msg = Some(lit_str.value());
                }
            }
        }
    }

    let validator = validator.expect("Validator function is required");
    let error_msg = error_msg.expect("Error message is required");

    let expanded = quote! {
        impl SimpleType<String> for #name {
            fn create(v: String) -> Result<Self, String> {
                if (#validator)(&v) {
                    Ok(Self(v))
                } else {
                    Err(#error_msg.to_string())
                }
            }

            fn value(&self) -> &String {
                &self.0
            }
        }
    };

    TokenStream::from(expanded)
}

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

// pub fn add(left: usize, right: usize) -> usize {
//     left + right
// }

// #[cfg(test)]
// mod tests {
//     use super::*;

//     #[test]
//     fn it_works() {
//         let result = add(2, 2);
//         assert_eq!(result, 4);
//     }
// }
