Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input")
        Dim games = input.Trim().Split(vbLf)
        Dim points = 0

        For Each game In games

            Dim other = game.Substring(0,1)
            Dim you = game.Substring(2,1)

            Select you
                Case "X" ' Rock
                    points += 1
                Case "Y" ' Paper
                    points += 2
                Case "Z" ' Scissors
                    points += 3
            End Select

            Select other
                Case "A" ' Rock
                    Select you
                        Case "X" ' Rock
                            points += 3
                        Case "Y" ' Paper
                            points += 6
                        Case "Z" ' Scissors
                            points += 0
                    End Select
                Case "B" ' Paper
                    Select you
                        Case "X" ' Rock
                            points += 0
                        Case "Y" ' Paper
                            points += 3
                        Case "Z" ' Scissors
                            points += 6
                    End Select
                Case "C" ' Scissors
                    Select you
                        Case "X" ' Rock
                            points += 6
                        Case "Y" ' Paper
                            points += 0
                        Case "Z" ' Scissors
                            points += 3
                    End Select
            End Select

        Next

        Console.WriteLine(points)

    End Sub
  
End Module