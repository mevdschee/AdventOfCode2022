Imports System.IO
Imports System.Text.RegularExpressions

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Trim().Replace(vbCrLf, vbLf)
        Dim lines = input.Split(vbLf)
        Dim coords = New Dictionary(Of (x As Integer, y As Integer, z As Integer), Boolean)()
        Dim min As (x As Integer, y As Integer, z As Integer) = (100,100,100)
        Dim max As (x As Integer, y As Integer, z As Integer) = (-100,-100,-100)

        For Each line In lines
            Dim xyz = line.Split(",")
            Dim x = Integer.Parse(xyz(0))
            Dim y = Integer.Parse(xyz(1))
            Dim z = Integer.Parse(xyz(2))
            coords((x,y,z)) = True
            min.x = Math.Min(min.x, x)
            min.y = Math.Min(min.y, y)
            min.z = Math.Min(min.z, z)
            max.x = Math.Max(max.x, x)
            max.y = Math.Max(max.y, y)
            max.z = Math.Max(max.z, z)
        Next

        Dim water = New Dictionary(Of (x As Integer, y As Integer, z As Integer), Boolean)()
        Dim frontier = New List(Of (x As Integer, y As Integer, z As Integer))()
        water((0,0,0)) = True
        frontier.Add((0,0,0))
        Do While frontier.Count > 0
            Dim newFrontier = New List(Of (x As Integer, y As Integer, z As Integer))()
            For Each c In frontier
                Dim neighbours = New List(Of (x As Integer, y As Integer, z As Integer))()
                neighbours.Add((c.x-1,c.y,c.z))
                neighbours.Add((c.x+1,c.y,c.z))
                neighbours.Add((c.x,c.y-1,c.z))
                neighbours.Add((c.x,c.y+1,c.z))
                neighbours.Add((c.x,c.y,c.z-1))
                neighbours.Add((c.x,c.y,c.z+1))
                For Each neighbour In neighbours
                    If neighbour.x<min.x-1 Or neighbour.y<min.y-1 Or neighbour.z<min.z-1 Then
                        Continue For
                    End If
                    If neighbour.x>max.x+1 Or neighbour.y>max.y+1 Or neighbour.z>max.z+1 Then
                        Continue For
                    End If
                    If coords.ContainsKey(neighbour) Or water.ContainsKey(neighbour) Or frontier.Contains(neighbour) 
                        Continue For
                    End If
                    water(neighbour) = True
                    newFrontier.Add(neighbour)
                Next
            Next            
            frontier = newFrontier
        Loop        

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
                If Not coords.ContainsKey(neighbour) and water.ContainsKey(neighbour) Then
                    count += 1
                End If
            Next
        Next
        
        Console.WriteLine(count)
       
    End Sub

End Module

'2645 too low