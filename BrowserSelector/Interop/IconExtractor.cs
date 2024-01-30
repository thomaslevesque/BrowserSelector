using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BrowserSelector.Interop;

public static class IconExtractor
{
    public static ImageSource? GetIcon(string path)
    {
        var sb = new StringBuilder(path, 260);
        ushort iIcon = 0;

        var hIcon = ExtractAssociatedIcon(IntPtr.Zero, sb, ref iIcon);
        if (hIcon != IntPtr.Zero)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                hIcon,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        return null;

        [DllImport("shell32.dll")]
        static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, StringBuilder iconPath, ref ushort iIcon);
    }
}
