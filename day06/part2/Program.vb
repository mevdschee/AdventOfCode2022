Imports System.IO

Module Program
    Sub Main()

        Dim input = File.ReadAllText("input").Trim()
        Dim recent = ""
        Dim result = 0
        
        For i = 0 To input.Length()-1
            recent &= input.Substring(i,1)
            If recent.Length() > 14
                recent = recent.Substring(1)
                Dim unique = True
                For j = 0 To recent.Length()-1
                    Dim other = recent.Substring(0,j) & recent.Substring(j+1)
                    If other.Contains(recent.Substring(j,1)) Then
                        unique = False
                    End If
                Next j
                If unique Then
                    result = i+1
                    Exit For
                End If
            End If
        Next i

        Console.WriteLine(result)
    End Sub
End Module