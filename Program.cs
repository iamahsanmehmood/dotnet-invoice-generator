using InvoiceGenerator;

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔═══════════════════════════════════════╗");
Console.WriteLine("║   .NET Invoice Generator               ║");
Console.WriteLine("║   github.com/iamahsanmehmood            ║");
Console.WriteLine("╚═══════════════════════════════════════╝\n");
Console.ResetColor();

// Build an invoice using the fluent API
var invoice = new InvoiceBuilder()
    .SetCompany(
        name: "XechTech",
        email: "info@xechtech.com",
        phone: "+92-300-5100317",
        address: "Islamabad, Pakistan",
        website: "https://xechtech.com"
    )
    .SetCustomer(
        name: "Ali's Restaurant",
        email: "ali@alirestaurant.pk",
        phone: "+92-312-1234567",
        address: "F-7 Markaz, Islamabad"
    )
    .SetInvoiceNumber("INV-2026-001")
    .SetDates(DateTime.Today, DateTime.Today.AddDays(30))
    .AddItem("RestoCare+ POS License", 2, 25000)
    .AddItem("Terminal Hardware Setup", 2, 15000)
    .AddItem("Staff Training (on-site)", 1, 12000)
    .AddItem("Monthly Support & Updates", 6, 5000)
    .AddItem("Custom Menu Integration", 1, 8000)
    .SetTaxRate(0.17m)
    .SetCurrency("PKR")
    .SetNotes("Thank you for choosing XechTech! Support hotline: +92-300-5100317")
    .SetPaymentTerms("Net 30")
    .Build();

// Preview in console
Console.WriteLine($"  Invoice: #{invoice.InvoiceNumber}");
Console.WriteLine($"  Customer: {invoice.Customer.Name}");
Console.WriteLine($"  Items: {invoice.Items.Count}");
Console.WriteLine($"  Subtotal: {invoice.FormattedCurrency(invoice.Subtotal)}");
Console.WriteLine($"  Tax (17%): {invoice.FormattedCurrency(invoice.TaxAmount)}");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"  Grand Total: {invoice.FormattedCurrency(invoice.GrandTotal)}");
Console.ResetColor();

// Generate PDF
var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "output", "invoice.pdf");
Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
invoice.GeneratePdf(outputPath);

Console.WriteLine($"\n⭐ Star this repo: github.com/iamahsanmehmood/dotnet-invoice-generator");
