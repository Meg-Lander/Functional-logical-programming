let respondToLanguage (language: string) =
    match language.ToLower() with
    | "f#" | "prolog" -> "Ты подлиза!"
    | _ -> "Неплохой выбор, но F# и Prolog лучше!"

[<EntryPoint>]
     let main argv =
         let number = 12345
         
         let result = WorkWithDigits.sum_digits_down number
         printfn "Сумма цифр равна %d" result

         let result = WorkWithDigits.sum_digits_top number
         printfn "Сумма цифр равна %d" result

         let result = WorkWithDigits.big_function (false)
         System.Console.WriteLine (result (6))

         let number = 123

         let sum_function = fun a b -> a + b
         let proiz_function = fun a b -> a * b
         let min_function = fun a b -> if a < b then a else b
         let max_function = fun a b -> if a > b then a else b
     
         let sumResult = WorkWithDigits.operation_on_digits_numbers (number) (sum_function) (10)
         System.Console.WriteLine("Сумма цифр числа: {0}", sumResult)
     
         let proizResult = WorkWithDigits.operation_on_digits_numbers (number) (proiz_function) (1)
         System.Console.WriteLine("Произведение цифр числа: {0}", proizResult)
     
         let minResult = WorkWithDigits.operation_on_digits_numbers (number) (min_function) (9)
         System.Console.WriteLine("Минимальная цифра числа: {0}", minResult)
     
         let maxResult = WorkWithDigits.operation_on_digits_numbers (number) (max_function) (0)
         System.Console.WriteLine("Максимальная цифра числа: {0}", maxResult)




         let sumEvenDigits = operation_on_digits_numbers_with_condition (number) (sum_function) (0) (fun x -> x % 2 = 0)
         System.Console.WriteLine("Сумма четных цифр числа: {0}", sumEvenDigits)

         let proizDigits = operation_on_digits_numbers_with_condition (number) (proiz_function) (1) (fun x -> x > 5)
         System.Console.WriteLine("Произведение цифр больше 5: {0}", proizDigits)

         let minDigit = operation_on_digits_numbers_with_condition (number) (min_function) (9) (fun x -> x > 2)
         System.Console.WriteLine("Минимальная цифра больше 2: {0}", minDigit)



         Console.WriteLine("Какой твой любимый язык программирования?")
         Console.ReadLine() |> respondToLanguage |> Console.WriteLine


        let getResponse = respondToLanguage 
        Console.WriteLine("Какой твой любимый язык программирования?")
        let userLanguage = Console.ReadLine()
        let response = getResponse(userLanguage)
        Console.WriteLine(response)

         0