Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input")
        Dim lines = input.Trim().Split(vbLf)
        Dim knots = New List(Of (x as Integer, y as Integer))()
        Dim visited = New Dictionary(Of (x as Integer, y as Integer), Boolean)()
        
        For i = 0 To 9
            knots.Add((0,0))
        Next

        visited((0,0)) = True
        For Each line In lines
            Dim operation = line.Split(" ")
            Dim direction = operation(0)
            Dim distance = Integer.Parse(operation(1))
            For i = 0 To distance-1
                
                Dim head = knots(0)
                
                ' Move head
                Select direction
                    Case "U" ' Up
                        head.y -= 1
                    Case "D" ' Down
                        head.y += 1
                    Case "L" ' Left
                        head.x -= 1
                    Case "R" ' Right
                        head.x += 1
                End Select

                knots(0) = (head.x, head.y)

                For j = 1 To knots.Count()-1

                    Dim prev = knots(j-1)
                    Dim curr = knots(j)

                    ' Should we move?
                    Dim move = False
                    If prev.x > curr.x+1 Then
                        move = True
                    ElseIf prev.x < curr.x-1 Then
                        move = True
                    End If
                    If prev.y > curr.y+1 Then
                        move = True
                    ElseIf prev.y < curr.y-1 Then
                        move = True
                    End If
                    
                    ' Perform move?
                    If move Then
                        If prev.x > curr.x Then
                            curr.x += 1
                        ElseIf prev.x < curr.x Then
                            curr.x -= 1
                        End If
                        If prev.y > curr.y Then
                            curr.y += 1
                        ElseIf prev.y < curr.y Then
                            curr.y -= 1
                        End If
                    End If

                    knots(j) = (curr.x, curr.y)

                    If j=9 Then
                        visited((curr.x,curr.y)) = True 
                    End If

                Next j

            Next i
        Next

        Console.WriteLine(visited.Count())

    End Sub
End Module
