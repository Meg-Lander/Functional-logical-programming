open WorkWithLists


let respondToLanguage (language: string) =
    match language.ToLower() with
    | "f#" | "prolog" -> "Ты подлиза!"
    | _ -> "Неплохой выбор, но F# и Prolog лучше!"


let getFunctionByNumber = function
    | 1 -> WorkWithDigits.sumPrimeDivisors
    | 2 -> WorkWithDigits.countOddDigitsGreaterThanThree
    | 3 -> WorkWithDigits.productDivisorsWithSmallerDigitSum
    | _ -> failwith "Неверный номер функции"

let WithCurrying (funcNumber, arg) =
    let selectedFunction = getFunctionByNumber funcNumber
    selectedFunction arg

let WithSuperpos (funcNumber, arg) =
    (getFunctionByNumber >> (fun f -> f arg)) funcNumber


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




         let sumEvenDigits = WorkWithDigits.operation_on_digits_numbers_with_condition (number) (sum_function) (0) (fun x -> x % 2 = 0)
         System.Console.WriteLine("Сумма четных цифр числа: {0}", sumEvenDigits)

         let proizDigits = WorkWithDigits.operation_on_digits_numbers_with_condition (number) (proiz_function) (1) (fun x -> x > 5)
         System.Console.WriteLine("Произведение цифр больше 5: {0}", proizDigits)

         let minDigit = WorkWithDigits.operation_on_digits_numbers_with_condition (number) (min_function) (9) (fun x -> x > 2)
         System.Console.WriteLine("Минимальная цифра больше 2: {0}", minDigit)



         Console.WriteLine("Какой твой любимый язык программирования?")
         Console.ReadLine() |> respondToLanguage |> Console.WriteLine


         let getResponse = respondToLanguage 
         Console.WriteLine("Какой твой любимый язык программирования?")
         let userLanguage = Console.ReadLine()
         let response = getResponse(userLanguage)
         Console.WriteLine(response)

         let n = 10

         let sumCoprimes =WorkWithDigits.obhodProstComp (n) (sum_function) 0
         Console.WriteLine("Сумма взаимно простых чисел: {0}", sumCoprimes)

         let productCoprimes = WorkWithDigits.obhodProstComp (n) (proiz_function) 1
         Console.WriteLine("Произведение взаимно простых чисел: {0}", productCoprimes)

         let minCoprime = WorkWithDigits.obhodProstComp (n) (min_function) (n)
         Console.WriteLine("Минимум среди взаимно простых чисел: {0}", minCoprime)


         let NumbEuler = WorkWithDigits.eulerNumber (n)
         Console.WriteLine("Число Эйлера: {0}", NumbEuler)


         let sum = WorkWithDigits.obhodProstCompWithCondition (n) (sum_function) (0) (fun x -> x > 5)
         Console.WriteLine("Сумма взаимно простых чисел с {0}, которые больше 5: {1}", n, sum)

         let proiz = WorkWithDigits.obhodProstCompWithCondition (n) (proiz_function) (1) (fun x -> x % 2 = 1)
         Console.WriteLine("Произведение чётных взаимно простых чисел с {0}: {1}", n, proiz)


         let sumPrimes = WorkWithDigits.sumPrimeDivisors n
         Console.WriteLine("Сумма простых делителей числа {0}: {1}", n, sumPrimes)

         let countOddDigits = WorkWithDigits.countOddDigitsGreaterThanThree number
         Console.WriteLine("Количество нечётных цифр > 3: {0}", countOddDigits)

         let prodDiv = WorkWithDigits.productDivisorsWithSmallerDigitSum n
         Console.WriteLine("Произведение делителей с меньшей суммой цифр: {0}", prodDiv)

         Console.WriteLine("Введите кортеж (номер функции, аргумент), например: 2,1234")
         let input = Console.ReadLine()
         let parsedInput = input.Split(',') |> Array.map int
         let funcNumber = parsedInput.[0]
         let arg = parsedInput.[1]

         let resultCurrying = WithCurrying (funcNumber, arg)
         Console.WriteLine("Результат (каррирование): {0}", resultCurrying)

         let resultSuperpos = WithSuperpos (funcNumber, arg)
         Console.WriteLine("Результат (суперпозиция): {0}", resultSuperpos)


         Console.WriteLine("Введите количество элементов: ")
         let n = Console.ReadLine() |> int
         let result = readList n
         Console.WriteLine("Полученный список: {0}", result)


         0