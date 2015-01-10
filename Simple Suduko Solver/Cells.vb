Public Class Cells

    Private Sub TextBox1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
        TextBox1.SelectAll()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Right Then
            My.Computer.Keyboard.SendKeys("{TAB}")
        ElseIf e.KeyCode = Keys.Down Then
            My.Computer.Keyboard.SendKeys("{TAB 9}")
        ElseIf e.KeyCode = Keys.Left Then
            My.Computer.Keyboard.SendKeys("+{TAB}")
        ElseIf e.KeyCode = Keys.Up Then
            My.Computer.Keyboard.SendKeys("+{TAB 9}")
        End If
    End Sub

    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        If Not IsNumeric(TextBox1.Text) Or TextBox1.Text = "0" Or TextBox1.Text = " " Then
            TextBox1.Text = ""
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
