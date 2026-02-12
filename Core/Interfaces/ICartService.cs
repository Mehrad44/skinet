using System;
using Core.Entities;

namespace Core.Interfaces;

public interface ICardService
{
    Task<ShoppingCart?> GetCartAsync(string key);

    Task<ShoppingCart?> SetCartAsync(ShoppingCart cart);

    Task<bool> DeletCartAsync(string key);

    



}
