using System.Reflection.Metadata;

public interface ICheckout
{
    public void Scan(string itemName);
    public int GetTotalPrice();
}

public class Till : ICheckout
{
    public Item[] AvailableItems = [
        new Item("A", 50, [3, 130]),
        new Item("B", 30, [2, 45]),
        new Item("C", 20, null),
        new Item("D", 15, null)
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
        int totalPrice = 0;
        List<string> visited = new List<string>();

        foreach (var item in ShoppingBasket)
        {
            totalPrice += item.UnitPrice;

            if (visited.Contains(item.Name))
                continue;
            
            visited.Add(item.Name);

            if (item.SpecialPrice != null)
            {
                var duplicateItems = ShoppingBasket.FindAll(itemToFind => itemToFind.Name == item.Name);
                
                int discountAmount = (item.UnitPrice * item.SpecialPrice[0]) - item.SpecialPrice[1];
                int numberOfDiscounts = duplicateItems.Count / item.SpecialPrice[0];

                totalPrice -= discountAmount * numberOfDiscounts;
            }
        }
        Console.WriteLine("Total Price: " + totalPrice);
        return totalPrice;
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
        till.Scan("B");
        till.Scan("A");
        till.Scan("B");
        till.Scan("A"); 
        till.GetTotalPrice();
    }
}
