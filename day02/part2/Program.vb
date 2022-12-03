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
                Case "X" ' Lose
                    points += 0
                Case "Y" ' Draw
                    points += 3
                Case "Z" ' Win
                    points += 6
            End Select

            Select other
                Case "A" ' Rock
                    Select you
                        Case "X" ' Lose = Scissors
                            points += 3
                        Case "Y" ' Draw = Rock
                            points += 1
                        Case "Z" ' Win = Paper
                            points += 2
                    End Select
                Case "B" ' Paper
                    Select you
                        Case "X" ' Lose = Rock
                            points += 1
                        Case "Y" ' Draw = Paper
                            points += 2
                        Case "Z" ' Win = Scissors
                            points += 3
                    End Select
                Case "C" ' Scissors
                    Select you
                        Case "X" ' Lose = Paper
                            points += 2
                        Case "Y" ' Draw = Scissors
                            points += 3
                        Case "Z" ' Win = Rock
                            points += 1
                    End Select
            End Select

        Next

        Console.WriteLine(points)

    End Sub
  
End Module