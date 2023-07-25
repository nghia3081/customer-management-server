

using System.Xml;
using IronPdf.Signing;
using System.Security.Cryptography.X509Certificates;
using IronSoftware.Drawing;

namespace Repository.Configuration
{
    public class FilePrinting
    {
        public static async Task<byte[]> PrintContract(string templateFilePath, BusinessObject.Models.Contract contract)
        {
            using var stream = new StreamReader(templateFilePath);
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(stream);
            var properties = contract.GetType().GetProperties();
            foreach (var property in properties)
            {
                var element = xDocument.SelectSingleNode($"//*[@contractField='{property.Name}']");
                if (element is null) continue;
                element.InnerText = property.GetValue(contract)?.ToString() ?? "";
            }
            var customerProperties = contract.Customer.GetType().GetProperties();
            foreach (var customerProperty in customerProperties)
            {
                var element = xDocument.SelectSingleNode($"//*[@customerField='{customerProperty.Name}']");
                if (element is null) continue;
                element.InnerText = customerProperty.GetValue(contract.Customer)?.ToString() ?? "";
            }
            XmlDocument detailTemp = new();
            var detailRowTemplate = xDocument.SelectSingleNode("//*[@type='detail-row']").OuterXml;
            detailTemp.LoadXml(detailRowTemplate);
            xDocument.SelectSingleNode($"//*[@id='detail-table']").InnerXml = "";
            foreach (var detail in contract.Details)
            {
                xDocument.SelectSingleNode($"//*[@id='detail-table']").InnerXml += GetDetailRow(detail, detailTemp).InnerXml;
            }
            return await PrintHtmlToPdf(xDocument.OuterXml);
        }
        private static XmlDocument GetDetailRow(BusinessObject.Models.ContractDetail detail, XmlDocument template)
        {
            var detailProperties = detail.GetType().GetProperties();
            foreach (var detailProperty in detailProperties)
            {
                var element = template.SelectSingleNode($"//*[@dtField='{detailProperty.Name}']");
                if (element is null) continue;
                element.InnerText = detailProperty.GetValue(detail)?.ToString();
            }
            return template;
        }

        public static async Task<byte[]> PrintHtmlToPdf(string html)
        {
            var renderer = new ChromePdfRenderer();
            var pdf = await renderer.RenderHtmlAsPdfAsync(html);
            return pdf.BinaryData;
        }
        public static async Task<byte[]> SignPdf(byte[] pdfFileBytes, string certPath, string certPassword, string certLogoPath)
        {
            PdfDocument document = new(pdfFileBytes);
            //Creates a digital signature.
            X509Certificate2 digitalId = new(certPath, certPassword, X509KeyStorageFlags.Exportable);
            PdfSignature signature = new(digitalId)
            {
                //Sets the signature information.
                SigningLocation = "Signing contract",
                SigningContact = "0123456789",

            };
            signature.SigningLocation = "FPT University";
            signature.SignatureImage = new PdfSignatureImage(certLogoPath, 0, new CropRectangle(200, 250,150,75));
            await Task.Run(() => document.Sign(signature));
            return document.BinaryData;

        }

    }
}
