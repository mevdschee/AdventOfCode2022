Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim lines = input.Trim().Split(vbLf)
        Dim path = ""
        Dim files = New Dictionary(Of String, Integer)()
        Dim dirs = New Dictionary(Of String, ArrayList)()

        For Each line In lines
            
            Dim words = line.Split(" ")
            If words(0) = "$" Then
                If words(1) = "cd" Then
                    If words(2) = "/" Then
                        path = "/"
                    ElseIf words(2) = ".." Then
                        path = path.Substring(0,path.LastIndexOf("/"))
                    Else
                        path &= "/" & words(2)
                    End If    
                ElseIf words(1) = "ls" Then
                    files(path) = 0
                    dirs(path) = new ArrayList()
                End If
            Else
                If words(0) = "dir" Then
                    dirs(path).Add(path & "/" & words(1))
                Else
                    files(path) += Integer.Parse(words(0))
                End If
            End If

        Next

        Dim sorted = files.OrderBy(Function(kv) kv.Key.Split("/").Length()*-1)
        For Each kv In sorted
            Dim sum = kv.Value
            For Each path In dirs(kv.Key)
                sum += files(path)
            Next
            files(kv.Key) = sum
        Next

        Dim total = 0
        For Each kv In files
            Dim sum = kv.Value
            If sum <= 100000 Then
                total += sum
            End If
        Next

        Console.WriteLine(total)

    End Sub
End Module
