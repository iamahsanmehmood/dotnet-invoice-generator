using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InvoiceGenerator;

/// <summary>
/// QuestPDF document template for generating professional PDF invoices.
/// </summary>
public class InvoiceDocument : IDocument
{
    private readonly Invoice _invoice;

    public InvoiceDocument(Invoice invoice)
    {
        _invoice = invoice;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(40);
            page.DefaultTextStyle(x => x.FontSize(10));

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(col =>
            {
                col.Item().Text(_invoice.Company.Name)
                    .FontSize(24).Bold().FontColor(Colors.Blue.Darken3);
                col.Item().Text(_invoice.Company.Email).FontSize(9).FontColor(Colors.Grey.Darken1);
                col.Item().Text(_invoice.Company.Phone).FontSize(9).FontColor(Colors.Grey.Darken1);
                if (!string.IsNullOrEmpty(_invoice.Company.Address))
                    col.Item().Text(_invoice.Company.Address).FontSize(9).FontColor(Colors.Grey.Darken1);
            });

            row.ConstantItem(150).Column(col =>
            {
                col.Item().Text("INVOICE").FontSize(28).Bold().FontColor(Colors.Blue.Darken3);
                col.Item().Text($"#{_invoice.InvoiceNumber}").FontSize(12).FontColor(Colors.Grey.Darken1);
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingVertical(20).Column(col =>
        {
            // Customer & dates
            col.Item().Row(row =>
            {
                row.RelativeItem().Column(c =>
                {
                    c.Item().Text("Bill To:").Bold().FontSize(11);
                    c.Item().Text(_invoice.Customer.Name).FontSize(11);
                    c.Item().Text(_invoice.Customer.Email).FontSize(9).FontColor(Colors.Grey.Darken1);
                });

                row.ConstantItem(150).Column(c =>
                {
                    c.Item().Text($"Issue Date: {_invoice.IssueDate:MMM dd, yyyy}").FontSize(9);
                    c.Item().Text($"Due Date: {_invoice.DueDate:MMM dd, yyyy}").FontSize(9);
                    c.Item().Text($"Terms: {_invoice.PaymentTerms}").FontSize(9);
                });
            });

            col.Item().PaddingVertical(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

            // Items table
            col.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.ConstantColumn(60);
                    columns.ConstantColumn(100);
                    columns.ConstantColumn(100);
                });

                // Header
                table.Header(header =>
                {
                    header.Cell().Background(Colors.Blue.Darken3).Padding(5)
                        .Text("Description").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Darken3).Padding(5)
                        .Text("Qty").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Darken3).Padding(5)
                        .Text("Unit Price").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Darken3).Padding(5)
                        .Text("Total").FontColor(Colors.White).Bold();
                });

                // Rows
                foreach (var item in _invoice.Items)
                {
                    var bgColor = _invoice.Items.IndexOf(item) % 2 == 0 
                        ? Colors.White : Colors.Grey.Lighten4;
                    
                    table.Cell().Background(bgColor).Padding(5).Text(item.Description);
                    table.Cell().Background(bgColor).Padding(5).Text(item.Quantity.ToString());
                    table.Cell().Background(bgColor).Padding(5).Text(_invoice.FormattedCurrency(item.UnitPrice));
                    table.Cell().Background(bgColor).Padding(5).Text(_invoice.FormattedCurrency(item.Total));
                }
            });

            col.Item().PaddingTop(10).AlignRight().Column(totals =>
            {
                totals.Item().Text($"Subtotal: {_invoice.FormattedCurrency(_invoice.Subtotal)}").FontSize(10);
                totals.Item().Text($"Tax ({_invoice.TaxRate:P0}): {_invoice.FormattedCurrency(_invoice.TaxAmount)}").FontSize(10);
                totals.Item().PaddingTop(5).Text($"TOTAL: {_invoice.FormattedCurrency(_invoice.GrandTotal)}")
                    .FontSize(14).Bold().FontColor(Colors.Blue.Darken3);
            });

            // Notes
            if (!string.IsNullOrEmpty(_invoice.Notes))
            {
                col.Item().PaddingTop(20).Column(notes =>
                {
                    notes.Item().Text("Notes:").Bold();
                    notes.Item().Text(_invoice.Notes).FontSize(9).FontColor(Colors.Grey.Darken1);
                });
            }
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.AlignCenter().Text(text =>
        {
            text.Span("Generated by ").FontSize(8).FontColor(Colors.Grey.Medium);
            text.Span(_invoice.Company.Name).FontSize(8).Bold().FontColor(Colors.Blue.Darken3);
            text.Span(" · Page ").FontSize(8).FontColor(Colors.Grey.Medium);
            text.CurrentPageNumber().FontSize(8);
            text.Span(" of ").FontSize(8).FontColor(Colors.Grey.Medium);
            text.TotalPages().FontSize(8);
        });
    }
}

/// <summary>
/// Static helper for generating PDFs.
/// </summary>
public static class PdfGenerator
{
    public static void GeneratePdf(this Invoice invoice, string filePath)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var document = new InvoiceDocument(invoice);
        document.GeneratePdf(filePath);
        Console.WriteLine($"✅ Invoice generated: {filePath}");
    }

    public static byte[] GeneratePdfBytes(this Invoice invoice)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var document = new InvoiceDocument(invoice);
        return document.GeneratePdf();
    }
}
