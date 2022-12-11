Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim lines = input.Trim().Split(vbLf)
        Dim head = (x:=0, y:=0)
        Dim tail = (x:=0, y:=0)
        Dim visited = New Dictionary(Of (x as Integer, y as Integer), Boolean)()
        
        visited((0,0)) = True
        For Each line In lines
            Dim operation = line.Split(" ")
            Dim direction = operation(0)
            Dim distance = Integer.Parse(operation(1))
            For i = 0 To distance-1
                
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
                
                ' Should we move?
                Dim move = False
                If head.x > tail.x+1 Then
                    move = True
                ElseIf head.x < tail.x-1 Then
                    move = True
                End If
                If head.y > tail.y+1 Then
                    move = True
                ElseIf head.y < tail.y-1 Then
                    move = True
                End If
                
                ' Perform move?
                If move Then
                    If head.x > tail.x Then
                        tail.x += 1
                    ElseIf head.x < tail.x Then
                        tail.x -= 1
                    End If
                    If head.y > tail.y Then
                        tail.y += 1
                    ElseIf head.y < tail.y Then
                        tail.y -= 1
                    End If
                End If

                visited((tail.x,tail.y)) = True
                
            Next i
        Next

        Console.WriteLine(visited.Count())

    End Sub
End Module
