Imports System.IO

Module Program
    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim pairs = input.Trim().Split(vbLf)
        Dim count = 0

        For Each pair In pairs

            Dim ranges = pair.Split(",")
            Dim range1 = ranges(0).Split("-")
            Dim range2 = ranges(1).Split("-")
            Dim r1Min = Integer.Parse(range1(0))
            Dim r1Max = Integer.Parse(range1(1))
            Dim r2Min = Integer.Parse(range2(0))
            Dim r2Max = Integer.Parse(range2(1))

            If (r1Min >= r2Min And r1Max <= r2Max) Or (r2Min >= r1Min And r2Max <= r1Max) Then
                count += 1
            End If

        Next

        Console.WriteLine(count)
    End Sub
End Module