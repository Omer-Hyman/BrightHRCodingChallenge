public interface ICheckout
{
    public void Scan(Item item);
    public int GetTotalPrice();
}

public class Till : ICheckout
{
    private Item[] ShoppingBasket = [];

    public void Scan(Item item)
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
        Item a = new Item("A", 50, [3, 130]);
        Item b = new Item("B", 50, [2, 45]);
        Item c = new Item("C", 50, null);
        Item d = new Item("D", 50, null);
    }
}
