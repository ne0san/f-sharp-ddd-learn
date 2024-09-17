use rust_decimal::Decimal;
struct CheckNumber(i32);
struct CardNumber(String);
enum CardType {
    Visa,
    MasterCard
}

enum PaymentMethod {
    Cash,
    Check(CheckNumber),
    Card(CardNumber)
}

struct PaymentAmount(Decimal);
enum Currency{
    EUR,
    USD
}
struct Payment {
    Amount: PaymentAmount,
    Currency:Currency,
    Method: PaymentMethod
}

fn main(){

}