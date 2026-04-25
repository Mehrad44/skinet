using System;
using API.DTOs;
using Core.Entities.OrderAggregate;

namespace API.Extensions;

public static class OrderMappingExtensions
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            BuyerEmail = order.BuyerEmail,
            OrderDate = order.OrderDate,
            shippingAddress = order.shippingAddress,
            paymentSummary = order.paymentSummary,
            DeliveryMethod = order.DeliveryMethod.Description,
            ShippingPrice = order.DeliveryMethod.Price,
            OrderItems = order.OrderItems.Select(x => x.ToDto()).ToList(),
            Total = order.GetTotal(),
            Subtotal = order.Subtotal,
            Status = order.Status.ToString(),
            PaymentIntentId = order.PaymentIntentId,
            



        };
    }

    public static OrderItemDto ToDto(this OrderItem orderItem)
    {
        return new OrderItemDto
        {
            ProductId = orderItem.ItemOrdred.ProductId,
            ProductName = orderItem.ItemOrdred.ProductName,
            PictureUrl = orderItem.ItemOrdred.PictureUrl,
            Price =orderItem.Price,
            Quantity = orderItem.Quantity,

        };
    }
}
