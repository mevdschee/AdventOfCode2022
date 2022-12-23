Imports System.IO
Imports System.Text.RegularExpressions

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim parts = input.Split(vbLf & vbLf)
        Dim fields = New Dictionary(Of (x As Integer, y As Integer), Char)()
        Dim pos As (x As Integer, y As Integer) = (-1,0)
        Dim dir = 0

        Dim lines = parts(0).Split(vbLf)
        For y=0 To lines.Count-1
            For x=0 To lines(y).Length-1
                Dim c = lines(y)(x)
                If c<>" " Then
                    If pos.x = -1 Then
                       pos = (x, 0) 
                    End If                    
                    fields((x,y)) = c
                End If                
            Next x
        Next y

        For Each match In Regex.Matches(parts(1).Trim(), "([0-9]+)(L|R)")

            Dim steps = Integer.Parse(match.Groups(1).Value)
            Dim turn = match.Groups(2).Value
            
            For s=0 To steps-1
                Dim newpos As (x As Integer, y As Integer) = (pos.x - (dir-1) Mod 2, pos.y - (dir-2) Mod 2)
                If Not fields.ContainsKey(newpos) Then
                    Do While fields.ContainsKey((newpos.x + (dir-1) Mod 2, newpos.y + (dir-2) Mod 2))
                        newpos = (newpos.x + (dir-1) Mod 2, newpos.y + (dir-2) Mod 2)
                    Loop
                End If
                If fields(newpos)<>"#" Then
                    pos = newpos
                End If                
            Next s                    

            Select (turn)
                Case "L"
                    dir = (dir+3) Mod 4
                Case "R"
                    dir = (dir+1) Mod 4
            End Select

        Next

        Console.WriteLine(1000 * (pos.y+1) + 4 * (pos.x+1) + dir)

    End Sub

End Module
