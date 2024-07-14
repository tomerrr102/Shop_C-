
namespace DO;

public struct OrderItem
{


    // צריך לבדוק איך להגדיר מספר רץ? בנתיים נעשה int
    // uniq number for recognize item in the order
    public int OrderItemUniqCode { get; set; }

    // the recognize number of order
    public int OrderID { get; set; }

    // the recognize number for product
    public int ProductID { get; set; }

    // price of item
    public double OrderProductPrice { get; set; }

    // amount of items
    public int OrderItemAmount { get; set; }

    public override string ToString() => $@"
    Order item uniq code = {OrderItemUniqCode}
    Order item ID = {OrderID}
    Order item recognize = {ProductID}
    Order item price = {OrderProductPrice}
    Order item amount = {OrderItemAmount}";
}