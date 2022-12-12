Imports System.IO

Module Program

    Function Traverse(path As List(Of (x As Integer, y As Integer)), pos As (x As Integer, y As Integer), width As Integer, height As Integer, goal As (x As Integer, y As Integer), fields As Dictionary(Of (Integer, Integer), Integer)) As Integer
        If goal.x = pos.x And goal.y = pos.y Then
            Return path.Count
        End If
        Dim moves() = {(0, -1), (0, 1), (-1, 0), (1, 0)}
        Dim results = New List(Of Integer)()
        For Each move In moves
            Dim newPos As (x As Integer, y As Integer) = (pos.x + move.Item1, pos.y + move.Item2)
            If newPos.x < 0 Or newPos.y < 0 Or newPos.x >= width Or newPos.y >= height Then
                Continue For
            End If
            If fields(newPos) > fields(pos) + 1 Or fields(newPos) < fields(pos) Then
                Continue For
            End If
            If path.Contains(newPos) Then
                Continue For
            End If
            Dim newPath = path.ToList()
            newPath.Add(newPos)
            Dim result = Traverse(newPath, newPos, width, height, goal, fields)
            If result <> -1 Then
                results.Add(result)
            End If
        Next
        results.Sort()
        If results.Count > 0 Then
            Return results(0)
        End If
        Return -1
    End Function

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

        Dim path = New List(Of (x As Integer, y As Integer))()
        Dim steps = Traverse(path, start, width, height, goal, fields)

        Console.WriteLine(steps)

    End Sub
End Module
