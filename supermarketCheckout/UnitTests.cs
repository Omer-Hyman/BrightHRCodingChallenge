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

        Assert.Equal(till.AvailableItems[0], till.ShoppingBasket[0]);
        Assert.Single(till.ShoppingBasket);
    }
    
    [Fact]
    public void Scan_ItemD()
    {
        Till till = new Till();

        till.Scan("D");
        
        Assert.Equal(till.AvailableItems[3], till.ShoppingBasket[0]);
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
        
        Assert.Equal(till.AvailableItems[0], till.ShoppingBasket[0]);
        Assert.Equal(till.AvailableItems[1], till.ShoppingBasket[1]);
        Assert.Equal(till.AvailableItems[2], till.ShoppingBasket[2]);
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