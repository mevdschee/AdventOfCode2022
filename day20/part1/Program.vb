Imports System.IO

Module Program

    Function MathMod(x as Integer, m as Integer) As Integer
        Return (Math.Abs(x * m) + x) Mod m
    End Function

    Sub Main()

        Dim input = File.ReadAllText("input").Trim().Replace(vbCrLf, vbLf)
        Dim lines = input.Split(vbLf)
        Dim sequence = New List(Of Integer)()
        Dim numbers = New List(Of Integer)()

        For Each line In lines
            sequence.Add(numbers.Count)
            numbers.Add(Integer.Parse(line))
        Next

        For i=0 to numbers.Count-1

            Dim oldpos = sequence.IndexOf(i)
            sequence.RemoveAt(oldpos)
            Dim number = numbers(i)
            Dim newpos = MathMod(oldpos+number, sequence.Count)
            sequence.Insert(newpos, i)
            
        Next

        Dim sum = 0
        Dim start = sequence.IndexOf(numbers.IndexOf(0))
        sum += numbers(sequence((start+1000) Mod sequence.Count))
        sum += numbers(sequence((start+2000) Mod sequence.Count))
        sum += numbers(sequence((start+3000) Mod sequence.Count))
        Console.WriteLine(sum)
        
    End Sub

End Module
