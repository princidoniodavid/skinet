using Core.Entities;

namespace Core.Interfaces;

public interface IBasketRepository
{
    Task<CustomerBusket?> GetBasketAsync(string basketId);
    Task<CustomerBusket?> UpdateBasketAsync(CustomerBusket basket);
    Task<bool> DeleteBasketAsync(string basketId);
}