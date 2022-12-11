Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input").Replace(vbCrLf, vbLf)
        Dim rucksacks = input.Trim().Split(vbLf)
        Dim sum = 0

        For Each rucksack In rucksacks

            Dim length = rucksack.Length()/2
            Dim match = rucksack.Substring(length,length)
            Dim letters = ""

            For i = 0 To length-1

                Dim letter = rucksack.Substring(i,1)

                If match.Contains(letter) Then 
                    
                    If Not letters.Contains(letter) Then
                        letters &= letter
                    End If

                End If
                 
            Next

            For i = 0 To letters.Length()-1

                Dim letter = letters.Substring(i,1)
                Dim priority = Asc(letter.ToLower()) - Asc("a") + 1
                
                If letter.ToUpper() = letter Then
                    priority += 26
                End If

                sum += priority

            Next

        Next

        Console.WriteLine(sum)

    End Sub
  
End Module