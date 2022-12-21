Imports System.IO
Imports System.Text.RegularExpressions

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Trim().Replace(vbCrLf, vbLf)
        Dim lines = input.Split(vbLf)
        Dim monkeys = New Dictionary(Of String, Long)()
        Dim expressions = New Dictionary(Of Integer,(name As String, arg1 As String, op As Char, arg2 As String))()
        
        For Each line In lines
            Dim match
            
            match = Regex.Match(line, "([a-z]+): ([0-9]+)")
            If match.Success Then
                Dim name = match.Groups(1).Value
                Dim value = Integer.Parse(match.Groups(2).Value)
                If name <> "humn" Then
                    monkeys(name) = value
                End If
            End If

            match = Regex.Match(line, "([a-z]+): ([a-z]+) ([-+*/]) ([a-z]+)")
            If match.Success Then
                Dim name = match.Groups(1).Value
                Dim arg1 = match.Groups(2).Value
                Dim op = match.Groups(3).Value
                Dim arg2 = match.Groups(4).Value
                If name = "root" Then
                    op = "="
                End If
                Select op
                    Case "-"
                        expressions(expressions.Count) = (name, arg1, "-", arg2)  '5=8-3
                        expressions(expressions.Count) = (arg1, name, "+", arg2)  '8=5+3
                        expressions(expressions.Count) = (arg2, arg1, "-", name)  '3=8-5
                    Case "+"
                        expressions(expressions.Count) = (name, arg1, "+", arg2)  '8=5+3
                        expressions(expressions.Count) = (arg1, name, "-", arg2)  '5=8-3
                        expressions(expressions.Count) = (arg2, name, "-", arg1)  '3=8-5
                    Case "*"
                        expressions(expressions.Count) = (name, arg1, "*", arg2)  '15=5*3
                        expressions(expressions.Count) = (arg1, name, "/", arg2)  '5=15/3
                        expressions(expressions.Count) = (arg2, name, "/", arg1)  '3=15*5
                    Case "/"                           
                        expressions(expressions.Count) = (name, arg1, "/", arg2)  '5=15/3
                        expressions(expressions.Count) = (arg1, name, "*", arg2)  '15=5*3
                        expressions(expressions.Count) = (arg2, arg1, "/", name)  '3=15/5
                    Case "="
                        expressions(expressions.Count) = (name, arg1, "=", arg2)
                        expressions(expressions.Count) = (name, arg2, "=", arg1)
                End Select
            End If
        Next

        Do Until expressions.Count=0

            For Each i In expressions.Keys
                
                Dim e = expressions(i)
                
                If e.name = "root" Then
                    If monkeys.ContainsKey(e.arg1) Then
                        monkeys(e.arg2) = monkeys(e.arg1)
                        expressions.Remove(i)
                    End If
                Else
                    If monkeys.ContainsKey(e.arg1) And monkeys.ContainsKey(e.arg2) Then
                        If Not monkeys.ContainsKey(e.name) Then
                            Select e.op
                                Case "-"
                                    monkeys(e.name) = monkeys(e.arg1) - monkeys(e.arg2)
                                Case "+"
                                    monkeys(e.name) = monkeys(e.arg1) + monkeys(e.arg2)
                                Case "*"
                                    monkeys(e.name) = monkeys(e.arg1) * monkeys(e.arg2)
                                Case "/"                           
                                    monkeys(e.name) = monkeys(e.arg1) \ monkeys(e.arg2)
                            End Select
                        End If
                        expressions.Remove(i)
                    End If
                End If                
            Next
   
        Loop

        Console.WriteLine(monkeys("humn"))

    End Sub

End Module
