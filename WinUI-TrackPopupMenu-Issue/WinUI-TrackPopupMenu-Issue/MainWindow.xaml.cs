// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI_TrackPopupMenu_Issue
{
    using System;
    using Microsoft.UI.Xaml;
    using WinRT.Interop;
    using static Vanara.PInvoke.User32;

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            var hwnd = WindowNative.GetWindowHandle(this);

            if (!SetForegroundWindow(hwnd))
                return;

            var menu = CreatePopupMenu();
            AppendMenu(menu, MenuFlags.MF_STRING, (IntPtr)1111, "Open");
            GetCursorPos(out var point);
            TrackPopupMenu(menu, TrackPopupMenuFlags.TPM_RIGHTALIGN, point.X, point.Y, 0, hwnd, null);
            PostMessage(hwnd, (uint)WindowMessage.WM_NULL, IntPtr.Zero, IntPtr.Zero);
            DestroyMenu(menu);
        }
    }
}
