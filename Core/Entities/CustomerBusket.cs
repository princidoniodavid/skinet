namespace Core.Entities;

public class CustomerBusket
{
    public CustomerBusket()
    {
        
    }
    public CustomerBusket(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
    public List<BasketItem> Items { get; set; } = new();
}