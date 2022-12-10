Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input")
        input = input.Replace("addx","cycle" & vbLf & "addx")
        Dim lines = input.Trim().Split(vbLf)
        Dim registers = new Dictionary(Of String, Integer)()
        registers("x") = 1
        
        Dim sum = 0
        For i = 1 To lines.Count()
            
            Dim parameters = lines(i-1).Split(" ")
            Dim operation = parameters(0)

            Dim pos = (i-1) Mod 40
            If pos = registers("x") Or pos = registers("x")-1 Or pos = registers("x")+1 Then
                Console.Write("#")
            Else
                Console.Write(".")
            End If
            If pos = 39 Then
                Console.WriteLine()
            End If

            Select operation
                Case "addx"
                    registers("x") += Integer.Parse(parameters(1))
            End Select
             
        Next i

    End Sub
End Module

'14640 too high