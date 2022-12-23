Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim lines = input.Split(vbLf)
        Dim field = New Dictionary(Of (x As Integer, y As Integer), Boolean)()

        For y = 0 To lines.Count - 1
            For x = 0 To lines(y).Length - 1
                If lines(y)(x) = "#" Then
                    field((x, y)) = True
                End If
            Next x
        Next y

        Dim round = 0
        For i = 0 To 1000000 - 1
            Dim stayed = 0
            Dim moves = New Dictionary(Of (x As Integer, y As Integer), List(Of (x As Integer, y As Integer)))()
            For Each kv In field
                Dim pos = kv.Key
                Dim newpos = pos
                For j = 0 To 4 - 1
                    Dim checkpos = New List(Of (x As Integer, y As Integer)) From {
                        (pos.x - 1, pos.y - 1),
                        (pos.x + 0, pos.y - 1),
                        (pos.x + 1, pos.y - 1),
                        (pos.x - 1, pos.y + 0),
                        (pos.x + 1, pos.y + 0),
                        (pos.x - 1, pos.y + 1),
                        (pos.x + 0, pos.y + 1),
                        (pos.x + 1, pos.y + 1)
                    }
                    Dim neighbours = 0
                    For Each cp In checkpos
                        If field.ContainsKey(cp) Then
                            neighbours += 1
                        End If
                    Next
                    If neighbours = 0 Then
                        stayed += 1
                        Exit For
                    End If
                    checkpos = New List(Of (x As Integer, y As Integer))()
                    Select Case (i + j) Mod 4
                        Case 0 ' north
                            checkpos.Add((pos.x - 1, pos.y - 1))
                            checkpos.Add((pos.x + 0, pos.y - 1))
                            checkpos.Add((pos.x + 1, pos.y - 1))
                        Case 1 ' south
                            checkpos.Add((pos.x - 1, pos.y + 1))
                            checkpos.Add((pos.x + 0, pos.y + 1))
                            checkpos.Add((pos.x + 1, pos.y + 1))
                        Case 2 ' west
                            checkpos.Add((pos.x - 1, pos.y - 1))
                            checkpos.Add((pos.x - 1, pos.y + 0))
                            checkpos.Add((pos.x - 1, pos.y + 1))
                        Case 3 ' east
                            checkpos.Add((pos.x + 1, pos.y - 1))
                            checkpos.Add((pos.x + 1, pos.y + 0))
                            checkpos.Add((pos.x + 1, pos.y + 1))
                    End Select
                    neighbours = 0
                    For Each cp In checkpos
                        If field.ContainsKey(cp) Then
                            neighbours += 1
                        End If
                    Next
                    If neighbours = 0 Then
                        newpos = checkpos(1)
                        Exit For
                    End If
                Next j
                If Not moves.ContainsKey(newpos) Then
                    moves(newpos) = New List(Of (x As Integer, y As Integer))
                End If
                moves(newpos).Add(pos)
            Next
            If stayed = field.Count Then
                round = i + 1
                Exit For
            End If
            Dim newfield = New Dictionary(Of (x As Integer, y As Integer), Boolean)()
            For Each kv In moves
                If kv.Value.Count = 1 Then
                    newfield(kv.Key) = True
                Else
                    For Each prevpos In kv.Value
                        newfield(prevpos) = True
                    Next
                End If
            Next
            field = newfield
        Next i

        Console.WriteLine(round)

    End Sub

End Module
