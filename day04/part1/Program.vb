Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input")
        Dim pairs = input.Trim().Split(vbLf)
        Dim count = 0

        For Each pair In pairs

            Dim ranges = pair.Split(",")
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