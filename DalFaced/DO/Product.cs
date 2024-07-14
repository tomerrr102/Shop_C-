
using static DO.Enums;

namespace DO;

public struct Product
{

    public int CarID { get; set; }
    public string? CarName { get; set; }
    public CarCategory? CarType { get; set; }
    public double CarPrice { get; set; }
    public int CarAmount { get; set; }


    public override string ToString() => $@"
    Car ID = {CarID}
    Car name = {CarName}
    Car type = {CarType}
    Car price = {CarPrice}
    Car amount = {CarAmount}";
}
