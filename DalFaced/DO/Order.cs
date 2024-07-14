
namespace DO;

public struct Order
{

    // צריך לבדוק איך להגדיר מספר רץ? בנתיים נעשה int
    // uniq code for recognize the order number
    public int OrderUniqCode { get; set; }

    // name of the customer
    public string? OrderCustomerName { get; set; }

    // Email of the customer
    public string? OrderCustomerEmail { get; set; }

    // Address of the customer
    public string? OrderCustomerAddress { get; set; }

    // the date we created the order
    public DateTime? OrderCreationDate { get; set; }

    // the date of the order
    public DateTime? OrderShipDate { get; set; }

    // the date of the actual delivery
    public DateTime? OrderDeliveryDate { get; set; }

    public override string ToString() => $@"
    Customer name = {OrderCustomerName}
    Customer email = {OrderCustomerEmail}
    Uniq code = {OrderUniqCode}
    Customer address = {OrderCustomerAddress}
    Order creation date = {OrderCreationDate}
    Order ship date = {OrderShipDate}
    Order delivery date = {OrderDeliveryDate}";
}
    