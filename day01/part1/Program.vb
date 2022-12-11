Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim elves = input.Trim().Split(vbLf & vbLf)
        Dim max = 0

        For Each elve In elves

            Dim counts = elve.Split(vbLf)
            Dim total = 0            

            For Each count In counts
                total += Integer.Parse(count)
            Next

            If total>max Then
                max = total
            End If

        Next

        Console.WriteLine(max)

    End Sub
  
End Module