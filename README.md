# .NET Invoice Generator 📄

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/QuestPDF-blue?style=for-the-badge" />
  <img src="https://img.shields.io/badge/License-MIT-green?style=for-the-badge" />
</p>

> A clean, production-ready PDF invoice generator built with .NET 8 and QuestPDF. Features a fluent builder API, customizable templates, and tax calculations for real business use.

## ✨ Features

- 📄 **Professional PDF output** with company branding
- 🏗️ **Fluent Builder API** for easy invoice construction
- 💰 **Tax calculations** with configurable rates
- 📋 **Line items** with quantity, price, and totals
- 🎨 **Customizable templates** (colors, fonts, layout)
- 📤 **Export options** — Save to file or memory stream
- 🏢 **Multi-currency** support (PKR, USD, AUD, EUR)

## 🚀 Quick Start

```bash
git clone https://github.com/iamahsanmehmood/dotnet-invoice-generator.git
cd dotnet-invoice-generator
dotnet run
```

## 💻 Usage

```csharp
var invoice = new InvoiceBuilder()
    .SetCompany("XechTech", "info@xechtech.com", "+92-300-5100317")
    .SetCustomer("Ali's Restaurant", "ali@restaurant.pk")
    .SetInvoiceNumber("INV-2026-001")
    .AddItem("POS Terminal Setup", 2, 15000)
    .AddItem("Staff Training", 1, 8000)
    .AddItem("Monthly Support", 3, 5000)
    .SetTaxRate(0.17m)
    .SetCurrency("PKR")
    .Build();

invoice.GeneratePdf("invoice.pdf");
```

## 🗂️ Project Structure

```
dotnet-invoice-generator/
├── src/
│   ├── InvoiceBuilder.cs      # Fluent builder API
│   ├── InvoiceDocument.cs     # QuestPDF document template
│   ├── Models.cs              # Invoice, LineItem, Company models
│   └── PdfGenerator.cs        # PDF generation logic
├── samples/
│   └── SampleInvoice.cs       # Example usage
├── Program.cs
├── InvoiceGenerator.csproj
├── LICENSE
└── README.md
```

## 👤 Author

**Ahsan Mehmood** — [LinkedIn](https://www.linkedin.com/in/iamahsanmehmood/) · [GitHub](https://github.com/iamahsanmehmood) · [XechTech](https://xechtech.com)

## 📄 License

MIT License
