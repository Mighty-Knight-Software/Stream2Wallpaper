Imports System.Runtime.InteropServices
Imports Stream2Wallpaper.WindowManager
Imports System.Threading

Public Class frmSetURL

    'Image positioning modes:
    '- Center
    '- Tile
    '- Stretch
    '- Collage (only for slideshows/video playlist)
    '- Fit aspect ratio (blur bar colors)
    '- Fill (zoom aspect ratio)

    'Source image (all videos have been muted):
    '- Animated photo slideshow
    '- Video playlist
    '- YouTube Live Stream URL
    '- Video Capture Device (e.g. Camera, DVR)
    '- Applications (Autostart not supported, Status filter: Windowed/Borderless, Full Screen, Full Screen Mismatched Resolution)

    'Transition mode:
    '- Fade (between frames or from/to black)
    '- Swipe (Horizontal or vertical)
    '- Dissolve
    '- Round

    'Pause mode:
    '- Immediate
    '- Slowmo/speedup (with adjustable speed curve)

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

    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
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

    'Dim youtubeurl = "https://www.youtube.com/watch?v=aLU9aHMeunM"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnApply.Click

        If TextBoxURL.Text.ToLowerInvariant.StartsWith("https://youtube.com/watch?v=") OrElse TextBoxURL.Text.ToLowerInvariant.StartsWith("https://www.youtube.com/watch?v=") Then

            My.Settings.lastYoutubeUrl = TextBoxURL.Text

        For Each pff As Process In Process.GetProcessesByName("ffplay")
            pff.Kill()
        Next

        Dim ytdlInfo As New ProcessStartInfo()
            ytdlInfo.FileName = IO.Path.GetDirectoryName(Application.ExecutablePath) & "\yt-dlp.exe"
            If CheckBoxUseCookies.Checked = True Then
                If IO.File.Exists(txtCookiesLocation.Text) Then
                    ytdlInfo.Arguments = "--cookies """ & txtCookiesLocation.Text & """ -g " & TextBoxURL.Text
                Else
                    MsgBox("Cookie file not specified or missing!")
                    Exit Sub
                End If
            Else
                ytdlInfo.Arguments = "-g " & TextBoxURL.Text
            End If
        ytdlInfo.UseShellExecute = False
        ytdlInfo.RedirectStandardOutput = True
        ytdlInfo.CreateNoWindow = True

        Dim yProc As Process = Process.Start(ytdlInfo)
        Dim rawOutput As String = yProc.StandardOutput.ReadToEnd()
            yProc.WaitForExit()

            'MsgBox(rawOutput)

        Dim streamUrl As String = Nothing
        For Each line As String In rawOutput.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
            streamUrl = line.Trim()
        Next

        Dim ffInfo As New ProcessStartInfo()
            ffInfo.FileName = IO.Path.GetDirectoryName(Application.ExecutablePath) & "\ffplay.exe"
            ffInfo.Arguments = "-i """ & streamUrl & """ -x " & Screen.PrimaryScreen.Bounds.Width & " -y " & Screen.PrimaryScreen.Bounds.Height & " -an -noborder -loglevel quiet"
        ffInfo.UseShellExecute = False
        ffInfo.CreateNoWindow = True

        Dim ffProc = Process.Start(ffInfo)


i:
            'Threading.Thread.Sleep(500)
        If ffProc.MainWindowHandle = IntPtr.Zero Then GoTo i

            WindowManager.MoveWindowBehindDesktopIcons(ffProc.MainWindowHandle)
            'Threading.Thread.Sleep(500)

            'MoveWindow(ffProc.MainWindowHandle,
            '           0, 0,
            '           Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,
            '           True)
            'Threading.Thread.Sleep(500)

            'Dim exStyle As Integer = GetWindowLong(ffProc.MainWindowHandle, GWL_EXSTYLE)
            'exStyle = (exStyle Or WS_EX_TOOLWINDOW) And Not WS_EX_APPWINDOW
            'SetWindowLong(ffProc.MainWindowHandle, GWL_EXSTYLE, exStyle)
            'SetWindowPos(ffProc.MainWindowHandle, IntPtr.Zero, 0, 0, 0, 0,
            '         SWP_NOMOVE Or SWP_NOSIZE Or SWP_NOZORDER Or SWP_FRAMECHANGED)

            My.Settings.autostartUrl = streamUrl

            WindowManager.MoveWindowBehindDesktopIcons(ffProc.MainWindowHandle)

            frmTrayLaunch.played = True
            frmTrayLaunch.NotifyIcon1.Text = "Stream2Wallpaper - Playing"
            WindowManager.set_static_background()
            frmTrayLaunch.Timer1.Start()
            frmTrayLaunch.PlayPauseStreamToolStripMenuItem.Text = "Pause stream"

        Else
            MsgBox("Please specify a valid YouTube live stream URL.")
        End If
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        If TextBoxURL.Text.ToLowerInvariant.StartsWith("https://youtube.com/watch?v=") OrElse TextBoxURL.Text.ToLowerInvariant.StartsWith("https://www.youtube.com/watch?v=") Then
            If Not ListBox1.Items.Contains(TextBoxURL.Text) Then
                ListBox1.Items.Add(TextBoxURL.Text)
                My.Settings.favorites.Add(TextBoxURL.Text)
            Else
                MsgBox("URL already exists.")
            End If
        Else
            MsgBox("Please specify a valid YouTube live stream URL.")
        End If
    End Sub

    Private Sub frmSetURL_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            Dim exStyle As Integer = GetWindowLong(&HE0CD2, GWL_EXSTYLE)
            exStyle = (exStyle Or WS_EX_TOOLWINDOW) And Not WS_EX_APPWINDOW
            SetWindowLong(&HE0CD2, GWL_EXSTYLE, exStyle)
            SetWindowPos(&HE0CD2, IntPtr.Zero, 0, 0, 0, 0,
                     SWP_NOMOVE Or SWP_NOSIZE Or SWP_NOZORDER Or SWP_FRAMECHANGED)

            If (My.Settings.favorites Is Nothing) Then
                My.Settings.favorites = New System.Collections.Specialized.StringCollection() ' you probably won't need to fully qualify this, but I have it for visibility
            End If
            CheckBoxUseCookies.Checked = My.Settings.UseCookies
            txtCookiesLocation.Enabled = CheckBoxUseCookies.Checked
            btnBrowseCookies.Enabled = CheckBoxUseCookies.Checked
            txtCookiesLocation.Text = My.Settings.lastCookieFile
            TextBoxURL.Text = My.Settings.lastYoutubeUrl
            For Each link In My.Settings.favorites
                ListBox1.Items.Add(link)
            Next
    End Sub

    Private Sub btnBrowseCookies_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseCookies.Click
        With New OpenFileDialog
            .Title = "Select Cookie File..."
            .Filter = "Cookie Text File (*.txt)|*.txt|All Files|*.*"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                txtCookiesLocation.Text = .FileName
                My.Settings.lastCookieFile = txtCookiesLocation.Text
            End If
        End With
    End Sub

    Private Sub CheckBoxUseCookies_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxUseCookies.CheckedChanged
        My.Settings.UseCookies = CheckBoxUseCookies.Checked
        txtCookiesLocation.Enabled = CheckBoxUseCookies.Checked
        btnBrowseCookies.Enabled = CheckBoxUseCookies.Checked
    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click
        For Each link In ListBox1.SelectedIndices
            My.Settings.favorites.Remove(ListBox1.Items(link))
            ListBox1.Items.RemoveAt(link)
        Next
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        If MessageBox.Show("Are you sure?", "Stream2Wallpaper", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
            My.Settings.favorites.Clear()
            ListBox1.Items.Clear()
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        TextBoxURL.Text = ListBox1.SelectedItem
    End Sub
End Class
