Imports System.IO
Imports System.Text.RegularExpressions

Module Program
    
    Sub Main()

        Dim input = File.ReadAllText("input").Trim().Replace(vbCrLf, vbLf)
        Dim lines = input.Split(vbLf)
        Dim sensors = New Dictionary(Of (x As Integer, y As Integer), Integer)()
        Dim beacons = New Dictionary(Of (x As Integer, y As Integer), Integer)()
        Dim minx = 0
        Dim maxx = 0

        For Each line In lines
        	Dim match As Match = Regex.Match(line, "Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)")
            Dim x1 = Integer.parse(match.Groups(1).Value)
            Dim y1 = Integer.parse(match.Groups(2).Value)
            Dim x2 = Integer.parse(match.Groups(3).Value)
            Dim y2 = Integer.parse(match.Groups(4).Value)
            Dim sensor As (x As Integer, y As Integer) = (x1,y1)
            Dim beacon As (x As Integer, y As Integer) = (x2,y2)
            Dim distance = Math.Abs(x1 - x2) + Math.Abs(y1 - y2)
            sensors(sensor) = distance
            beacons(beacon) = distance
            minx = Math.Min(minx, x1 - distance)
            maxx = Math.Max(maxx, x1 + distance)
        Next

        Dim edges = New Dictionary(Of (x As Integer, y As Integer), Boolean)()
        For Each kv1 In sensors
            Dim sensor = kv1.Key
            Dim distance = kv1.Value
            Dim edge = distance + 1
            ' left-upper side /
            For e = 0 to edge-1
                Dim x = sensor.x - (edge-e)
                Dim y = sensor.y - e
                If x>=0 And y>=0 And x<4000000 And y<4000000 Then
                    edges((x,y)) = True
                End If
            Next e
            ' right-upper side \
            For e = 0 to edge-1
                Dim x = sensor.x + e
                Dim y = sensor.y - (edge-e)
                If x>=0 And y>=0 And x<4000000 And y<4000000 Then
                    edges((x,y)) = True
                End If
            Next e
            ' right-down side /
            For e = 0 to edge-1
                Dim x = sensor.x + (edge-e)
                Dim y = sensor.y + e
                If x>=0 And y>=0 And x<4000000 And y<4000000 Then
                    edges((x,y)) = True
                End If
            Next e
            ' right-down side \
            For e = 0 to edge-1
                Dim x = sensor.x - e
                Dim y = sensor.y - (edge-e)
                If x>=0 And y>=0 And x<4000000 And y<4000000 Then
                    edges((x,y)) = True
                End If
            Next e
        Next

        Dim result As Long = 0
        For Each edge In edges.Keys
            Dim found = True
            For Each kv In sensors
                Dim sensor = kv.Key
                Dim distance = kv.Value
                If Math.Abs(edge.x - sensor.x) + Math.Abs(edge.y - sensor.y) <= distance Then 
                    found = False
                    Exit For
                End If
            Next     
            If found Then
                result = CLng(edge.x) * 4000000 + edge.y
                Exit For
            End If
        Next
        
        Console.WriteLine("{0}", result)

    End Sub
    
End Module
