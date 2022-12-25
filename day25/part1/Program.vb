Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim lines = input.Trim().Split(vbLf)
        Dim characters = "=-012"

        Dim total As Long = 0

        For Each line In lines
            Dim sum as Long = 0
            For p=0 To line.Length-1
                Dim c = line.Substring(line.Length-1-p,1)
                Dim n = characters.IndexOf(c)
                sum += (n-2)*Math.Pow(5,p)
            Next p
            total += sum
        Next
        
        Dim snafu = ""
        Do Until total=0
            Dim digit = total Mod 5
            total = total \ 5
            If digit > 2 Then
                digit -= 5
                total += 1
            End If
            snafu = characters.Substring(digit+2,1) & snafu
        Loop

        Console.WriteLine(snafu)

    End Sub

End Module
