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
    public void Scan_ItemA()
    {
        Till till = new Till();

        till.Scan("A");
        
        Assert.Equal(till.AvailableItems[0].Name, till.ShoppingBasket[0].Name);
        Assert.Equal(till.AvailableItems[0].UnitPrice, till.ShoppingBasket[0].UnitPrice);
        Assert.Equal(till.AvailableItems[0].SpecialPrice, till.ShoppingBasket[0].SpecialPrice);
        Assert.Single(till.ShoppingBasket);
    }
    
    [Fact]
    public void Scan_ItemD()
    {
        Till till = new Till();

        till.Scan("D");
        
        Assert.Equal(till.AvailableItems[3].Name, till.ShoppingBasket[0].Name);
        Assert.Equal(till.AvailableItems[3].UnitPrice, till.ShoppingBasket[0].UnitPrice);
        Assert.Equal(till.AvailableItems[3].SpecialPrice, till.ShoppingBasket[0].SpecialPrice);
        Assert.Single(till.ShoppingBasket);
    }

    [Fact]
    public void Scan_InvalidItem()
    {
        Till till = new Till();

        till.Scan("F");
        
        Assert.Empty(till.ShoppingBasket);
    }

    [Fact]
    public void Scan_MultipleItems()
    {
        Till till = new Till();

        till.Scan("A");
        till.Scan("B");
        till.Scan("C");
        
        Assert.Equal(till.AvailableItems[0].Name, till.ShoppingBasket[0].Name);
        Assert.Equal(till.AvailableItems[0].UnitPrice, till.ShoppingBasket[0].UnitPrice);
        Assert.Equal(till.AvailableItems[0].SpecialPrice, till.ShoppingBasket[0].SpecialPrice);
        
        Assert.Equal(till.AvailableItems[1].Name, till.ShoppingBasket[1].Name);
        Assert.Equal(till.AvailableItems[1].UnitPrice, till.ShoppingBasket[1].UnitPrice);
        Assert.Equal(till.AvailableItems[1].SpecialPrice, till.ShoppingBasket[1].SpecialPrice);
        
        Assert.Equal(till.AvailableItems[2].Name, till.ShoppingBasket[2].Name);
        Assert.Equal(till.AvailableItems[2].UnitPrice, till.ShoppingBasket[2].UnitPrice);
        Assert.Equal(till.AvailableItems[2].SpecialPrice, till.ShoppingBasket[2].SpecialPrice);

        Assert.Equal(3, till.ShoppingBasket.Count);
    }
}