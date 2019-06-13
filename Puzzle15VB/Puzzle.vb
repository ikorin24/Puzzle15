Public Class Puzzle

    Public Const ColumnCount As Integer = 4
    Public Const RowCount As Integer = 4

    Public Const BitmapWidth As Integer = 400
    Public Const BitmapHeight As Integer = 400

    Public Const CellPixelWidth As Integer = BitmapWidth \ ColumnCount
    Public Const CellPixelHeight As Integer = BitmapHeight \ RowCount

    Private ReadOnly _backColor As Color = Color.Black
    Private _cells As Cell()
    Private _emptyCell As EmptyCell

    Public Event ImageUpdated(sender As Object, e As EventArgs)

    Public ReadOnly Property Img

    Public Sub New()
        Img = New Bitmap(BitmapWidth, BitmapHeight)
        _emptyCell = New EmptyCell(New Point(ColumnCount - 1, RowCount - 1))
        _cells = Enumerable.Range(0, ColumnCount * RowCount - 1).Select(Function(i) New Cell(i + 1, New Point(i Mod ColumnCount, i \ ColumnCount), $"img{i + 1}")).ToArray()
        UpdateVisual()
    End Sub

    Public Sub Shuffle()
        Me.Shuffle(Date.Now.Millisecond)
    End Sub

    Public Sub Shuffle(seed As Integer)
        Dim rand = New Random(seed)
        Dim nums = Enumerable.Range(0, _cells.Length).Select(Function(x) Tuple.Create(x, rand.Next())).OrderBy(Function(x) x.Item2).Select(Function(x, i) Tuple.Create(x.Item1, i))
        For Each num In nums
            _cells(num.Item1).Position = New Point(num.Item2 Mod ColumnCount, num.Item2 \ RowCount)
        Next
        _emptyCell.Position = New Point(ColumnCount - 1, RowCount - 1)
        If IsCompleted() Then
            Shuffle(seed + 1)
        End If
        UpdateVisual()
    End Sub

    Public Sub ToComplete()
        For i = 0 To _cells.Length - 1
            _cells(i).Position = New Point(i Mod ColumnCount, i \ ColumnCount)
        Next
        _emptyCell.Position = New Point(ColumnCount - 1, RowCount - 1)
        UpdateVisual()
    End Sub

    Public Sub MoveCell(targetCell As Point)
        Dim target = _cells.FirstOrDefault(Function(cell) cell.Position = targetCell)
        target.Position = _emptyCell.Position
        _emptyCell.Position = targetCell
        UpdateVisual()
    End Sub

    Public Function IsCompleted() As Boolean
        Return _cells.All(Function(cell) (cell.Number - 1) = (cell.PosY * ColumnCount + cell.PosX))
    End Function

    Public Function CanMove(targetCell As Point) As Boolean
        Dim target = _cells.FirstOrDefault(Function(cell) cell.Position = targetCell)
        If target Is Nothing Then
            Return False
        End If
        Return Math.Abs(_emptyCell.PosX - target.PosX) + Math.Abs(_emptyCell.PosY - target.PosY) = 1
    End Function


    Private Sub UpdateVisual()
        Using g As Graphics = Graphics.FromImage(Img)
            g.Clear(_backColor)
            For Each cell In _cells
                g.DrawImage(cell.Img, New Point(cell.PosX * CellPixelWidth, cell.PosY * CellPixelHeight))
            Next
        End Using
        RaiseEvent ImageUpdated(Me, EventArgs.Empty)
    End Sub
End Class
