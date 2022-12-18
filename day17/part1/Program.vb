Imports System.IO
Imports System.Text

Module Program

    Class Block

        Public x As Integer
        Public y As Integer
        Public shape As String

        Public Sub New(x As Integer, y As Integer, shape As String)
            Me.x = x
            Me.y = y
            Me.shape = shape
        End Sub

    End Class

    Function MoveBlock(block As Block, direction As Char, field As Dictionary(Of (x As Integer, y As Integer), Char), width As Integer) As Boolean
        Dim dx = 0
        Dim dy = 0
        Select direction
            Case "<"
                dx = -1
            Case ">"
                dx = 1
            Case "v"
                dy = -1
        End Select
        Dim newBlock = new Block(block.x + dx, block.y + dy, block.shape)
        If CanPlaceBlock(newBlock, field, width) Then
            ClearBlock(False, field)
            block.x += dx
            block.y += dy
            PlaceBlock(block,field)
            Return True
        End If
        Return False
    End Function

    Function BlockHeight(frozen As Boolean, field As Dictionary(Of (x As Integer, y As Integer), Char)) As Integer
        Dim top = -1
        For Each c In field.Keys
            If field(c) = "#" Or Not frozen
                top = Math.max(top, c.y)
            End If
        Next
        Return top+1
    End Function

    Function NewBlock(shape As String, field As Dictionary(Of (x As Integer, y As Integer), Char)) As Block
        Dim lines = shape.Split("|")
        Dim top = BlockHeight(True, field)
        Return New Block(2, top + 3 + lines.Count-1, shape)
    End Function

    Function CanPlaceBlock(block As Block, field As Dictionary(Of (x As Integer, y As Integer), Char), width As Integer) As Boolean
        Dim lines = block.shape.Split("|")
        For i=0 To lines.Count-1
            Dim y = block.y - i
            For j=0 To lines(i).Length-1
                Dim x = block.x + j
                Dim c = lines(i).Substring(j,1)
                If c<>"." Then
                    If x<0 Or y<0 Or x>=width Then
                        Return False
                    End If
                    If field.ContainsKey((x,y)) Then
                        If field((x,y)) = "#" Then
                            Return False
                        End If
                    End If
                End If
            Next j
        Next i
        Return True
    End Function

    Function ClearBlock(freeze As Boolean, field As Dictionary(Of (x As Integer, y As Integer), Char)) As Boolean
        Dim result = False
        For Each kv In field
            If kv.Value = "@" Then
                If freeze Then
                    field((kv.Key.x, kv.Key.y)) = "#"
                Else
                    field.Remove((kv.Key.x,kv.Key.y))
                End If            
                result = True
            End If                        
        Next
        Return result
    End Function

    Sub PlaceBlock(block As Block, field As Dictionary(Of (x As Integer, y As Integer), Char))
        Dim lines = block.shape.Split("|")
        For i=0 To lines.Count-1
            Dim y = block.y - i
            For j=0 To lines(i).Length-1
                Dim x = block.x + j
                Dim c = lines(i).Substring(j,1)
                If c<>"." Then
                    field((x,y)) = "@"    
                End If
            Next j
        Next i
    End Sub

    Function FieldString(startx As Integer, starty As Integer, width As Integer, height As Integer, field As Dictionary(Of (x As Integer, y As Integer), Char)) As String
        Dim s = New StringBuilder()
        For y = starty To height-1
            For x = starty To width-1
                If field.ContainsKey((x,y)) Then
                    s.Append(field((x,y)))
                Else
                    s.Append(".")
                End If                
            Next x
        Next y
        return s.ToString()
    End Function

    Sub PrintField(width As Integer, field As Dictionary(Of (x As Integer, y As Integer), Char))
        Dim height = BlockHeight(False, field)
        Dim str = FieldString(0, 0, width, height, field)
        For y = height-1 To 0 Step -1
            Console.WriteLine("|" & str.Substring(y * width, width) & "|")
        Next y
        Console.WriteLine("+" & Strings.StrDup(width, "-") & "+")
    End Sub

    Sub Main()
        Dim input = File.ReadAllText("input").Trim()
        Dim field = New Dictionary(Of (x As Integer, y As Integer), Char)()
        Dim shapes As String() = {"####",".#|###|.#","..#|..#|###","#|#|#|#","##|##"}
        Dim shapeIndex = 0
        Dim block = NewBlock(shapes(shapeIndex), field)
        Dim width = 7
        Dim stopped = 0
        Dim i = 0
        PlaceBlock(block, field)
        Do Until stopped = 2022
            Dim direction = input.Substring(i Mod input.Length, 1)
            MoveBlock(block, direction, field, width)
            If Not MoveBlock(block, "v", field, width) Then
                stopped += 1
                shapeIndex = (shapeIndex+1) Mod shapes.Count
                ClearBlock(True, field)
                block = NewBlock(shapes(shapeIndex), field)
                PlaceBlock(block, field)
                'PrintField(width, field)
            End If            
            i += 1
        Loop
        'PrintField(width, field)
        Dim height = BlockHeight(True, field)
        Console.WriteLine(height)
    End Sub

End Module
