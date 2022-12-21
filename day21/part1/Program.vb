Imports System.IO
Imports System.Text.RegularExpressions

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Trim().Replace(vbCrLf, vbLf)
        Dim lines = input.Split(vbLf)
        Dim monkeys = New Dictionary(Of String, Long)()
        Dim expressions = New Dictionary(Of String, (arg1 as String, op as Char, arg2 as String))()
        
        For Each line In lines
            Dim match
            match = Regex.Match(line, "([a-z]+): ([0-9]+)")
            If match.Success Then
                Dim name = match.Groups(1).Value
                Dim value = Integer.Parse(match.Groups(2).Value)
                monkeys(name) = value
            End If
            match = Regex.Match(line, "([a-z]+): ([a-z]+) ([-+*/]) ([a-z]+)")
            If match.Success Then
                Dim name = match.Groups(1).Value
                Dim arg1 = match.Groups(2).Value
                Dim op = match.Groups(3).Value
                Dim arg2 = match.Groups(4).Value
                expressions(name) = (arg1,op,arg2)
            End If
        Next

        Do Until expressions.Count=0
            For Each name In expressions.Keys
                Dim e = expressions(name)
                If monkeys.ContainsKey(e.arg1) And monkeys.ContainsKey(e.arg2) Then
                    Select e.op
                        Case "-"
                            monkeys(name) = monkeys(e.arg1) - monkeys(e.arg2)
                        Case "+"
                            monkeys(name) = monkeys(e.arg1) + monkeys(e.arg2)
                        Case "*"
                            monkeys(name) = monkeys(e.arg1) * monkeys(e.arg2)
                        Case "/"                           
                            monkeys(name) = monkeys(e.arg1) \ monkeys(e.arg2)
                    End Select
                    expressions.Remove(name)
                End If            
            Next                        
        Loop
        
        Console.WriteLine(monkeys("root"))

    End Sub

End Module
