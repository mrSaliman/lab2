namespace lab2.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public string Education { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
