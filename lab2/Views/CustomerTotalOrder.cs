using System;
using System.Collections.Generic;

namespace lab2;

public partial class CustomerTotalOrder
{
    public int CustomerId { get; set; }

    public string? CompanyName { get; set; }

    public string? RepresentativeLastName { get; set; }

    public string? RepresentativeFirstName { get; set; }

    public string? RepresentativeMiddleName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public decimal? TotalPurchases { get; set; }
}
