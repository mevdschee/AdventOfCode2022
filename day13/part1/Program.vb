Imports System.IO
Imports System.Text.Json
Imports System.Text.Json.Nodes

Module Program

    Function Compare(left as JsonNode, right as JsonNode) as Integer 

        If TypeOf left Is JsonValue And TypeOf right Is JsonValue Then

            Dim leftNumber = Integer.Parse(left.ToString())
            Dim rightNumber = Integer.Parse(right.ToString())

            'Console.WriteLine("- Compare {0} vs {1}",leftNumber, rightNumber)
            If leftNumber < rightNumber Then
                Return 1
            End If
            If leftNumber > rightNumber Then
                Return -1
            End If
            Return 0

        End If

        If Not TypeOf left Is JsonArray Then
            left = JsonSerializer.Deserialize(Of JsonNode)("[" & left.ToString() & "]")
        End If
        If Not TypeOf right Is JsonArray Then
            right = JsonSerializer.Deserialize(Of JsonNode)("[" & right.ToString() & "]")
        End If

        Dim leftArray as JsonArray = left.AsArray()
        Dim rightArray as JsonArray = right.AsArray()

        'Console.WriteLine("- Compare {0} vs {1}",leftArray.ToJsonString(),rightArray.ToJsonString())
        For i = 0 To leftArray.Count-1
            If i >= rightArray.Count Then
                Exit For
            End If
            Dim result = Compare(leftArray.Item(i),rightArray.Item(i))
            If result <> 0 Then
                Return result
            End If
        Next i
        
        If leftArray.Count < rightArray.Count Then
            Return 1
        End If
        If leftArray.Count > rightArray.Count Then
            Return -1
        End If
        Return 0

    End Function
    
    Sub Main()

        Dim input = File.ReadAllText("input").Trim().Replace(vbCrLf, vbLf)
        Dim pairs = input.Split(vbLf & vbLf)
        
        Dim sum = 0
        For i = 1 To pairs.Count

            Dim lines = pairs(i-1).Split(vbLf)
            Dim left = JsonSerializer.Deserialize(Of JsonNode)(lines(0))
            Dim right = JsonSerializer.Deserialize(Of JsonNode)(lines(1))
            
            'Console.WriteLine("== Pair {0} ==",i)
            Dim result = Compare(left,right)
            If result = 1 Then
                sum += i
            End If
            
        Next i

        Console.WriteLine(sum)

    End Sub
    
End Module
