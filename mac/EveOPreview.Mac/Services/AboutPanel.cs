using System;
using System.Runtime.InteropServices;

namespace EveOPreview.Mac.Services;

// Shows the standard macOS About panel, which reads the app name, icon and
// version from the bundle's Info.plist.
internal static class AboutPanel
{
    public static void TryShow()
    {
        if (!OperatingSystem.IsMacOS())
        {
            return;
        }

        var applicationClass = objc_getClass("NSApplication");
        if (applicationClass == IntPtr.Zero)
        {
            return;
        }

        var application = objc_msgSend_IntPtr(applicationClass, sel_registerName("sharedApplication"));
        if (application == IntPtr.Zero)
        {
            return;
        }

        objc_msgSend_Void_Bool(application, sel_registerName("activateIgnoringOtherApps:"), true);
        objc_msgSend_Void_IntPtr(application, sel_registerName("orderFrontStandardAboutPanel:"), IntPtr.Zero);
    }

    [DllImport("/usr/lib/libobjc.A.dylib")]
    private static extern IntPtr objc_getClass(string name);

    [DllImport("/usr/lib/libobjc.A.dylib")]
    private static extern IntPtr sel_registerName(string name);

    [DllImport("/usr/lib/libobjc.A.dylib", EntryPoint = "objc_msgSend")]
    private static extern IntPtr objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector);

    [DllImport("/usr/lib/libobjc.A.dylib", EntryPoint = "objc_msgSend")]
    private static extern void objc_msgSend_Void_Bool(IntPtr receiver, IntPtr selector, [MarshalAs(UnmanagedType.I1)] bool arg);

    [DllImport("/usr/lib/libobjc.A.dylib", EntryPoint = "objc_msgSend")]
    private static extern void objc_msgSend_Void_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg);
}
