open System

let helloWorld = 
    printfn "Hello World"

let solveQuadratic a b c =
            let a', b', c' = float a, float b, float c
            let d = b' * b' - 4.0 * a' * c'
            match d with
            | d when d > 0.0 ->
                let x1 = (-b' + sqrt d) / (2.0 * a')
                let x2 = (-b' - sqrt d) / (2.0 * a')
                Some (x1, x2)
            | 0.0 ->
                let x = -b' / (2.0 * a')
                Some (x, x)
            | _ -> None

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

         helloWorld
    
         // Задание 2 — Решение квадратного уравнения
         let a, b, c = 1, -3, 2
         let quadResult = solveQuadratic a b c
         match quadResult with
         | Some (x1, x2) -> printfn "Решения квадратного уравнения: x1 = %f, x2 = %f" x1 x2
         | None -> printfn "Квадратное уравнение не имеет корней"
         
         // Задание 3 — Площадь круга и объём цилиндра
         let circleArea r = 3.14 * r * r
         let cylinderVolume h = circleArea >> (*) h

         let radius = 5.0
         let areaResult = circleArea radius
         printfn "Площадь круга с радиусом %f: %f" radius areaResult
    
         let height = 10.0
         let volumeResult = cylinderVolume height radius
         printfn "Объем цилиндра с радиусом %f и высотой %f: %f" radius height volumeResult


         // Задание 4 - Написать рекурсивную функцию для нахождения суммы цифр числа. Ис-пользовать рекурсию вверх
         let number = 12345
         
         let result = WorkWithDigits.sum_digits_down number
         printfn "Сумма цифр равна вниз %d" result

         // Задание 5 - Использовать рекурсию вниз. Написать  рекурсивную функцию с телом выражение 
         // для нахождения суммы цифр числа на основе хвостовой рекурсии.
         let result = WorkWithDigits.sum_digits_top number
         printfn "Сумма цифр равна вверх %d" result

         // Задание 6 - функция, которая принимает аргумент и возвращает функцию. Первый аргумент, тип логический, 
         // если он ИСТИНА, возвращаем функцию, счи-тающую сумму цифр числа, если он ЛОЖЬ, возвращаем функцию, 
         // считающую факториал числа
         let result = WorkWithDigits.big_function (false)
         System.Console.WriteLine (result (6))

         // Задание 7 - функции обхода числа, которая выполняет операции на цифрами числа, принимает три аргумента, число, \
         // функция (например, сумма, произведе-ние, минимум, максимум) и инициализирующее значение. 
         // Функция должна иметь два Int аргумента и возвращать Int
         let number = 123

         let sum_function = fun a b -> a + b
         let proiz_function = fun a b -> a * b
         let min_function = fun a b -> 
            match a < b with
            | true -> a
            | false -> b
         let max_function = fun a b -> 
            match a > b with
            | true -> a
            | false -> b
         

         // Задание 8 - Для тестирования и передачи аргумента использовать лямбда выражения. 
         // Инициализирующее заполнение должно иметь значение по умолчанию
         let sumResult = WorkWithDigits.operation_on_digits_numbers (number) (sum_function) (10)
         System.Console.WriteLine("Сумма цифр числа: {0}", sumResult)
     
         let proizResult = WorkWithDigits.operation_on_digits_numbers (number) (proiz_function) (1)
         System.Console.WriteLine("Произведение цифр числа: {0}", proizResult)
     
         let minResult = WorkWithDigits.operation_on_digits_numbers (number) (min_function) (9)
         System.Console.WriteLine("Минимальная цифра числа: {0}", minResult)
     
         let maxResult = WorkWithDigits.operation_on_digits_numbers (number) (max_function) (0)
         System.Console.WriteLine("Максимальная цифра числа: {0}", maxResult)



         // Задание 9, 10 - Реализовать функцию обход числа с условием, которая выполняет опера-ции над цифрами, если цифры удовлетворяют заданному условию. 
         // Аргу-менты функции: число, функция с двумя аргументами Int, возвращающая Int, 
         // инициализирующее заполнение, функция с одним аргументом Int, возвращающая true-false.
         // Проверить функцию на 3 различных примерах.
         let sumEvenDigits = WorkWithDigits.operation_on_digits_numbers_with_condition (number) (sum_function) (0) (fun x -> x % 2 = 0)
         System.Console.WriteLine("Сумма четных цифр числа: {0}", sumEvenDigits)

         let proizDigits = WorkWithDigits.operation_on_digits_numbers_with_condition (number) (proiz_function) (1) (fun x -> x > 5)
         System.Console.WriteLine("Произведение цифр больше 5: {0}", proizDigits)

         let minDigit = WorkWithDigits.operation_on_digits_numbers_with_condition (number) (min_function) (9) (fun x -> x > 2)
         System.Console.WriteLine("Минимальная цифра больше 2: {0}", minDigit)



         // Задание 11 - Спросить у пользователя, какой язык у него любимый, если это F# или Prolog, ответь пользователю, что он — подлиза, 
         // для других языков при-думать комментарий, реализовать функцию, 
         // принимающую аргументом ответ пользователя и возвращающую наш ответ пользователю
         Console.WriteLine("Какой твой любимый язык программирования?")
         Console.ReadLine() |> respondToLanguage |> Console.WriteLine

         // Задание 12 - помощью только оператора суперпозиции, потом только с помощью опе-ратора каррирования.
         let getResponse = respondToLanguage 
         Console.WriteLine("Какой твой любимый язык программирования?")
         let userLanguage = Console.ReadLine()
         let response = getResponse(userLanguage)
         Console.WriteLine(response)


         // Задание 13 - Написать функцию обход взаимно простых компонентов числа, которая выполняет операции над числами, 
         // взаимно простыми с данным, прини-мает три аргумента, число, функция (например, сумма, произведение, минимум, максимум, количество) 
         // и инициализирующее значение
         let n = 10

         let sumCoprimes =WorkWithDigits.obhodProstComp (n) (sum_function) 0
         Console.WriteLine("Сумма взаимно простых чисел: {0}", sumCoprimes)

         let productCoprimes = WorkWithDigits.obhodProstComp (n) (proiz_function) 1
         Console.WriteLine("Произведение взаимно простых чисел: {0}", productCoprimes)

         let minCoprime = WorkWithDigits.obhodProstComp (n) (min_function) (n)
         Console.WriteLine("Минимум среди взаимно простых чисел: {0}", minCoprime)


         // Задание 14 - Построить на её основе функции для вычисления числа Эйлера
         let NumbEuler = WorkWithDigits.eulerNumber (n)
         Console.WriteLine("Число Эйлера: {0}", NumbEuler)


         // Задание 15 - На основе написанных функций построить функции обход  взаимнопро-стых с условием.
         let sum = WorkWithDigits.obhodProstCompWithCondition (n) (sum_function) (0) (fun x -> x > 5)
         Console.WriteLine("Сумма взаимно простых чисел с {0}, которые больше 5: {1}", n, sum)

         let proiz = WorkWithDigits.obhodProstCompWithCondition (n) (proiz_function) (1) (fun x -> x % 2 = 1)
         Console.WriteLine("Произведение чётных взаимно простых чисел с {0}: {1}", n, proiz)


         // Задание 16 - Составить 3 функции для работы с цифрами или делителей числа на ос-новании варианта 
         // с использованием только хвостовой рекурсии и функ-ций высших порядков

         // Метод 1. Найти сумму простых делителей числа. 
         let sumPrimes = WorkWithDigits.sumPrimeDivisors n
         Console.WriteLine("Сумма простых делителей числа {0}: {1}", n, sumPrimes)

         // Метод 2. Найти количество нечетных цифр числа, больших 3
         let countOddDigits = WorkWithDigits.countOddDigitsGreaterThanThree number
         Console.WriteLine("Количество нечётных цифр > 3: {0}", countOddDigits)

         // Метод 3. Найти прозведение таких делителей числа, сумма цифр которых меньше, чем сумма цифр исходного числа
         let prodDiv = WorkWithDigits.productDivisorsWithSmallerDigitSum n
         Console.WriteLine("Произведение делителей с меньшей суммой цифр: {0}", prodDiv)


         // Задание 20 - Напишите программу, в которой пользователь вводит кортеж из двух чисел, где первое число это 
         // номер одной из трех функций вашего варианта, второе число аргумент этой функции. Построить функцию, 
         // которая при-нимает номер от 1 до 3 и возвращает одну из трех написанных функций. Далее программа выполняет 
         // указанную функцию и выдает результат на экран. Для реализации функции main использовать только оператор кар-рирования, 
         // потом только оператор суперпозиции
         Console.WriteLine("Введите кортеж (номер функции, аргумент), например: 2,1234")
         let input = Console.ReadLine()
         let parsedInput = input.Split(',') |> Array.map int
         let funcNumber = parsedInput.[0]
         let arg = parsedInput.[1]

         let resultCurrying = WithCurrying (funcNumber, arg)
         Console.WriteLine("Результат (каррирование): {0}", resultCurrying)

         let resultSuperpos = WithSuperpos (funcNumber, arg)
         Console.WriteLine("Результат (суперпозиция): {0}", resultSuperpos)



         0