namespace supermarketCheckout;
using Xunit;

public class UnitTests
{
    [Fact]
    public void ScanItems_A()
    {
        Till till = new Till();

        till.Scan("1");

        Assert.Equal([new Item("A", 50, [3, 130])], till.ShoppingBasket);
    }
}