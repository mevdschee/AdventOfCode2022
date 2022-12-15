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
            Dim x1 = Integer.Parse(match.Groups(1).Value)
            Dim y1 = Integer.Parse(match.Groups(2).Value)
            Dim x2 = Integer.Parse(match.Groups(3).Value)
            Dim y2 = Integer.Parse(match.Groups(4).Value)
            Dim sensor As (x As Integer, y As Integer) = (x1, y1)
            Dim beacon As (x As Integer, y As Integer) = (x2, y2)
            Dim distance = Math.Abs(x1 - x2) + Math.Abs(y1 - y2)
            sensors(sensor) = distance
            beacons(beacon) = distance
            minx = Math.Min(minx, x1 - distance)
            maxx = Math.Max(maxx, x1 + distance)
        Next

        Dim count = 0
        Dim y = 2000000
        For x = minx To maxx
            If Not beacons.ContainsKey((x, y)) Then
                For Each kv In sensors
                    Dim sensor = kv.Key
                    Dim distance = kv.Value
                    If Math.Abs(x - sensor.x) + Math.Abs(y - sensor.y) <= distance Then
                        count += 1
                        Exit For
                    End If
                Next
            End If
        Next x

        Console.WriteLine(count)

    End Sub

End Module
