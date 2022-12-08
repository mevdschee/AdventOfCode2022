Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input")
        Dim lines = input.Trim().Split(vbLf)
        Dim fields = New Dictionary(Of (x as Integer, y as Integer), Integer)()
        Dim width = lines(0).Length()
        Dim height = lines.Length()

        For y = 0 To height-1
            For x = 0 To width-1
                fields((x,y)) = Integer.Parse(lines(y).Substring(x,1))
            Next x
        Next y

        Dim best = 0
        For sy = 0 To height-1
            For sx = 0 To width-1
                
                Dim score = 1
                Dim visible
                ' left to right
                visible = 0
                For x = sx+1 To width-1
                    visible += 1
                    If fields((x,sy))>=fields((sx,sy)) Then
                        Exit For
                    End If
                Next x
                score *= visible
                ' right to left
                visible = 0
                For x = sx-1 To 0 Step -1
                    visible += 1
                    If fields((x,sy))>=fields((sx,sy)) Then
                        Exit For
                    End If
                Next x
                score *= visible
                ' top to bottom
                visible = 0
                For y = sy+1 To height-1
                    visible += 1
                    If fields((sx,y))>=fields((sx,sy)) Then
                        Exit For
                    End If
                Next y
                score *= visible
                ' bottom to top
                visible = 0
                For y = sy-1 To 0 Step -1
                    visible += 1
                    If fields((sx,y))>=fields((sx,sy)) Then
                        Exit For
                    End If
                Next y
                score *= visible
                
                If score > best Then 
                    best = score
                End If
                
            Next sx
        Next sy
        
        Console.WriteLine(best)

    End Sub
End Module
