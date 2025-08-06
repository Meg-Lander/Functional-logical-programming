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


[<EntryPoint>]
let main _ =
    helloWorld
    match solveQuadratic 1 2 1 with
    | Some (r1, r2) -> printfn $"Корни : {r1}, {r2}"
    | None -> printfn "Корней нет"
    0
