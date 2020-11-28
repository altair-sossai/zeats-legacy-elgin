using System;
using System.Text;

namespace Zeats.Legacy.Elgin.Helpers
{
    public static class ElginHelper
    {
        public static bool PrintText(IntPtr printer, string data)
        {
            var brazilian = Encoding.GetEncoding("IBM860");
            var byteData = brazilian.GetBytes(data);

            return RawPrinterHelper.WritePrinter(printer, byteData);
        }

        public static bool NormalModeText(IntPtr printer)
        {
            byte[] normal = {27, 33, 0};

            return RawPrinterHelper.WritePrinter(printer, normal);
        }

        public static bool CharFontBText(IntPtr printer)
        {
            byte[] fontB = {27, 33, 1};

            return RawPrinterHelper.WritePrinter(printer, fontB);
        }

        public static bool CharFontCText(IntPtr printer)
        {
            byte[] fontB = {27, 33, 2};

            return RawPrinterHelper.WritePrinter(printer, fontB);
        }

        public static bool EmphasizedModeText(IntPtr printer)
        {
            byte[] mode = {27, 33, 8};

            return RawPrinterHelper.WritePrinter(printer, mode);
        }

        public static bool DoubleHeightText(IntPtr printer)
        {
            byte[] height = {27, 33, 16};

            return RawPrinterHelper.WritePrinter(printer, height);
        }

        public static bool DoubleWidthText(IntPtr printer)
        {
            byte[] width = {27, 33, 32};

            return RawPrinterHelper.WritePrinter(printer, width);
        }

        public static bool UnderlineModeText(IntPtr printer)
        {
            byte[] underline = {27, 33, 128};

            return RawPrinterHelper.WritePrinter(printer, underline);
        }

        public static bool LineFeed(IntPtr printer, int numLines)
        {
            var success = true;

            byte[] lf = {10};

            for (var i = 1; i <= numLines; i++)
                success = success && RawPrinterHelper.WritePrinter(printer, lf);

            return success;
        }

        public static bool CutPaper(IntPtr printer)
        {
            byte[] cut = {29, 86, 0};

            return RawPrinterHelper.WritePrinter(printer, cut);
        }

        public static bool BarcodeHeight(IntPtr printer, byte range = 162)
        {
            byte[] height = {29, 104, range};

            return RawPrinterHelper.WritePrinter(printer, height);
        }

        public static bool BarcodeWidth(IntPtr printer, byte range = 3)
        {
            byte[] width = {29, 119, range};

            return RawPrinterHelper.WritePrinter(printer, width);
        }

        public static bool BarcodeHriChars(IntPtr printer, byte fontCode = 0)
        {
            byte[] hri = {29, 102, fontCode};

            return RawPrinterHelper.WritePrinter(printer, hri);
        }

        public static bool BarcodeHriPostion(IntPtr printer, byte positionCode = 1)
        {
            byte[] printPosition = {29, 72, positionCode};

            return RawPrinterHelper.WritePrinter(printer, printPosition);
        }

        public static bool PrintBarcode(IntPtr printer, string data, byte type = 2)
        {
            byte[] barcode = {29, 107, type};

            var success = RawPrinterHelper.WritePrinter(printer, barcode);
            success = success && RawPrinterHelper.WritePrinter(printer, data);

            byte[] nul = {0};
            success = success && RawPrinterHelper.WritePrinter(printer, nul);

            return success;
        }
    }
}