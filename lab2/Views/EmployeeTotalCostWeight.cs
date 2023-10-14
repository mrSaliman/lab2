using System;
using System.Collections.Generic;

namespace lab2;

public partial class EmployeeTotalCostWeight
{
    public int EmployeeId { get; set; }

    public string? EmployeeLastName { get; set; }

    public string? EmployeeFirstName { get; set; }

    public string? EmployeeMiddleName { get; set; }

    public string? EmployeePosition { get; set; }

    public string? EmployeeEducation { get; set; }

    public decimal? TotalCost { get; set; }

    public decimal? TotalWeight { get; set; }
}
