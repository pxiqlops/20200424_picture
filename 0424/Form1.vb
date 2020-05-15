Public Class Form1
    Dim FullPaths As New ArrayList
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'フォームタイトル表示
        Me.Text = "画像表示プログラム"
    End Sub
    Private Sub lstFileName_DragEnter(ByVal sender As Object, e As DragEventArgs) Handles lstFileName.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub lstFileName_DragDrop(ByVal sender As Object, e As DragEventArgs) Handles lstFileName.DragDrop
        Dim FileName As String

        FileName = CType(e.Data.GetData(DataFormats.FileDrop), String())(0)

        FullPaths.Add(FileName)
        lstFileName.Items.Add(IO.Path.GetFileName(FileName))

        btnSave.Enabled = True
    End Sub

    Private Sub lstFileName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFileName.SelectedIndexChanged
        Dim FullPath As String

        FullPath = FullPaths(lstFileName.SelectedIndex)

        WebBrowser1.Navigate(FullPath)

        Me.Text = "画像表示プログラム - " & FullPath
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim Dialog As New SaveFileDialog

        If Dialog.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            Dim Writer As New IO.StreamWriter(Dialog.FileName)

            For Index As Integer = 0 To FullPaths.Count - 1
                Writer.WriteLine(FullPaths(Index))
            Next

            Writer.Close()

        End If

    End Sub


    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click

        Dim Dialog As New OpenFileDialog

        If Dialog.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

            Dim Value As String
            Dim Reader As New IO.StreamReader(Dialog.FileName)

            FullPaths.Clear()
            lstFileName.Items.Clear()

            Do
                Value = Reader.ReadLine
                If Value Is Nothing Then
                    Exit Do
                Else
                    FullPaths.Add(Value)
                    lstFileName.Items.Add(IO.Path.GetFileName(Value))
                End If
            Loop

            Reader.Close()

            btnSave.Enabled = True

        End If

    End Sub



End Class
