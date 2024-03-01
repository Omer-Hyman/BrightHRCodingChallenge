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
        int totalPrice = 0;
        List<int> visited = new List<int>();
        foreach (var item in ShoppingBasket)
        {
            if (item.SpecialPrice == null)
            {
                totalPrice += item.UnitPrice;
            }
            else
            {
                var quantity = item.SpecialPrice[0];
                var duplicateItems = ShoppingBasket.FindAll(itemToFind => itemToFind.Name == item.Name);
                Console.WriteLine(item.Name);
                Console.WriteLine(duplicateItems.Count);

                int discountReduction = (item.UnitPrice * item.SpecialPrice[0]) - item.SpecialPrice[1];
                int numberOfDiscounts = duplicateItems.Count / quantity;

                // can't subtract the discount from the total right here because it will 
                // remove it again the next time it hits the same item
                
                // Can't remove items from the list because it is being looped through
                // maybe a visited list is needed? 
            }
        }
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
        till.Scan("B");
        till.Scan("D");    
        till.GetTotalPrice();
    }
}
