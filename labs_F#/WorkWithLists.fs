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

let filteredFold list (f : int -> int -> int) (p : int -> bool) (acc : int) =
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

let mostCount list =
    let rec processTail list dict maxElem maxCount =
        match list with
        | [] -> maxElem
        | head::tail ->
            let currentCount = 
                match Map.tryFind head dict with
                | Some cnt -> cnt + 1
                | None -> 1
            let newDict = Map.add head currentCount dict
            let newMaxElem, newMaxCount =
                match currentCount > maxCount with
                | true -> head, currentCount
                | false -> maxElem, maxCount
            processTail tail newDict newMaxElem newMaxCount
    
    match list with
    | [] -> failwith "Список пуст!"
    | head::tail -> processTail tail (Map.add head 1 Map.empty) head 1

let mostCountList list =
    match list with
    | [] -> failwith "Список пуст"
    | _ ->
        list |> List.countBy id  |> List.maxBy snd |> fst

let countSquareElements (list: int list) =
    let squares = list |> List.map (fun x -> x * x)
    list |> List.filter (fun x -> List.contains x squares) |> List.length 

let sumOfDigits n =
    let rec loop num acc =
        match num with
        | 0 -> acc
        | _ -> loop (num / 10) (acc + num % 10)
    loop (abs n) 0

let countDivisors n =
    match n with
    | 0 -> 0
    | _ ->
        [1..n] |> List.filter (fun x -> n % x = 0) |> List.length

let combineLists (listA) (listB) (listC) =
    let sortedA = listA |> List.sortDescending
    
    let sortedB = 
        listB 
        |> List.sortBy sumOfDigits
    
    let sortedC = 
        listC 
        |> List.sortWith (fun x y ->
            match compare (countDivisors y) (countDivisors x) with
            | 0 -> compare (abs y) (abs x)
            | c -> c)
    
    List.zip3 sortedA sortedB sortedC