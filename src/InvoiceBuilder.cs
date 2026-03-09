namespace InvoiceGenerator;

/// <summary>
/// Fluent builder for constructing Invoice objects step by step.
/// </summary>
public class InvoiceBuilder
{
    private readonly Invoice _invoice = new();

    public InvoiceBuilder SetInvoiceNumber(string number)
    {
        _invoice.InvoiceNumber = number;
        return this;
    }

    public InvoiceBuilder SetCompany(string name, string email, string phone, string? address = null, string? website = null)
    {
        _invoice.Company = new Company
        {
            Name = name,
            Email = email,
            Phone = phone,
            Address = address ?? string.Empty,
            Website = website ?? string.Empty
        };
        return this;
    }

    public InvoiceBuilder SetCustomer(string name, string email, string? phone = null, string? address = null)
    {
        _invoice.Customer = new Customer
        {
            Name = name,
            Email = email,
            Phone = phone ?? string.Empty,
            Address = address ?? string.Empty
        };
        return this;
    }

    public InvoiceBuilder SetDates(DateTime issueDate, DateTime dueDate)
    {
        _invoice.IssueDate = issueDate;
        _invoice.DueDate = dueDate;
        return this;
    }

    public InvoiceBuilder AddItem(string description, int quantity, decimal unitPrice)
    {
        _invoice.Items.Add(new LineItem
        {
            Description = description,
            Quantity = quantity,
            UnitPrice = unitPrice
        });
        return this;
    }

    public InvoiceBuilder SetTaxRate(decimal rate)
    {
        _invoice.TaxRate = rate;
        return this;
    }

    public InvoiceBuilder SetCurrency(string currency)
    {
        _invoice.Currency = currency;
        return this;
    }

    public InvoiceBuilder SetNotes(string notes)
    {
        _invoice.Notes = notes;
        return this;
    }

    public InvoiceBuilder SetPaymentTerms(string terms)
    {
        _invoice.PaymentTerms = terms;
        return this;
    }

    public Invoice Build()
    {
        if (string.IsNullOrEmpty(_invoice.InvoiceNumber))
            _invoice.InvoiceNumber = $"INV-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..4].ToUpper()}";

        return _invoice;
    }
}
