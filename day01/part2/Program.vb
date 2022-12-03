Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input")
        Dim elves = input.Trim().Split(vbLf & vbLf)
        Dim totals(elves.Count()) as Integer

        For i = 0 To elves.Count() - 1

            Dim counts = elves(i).Split(vbLf)
            Dim total = 0
            For Each count In counts
                total += Integer.Parse(count)
            Next
            totals(i) = total

        Next

        Array.Sort(totals)
        Array.Reverse(totals)
        Console.WriteLine(totals(0)+totals(1)+totals(2))

    End Sub
  
End Module