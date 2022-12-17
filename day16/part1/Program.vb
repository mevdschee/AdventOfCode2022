Imports System.IO
Imports System.Text.RegularExpressions

Module Program

    Function Calculate(connections As Dictionary(Of String, String())) As Dictionary(Of (String, String), Integer)
        
        Dim distances = new Dictionary(Of (String, String), Integer)()
        For Each valve In connections.Keys
            For Each nextValve In connections.Keys
                If valve = nextValve Then
                    distances((valve,nextValve)) = 0
                ElseIf connections(valve).Contains(nextValve) Then
                    distances((valve,nextValve)) = 1
                Else
                    distances((valve,nextValve)) = connections.Count
                End If
            Next
        Next
        
        For i=0 To connections.Count-1
            For Each valve In connections.Keys
                For Each intermediateValve In connections.Keys
                    For Each nextValve In connections.Keys
                        If distances((valve,intermediateValve)) <> connections.Count And connections(intermediateValve).Contains(nextValve) Then
                            distances((valve,nextValve)) = Math.Min(distances((valve,nextValve)), distances((valve,intermediateValve)) + 1)
                        End If
                    Next
                Next
            Next
        Next i

        Return distances

    End Function
    
    Function Permutate(time As Integer, pos As String, path As List(Of String), nodes As List(Of String), flows As Dictionary(Of String, Integer), distances As Dictionary(Of (String, String), Integer)) As Integer
        
        If path.Count = nodes.Count Then
            Return 0
        End If

        Dim best = 0
        For Each newPos In nodes
            If path.Contains(newPos) Then
                Continue For
            End If
            Dim newPath = New List(Of String)(path)
            newPath.Add(newPos)
            Dim newTime = time - distances((pos, newPos)) - 1
            If newTime>0 Then
                best = Math.Max(best, flows(newPos)*newtime + Permutate(newTime, newPos, newPath, nodes, flows, distances))
            End If
        Next

        Return best

    End Function

    Sub Main()

        Dim input = File.ReadAllText("input.test").Trim().Replace(vbCrLf, vbLf)
        Dim lines = input.Split(vbLf)
        Dim flows = New Dictionary(Of String, Integer)()
        Dim connections = New Dictionary(Of String, String())()

        For Each line In lines
            Dim match As Match = Regex.Match(line, "Valve ([A-Z]+) has flow rate=(\d+); tunnels? leads? to valves? (.+)")
            Dim valve = match.Groups(1).Value
            Dim flow = Integer.Parse(match.Groups(2).Value)
            Dim valves = match.Groups(3).Value.Split(", ")
            flows(valve) = flow
            connections(valve) = valves
        Next

        Dim distances = Calculate(connections)

        Dim path = New List(Of String)({})
        Dim nodes = flows.Keys.Where(Function (valve) flows(valve)>0).ToList()
        Dim best = Permutate(30, "AA", path, nodes, flows, distances)
        
        Console.WriteLine(best)
        
    End Sub

End Module
