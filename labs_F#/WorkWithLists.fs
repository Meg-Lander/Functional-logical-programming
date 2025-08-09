module WorkWithLists

let readList n =
        let rec readNumbers remaining accum =
            match remaining with
            | 0 -> List.rev accum
            | x when x > 0 ->
                System.Console.WriteLine("Введите число: ")
                match System.Int32.TryParse(System.Console.ReadLine()) with
                | true, num -> readNumbers (remaining - 1) (num :: accum)
                | false, _ -> 
                    System.Console.WriteLine("Введено не число!")
                    readNumbers remaining accum  
            | _ -> failwith "Ошибка в рекурсивной функции"

        match n with 
        | x when x > 0 -> readNumbers n []
        | x when x < 0 -> failwith "Количество элементов не может быть отрицательным"
        | 0 -> []


let printList list =
    let rec printHeadList remaining =
        match remaining with
        | [] -> ()
        | head :: tail ->
            printfn "%A" head  
            printHeadList tail
    
    printHeadList list

let filteredFold (list : int list) (f : int -> int -> int) (p : int -> bool) (acc : int) : int =
    let rec loop lst currentAcc =
        match lst with
        | [] -> currentAcc
        | head :: tail ->
            let newAcc =
                match p head with
                | true -> f currentAcc head
                | false -> currentAcc
            loop tail newAcc
    loop list acc

let sumEvenList list =
    filteredFold list (+) (fun a -> a % 2 = 0) 0

let countOddList list =
    filteredFold list (fun a _ -> a + 1) (fun a -> a % 2 <> 0) 0

let minList list =
    filteredFold list min (fun a -> true) 10