using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services;

public class PaymentService(
    IConfiguration config , 
    ICartService cartService , 
    IUnitOfWork unit) : IpaymentService
{
    public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cardId)
    {
        StripeConfiguration.ApiKey = config["StripeSettings:Secret key"];

        var cart = await cartService.GetCartAsync(cardId);

        if(cart == null) return null;

        var shippingPrice = 0m;

        if (cart.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await unit.Repository<DeliveryMethod>().GetByIdAsync((int)cart.DeliveryMethodId);

            if(deliveryMethod == null) return null;

            shippingPrice = deliveryMethod.Price;


        }

        foreach(var item in cart.Items)
        {
            var productIem = await unit.Repository<Core.Entities.Product>().GetByIdAsync(item.ProductId);

            if(productIem == null) return null;

            if(item.Price != productIem.Price)
            {
                item.Price = productIem.Price;
            }
        }

        var service = new PaymentIntentService();

        PaymentIntent? intent = null;

        if (string.IsNullOrEmpty(cart.PayementIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long) cart.Items.Sum(x => x.Quantity * (x.Price * 100))  
                 + (long) shippingPrice * 100, 

                 Currency = "usd",
                 PaymentMethodTypes = ["card"]
            };

            intent = await service.CreateAsync(options);
            cart.PayementIntentId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long) cart.Items.Sum(x => x.Quantity * (x.Price * 100))
                 + (long) shippingPrice * 100, 
            };
            intent = await service.UpdateAsync(cart.PayementIntentId, options);

        }

        await cartService.SetCartAsync(cart);

        return cart;
    }
}
