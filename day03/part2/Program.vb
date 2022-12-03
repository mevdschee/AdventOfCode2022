Imports System.IO

Module Program

    Sub Main()

        Dim input = File.ReadAllText("input")
        Dim rucksacks = input.Trim().Split(vbLf)
        Dim sum = 0

        For i = 0 To rucksacks.Length()/3-1

            Dim rucksack1 = rucksacks(i*3+0)
            Dim rucksack2 = rucksacks(i*3+1)
            Dim rucksack3 = rucksacks(i*3+2)
            Dim letters = ""

            For j = 0 To rucksack1.Length()-1

                Dim letter = rucksack1.Substring(j,1)

                If rucksack2.Contains(letter) And rucksack3.Contains(letter) Then 
                    
                    If Not letters.Contains(letter) Then
                        letters &= letter
                    End If

                End If
                 
            Next

            For j = 0 To letters.Length()-1

                Dim letter = letters.Substring(j,1)
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