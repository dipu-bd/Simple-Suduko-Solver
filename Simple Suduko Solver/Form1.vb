Public Class Form1

    Sub columnRowCheck()
        For i = 0 To 8
            Dim carry() As String = {"", "", "", "", "", "", "", "", ""}
            For j = 0 To 8
                Dim ctl As Cells = SudokuPanel.Controls("Cell" & i & j)
                Dim p As String = ctl.TextBox1.Text
                'pre check for special check
                For k = 0 To p.Length - 1
                    carry(CInt(p(k).ToString()) - 1) &= i & j
                Next
                'general check
                If Not p.Length = 1 Then Continue For
                For a = 0 To 8
                    'row
                    Dim ctl1 As Cells = SudokuPanel.Controls("Cell" & i & a)
                    Dim q1 As String = ctl1.TextBox1.Text
                    If Not q1.Length = 1 Then ctl1.TextBox1.Text = q1.Replace(p, "")
                    'column
                    Dim ctl2 As Cells = SudokuPanel.Controls("Cell" & a & j)
                    Dim q2 As String = ctl2.TextBox1.Text
                    If Not q2.Length = 1 Then ctl2.TextBox1.Text = q2.Replace(p, "")
                Next
            Next
            'special check
            For a = 0 To 8
                Dim q As String = carry(a)
                If Not q.Length = 2 Then Continue For
                'set cell
                Dim ctl As Cells = SudokuPanel.Controls("Cell" & q)
                ctl.TextBox1.Text = a + 1
            Next
        Next
    End Sub

    Sub boxCheck()
        For i = 0 To 8 Step 3
            For j = 0 To 8 Step 3
                Dim carry() As String = {"", "", "", "", "", "", "", "", ""}
                For k = i To i + 2
                    For h = j To j + 2
                        Dim ctl As Cells = SudokuPanel.Controls("Cell" & k & h)
                        Dim p As String = ctl.TextBox1.Text
                        'pre check for special check
                        For a = 0 To p.Length - 1
                            carry(CInt(p(a).ToString()) - 1) &= k & h
                        Next
                        'general check
                        If Not p.Length = 1 Then Continue For
                        For a = i To i + 2
                            For b = j To j + 2
                                Dim ct As Cells = SudokuPanel.Controls("Cell" & a & b)
                                Dim q As String = ct.TextBox1.Text
                                If q.Length = 1 Then Continue For
                                ct.TextBox1.Text = q.Replace(p, "")
                            Next
                        Next
                    Next
                Next
                'special check
                For a = 0 To 8
                    Dim q As String = carry(a)
                    If Not q.Length = 2 Then Continue For
                    'set cell
                    Dim ctl As Cells = SudokuPanel.Controls("Cell" & q)
                    ctl.TextBox1.Text = a + 1
                Next
            Next
        Next
    End Sub

    Function checkSolve() As Boolean
        For i = 0 To 8
            Dim p As String = ""
            Dim q As String = ""
            For j = 0 To 8
                'row
                Dim row As Cells = SudokuPanel.Controls("Cell" & i & j) 'row
                Dim rowtx As String = row.TextBox1.Text
                If (rowtx.Length = 1) Then
                    If Not (p.Contains(rowtx)) Then p &= rowtx
                    row.TextBox1.BackColor = Color.MintCream
                Else
                    row.TextBox1.BackColor = Color.Red
                    Return False
                End If
                'column
                Dim col As Cells = SudokuPanel.Controls("Cell" & j & i) 'col
                Dim coltx As String = col.TextBox1.Text
                If (coltx.Length = 1) Then
                    If Not (q.Contains(coltx)) Then q &= coltx
                    col.TextBox1.BackColor = Color.MintCream
                Else
                    col.TextBox1.BackColor = Color.Red
                    Return False
                End If
            Next
            If Not (p.Length = 9) Then Return False
            If Not (q.Length = 9) Then Return False
        Next
        Return True
    End Function

    Sub moreTry()
        Dim q As Integer = 2
        Dim whole As String = getWholeData()
        While (q <= 9)
            For i = 0 To 8
                For j = 0 To 8
                    Dim ctl As Cells = SudokuPanel.Controls("Cell" & j & i) 'col
                    Dim p As String = ctl.TextBox1.Text
                    If Not p.Length = q Then Continue For
                    ctl.TextBox1.Text = p(0)
                    Solve_Button.PerformClick()
                    If (checkSolve()) Then Continue For
                    setWholeData(whole)
                    ctl.TextBox1.Text = p(1)
                    Solve_Button.PerformClick()
                    If (checkSolve()) Then Continue For
                    setWholeData(whole)
                Next
            Next
            q += 1
        End While
    End Sub

    Sub setArray()
        Dim p As String = "123456789"
        'get array
        For i = 0 To 8
            For j = 0 To 8
                Dim ctl As Cells = SudokuPanel.Controls("Cell" & i & j)
                If Not ctl.TextBox1.Text = "" Then Continue For
                ctl.TextBox1.Text = p
                ctl.TextBox1.BackColor = Color.MintCream
            Next
        Next
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        For i = 0 To 8
            For j = 0 To 8
                Dim ctl As Cells = SudokuPanel.Controls("Cell" & i & j)
                ctl.TextBox1.Text = ""
                ctl.TextBox1.Font = New Font(ctl.TextBox1.Font, FontStyle.Regular)
                ctl.TextBox1.BackColor = Color.MintCream
            Next
        Next
        TextBox1.Clear()
        ComboBox1.Items.Clear()
    End Sub

    Sub openFile(ByVal fileName)
        Dim whole As String = My.Computer.FileSystem.ReadAllText(fileName)
        TextBox1.Text = whole
        Button1.PerformClick()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Dim open1 As New OpenFileDialog
        open1.Filter = "Sudoku File|*.sud"
        If open1.ShowDialog = 1 Then
            openFile(open1.FileName)
        End If
    End Sub

    Function getWholeData() As String
        Dim whole As String = ""
        For i = 0 To 8
            Dim row As String = ""
            For j = 0 To 8
                Dim ctl As Cells = SudokuPanel.Controls("Cell" & i & j)
                Dim p As String = ctl.TextBox1.Text
                row &= p & " "
            Next
            whole &= row & vbCrLf
        Next
        Return whole
    End Function

    Sub setWholeData(ByVal data As String, Optional ByVal starting As Boolean = False)
        Dim whole As String() = Split(data, vbCrLf)
        'adding controls
        For i = 0 To 8 'top to bottom
            Dim row() As String = Split(whole(i), " ")
            For j = 0 To 8 'left to right
                Dim ctl As Cells = SudokuPanel.Controls("Cell" & i & j)
                Dim txt As String = row(j)
                If starting Then
                    If row(j).Length = 1 Then
                        ctl.TextBox1.Font = New Font(ctl.TextBox1.Font, FontStyle.Bold)
                    Else
                        ctl.TextBox1.Font = New Font(ctl.TextBox1.Font, FontStyle.Regular)
                    End If
                End If
                ctl.TextBox1.Text = row(j)
            Next
        Next
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Dim save1 As New SaveFileDialog
        save1.Filter = "Sudoku File|*.sud"
        If save1.ShowDialog = 1 Then
            My.Computer.FileSystem.WriteAllText(save1.FileName, getWholeData, False)
        End If
    End Sub

    Sub makeSudoku()
        SudokuPanel.Controls.Clear()
        Dim left, top As Integer
        left = 4
        top = (SudokuPanel.Height - 282) / 2
        Dim width As Integer = SudokuPanel.Width / 9 - 16 / 9
        'adding controls
        For i = 0 To 8 'top to bottom
            For j = 0 To 8 'left to right
                Dim ctl As New Cells()
                ctl.Location = New Point(left, top)
                ctl.Size = New Size(width, 30)
                ctl.Name = "Cell" & i & j
                SudokuPanel.Controls.Add(ctl)
                left += width
                If (j + 1) Mod 3 = 0 Then left += 4
            Next
            left = 4
            top += 30
            If ((i + 1) Mod 3) = 0 Then top += 4
        Next
        'set text
        If Not ComboBox1.Items.Count = 0 Then
            ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
        Else
            Button1.PerformClick()
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        makeSudoku()
        Try
            openFile(My.Application.CommandLineArgs(0))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Solve_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Solve_Button.Click
        Dim exist As Boolean = False
        If TextBox1.Text = "" Then TextBox1.Text = getWholeData()
        setArray()
        While Not exist
            'check exist
            Dim txt As String = getWholeData()
            If ComboBox1.Items.Contains(txt) Then
                exist = True
                Continue While
            End If
            ComboBox1.Items.Add(txt)
            'call functions
            columnRowCheck()
            boxCheck()
        End While
        ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
        LinkLabel1.Visible = True
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        setWholeData(ComboBox1.SelectedItem.ToString())
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not TextBox1.Text.Trim = "" Then
            setWholeData(TextBox1.Text, True)
            ComboBox1.Items.Clear()
        End If
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        makeSudoku()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim p As Integer = ComboBox1.SelectedIndex
            If ComboBox1.Items.Count = p + 1 Then p = -1
            ComboBox1.SelectedIndex = p + 1
        Catch
        End Try
    End Sub

    Private Sub HelpToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripButton.Click
        HelpBox.ShowDialog()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim solved As Boolean = checkSolve()
        Dim msg As String = "There are mistakes. Retry?"
        If solved Then
            msg = "No mistake was found. Sudoku solved successfully"
            MsgBox(msg)
        Else
            If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                moreTry()
            End If
        End If
    End Sub
End Class
