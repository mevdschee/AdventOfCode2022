Imports System.IO

Module Program

    Function MathMod(x as Long, m as Long) As Long
        Return (Math.Abs(x * m) + x) Mod m
    End Function

    Sub Main()

        Dim input = File.ReadAllText("input").Trim().Replace(vbCrLf, vbLf)
        Dim lines = input.Split(vbLf)
        Dim sequence = New List(Of Integer)()
        Dim numbers = New List(Of Long)()

        For Each line In lines
            sequence.Add(numbers.Count)
            numbers.Add(Long.Parse(line)*811589153)
        Next

        For time = 1 To 10
            For i=0 to numbers.Count-1

                Dim oldpos = sequence.IndexOf(i)
                sequence.RemoveAt(oldpos)
                Dim newpos = MathMod(oldpos+numbers(i), sequence.Count)
                sequence.Insert(newpos, i)
                
            Next
        Next

        Dim sum as Long = 0
        Dim start = sequence.IndexOf(numbers.IndexOf(0))
        sum += numbers(sequence((start+1000) Mod sequence.Count))
        sum += numbers(sequence((start+2000) Mod sequence.Count))
        sum += numbers(sequence((start+3000) Mod sequence.Count))
        Console.WriteLine(sum)
        
    End Sub

End Module
