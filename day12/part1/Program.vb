Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input")
        Dim lines = input.Trim().Replace(vbCrLf, vbLf).Split(vbLf)
        Dim fields = New Dictionary(Of (Integer, Integer), Integer)()
        Dim width = lines(0).Length
        Dim height = lines.Length
        Dim start As (x As Integer, y As Integer) = (0, 0)
        Dim goal As (x As Integer, y As Integer) = (0, 0)

        For y = 0 To height - 1
            For x = 0 To width - 1
                Dim character = lines(y).Substring(x, 1)
                Select Case character
                    Case "S"
                        start = (x, y)
                        fields((x, y)) = -1
                    Case "E"
                        goal = (x, y)
                        fields((x, y)) = Asc("z") - Asc("a") + 1
                    Case Else
                        fields((x, y)) = Asc(character) - Asc("a")
                End Select
            Next x
        Next y

        Dim firstPath As New List(Of (x As Integer, y As Integer))({start})
        Dim paths As New List(Of List(Of (x As Integer, y As Integer)))({firstPath})
        Dim steps = 1
        Do
            Dim moves() As (x As Integer, y As Integer) = {(0, -1), (0, 1), (-1, 0), (1, 0)}
            Dim newPaths = New List(Of List(Of (x As Integer, y As Integer)))()
            For Each path In paths
                For Each move In moves
                    Dim pos = path.Last()
                    Dim newPos As (x As Integer, y As Integer) = (pos.x + move.x, pos.y + move.y)
                    If newPos.x < 0 Or newPos.y < 0 Or newPos.x >= width Or newPos.y >= height Then
                        Continue For
                    End If
                    If fields(newPos) > fields(pos) + 1 Or fields(newPos) < fields(pos) Then
                        Continue For
                    End If
                    If path.Contains(newPos) Then
                        Continue For
                    End If
                    If newPos.x = goal.x And newPos.y = goal.y Then
                        Exit Do
                    End If
                    Dim newPath = path.ToList()
                    newPath.Add(newPos)
                    newPaths.Add(newPath)
                Next
            Next
            paths = newPaths
            steps += 1
        Loop While True

        Console.WriteLine(steps)

    End Sub
End Module
