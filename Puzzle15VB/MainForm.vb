Public Class MainForm

    Private _puzzle As Puzzle

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _puzzle = New Puzzle()
        _puzzle.Shuffle()
        AddHandler _puzzle.ImageUpdated, AddressOf PuzzleImageUpdated
        PctPuzzle.Image = _puzzle.Img
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        _puzzle.Shuffle()
        PctPuzzle.Enabled = True
    End Sub

    Private Sub PuzzleImageUpdated(sender As Object, e As EventArgs)
        PctPuzzle.Image = Nothing
        PctPuzzle.Image = _puzzle.Img
    End Sub

    Private Sub PctPuzzle_MouseDown(sender As Object, e As MouseEventArgs) Handles PctPuzzle.MouseDown
        Dim targetCell = New Point(Math.Floor(e.Location.X / Puzzle.CellPixelWidth), Math.Floor(e.Location.Y / Puzzle.CellPixelHeight))
        Debug.WriteLine(targetCell)
        If _puzzle.CanMove(targetCell) Then
            _puzzle.MoveCell(targetCell)
        End If
        If _puzzle.IsCompleted() Then
            PctPuzzle.Enabled = False
            MessageBox.Show("おめでとうございます ！！！", "Puzzle 15", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class
