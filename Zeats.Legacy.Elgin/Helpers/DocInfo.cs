using System.Runtime.InteropServices;

namespace Zeats.Legacy.Elgin.Helpers
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class DocInfo
    {
        // ReSharper disable All

        [MarshalAs(UnmanagedType.LPStr)]
        public string pDocName;

        [MarshalAs(UnmanagedType.LPStr)]
        public string pOutputFile;

        [MarshalAs(UnmanagedType.LPStr)]
        public string pDataType;
    }
}