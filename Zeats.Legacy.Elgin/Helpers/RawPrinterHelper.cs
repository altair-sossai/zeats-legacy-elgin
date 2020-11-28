using System;
using System.Runtime.InteropServices;

namespace Zeats.Legacy.Elgin.Helpers
{
    public class RawPrinterHelper
    {
        public static IntPtr? OpenPrinter(string printerName)
        {
            var docInfo = new DocInfo
            {
                pDocName = "i9 RAW Document",
                pDataType = "RAW"
            };

            if (!PrinterHelper.OpenPrinter(printerName.Normalize(), out var printer, IntPtr.Zero))
                return null;

            if (!PrinterHelper.StartDocPrinter(printer, 1, docInfo))
                return null;

            if (!PrinterHelper.StartPagePrinter(printer))
                return null;

            return printer;
        }

        public static bool WritePrinter(IntPtr printer, byte[] bytes)
        {
            var destination = Marshal.AllocCoTaskMem(bytes.Length);

            Marshal.Copy(bytes, 0, destination, bytes.Length);

            return PrinterHelper.WritePrinter(printer, destination, bytes.Length, out _);
        }

        public static bool WritePrinter(IntPtr printer, string value)
        {
            var count = value.Length;
            var bytes = Marshal.StringToCoTaskMemAnsi(value);

            return PrinterHelper.WritePrinter(printer, bytes, count, out _);
        }

        public static bool ClosePrinter(IntPtr printer)
        {
            return PrinterHelper.EndPagePrinter(printer)
                   && PrinterHelper.EndDocPrinter(printer)
                   && PrinterHelper.ClosePrinter(printer);
        }
    }
}