using System;

namespace Core.Entities.OrderAggregate;

public class OrderItem : BaseEntity
{   
    public ProductItemOrdred ItemOrdred { get; set; }   = null!;

    public decimal Price {get;set;}


    public int Quantity {get;set;}


}
