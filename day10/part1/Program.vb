Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        input = input.Replace("addx","cycle" & vbLf & "addx")
        Dim lines = input.Trim().Split(vbLf)
        
        Dim registers = new Dictionary(Of String, Integer)()
        registers("x") = 1
        
        Dim sum = 0
        For i = 1 To lines.Count()
            
            Dim parameters = lines(i-1).Split(" ")
            Dim operation = parameters(0)

            If (i+20) Mod 40 = 0 Then
                sum += i * registers("x")
            End If

            Select operation
                Case "addx"
                    registers("x") += Integer.Parse(parameters(1))
            End Select
             
        Next i

        Console.WriteLine(sum)

    End Sub
End Module

'14640 too high