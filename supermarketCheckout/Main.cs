public interface ICheckout
{
    public void Scan(string itemName);
    public int GetTotalPrice();
}

public class Till : ICheckout
{
    private Item[] AvailableItems = [
        new Item("A", 50, [3, 130]),
        new Item("B", 50, [2, 45]),
        new Item("C", 50, null),
        new Item("D", 50, null)
    ];
    public Item[] ShoppingBasket = [];

    public void Scan(string itemName)
    {
        
    }

    public int GetTotalPrice()
    {
        return 0;
    }
}

public class Item
{
    string Name;
    int UnitPrice;
    int[]? SpecialPrice;

    public Item(string name, int unitPrice, int[]? specialPrice)
    {
        Name = name;
        UnitPrice = unitPrice;
        SpecialPrice = specialPrice;
    }
}

class Program
{
    static void Main()
    {
        
    }
}
