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
    0
