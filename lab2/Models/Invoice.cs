namespace lab2.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public string SupplierName { get; set; } = null!;

    public DateTime DeliveryDate { get; set; }

    public string MaterialType { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal Weight { get; set; }

    public int ReceivedByEmployeeId { get; set; }

    public virtual Employee ReceivedByEmployee { get; set; } = null!;
}
