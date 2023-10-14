namespace lab2.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string RepresentativeLastName { get; set; } = null!;

    public string RepresentativeFirstName { get; set; } = null!;

    public string RepresentativeMiddleName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
