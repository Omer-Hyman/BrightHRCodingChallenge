namespace supermarketCheckout;
using Xunit;
using Xunit.Abstractions;

public class UnitTests
{

    private readonly ITestOutputHelper output;

    public UnitTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void ScanItems_A()
    {
        Till till = new Till();

        till.Scan("A");
        
        Assert.Equal(till.AvailableItems[0].Name, till.ShoppingBasket[0].Name);
        Assert.Equal(till.AvailableItems[0].UnitPrice, till.ShoppingBasket[0].UnitPrice);
        Assert.Equal(till.AvailableItems[0].SpecialPrice, till.ShoppingBasket[0].SpecialPrice);
        Assert.Single(till.ShoppingBasket);

        // Couldn't get this approach to work: 
        // Assert.Equal(new List<Item>{new Item("A", 50, [3, 130])}, till.ShoppingBasket);
        // Assert.Equal([new Item("A", 50, [3, 130])], till.ShoppingBasket);
        // Assert.Contains(till.ShoppingBasket, item => item.Equals(new Item("A", 50, [3, 130])));
    }
}