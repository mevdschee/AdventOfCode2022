Imports System.IO

Module Program

    Function ListDir(Path as String, Files as String) As Integer
        Dim sum = 0

    End Function
    

    Sub Main()

        Dim input = File.ReadAllText("input.test")
        Dim commands = input.Trim().Split(vbLf & "$")
        Dim sum = 0
        Dim path = ""


        For Each command In commands

            Dim words = command.Substring(1,command.IndexOf(vbLf)).Split(" ")

            If words(0) = "cd" Then
                If words(1) = ".." Then
                    path = path.S
                Else
                    path.Push(words(1))
                End If    
            ElseIf words(0) = "ls" Then
                Dim files = command.Substring(command.IndexOf(vbLf))

            End If

            
            For i = 1 To lines.Length()-1

            Next i

            Dim command = pair.Split(",")
            Dim range1 = ranges(0).Split("-")
            Dim range2 = ranges(1).Split("-")
            Dim r1min = Integer.Parse(range1(0))
            Dim r1max = Integer.Parse(range1(1))
            Dim r2min = Integer.Parse(range2(0))
            Dim r2max = Integer.Parse(range2(1))

            If (r1min >= r2min And r1max <= r2max) Or (r2min >= r1min And r2max <= r1max) Then

                count += 1

            End If

        Next

        Console.WriteLine(count)

    End Sub
End Module