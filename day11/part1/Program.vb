Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Trim()
        Dim monkeys = new List(Of List(Of Integer))()
        Dim operations = new List(Of List(Of String))()
        Dim devisors = new List(Of Integer)()
        Dim divisibleSuccessor = new List(Of Integer)()
        Dim defaultSuccessor = new List(Of Integer)()
        Dim inspectionCount = new List(Of Integer)()

        For Each segment In input.Split(vbLf & vbLf)

            For Each line In segment.Split(vbLf)
                
                Dim parts = line.Split(":")
                Dim keys = parts(0).Trim().Split(" ")
                Dim values = parts(1).Trim().Split(" ")

                Select keys(0)
                    Case "Starting"
                        Dim monkey = new List(Of Integer)()
                        For Each number In values
                           monkey.Add(Integer.Parse(number.Trim(",")))
                        Next
                        monkeys.Add(monkey)
                    Case "Operation"
                        operations.Add(new List(Of String)({values(3),values(4)}))
                    Case "Test"
                        devisors.Add(values(2))
                    Case "If"
                        If keys(1)="true" Then
                            divisibleSuccessor.Add(Integer.Parse(values(3)))
                        Else
                            defaultSuccessor.Add(Integer.Parse(values(3)))
                        End If
                End Select

            Next
            
        Next

        For i = 0 to monkeys.Count()-1
            inspectionCount.Add(0)
        Next i

        For round = 1 to 20
            'Console.WriteLine("Round {0}:", round)

            For i = 0 to monkeys.Count()-1
                'Console.WriteLine("  Monkey {0}:", i)
                
                For Each level In monkeys(i)
                    'Console.WriteLine("    Monkey inspects an item with a worry level of {0}.", level)

                    inspectionCount(i) += 1
                    
                    Dim operand As Integer
                    If Not Integer.TryParse(operations(i)(1), operand) Then
                        operand = level
                    End If
                    
                    Select operations(i)(0)
                        Case "+"
                            level += operand
                        Case "*"
                            level *= operand
                    End Select
                    
                    'Console.WriteLine("      Worry level is {0} by {1} to {2}.", operations(i)(0), operations(i)(1), level)
                    level = level \ 3
                    'Console.WriteLine("      Monkey gets bored with item. Worry level is divided by {0} to {1}.", 3, level)
                    
                    Dim nextMonkey
                    If level Mod devisors(i) = 0 Then
                        'Console.WriteLine("      Current worry level is divisible by {0}.", devisors(i))
                        nextMonkey = divisibleSuccessor(i)
                    Else
                        'Console.WriteLine("      Current worry level is not divisible by {0}.", devisors(i))
                        nextMonkey = defaultSuccessor(i)
                    End If
                    
                    'Console.WriteLine("      Item with worry level {0} is thrown to monkey {1}.", level, nextMonkey)
                    monkeys(nextMonkey).Add(level)

                Next       

                monkeys(i).Clear()

            Next i

            'Console.WriteLine("After round {0}, the monkeys are holding items with these worry levels:", round)
            For i = 0 to monkeys.Count()-1
                'Console.WriteLine("Monkey {0}: {1}", i, String.Join(", ", monkeys(i).ToArray()))
            Next i

        Next round

        For i = 0 to monkeys.Count()-1
            inspectionCount.Add(0)
            'Console.WriteLine("Monkey {0} inspected items {1} times.", i, inspectionCount(i))
        Next i

        inspectionCount.Sort()
        inspectionCount.Reverse()

        Console.WriteLine(inspectionCount(0) * inspectionCount(1))

    End Sub
End Module

'14640 too high