Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim lines = input.Trim().Split(vbLf)
        Dim width = lines(0).Count - 2
        Dim height = lines.Count - 2
        Dim field = New Dictionary(Of (x As Integer, y As Integer), List(Of Integer))()
        Dim start As (x As Integer, y As Integer) = (0, -1)
        Dim goal As (x As Integer, y As Integer) = (width - 1, height)

        For y = 0 To height - 1
            For x = 0 To width - 1
                Dim list = New List(Of Integer)()
                Select Case lines(y + 1).Substring(x + 1, 1)
                    Case ">"
                        list.Add(0)
                    Case "v"
                        list.Add(1)
                    Case "<"
                        list.Add(2)
                    Case "^"
                        list.Add(3)
                End Select
                field((x, y)) = list
            Next x
        Next y

        Dim round = 0
        Dim frontiers = New Dictionary(Of (x As Integer, y As Integer), Boolean)()
        frontiers(start) = True
        For i = 1 To 100000
            Dim newfield = New Dictionary(Of (x As Integer, y As Integer), List(Of Integer))()
            For y = 0 To height - 1
                For x = 0 To width - 1
                    newfield((x, y)) = New List(Of Integer)()
                Next x
            Next y
            For Each kv In field
                Dim pos = kv.Key
                For Each blizzard In kv.Value
                    Dim newpos As (x As Integer, y As Integer) = (pos.x - (blizzard - 1) Mod 2, pos.y - (blizzard - 2) Mod 2)
                    If Not newfield.ContainsKey(newpos) Then
                        Do While newfield.ContainsKey((newpos.x + (blizzard - 1) Mod 2, newpos.y + (blizzard - 2) Mod 2))
                            newpos = (newpos.x + (blizzard - 1) Mod 2, newpos.y + (blizzard - 2) Mod 2)
                        Loop
                    End If
                    newfield(newpos).Add(blizzard)
                Next
            Next
            Dim newfrontiers = New Dictionary(Of (x As Integer, y As Integer), Boolean)()
            For Each kv In frontiers
                Dim pos = kv.Key
                For d = 0 To 3
                    Dim newpos As (x As Integer, y As Integer) = (pos.x - (d - 1) Mod 2, pos.y - (d - 2) Mod 2)
                    If newfield.ContainsKey(newpos) Then
                        If newfield(newpos).Count = 0 Then
                            newfrontiers(newpos) = True
                        End If
                    End If
                    If newpos.x = goal.x And newpos.y = goal.y Then
                        newfrontiers(newpos) = True
                    End If
                Next
                If newfield.ContainsKey(pos) Then
                    If newfield(pos).Count = 0 Then
                        newfrontiers(pos) = True
                    End If
                End If
                If pos.x = start.x And pos.y = start.y Then
                    newfrontiers(pos) = True
                End If
            Next
            frontiers = newfrontiers
            field = newfield
            If frontiers.ContainsKey(goal) Then
                round = i
                Exit For
            End If
        Next i

        Console.WriteLine(round)

    End Sub

End Module
