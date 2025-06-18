<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetURL
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetURL))
        Me.btnApply = New System.Windows.Forms.Button()
        Me.TextBoxURL = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.CheckBoxUseCookies = New System.Windows.Forms.CheckBox()
        Me.txtCookiesLocation = New System.Windows.Forms.TextBox()
        Me.btnBrowseCookies = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(351, 42)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(89, 23)
        Me.btnApply.TabIndex = 0
        Me.btnApply.Text = "Apply && Play"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'TextBoxURL
        '
        Me.TextBoxURL.Location = New System.Drawing.Point(26, 44)
        Me.TextBoxURL.Name = "TextBoxURL"
        Me.TextBoxURL.Size = New System.Drawing.Size(315, 20)
        Me.TextBoxURL.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(387, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Enter the YouTube URL to the field below e.g. https://youtube.com/watch?v=..."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Favorites"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(26, 95)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(315, 134)
        Me.ListBox1.TabIndex = 4
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(351, 95)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(89, 23)
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(351, 124)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(89, 23)
        Me.btnRemove.TabIndex = 6
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(351, 153)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(89, 23)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'CheckBoxUseCookies
        '
        Me.CheckBoxUseCookies.AutoSize = True
        Me.CheckBoxUseCookies.Location = New System.Drawing.Point(26, 248)
        Me.CheckBoxUseCookies.Name = "CheckBoxUseCookies"
        Me.CheckBoxUseCookies.Size = New System.Drawing.Size(103, 17)
        Me.CheckBoxUseCookies.TabIndex = 8
        Me.CheckBoxUseCookies.Text = "Use Cookie File:"
        Me.CheckBoxUseCookies.UseVisualStyleBackColor = True
        '
        'txtCookiesLocation
        '
        Me.txtCookiesLocation.Enabled = False
        Me.txtCookiesLocation.Location = New System.Drawing.Point(135, 246)
        Me.txtCookiesLocation.Name = "txtCookiesLocation"
        Me.txtCookiesLocation.Size = New System.Drawing.Size(206, 20)
        Me.txtCookiesLocation.TabIndex = 9
        '
        'btnBrowseCookies
        '
        Me.btnBrowseCookies.Enabled = False
        Me.btnBrowseCookies.Location = New System.Drawing.Point(351, 244)
        Me.btnBrowseCookies.Name = "btnBrowseCookies"
        Me.btnBrowseCookies.Size = New System.Drawing.Size(89, 23)
        Me.btnBrowseCookies.TabIndex = 10
        Me.btnBrowseCookies.Text = "Browse..."
        Me.btnBrowseCookies.UseVisualStyleBackColor = True
        '
        'frmSetURL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(457, 283)
        Me.Controls.Add(Me.btnBrowseCookies)
        Me.Controls.Add(Me.txtCookiesLocation)
        Me.Controls.Add(Me.CheckBoxUseCookies)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxURL)
        Me.Controls.Add(Me.btnApply)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetURL"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Set YouTube Live Stream URL..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnApply As Button
    Friend WithEvents TextBoxURL As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents CheckBoxUseCookies As System.Windows.Forms.CheckBox
    Friend WithEvents txtCookiesLocation As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseCookies As System.Windows.Forms.Button
End Class
