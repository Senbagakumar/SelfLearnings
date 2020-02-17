using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using System.Diagnostics;
using System.Data;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;


namespace FileDownload.Controllers
{
    public class PDFform
    {
        readonly static Color TableBorder = new Color(81, 125, 192);
        readonly static Color TableBlue = new Color(235, 240, 249);
        readonly static Color TableGray = new Color(242, 242, 242);

        static string Author = "Lakshmi Kanth";
        static string CompanyName = "Golden Infotech";
        static string Address = "Chennai, India";

        /// <summary>
        /// The MigraDoc document that represents the invoice.
        /// </summary>
        Document document;

        /// <summary>
        /// An XML invoice based on a sample created with Microsoft InfoPath.
        /// </summary>
        DataTable dt;
        string IqLogo;
        /// <summary>
        /// The root navigator for the XML document.
        /// </summary>


        /// <summary>
        /// The text frame of the MigraDoc document that contains the address.
        /// </summary>
        TextFrame addressFrame;

        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        Table table;

        /// <summary>
        /// Initializes a new instance of the class BillFrom and opens the specified XML document.
        /// </summary>
        /// 

        string cilogo;
        public PDFform(DataTable dtIn, string iqlogo, string ciilogo)
        {
            dt = dtIn;
            IqLogo = iqlogo; //~/img/iqlogo.png
            cilogo = ciilogo;
        }

        /// <summary>
        /// Creates the invoice document.
        /// </summary>
        public Document CreateDocument(string companyName, string address, string contactName)
        {
            CompanyName = companyName;
            Address = address;
            Author = contactName;

            // Create a new MigraDoc document
            this.document = new Document();
            this.document.Info.Title = "Assessment Report";
            this.document.Info.Subject = "";
            this.document.Info.Author = Author;

            DefineStyles();

            CreatePage();

            FillContent();

            return this.document;
        }

        /// <summary>
        /// Defines the styles used to format the MigraDoc document.
        /// </summary>
        void DefineStyles()
        {
            // Get the predefined style Normal.
            Style style = this.document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";

            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }



        void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            Section section = this.document.AddSection();

            // Put a logo in the header
            Image iqimage = section.AddImage(IqLogo);


            iqimage.Top = ShapePosition.Top;
            iqimage.Left = ShapePosition.Left;
            iqimage.WrapFormat.Style = WrapStyle.Through;

            Image ciLogo = section.AddImage(cilogo);
            ciLogo.Top = ShapePosition.Top;
            ciLogo.Left = ShapePosition.Right;
            ciLogo.WrapFormat.Style = WrapStyle.Through;


            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText(CompanyName);
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the address
            this.addressFrame = section.AddTextFrame();
            this.addressFrame.Height = "3.0cm";
            this.addressFrame.Width = "7.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.Top = "5.0cm";
            this.addressFrame.RelativeVertical = RelativeVertical.Page;

            // Put sender in address frame
            paragraph = this.addressFrame.AddParagraph(Address);
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "6cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Question Information", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Date, ");
            paragraph.AddDateField("dd.MM.yyyy");

            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;




            int j = 0;
            // Before you can add a row, you must define the columns
            Column column;
            foreach (DataColumn col in dt.Columns)
            {
                if(j ==0)
                    column = this.table.AddColumn(Unit.FromCentimeter(1));
                else if(j==1)
                    column = this.table.AddColumn(Unit.FromCentimeter(4));
                else if(j==2)
                    column = this.table.AddColumn(Unit.FromCentimeter(7));
                else
                    column = this.table.AddColumn(Unit.FromCentimeter(4));

                column.Format.Alignment = ParagraphAlignment.Center;
                j++;

            }

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;


            for (int i = 0; i < dt.Columns.Count; i++)
            {

                row.Cells[i].AddParagraph(dt.Columns[i].ColumnName);
                row.Cells[i].Format.Font.Bold = false;
                row.Cells[i].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[i].VerticalAlignment = VerticalAlignment.Bottom;


            }

            this.table.SetEdge(0, 0, dt.Columns.Count, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);


        }



        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        void FillContent()
        {
            // Fill address in address text frame

            Paragraph paragraph = this.addressFrame.AddParagraph();
            paragraph.AddText(Author);
            paragraph.AddLineBreak();
            paragraph.AddText(CompanyName);
            paragraph.AddLineBreak();
            paragraph.AddText(Address);

            Row row1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row1 = this.table.AddRow();

                row1.TopPadding = 1.5;


                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row1.Cells[j].Shading.Color = TableGray;
                    row1.Cells[j].VerticalAlignment = VerticalAlignment.Center;
                    row1.Cells[j].Format.Alignment = ParagraphAlignment.Left;
                    row1.Cells[j].Format.FirstLineIndent = 1;
                    row1.Cells[j].AddParagraph(dt.Rows[i][j].ToString());

                    this.table.SetEdge(0, this.table.Rows.Count - 2, dt.Columns.Count, 1, Edge.Box, BorderStyle.Single, 0.75);
                }
            }


            // Add the notes paragraph
            paragraph = this.document.LastSection.AddParagraph();
            paragraph.Format.SpaceBefore = "1cm";
            paragraph.Format.Borders.Width = 0.75;
            paragraph.Format.Borders.Distance = 3;
            paragraph.Format.Borders.Color = TableBorder;
            paragraph.Format.Shading.Color = TableGray;

            paragraph.AddText("Thanks for attending the Survey");


        }
    }
}