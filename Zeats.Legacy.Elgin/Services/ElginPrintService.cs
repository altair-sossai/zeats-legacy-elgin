using System;
using Zeats.Legacy.Elgin.Helpers;
using Zeats.Legacy.PlainTextTable.Print.Enums;
using Zeats.Legacy.PlainTextTable.Print.Print;

namespace Zeats.Legacy.Elgin.Services
{
    public class ElginPrintService : IPrintService
    {
        private static readonly object Lock = new object();

        public void Print(PrintCollection printCollection)
        {
            lock (Lock)
            {
                var printer = RawPrinterHelper.OpenPrinter(printCollection.Options.PortName);
                if (printer == null)
                    return;

                foreach (var printItem in printCollection)
                {
                    switch (printItem.FontType)
                    {
                        case FontType.Text:
                            PrintText(printer.Value, printItem);
                            break;

                        case FontType.BarCode:
                            PrintBarCode(printer.Value, printItem);
                            break;
                    }
                }

                RawPrinterHelper.ClosePrinter(printer.Value);
            }
        }

        public void Cut(string portName, CutType cutType = CutType.Full)
        {
            lock (Lock)
            {
                var printer = RawPrinterHelper.OpenPrinter(portName);
                if (printer == null)
                    return;

                ElginHelper.LineFeed(printer.Value, 2);
                ElginHelper.CutPaper(printer.Value);

                RawPrinterHelper.ClosePrinter(printer.Value);
            }
        }

        private static void PrintText(IntPtr printer, PrintItem printItem)
        {
            ElginHelper.NormalModeText(printer);

            if (printItem.Bold)
                ElginHelper.EmphasizedModeText(printer);

            if (printItem.Italic)
                ElginHelper.CharFontCText(printer);

            if (printItem.Underline)
                ElginHelper.UnderlineModeText(printer);

            switch (printItem.FontSize)
            {
                case FontSize.Small:
                    ElginHelper.CharFontBText(printer);
                    break;

                case FontSize.Large:
                    ElginHelper.NormalModeText(printer);
                    ElginHelper.DoubleHeightText(printer);
                    ElginHelper.DoubleWidthText(printer);
                    break;
            }

            ElginHelper.PrintText(printer, printItem.Content);
        }

        private static void PrintBarCode(IntPtr printer, PrintItem printItem)
        {
			ElginHelper.BarcodeHeight(printer, 80);
            ElginHelper.BarcodeWidth(printer);
            ElginHelper.BarcodeHriChars(printer, 1);
            ElginHelper.BarcodeHriPostion(printer, 2);

            switch (printItem.BarCodeType)
            {
                case BarCodeType.Ean13:
                    ElginHelper.PrintBarcode(printer, printItem.Content);
                    break;

                case BarCodeType.Code128:
                    ElginHelper.PrintBarcode(printer, printItem.Content, 8);
                    break;

                default:
                    ElginHelper.PrintBarcode(printer, printItem.Content);
                    break;
            }

            ElginHelper.LineFeed(printer, 1);
        }
    }
}