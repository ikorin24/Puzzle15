Public Class Cell

    Protected _position As Integer

    Public Property Number As Integer
    Public Property Img As Bitmap
    Public Property Position As Point
        Get
            Return New Point(PosX, PosY)
        End Get
        Set(value As Point)
            _position = value.Y * Puzzle.ColumnCount + value.X
        End Set
    End Property

    Public ReadOnly Property PosX As Integer
        Get
            Return _position Mod Puzzle.ColumnCount
        End Get
    End Property

    Public ReadOnly Property PosY As Integer
        Get
            Return _position \ Puzzle.ColumnCount
        End Get
    End Property

    Public Sub New(num As Integer, pos As Point, imgName As String)
        Number = num
        Position = pos
        Try
            Dim bmp = CType(My.Resources.ResourceManager.GetObject(imgName, My.Resources.Culture), Bitmap)
            Img = New Bitmap(bmp, New Size(Puzzle.CellPixelWidth, Puzzle.CellPixelHeight))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub New(num As Integer, pos As Point)
        Number = num
        Position = pos
    End Sub
End Class

Public Class EmptyCell
    Inherits Cell

    Private Const _emptyCellNumber As Integer = 0

    Public Sub New(pos As Point)
        MyBase.New(_emptyCellNumber, pos)
        Position = pos
    End Sub
End Class
