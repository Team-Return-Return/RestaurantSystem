﻿namespace RestaurantSystem.PDFManaging
{
    using RestaurantSystem.Models;
    using System.Collections.Generic;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System.IO;
    using System.Linq;

    public class SalesPDFManager : ISalesPDFManager
    {
        private const string FileHeader = "Sales Report";
        private const string FileFooter = "Total sales: ";
        private readonly string fileName = Directory.GetCurrentDirectory() + "/Reports/Report.pdf";

        public byte[] ExportSalesFile(IList<Sale> sales)
        {
            // The numbers below are margins to be used in the document - left, right, top, bottom
            Document doc = new Document(PageSize.LETTER, 10, 10, 42, 35);
            
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(this.fileName, FileMode.Create));
                doc.Open();
                this.CreateTitleHeader(doc);
                this.CreateTableHeader(doc);
                this.CreateTable(sales, doc);
                doc.Close();
                return stream.ToArray();
            }
        }

        private void CreateTitleHeader(Document doc)
        {
            PdfPTable titleHeader = new PdfPTable(1); // this number is the number of columns
            PdfPCell cellHeader = new PdfPCell(this.MakeBold(FileHeader));
            cellHeader.HorizontalAlignment = 1;

            titleHeader.AddCell(cellHeader);

            doc.Add(titleHeader);
        }

        private void CreateTableHeader(Document doc)
        {
            PdfPTable tableHeader = new PdfPTable(5); // num of columns
            PdfPCell cellHeaderTitle = new PdfPCell(new Phrase("Sales Report"));
            cellHeaderTitle.BackgroundColor = new BaseColor(210, 210, 210);
            cellHeaderTitle.Colspan = 5;

            PdfPCell saleId = new PdfPCell(this.MakeBold("Sale ID"));
            saleId.BackgroundColor = new BaseColor(210, 210, 210);

            PdfPCell modifiedDate = new PdfPCell(this.MakeBold("Modified On"));
            saleId.BackgroundColor = new BaseColor(210, 210, 210);

            PdfPCell tableNumber = new PdfPCell(this.MakeBold("Table Number"));
            saleId.BackgroundColor = new BaseColor(210, 210, 210);

            PdfPCell waiterId = new PdfPCell(this.MakeBold("Waiter ID"));
            saleId.BackgroundColor = new BaseColor(210, 210, 210);

            PdfPCell waiter = new PdfPCell(this.MakeBold("Waiter"));
            saleId.BackgroundColor = new BaseColor(210, 210, 210);

            tableHeader.AddCell(saleId);
            tableHeader.AddCell(modifiedDate);
            tableHeader.AddCell(tableNumber);
            tableHeader.AddCell(waiterId);
            tableHeader.AddCell(waiter);

            doc.Add(tableHeader);
        }

        private void CreateTable(IList<Sale> sales, Document doc)
        {
            PdfPTable tableBody = new PdfPTable(5);

            foreach (var sale in sales)
            {
                 tableBody.AddCell(sale.ToString()); 
            }

            PdfPTable footer = new PdfPTable(5);

            PdfPCell totalSalesTextCell = new PdfPCell(new Phrase(FileFooter));
            totalSalesTextCell.Colspan = 4;
            totalSalesTextCell.HorizontalAlignment = 2;
            footer.AddCell(totalSalesTextCell);

            // Count how many sales have been added to the table and write it in the footer
            PdfPCell totalSalesCell = new PdfPCell(new Phrase(sales.ToList().Count.ToString()));
            totalSalesCell.HorizontalAlignment = 1;
            footer.AddCell(totalSalesCell);

            doc.Add(tableBody);
            doc.Add(footer);
        }

        private Phrase MakeBold(string text)
        {
            var phrase = new Phrase();
            phrase.Add(new Chunk(text, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));

            return phrase;
        }
    }
}
