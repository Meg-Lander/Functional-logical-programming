open System

// Задание 1 — Hello World
let helloWorld = 
    printfn "Hello World"

// Задание 2 — Решение квадратного уравнения
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

// Задание 3 — Площадь круга и объём цилиндра
let circleArea r = 3.14 * r * r
let cylinderVolume h = circleArea >> (*) h

// Задание 4 — Сумма цифр (рекурсия вверх)
let rec sumDigitsUp n =
    match n with
    | x when x < 10 -> x
    | _ -> (n % 10) + sumDigitsUp (n / 10)


// Задание 5 — Сумма цифр (рекурсия вниз и хвоставая)
let rec sumDigitsDown n acc =
    match n with
    | 0 -> acc
    | _ -> sumDigitsDown (n / 10) (acc + (n % 10))

let sumDigitsTail n = sumDigitsDown n 0

// Задание 6 — Функция, возвращающая функцию
let getOperation flag =
    match flag with
    | true -> sumDigitsTail
    | false ->
        let rec factorial n =
            match n with
            | 0 -> 1
            | _ -> n * factorial (n - 1)
        factorial

// Задание 7 — обход цифр
let rec processDigits num func acc =
    match num with
    | 0 -> acc
    | _ -> processDigits (num / 10) func (func acc (num % 10))

// Задание 9 — Обход цифр по условию
let rec processDigitsIf num func acc predicate =
    match num with
    | 0 -> acc
    | _ ->
        let digit = num % 10
        let next = if predicate digit then func acc digit else acc
        processDigitsIf (num / 10) func next predicate

[<EntryPoint>]
let main _ =
    helloWorld

    match solveQuadratic 1 2 1 with
    | Some (r1, r2) -> printfn $"Корни : {r1}, {r2}"
    | None -> printfn "Корней нет"

    let radius = 3.0
    let height = 5.0

    let area = circleArea radius
    let volume = cylinderVolume height radius

    printfn $"Площадь круга: {area}"
    printfn $"Объём цилиндра: {volume}"

    
    let num = 12345
    let sumUp = sumDigitsUp num
    let sumTail = sumDigitsTail num
    printfn $"Сумма цифр вверх: {sumUp}"
    printfn $"Сумма цифр вниз: {sumTail}"

    let f1 = getOperation true
    let f2 = getOperation false
    printfn $"Функция по условию (true): {f1 4321}"
    printfn $"Функция по условию (false): {f2 5}"

    printfn $"Обход суммы: {processDigits 12345 (fun a b -> a + b) 0}"
    printfn $"Обход произведения: {processDigits 12345 (fun a b -> a * b) 1}"
    printfn $"Минимум цифр: {processDigits 12345 min 9}"
    printfn $"Максимум цифр: {processDigits 12345 max 0}"

    printfn $"Сумма чётных цифр: {processDigitsIf 123456 (+) 0 (fun x -> x % 2 = 0)}"
    printfn $"Произведение нечётных: {processDigitsIf 13579 (*) 1 (fun x -> x % 2 <> 0)}"
    printfn $"Максимум > 5: {processDigitsIf 987654321 max 0 (fun x -> x > 5)}"
    0
