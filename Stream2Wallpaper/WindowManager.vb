Imports System
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging

Public Class WindowManager
    ' Delegate used for enumerating windows.
    Private Delegate Function EnumWindowsProc(ByVal hWnd As IntPtr, ByVal lParam As IntPtr) As Boolean
    Private Shared workerw As IntPtr = IntPtr.Zero

    ' Import necessary Win32 functions.
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function EnumWindows(ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindWindowEx(ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr,
                                         ByVal lpszClass As String, ByVal lpszWindow As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SendMessageTimeout(hWnd As IntPtr, msg As UInteger,
                                           wParam As IntPtr, lParam As IntPtr,
                                           fuFlags As UInteger, uTimeout As UInteger,
                                           ByRef lpdwResult As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function PrintWindow(hWnd As IntPtr, hdcBlt As IntPtr, nFlags As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowRect(hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SystemParametersInfo(ByVal uAction As UInteger, ByVal uParam As UInteger, ByVal lpvParam As String, ByVal fuWinIni As UInteger) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Public Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    ' Constant for the message to spawn the WorkerW window.
    Private Const WM_SPAWN_WORKER As Integer = &H52C
    Private Const SPI_SETDESKWALLPAPER As UInteger = 20
    Private Const SPIF_UPDATEINIFILE As UInteger = 1
    Private Const SPIF_SENDWININICHANGE As UInteger = 2

    Private Shared workerwHandle As IntPtr = IntPtr.Zero

    ' Enumerate through top-level windows to find the one that hosts the desktop icons.
    Private Shared Function EnumWndProc(ByVal tophandle As IntPtr, ByVal lParam As IntPtr) As Boolean
        ' The desktop icons are contained within a window of class "SHELLDLL_DefView".
        Dim defViewHandle As IntPtr = FindWindowEx(tophandle, IntPtr.Zero, "SHELLDLL_DefView", Nothing)

        MsgBox(Hex(CInt(defViewHandle)))

        If defViewHandle <> IntPtr.Zero Then
            ' If found, get its sibling WorkerW window.
            workerwHandle = FindWindowEx(IntPtr.Zero, tophandle, "WorkerW", Nothing)
        End If
        Return True
    End Function

    ''' <summary>
    ''' Sets the specified window (by its handle) as a child of the WorkerW window.
    ''' This causes the window to appear behind the desktop icons.
    ''' </summary>
    ''' 
    Public Shared Sub MoveWindowBehindDesktopIcons(targetHandle As IntPtr)
        ' 1. Send WM_SPAWN_WORKER to Progman
        Dim progman = FindWindow("Progman", Nothing)
        Dim result As IntPtr
        SendMessageTimeout(progman, &H52C, IntPtr.Zero, IntPtr.Zero, 0, 1000, result)

        ' 2. Find the WorkerW window
        EnumWindows(Function(hWnd, lParam)
                        Dim shellView = FindWindowEx(hWnd, IntPtr.Zero, "SHELLDLL_DefView", Nothing)
                        If shellView <> IntPtr.Zero Then
                            workerw = FindWindowEx(IntPtr.Zero, hWnd, "WorkerW", Nothing)
                            Return False ' stop enumerating
                        End If
                        Return True
                    End Function, IntPtr.Zero)

        ' 3. Set parent of the target handle
        If workerw <> IntPtr.Zero Then
            SetParent(targetHandle, workerw)
        Else
            MessageBox.Show("WorkerW window not found.")
        End If
    End Sub

    Public Shared Sub set_static_background()
        Dim rect As RECT
        GetWindowRect(workerw, rect)
        Dim width As Integer = rect.Right - rect.Left
        Dim height As Integer = rect.Bottom - rect.Top

        Dim bmp As New Bitmap(width, height, PixelFormat.Format32bppArgb)
        Using gfxBmp As Graphics = Graphics.FromImage(bmp)
            Dim hdcBitmap As IntPtr = gfxBmp.GetHdc()
            PrintWindow(workerw, hdcBitmap, 0)
            gfxBmp.ReleaseHdc(hdcBitmap)
        End Using

        bmp.Save(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\static-image.jpg", ImageFormat.Jpeg)

        'Threading.Thread.Sleep(1000)

        'Set static desktop background
        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\static-image.jpg", SPIF_UPDATEINIFILE Or SPIF_SENDWININICHANGE)

    End Sub

End Class
