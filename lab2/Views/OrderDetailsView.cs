using System;
using System.Collections.Generic;

namespace lab2;

public partial class OrderDetailsView
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int CustomerId { get; set; }

    public string? CompanyName { get; set; }

    public string? RepresentativeLastName { get; set; }

    public string? RepresentativeFirstName { get; set; }

    public string? RepresentativeMiddleName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int? FurnitureId { get; set; }

    public string? FurnitureName { get; set; }

    public string? FurnitureDescription { get; set; }

    public string? FurnitureMaterial { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalPrice { get; set; }
}
