Imports System.Net
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Public Class frmTrayLaunch

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindWindowEx(parentHandle As IntPtr, childAfter As IntPtr, lpszClass As String, lpszWindow As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function SetWindowPos(
        ByVal hWnd As IntPtr,
        ByVal hWndInsertAfter As IntPtr,
        ByVal X As Integer,
        ByVal Y As Integer,
        ByVal cx As Integer,
        ByVal cy As Integer,
        ByVal uFlags As UInteger) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function MoveWindow(hWnd As IntPtr,
                                       X As Integer, Y As Integer,
                                       nWidth As Integer, nHeight As Integer,
                                       bRepaint As Boolean) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
    End Function

    ' Constants to control behavior.
    Public Const SWP_NOSIZE As UInteger = &H1    ' Do not change size.
    Public Const SWP_NOMOVE As UInteger = &H2     ' Do not change position.
    Public Const SWP_NOACTIVATE As UInteger = &H10 ' Do not activate window.
    Private Const GWL_EXSTYLE As Integer = -20
    Private Const WS_EX_TOOLWINDOW As Integer = &H80
    Private Const WS_EX_APPWINDOW As Integer = &H40000
    Private Const SWP_NOZORDER As Integer = &H4
    Private Const SWP_FRAMECHANGED As Integer = &H20

    'Tray menu options for this time:
    '- Set livestream URL... (with favorites list and cookies from browser option)
    '- Play/Pause stream
    '- Skip ahead to live broadcast
    '- Adjust audio volume
    '- Auto-launch on windows startup (checkbox)
    '- Exit

    Public played As Boolean = False

    Private Sub frmTrayLaunch_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If played = True Then
            WindowManager.set_static_background()
            For Each pff As Process In Process.GetProcessesByName("ffplay")
                pff.Kill()
            Next
        End If
    End Sub

    Function UrlExists(destination As String) As Boolean
        Try
            Dim objResponse As HttpWebResponse
            Dim objRequest As HttpWebRequest
            objRequest = HttpWebRequest.Create(destination)
            objRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.115 Safari/537.36"
            objResponse = objRequest.GetResponse()
            Return True
        Catch ex As WebException
            Return False
        End Try
    End Function

    Private Sub frmTrayLaunch_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Me.ShowInTaskbar = False
        Me.Hide()

        If IO.File.Exists(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\yt-dlp.exe") AndAlso IO.File.Exists(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\ffplay.exe") Then

            Dim value = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Stream2Wallpaper", Nothing)

            If value IsNot Nothing Then
                AutolaunchOnWindowsStartupcheckboxToolStripMenuItem.Checked = True
            End If


            'check if link is expired

            If My.Settings.autostartUrl.StartsWith("https://manifest.googlevideo.com") Then

                Dim streamUrl As String = Nothing
                For Each line As String In My.Settings.autostartUrl.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
                    streamUrl = line.Trim()
                Next

                For Each pff As Process In Process.GetProcessesByName("ffplay")
                    pff.Kill()
                Next

                If UrlExists(streamUrl) Then

                    Dim ffInfo As New ProcessStartInfo()
                    ffInfo.FileName = IO.Path.GetDirectoryName(Application.ExecutablePath) & "\ffplay.exe"
                    ffInfo.Arguments = "-i """ & streamUrl & """ -x " & Screen.PrimaryScreen.Bounds.Width & " -y " & Screen.PrimaryScreen.Bounds.Height & " -an -noborder -loglevel quiet"
                    ffInfo.UseShellExecute = False
                    ffInfo.CreateNoWindow = True

                    Dim ffProc = Process.Start(ffInfo)

i:                  If ffProc.MainWindowHandle = IntPtr.Zero Then GoTo i

                    WindowManager.MoveWindowBehindDesktopIcons(ffProc.MainWindowHandle)

                    played = True
                    NotifyIcon1.Text = "Stream2Wallpaper - Playing"
                    WindowManager.set_static_background()
                    Timer1.Start()
                    PlayPauseStreamToolStripMenuItem.Text = "Pause stream"

                End If

            End If

            Else
            MessageBox.Show(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\yt-dlp and ffplay are required to be put together in the same folder with this executable. If those files are missing, you can search them online and download them.", "Failed to launch Stream2Wallpaper", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Process.GetCurrentProcess.Kill()
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub SetLivestreamURLToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SetLivestreamURLToolStripMenuItem.Click
        frmSetURL.Show()
    End Sub

    Private Sub PlayPauseStreamToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PlayPauseStreamToolStripMenuItem.Click
        If played = True Then

            Timer1.Stop()
            WindowManager.set_static_background()

            For Each pff As Process In Process.GetProcessesByName("ffplay")
                pff.Kill()
            Next
            PlayPauseStreamToolStripMenuItem.Text = "Play stream"
            NotifyIcon1.Text = "Stream2Wallpaper - Stopped"
            played = False
        Else
            If My.Settings.autostartUrl.StartsWith("https://manifest.googlevideo.com") Then
                Dim streamUrl As String = Nothing
                For Each line As String In My.Settings.autostartUrl.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
                    streamUrl = line.Trim()
                Next
                Dim ffInfo As New ProcessStartInfo()
                ffInfo.FileName = IO.Path.GetDirectoryName(Application.ExecutablePath) & "\ffplay.exe"
                ffInfo.Arguments = "-i """ & streamUrl & """ -x " & Screen.PrimaryScreen.Bounds.Width & " -y " & Screen.PrimaryScreen.Bounds.Height & " -an -noborder -loglevel quiet"
                ffInfo.UseShellExecute = False
                ffInfo.CreateNoWindow = True

                Dim ffProc = Process.Start(ffInfo)


i:
                If ffProc.MainWindowHandle = IntPtr.Zero Then GoTo i

                WindowManager.MoveWindowBehindDesktopIcons(ffProc.MainWindowHandle)

                NotifyIcon1.Text = "Stream2Wallpaper - Playing"

                PlayPauseStreamToolStripMenuItem.Text = "Pause stream"

                WindowManager.set_static_background()
                Timer1.Start()

                played = True
            Else
                frmSetURL.Show()
            End If
        End If
    End Sub

    Private Sub AutolaunchOnWindowsStartupcheckboxToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AutolaunchOnWindowsStartupcheckboxToolStripMenuItem.Click
        If AutolaunchOnWindowsStartupcheckboxToolStripMenuItem.Checked = True Then
            Using key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", writable:=True)
                key.DeleteValue("Stream2Wallpaper")
            End Using
            AutolaunchOnWindowsStartupcheckboxToolStripMenuItem.Checked = False
        Else
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Stream2Wallpaper", Application.ExecutablePath)
            AutolaunchOnWindowsStartupcheckboxToolStripMenuItem.Checked = True
        End If
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        WindowManager.set_static_background()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        frmSetURL.Show()
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        ContextMenuStrip1.Show()
        ContextMenuStrip1.Location = Cursor.Position
    End Sub
End Class