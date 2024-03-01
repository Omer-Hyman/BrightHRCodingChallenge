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

    [Fact]
    public void CalculateTotalPrice_NoDiscounts()
    {
        Till till = new Till();

        till.Scan("A");
        till.Scan("B");
        till.Scan("C");
        till.Scan("D");
        var totalPrice = till.GetTotalPrice();

        Assert.Equal(115, totalPrice);
    }

    [Fact]
    public void CalculateTotalPrice_SingleDiscount()
    {
        Till till = new Till();

        till.Scan("A");
        till.Scan("B");
        till.Scan("B");
        till.Scan("D");
        var totalPrice = till.GetTotalPrice();

        Assert.Equal(110, totalPrice);

    }

    [Fact]
    public void CalculateTotalPrice_TwoDiscountsInRandomOrder()
    {
        Till till = new Till();

        till.Scan("A");
        till.Scan("B");
        till.Scan("A");
        till.Scan("B");
        till.Scan("A");
        var totalPrice = till.GetTotalPrice();

        Assert.Equal(175, totalPrice);
    }

    [Fact]
    public void CalculateTotalPrice_EmptyShoppingBasket()
    {
        Till till = new Till();

        var totalPrice = till.GetTotalPrice();

        Assert.Equal(0, totalPrice);
    }

    [Fact]
    public void CalculateTotalPrice_QuantityTooLowForDiscount()
    {
        Till till = new Till();

        till.Scan("A");
        till.Scan("A");
        var totalPrice = till.GetTotalPrice();

        Assert.Equal(100, totalPrice);
    }
    
    [Fact]
    public void CalculateTotalPrice_MoreThanEnoughForDiscount()
    {
        Till till = new Till();

        till.Scan("A");
        till.Scan("A");
        till.Scan("A");
        till.Scan("A");
        var totalPrice = till.GetTotalPrice();

        Assert.Equal(180, totalPrice);
    }
}