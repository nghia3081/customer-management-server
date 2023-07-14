
using iText.Commons.Bouncycastle.Cert;
using iText.Commons.Bouncycastle.Crypto;
using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using System.Data;

namespace BusinessObject
{
    public enum PrintType
    {
        PDF,
        XLSX
    }
    public class FilePrinting
    {
        public static async Task<byte[]> Print(object data, string templateFilePath, PrintType printType = PrintType.PDF, int dataMember = 0)
        {
            ////XtraReport report = XtraReport.FromFile(templateFilePath, true);
            ////if (data != null)
            ////{
            ////    report.DataSource = data;
            ////}

            ////if (data is DataSet)
            ////{
            ////    DataSet ds = data as DataSet;
            ////    if (ds.Tables.Count > 0)
            ////    {
            ////        report.DataMember = ds.Tables[dataMember].TableName;
            ////    }
            ////}
            //report.CreateDocument();
            using MemoryStream ms = new();
            //await Task.Run(() =>
            //{
            //    switch (printType)
            //    {
            //        case PrintType.PDF: report.ExportToPdf(ms); break;
            //        case PrintType.XLSX: report.ExportToXlsx(ms); break;
            //    }
            //});
            return ms.ToArray();
        }
        public static async Task<byte[]> SignPdf(byte[] pdfFileBytes, string certPath, string certPassword, BusinessObject.Models.Contract contract)
        {
            using FileStream fileStream = new FileStream(certPath, FileMode.Open, FileAccess.Read);
            Pkcs12Store pk12 = new Pkcs12Store(fileStream, certPassword.ToCharArray());
            string alias = null;
            foreach (string aliasTemp in pk12.Aliases)
            {
                alias = aliasTemp;
                if (pk12.IsKeyEntry(alias))
                {
                    break;
                }
            }
            IPrivateKey pk = (IPrivateKey)pk12.GetKey(alias);

            X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
            X509Certificate[] chain = new X509Certificate[ce.Length];
            for (int k = 0; k < ce.Length; ++k)
            {
                chain[k] = ce[k].Certificate;
            }
            using MemoryStream readPdfStream = new(pdfFileBytes);
            PdfReader reader = new PdfReader(readPdfStream);
            using MemoryStream outputStream = new();
            PdfSigner signer = new PdfSigner(reader, outputStream, new StampingProperties());

            PdfSignatureAppearance appearance = signer.GetSignatureAppearance();
            appearance.SetReason("Sign contract")
                .SetContact(contract.Customer?.Phone)
                .SetCertificate(((IX509Certificate[])chain)[0])
                .SetLocation("VietNam")
                .SetPageRect(new iText.Kernel.Geom.Rectangle(36, 648, 200, 100))
                .SetPageNumber(1);
            signer.SetFieldName("ContractSignature");

            IExternalSignature pks = new PrivateKeySignature(pk, DigestAlgorithms.SHA256);
            signer.SignDetached(pks, (IX509Certificate[])chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
            return outputStream.ToArray();
        }

    }
}
