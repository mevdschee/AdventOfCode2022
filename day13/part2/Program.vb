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
        Dim lines = input.Replace(vbLf & vbLf, vbLf).Split(vbLf)
        
        Dim packets = new List(Of JsonNode)()
        For Each line In lines
            packets.Add(JsonSerializer.Deserialize(Of JsonNode)(line))
        Next
        packets.Add(JsonSerializer.Deserialize(Of JsonNode)("[[2]]"))
        packets.Add(JsonSerializer.Deserialize(Of JsonNode)("[[6]]"))

        packets.Sort(Function(left As JsonNode, right As JsonNode)
               Return -Compare(left, right)
           End Function)

        Dim result = 1
        For i = 1 To packets.Count
            Dim packet = packets(i-1).ToJsonString()
            'Console.WriteLine(packet)
            If packet = "[[2]]" Or packet = "[[6]]" Then
                result *= i
            End If            
        Next
         
        Console.WriteLine(result)

    End Sub
    
End Module
