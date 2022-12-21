Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Trim().Replace(vbCrLf, vbLf)
        Dim lines = input.Split(vbLf)
        Dim coords = New Dictionary(Of (x As Integer, y As Integer, z As Integer), Boolean)()

        For Each line In lines
            Dim xyz = line.Split(",")
            Dim x = Integer.Parse(xyz(0))
            Dim y = Integer.Parse(xyz(1))
            Dim z = Integer.Parse(xyz(2))
            coords((x,y,z)) = True
        Next

        Dim count = 0
        For Each c In coords.Keys
            Dim neighbours = New List(Of (x As Integer, y As Integer, z As Integer))()
            neighbours.Add((c.x-1,c.y,c.z))
            neighbours.Add((c.x+1,c.y,c.z))
            neighbours.Add((c.x,c.y-1,c.z))
            neighbours.Add((c.x,c.y+1,c.z))
            neighbours.Add((c.x,c.y,c.z-1))
            neighbours.Add((c.x,c.y,c.z+1))
            For Each neighbour In neighbours
                If Not coords.ContainsKey(neighbour) Then
                    count += 1
                End If
            Next
        Next
        
        Console.WriteLine(count)
       
    End Sub

End Module
