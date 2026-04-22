using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IpaymentService
{
 Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cardId);
}
