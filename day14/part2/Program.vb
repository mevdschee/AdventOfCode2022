Imports System.IO

Module Program
    
    Sub Main()

        Dim input = File.ReadAllText("input").Trim().Replace(vbCrLf, vbLf)
        Dim lines = input.Split(vbLf)
        Dim field = New Dictionary(Of (Integer, Integer), Char)()
        
        Dim maxy = 0
        For Each line In lines
            Dim pos As (x As Integer, y As Integer) = (-1, -1)
            Dim pairs = line.Split(" -> ")
            For Each pair In pairs
                Dim coords = pair.Split(",")
                Dim cx = Integer.Parse(coords(0))
                Dim cy = Integer.Parse(coords(1))
                Dim coord As (x As Integer, y As Integer) = (cx,cy)
                If pos.x <> -1 Then
                    If pos.x = coord.x Then 'vertical
                        For y = pos.y To coord.y Step If(coord.y > pos.y, 1, -1)
                            field((pos.x, y)) = "#"
                        Next y
                    Else 'horizontal                        
                        For x = pos.x To coord.x Step If(coord.x > pos.x, 1, -1)
                            field((x, pos.y)) = "#"
                        Next x
                    End If                    
                End If
                maxy = Math.Max(maxy, coord.y)
                pos = coord
            Next
        Next

        Dim count = 0
        Dim sand As (x As Integer, y As Integer) = (500, 0)
        Do Until field.ContainsKey(sand)
            If Not field.ContainsKey((sand.x, sand.y + 1)) And Not sand.y + 1 >= maxy + 2 Then
                sand.y += 1
            ElseIf Not field.ContainsKey((sand.x - 1, sand.y + 1)) And Not sand.y + 1 >= maxy + 2 Then
                sand.x -= 1
                sand.y += 1
            ElseIf Not field.ContainsKey((sand.x + 1, sand.y + 1)) And Not sand.y + 1 >= maxy + 2 Then
                sand.x += 1
                sand.y += 1
            Else
                field(sand) = "o"
                count += 1
                sand = (500, 0)
            End If
        Loop

        Console.WriteLine(count)

    End Sub
    
End Module
