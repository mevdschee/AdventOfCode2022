Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf).Trim()
        Dim monkeys = new List(Of List(Of Long))()
        Dim operations = new List(Of List(Of String))()
        Dim devisors = new List(Of Long)()
        Dim divisibleSuccessor = new List(Of Long)()
        Dim defaultSuccessor = new List(Of Long)()
        Dim inspectionCount = new List(Of Long)()
        Dim reductionModulo = 1

        For Each segment In input.Split(vbLf & vbLf)

            For Each line In segment.Split(vbLf)
                
                Dim parts = line.Split(":")
                Dim keys = parts(0).Trim().Split(" ")
                Dim values = parts(1).Trim().Split(" ")

                Select keys(0)
                    Case "Starting"
                        Dim monkey = new List(Of Long)()
                        For Each number In values
                           monkey.Add(Long.Parse(number.Trim(",")))
                        Next
                        monkeys.Add(monkey)
                    Case "Operation"
                        operations.Add(new List(Of String)({values(3),values(4)}))
                    Case "Test"
                        devisors.Add(Long.Parse(values(2)))
                        reductionModulo *= Long.Parse(values(2))
                    Case "If"
                        If keys(1)="true" Then
                            divisibleSuccessor.Add(Long.Parse(values(3)))
                        Else
                            defaultSuccessor.Add(Long.Parse(values(3)))
                        End If
                End Select

            Next
            
        Next

        For i = 0 to monkeys.Count()-1
            inspectionCount.Add(0)
        Next i

        For round = 1 to 10000

            For i = 0 to monkeys.Count()-1
                
                For Each level In monkeys(i)

                    inspectionCount(i) += 1
                    
                    Dim operand As Long
                    If Not Long.TryParse(operations(i)(1), operand) Then
                        operand = level
                    End If
                    
                    Select operations(i)(0)
                        Case "+"
                            level += operand
                        Case "*"
                            level *= operand
                    End Select
                    
                    level = level Mod reductionModulo
                    
                    Dim nextMonkey
                    If level Mod devisors(i) = 0 Then
                        nextMonkey = divisibleSuccessor(i)
                    Else
                        nextMonkey = defaultSuccessor(i)
                    End If

                    monkeys(nextMonkey).Add(level)

                Next       

                monkeys(i).Clear()

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
