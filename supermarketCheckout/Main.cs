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

    public void InputCustomPricing()
    {
        string name;
        int unitPrice;
        int multiBuyQuantity;
        int multiBuyPrice;
        bool repeat = true; 
        
        Console.WriteLine("Do you want to add or change any pricing rules? (y/n)");
        var userInput = Console.ReadLine();

        if (userInput?.ToLower() != "y" )
            return;

        while(repeat)
        {
            Console.WriteLine("What's the name of the item you would like to add or edit? (Must be only one letter!)");
            userInput = Console.ReadLine();

            if (!(userInput?.Length == 1 && char.IsLetter(userInput[0])))
                return;
            
            name = userInput;

            Console.WriteLine("What's the unit price for " + name + "? (Whole positive numbers only!)");
            userInput = Console.ReadLine();

            if (!(int.TryParse(userInput, out int number) && number > 0))
                return;
                
            unitPrice = number;
            
            Console.WriteLine("Should " + name + " have a multibuy offer? (y/n)");
            userInput = Console.ReadLine();

            if (userInput?.ToLower() == "y")
            {
                Console.WriteLine("How many of this item must the customer purchase for the discount to apply? (Whole positive numbers only!)");
                userInput = Console.ReadLine();

                if (!(int.TryParse(userInput, out int num) && num > 0))
                    return;
                
                multiBuyQuantity = num;

                Console.WriteLine($"What should the price be for {multiBuyQuantity} {name}'s? (Whole positive numbers only!)");
                userInput = Console.ReadLine();

                if (!(int.TryParse(userInput, out int price) && price > 0))
                    return;
                
                multiBuyPrice = price;

                Item item = new Item(name, unitPrice, [multiBuyQuantity, multiBuyPrice]);
                UpdateAvailableItems(item);
            }
            else if (userInput?.ToLower() == "n")
            {
                Item item = new Item(name, unitPrice, null);
                UpdateAvailableItems(item);
            }

            Console.WriteLine("Do you want to add or edit another item? (y/n)");
            userInput = Console.ReadLine();

            if (userInput?.ToLower() != "y" )
                repeat = false;
        }
    }

    private void UpdateAvailableItems(Item item)
    {

        var itemIndex = Array.FindIndex(AvailableItems, x => x.Name == item.Name);
        // var itemToFind = Array.Find(AvailableItems, x => x.Name == item.Name);

        if (itemIndex == -1)
            AvailableItems.Append(item);
        else
            AvailableItems[itemIndex] = item;
    }

    public void Testing()
    {
        Console.WriteLine("hello");
        var input = Console.ReadLine();
        Console.WriteLine(input);
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
        till.InputCustomPricing();
        till.Scan("A");
        till.Scan("B");
        till.Scan("A");
        till.Scan("B");
        till.Scan("A");
        Console.WriteLine(till.GetTotalPrice());
    }
}
