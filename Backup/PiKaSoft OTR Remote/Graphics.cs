using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Crazysoft.OTR_Remote
{
    class Graphics
    {
        #region DllImports for Visual Style Detection
        [DllImport("UxTheme.dll", CharSet = CharSet.Unicode)]
        public static extern int GetCurrentThemeName(StringBuilder
            pszThemeFileName, int dwMaxNameChars,
            StringBuilder pszColorBuff, int cchMaxColorChars,
            StringBuilder pszSizeBuff, int cchMaxSizeChars);

        [DllImport("UxTheme.dll")]
        public static extern bool IsAppThemed();
        #endregion

        #region DllImports and declarations for Vista Aero Glass
        [DllImport("dwmapi.dll")]
        private static extern void DwmIsCompositionEnabled(ref bool pfEnabled);
        [DllImport("dwmapi.dll")]
        private static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMargins);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        [DllImport("UxTheme.dll", CharSet = CharSet.Unicode)]
        private static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DTTOPTS pOptions);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, BITMAPINFO pbmi, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);

        [StructLayout(LayoutKind.Sequential)]
        private struct DTTOPTS
        {
            public int dwSize;
            public int dwFlags;
            public int crText;
            public int crBorder;
            public int crShadow;
            public int iTextShadowType;
            public POINT ptShadowOffset;
            public int iBorderSize;
            public int iFontPropId;
            public int iColorPropId;
            public int iStateId;
            public bool fApplyOverlay;
            public int iGlowSize;
            public int pfnDrawTextCallback;
            public IntPtr lParam;
        }

        private const int DTT_COMPOSITED = 8192;
        private const int DTT_GLOWSIZE = 2048;
        private const int DTT_TEXTCOLOR = 1;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class BITMAPINFO
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MARGINS
        {
            public int left, right, top, bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(Rectangle rectangle)
            {
                Left = rectangle.X;
                Top = rectangle.Y;
                Right = rectangle.Right;
                Bottom = rectangle.Bottom;
            }

            public Rectangle ToRectangle()
            {
                return new Rectangle(Left, Top, Right - Left, Bottom - Top);
            }

            public override string ToString()
            {
                return "Left: " + Left + ", " + "Top: " + Top + ", Right: " + Right + ", Bottom: " + Bottom;
            }
        }
        #endregion

        public static void DrawGraphicalHeader(PaintEventArgs e, Rectangle rect1, Rectangle rect2)
        {
            return;
        }

        public static void DrawGraphicalHeader(PaintEventArgs e, Form form, Panel panel)
        {
            // If Vista Aero Glass is not available, draw colored window header
            if (!IsCompositionEnabled)
            {
                Rectangle rect1 = new Rectangle(panel.Left, panel.Top, panel.Width, panel.Height);
                Rectangle rect2 = new Rectangle(panel.Left, panel.Top + panel.Height, panel.Width, 10);

                Color headerColor1;
                Color headerColor2;
                Color borderColor1 = Color.FromArgb(255, 204, 51);
                Color borderColor2 = Color.FromArgb(255, 102, 51);

                if (Environment.OSVersion.Version.Major >= 5)
                {
                    // Get current visual style scheme
                    StringBuilder sb1 = new StringBuilder(256);
                    StringBuilder sb2 = new StringBuilder(256);
                    GetCurrentThemeName(sb1, sb1.Capacity, sb2, sb2.Capacity, null, 0);
                    string styleFile = sb1.ToString();
                    string styleName = sb2.ToString();

                    if (styleFile.ToLower().EndsWith("luna.msstyles")) // Windows XP
                    {
                        switch (styleName.ToLower())
                        {
                            case "homestead":
                                // OliveGreen color scheme
                                headerColor1 = Color.FromArgb(194, 205, 149);
                                headerColor2 = Color.FromArgb(112, 129, 72);
                                break;
                            case "metallic":
                                // Silver color scheme
                                headerColor1 = Color.FromArgb(251, 252, 252);
                                headerColor2 = Color.FromArgb(107, 117, 143);
                                break;
                            default:
                                // Blue color scheme
                                headerColor1 = Color.FromArgb(3, 114, 255);
                                headerColor2 = Color.FromArgb(0, 59, 190);
                                break;
                        }
                    }
                    else if (styleFile.ToLower().EndsWith("royale.msstyles")) // Windows Media Center
                    {
                        headerColor1 = Color.FromArgb(144, 195, 230);
                        headerColor2 = Color.FromArgb(34, 69, 149);
                    }
                    else if (styleFile.ToLower().EndsWith("aero.msstyles")) // Windows Vista
                    {
                        headerColor1 = Color.FromArgb(65, 111, 166);
                        headerColor2 = Color.FromArgb(103, 177, 115);
                    }
                    else
                    {
                        headerColor1 = Color.FromArgb(212, 208, 200);
                        headerColor2 = Color.FromArgb(128, 128, 128);
                    }
                }
                else
                {
                    headerColor1 = Color.FromArgb(212, 208, 200);
                    headerColor2 = Color.FromArgb(128, 128, 128);
                }

                System.Drawing.Graphics g = e.Graphics;
                LinearGradientBrush brush;

                // Fill the form header with a gradient
                brush = new LinearGradientBrush(rect1, headerColor1, headerColor2, LinearGradientMode.ForwardDiagonal);
                g.FillRectangle(brush, rect1);

                // Draw a nice border underneath
                brush = new LinearGradientBrush(rect2, borderColor1, borderColor2, LinearGradientMode.Vertical);
                g.FillRectangle(brush, rect2);
            }
            else
            {
                Rectangle rect = new Rectangle(form.ClientRectangle.Left, form.ClientRectangle.Top, form.ClientRectangle.Width, panel.Height);
                panel.Hide();

                string text = "";
                if (panel.Controls.Count == 0)
                {
                    if (form.Controls["lblHeader"] != null)
                    {
                        text = form.Controls["lblHeader"].Text;
                    }
                    else
                    {
                        text = Lang.OTRRemote.ResourceManager.GetString(form.Name + "_lblHeader");
                    }
                }
                else
                {
                    text = panel.Controls["lblHeader"].Text;
                }
                rect.Height += 10;
                DrawGlassHeader(form, e, rect, 0, 0, 0, panel.Height + 10, text, true);
            }
        }

        public static void DrawGlassHeader(Form form, PaintEventArgs e, Rectangle headerRect, int left, int right, int top, int height, string text, bool showImage)
        {
            // Fill the rectangle with black to indicate, where glass should be
            e.Graphics.FillRectangle(Brushes.Black, headerRect);

            // Extend glass border to given rectangle
            ExtendGlassIntoClientArea(form, left, height, right, top);

            // Draw glowing text
            TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter |
                                    TextFormatFlags.HorizontalCenter;
            Font font = new Font("Segoe UI", 18);

            // Calculate, if the image can be shown and if the text can be centered
            if (showImage)
            {
                int halfPanel = headerRect.Width / 2;
                int halfLabel = Convert.ToInt32(e.Graphics.MeasureString(text, font).Width / 2);
                int cameraSize = Resources.Camera.Width + 14;
                if (halfPanel - halfLabel < cameraSize)
                {
                    if (halfPanel * 2 - halfLabel * 2 - 14 < cameraSize)
                    {
                        showImage = false;
                    }
                    else
                    {
                        flags = TextFormatFlags.SingleLine | TextFormatFlags.Right |
                                TextFormatFlags.VerticalCenter;
                        headerRect.Width -= 14;
                    }
                }
            }

            // Also if no text is given, this function has to be called
            // Otherwise, the glass area will be plain black
            // TODO: Find out, why the glass area is black when no glowing text is drawn
            DrawGlowingText(e.Graphics, text, font, headerRect, Color.Black, flags);

            if (showImage)
            {
                // Put a nice icon in the header for a more interesting interface
                e.Graphics.DrawImage(Resources.Camera, 14, headerRect.Height / 2 - Resources.Camera.Height / 2,
                                     Resources.Camera.Width, Resources.Camera.Height);
            }
        }

        public static bool IsCompositionEnabled
        {
            get
            {
                if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Version.Major < 6)
                    return false;

                bool compositionEnabled = false;
                DwmIsCompositionEnabled(ref compositionEnabled);
                return compositionEnabled;
            }
        }

        public static void ExtendGlassIntoClientArea(Form form, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            MARGINS m = new MARGINS();
            m.left = leftMargin;
            m.right = rightMargin;
            m.top = topMargin;
            m.bottom = bottomMargin;

            DwmExtendFrameIntoClientArea(form.Handle, ref m);
        }

        public static void DrawGlowingText(System.Drawing.Graphics graphics, string text, Font font, Rectangle bounds, Color color, TextFormatFlags flags)
        {
            IntPtr primaryHdc = graphics.GetHdc();

            // Create a memory DC so we can work offscreen
            IntPtr memoryHdc = CreateCompatibleDC(primaryHdc);

            // Create a device-independent bitmap and select it into our DC
            BITMAPINFO info = new BITMAPINFO();
            info.biSize = Marshal.SizeOf(info);
            info.biWidth = bounds.Width;
            info.biHeight = -bounds.Height;
            info.biPlanes = 1;
            info.biBitCount = 32;
            info.biCompression = 0; // BI_RGB
            IntPtr dib = CreateDIBSection(primaryHdc, info, 0, 0, IntPtr.Zero, 0);
            SelectObject(memoryHdc, dib);

            // Create and select font
            IntPtr fontHandle = font.ToHfont();
            SelectObject(memoryHdc, fontHandle);

            // Draw glowing text
            System.Windows.Forms.VisualStyles.VisualStyleRenderer renderer = new System.Windows.Forms.VisualStyles.VisualStyleRenderer(System.Windows.Forms.VisualStyles.VisualStyleElement.Window.Caption.Active);
            DTTOPTS dttOpts = new DTTOPTS();
            dttOpts.dwSize = Marshal.SizeOf(typeof(DTTOPTS));
            dttOpts.dwFlags = DTT_COMPOSITED | DTT_GLOWSIZE | DTT_TEXTCOLOR;
            dttOpts.crText = ColorTranslator.ToWin32(color);
            dttOpts.iGlowSize = 15; // This is about the size Microsoft Word 2007 uses
            RECT textBounds = new RECT(0, 0, bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
            DrawThemeTextEx(renderer.Handle, memoryHdc, 0, 0, text, -1, (int)flags, ref textBounds, ref dttOpts);

            // Copy to foreground
            const int SRCCOPY = 0x00CC0020;
            BitBlt(primaryHdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, memoryHdc, 0, 0, SRCCOPY);

            // Clean up
            DeleteObject(fontHandle);
            DeleteObject(dib);
            DeleteDC(memoryHdc);

            graphics.ReleaseHdc(primaryHdc);
        }
    }
}
