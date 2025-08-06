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

    
    let number = 12345
    let sum = sumDigitsUp number

    printfn $"Сумма цифр (вверх): {sum}"
    0
