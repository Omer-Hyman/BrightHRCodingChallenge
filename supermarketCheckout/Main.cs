public interface ICheckout
{
    public void Scan(string itemName);
    public int GetTotalPrice();
}

public class Till : ICheckout
{
    public Item[] AvailableItems = [
        new Item("A", 50, [3, 130]),
        new Item("B", 50, [2, 45]),
        new Item("C", 50, null),
        new Item("D", 50, null)
    ];
    public List<Item> ShoppingBasket = new List<Item>();

    public void Scan(string itemName)
    {
        var arraySearch = Array.Find(AvailableItems, element => element.Name == itemName);
        if(arraySearch != null)
            ShoppingBasket.Add(arraySearch);
    }

    public int GetTotalPrice()
    {
        return 0;
    }
}

public class Item
{
    public string Name;
    public int UnitPrice;
    public int[]? SpecialPrice;

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
        Till till = new Till();
        till.Scan("A");
    }
}
