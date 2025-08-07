let respondToLanguage (language: string) =
    match language.ToLower() with
    | "f#" | "prolog" -> "Ты подлиза!"
    | _ -> "Неплохой выбор, но F# и Prolog лучше!"

let rec nod a b =
    if b = 0 then a
    else nod b (a % b)

let obhodProstComp number operation initial =
    let rec oper num acc =
        if num >= number then acc
        else
            if nod num number = 1 then
                oper (num + 1) (operation acc num)
            else
                oper (num + 1) acc
    oper 1 initial

let obhodProstCompWithCondition number operation initial condition =
    let rec oper num acc =
        if num >= number then acc
        else
            if nod num number = 1 && condition num then
                oper (num + 1) (operation acc num)
            else
                oper (num + 1) acc
    oper 1 initial

let eulerNumber n =
    obhodProstComp n (fun acc _ -> acc + 1) 0

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

         let n = 10

         let sumCoprimes = obhodProstComp (n) (sum_function) 0
         Console.WriteLine("Сумма взаимно простых чисел: {0}", sumCoprimes)

         let productCoprimes = obhodProstComp (n) (proiz_function) 1
         Console.WriteLine("Произведение взаимно простых чисел: {0}", productCoprimes)

         let minCoprime = obhodProstComp (n) (min_function) (n)
         Console.WriteLine("Минимум среди взаимно простых чисел: {0}", minCoprime)


         let NumbEuler = eulerNumber (n)
         Console.WriteLine("Число Эйлера: {0}", NumbEuler)


         let sum = obhodProstCompWithCondition (n) (sum_function) (0) (fun x -> x > 5)
         Console.WriteLine("Сумма взаимно простых чисел с {0}, которые больше 5: {1}", n, sum)

         let proiz = obhodProstCompWithCondition (n) (proiz_function) (1) (fun x -> x % 2 = 1)
         Console.WriteLine("Произведение чётных взаимно простых чисел с {0}: {1}", n, proiz)

         let sumPrimes = WorkWithDigits.sumPrimeDivisors n
         Console.WriteLine("Сумма простых делителей числа {0}: {1}", n, sumPrimes)

         let countOddDigits = WorkWithDigits.countOddDigitsGreaterThanThree number
         Console.WriteLine("Количество нечётных цифр > 3: {0}", countOddDigits)

         let prodDiv = WorkWithDigits.productDivisorsWithSmallerDigitSum n
         Console.WriteLine("Произведение делителей с меньшей суммой цифр: {0}", prodDiv)


         0