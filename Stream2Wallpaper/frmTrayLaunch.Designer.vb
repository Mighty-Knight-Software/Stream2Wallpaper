﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTrayLaunch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTrayLaunch))
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SetLivestreamURLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlayPauseStreamToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutolaunchOnWindowsStartupcheckboxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Stream2Wallpaper - Stream videos to the desktop background"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetLivestreamURLToolStripMenuItem, Me.PlayPauseStreamToolStripMenuItem, Me.AutolaunchOnWindowsStartupcheckboxToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(249, 92)
        '
        'SetLivestreamURLToolStripMenuItem
        '
        Me.SetLivestreamURLToolStripMenuItem.Name = "SetLivestreamURLToolStripMenuItem"
        Me.SetLivestreamURLToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.SetLivestreamURLToolStripMenuItem.Text = "Set livestream URL..."
        '
        'PlayPauseStreamToolStripMenuItem
        '
        Me.PlayPauseStreamToolStripMenuItem.Name = "PlayPauseStreamToolStripMenuItem"
        Me.PlayPauseStreamToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.PlayPauseStreamToolStripMenuItem.Text = "Play stream"
        '
        'AutolaunchOnWindowsStartupcheckboxToolStripMenuItem
        '
        Me.AutolaunchOnWindowsStartupcheckboxToolStripMenuItem.Name = "AutolaunchOnWindowsStartupcheckboxToolStripMenuItem"
        Me.AutolaunchOnWindowsStartupcheckboxToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.AutolaunchOnWindowsStartupcheckboxToolStripMenuItem.Text = "Auto-launch on windows startup"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'Timer1
        '
        Me.Timer1.Interval = 600000
        '
        'frmTrayLaunch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(100, 100)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTrayLaunch"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "frmTrayLaunch"
        Me.TransparencyKey = System.Drawing.SystemColors.Control
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SetLivestreamURLToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PlayPauseStreamToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutolaunchOnWindowsStartupcheckboxToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
