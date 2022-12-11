Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim lines = input.Trim().Split(vbLf)
        Dim fields = New Dictionary(Of (x as Integer, y as Integer), Integer)()
        Dim visible = New Dictionary(Of (x as Integer, y as Integer), Boolean)()
        Dim width = lines(0).Length()
        Dim height = lines.Length()

        For y = 0 To height-1
            For x = 0 To width-1
                fields((x,y)) = Integer.Parse(lines(y).Substring(x,1))
                visible((x,y)) = False
            Next x
        Next y

        ' left to right
        For y = 0 To height-1
            Dim top = -1
            For x = 0 To width-1
                If fields((x,y))>top Then
                    top = fields((x,y))
                    visible((x,y)) = True
                End If
            Next x
        Next y
        ' right to left
        For y = 0 To height-1
            Dim top = -1
            For x = width-1 To 0 Step -1
                If fields((x,y))>top Then
                    top = fields((x,y))
                    visible((x,y)) = True
                End If
            Next x
        Next y
        ' top to bottom
        For x = 0 To width-1
            Dim top = -1
            For y = 0 To height-1
                If fields((x,y))>top Then
                    top = fields((x,y))
                    visible((x,y)) = True
                End If
            Next y
        Next x
        ' bottom to top
        For x = 0 To width-1
            Dim top = -1
            For y = height-1 To 0 Step -1
                If fields((x,y))>top Then
                    top = fields((x,y))
                    visible((x,y)) = True
                End If
            Next y
        Next x
        
        Dim total = 0
        For y = 0 To height-1
            For x = 0 To width-1
                If visible((x,y)) Then
                    total += 1
                End If
            Next x
        Next y
        
        Console.WriteLine(total)

    End Sub
End Module
