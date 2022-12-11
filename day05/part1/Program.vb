Imports System.IO

Module Program
    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim parts = input.Split(vbLf & vbLf)
        Dim lines = parts(0).Split(vbLf)
        Dim columns = (lines(0).Length() + 1)/4

        Dim stacks(columns) as Stack
        For i = 0 To columns - 1
            stacks(i) = new Stack()
        Next i

        For Each line In lines

            If line = "" Then
                Exit For
            End If

            For i = 0 To columns - 1

                Dim letter = line.Substring(i*4 + 1, 1)

                If Asc(letter) >= Asc("A") And Asc(letter) <= Asc("Z") Then
                    stacks(i).Push(letter)
                End If

            Next i


        Next

        Dim reverse(columns) as Stack
        For i = 0 To columns - 1

            reverse(i) = new Stack()
            Do While stacks(i).Count() > 0
                reverse(i).Push(stacks(i).Pop())
            Loop

        Next i
        stacks = reverse

        lines = parts(1).Trim().Split(vbLf)
        For Each line In lines

            Dim words = line.Split(" ")
            Dim count = Integer.Parse(words(1))
            Dim source = Integer.Parse(words(3)) - 1
            Dim destination = Integer.Parse(words(5)) - 1

            For i = 0 To count - 1

                stacks(destination).Push(stacks(source).Pop())

            Next i

        Next

        Dim solution = ""
        For i = 0 To columns - 1
            solution &= stacks(i).Pop()
        Next i

        Console.WriteLine(solution)
    End Sub
End Module