namespace InvoiceGenerator;

/// <summary>
/// Core models for the invoice generator.
/// </summary>

public class Company
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string LogoPath { get; set; } = string.Empty;
}

public class Customer
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

public class LineItem
{
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;
}

public class Invoice
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; } = DateTime.Today;
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(30);
    public Company Company { get; set; } = new();
    public Customer Customer { get; set; } = new();
    public List<LineItem> Items { get; set; } = new();
    public decimal TaxRate { get; set; }
    public string Currency { get; set; } = "PKR";
    public string Notes { get; set; } = string.Empty;
    public string PaymentTerms { get; set; } = "Net 30";

    // Calculated properties
    public decimal Subtotal => Items.Sum(i => i.Total);
    public decimal TaxAmount => Subtotal * TaxRate;
    public decimal GrandTotal => Subtotal + TaxAmount;
    
    public string FormattedCurrency(decimal amount) =>
        $"{Currency} {amount:N2}";
}
