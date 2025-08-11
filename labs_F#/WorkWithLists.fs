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

let sortOnLength (listString) =
    List.sortBy(fun x -> String.length(x)) listString

let findMinIndexRecursion list =
    let rec loop idx minVal minIdx = function
        | [] -> minIdx
        | head::tail ->
            let newMinVal, newMinIdx = 
                match head < minVal with
                | true -> (head, idx)
                | false -> (minVal, minIdx)
            loop (idx + 1) newMinVal newMinIdx tail
    match list with
    | [] -> -1
    | head::tail -> loop 0 head 0 tail

let findMinIndex list = 
    match list with
    | [] -> 0
    | _ -> let minVal = List.min list
           List.findIndex (fun x -> x = minVal) list

let reverseBetweenMinMaxList list =
    match list with
    | [] | [_] -> list
    | _ ->
        let minVal = List.min list
        let maxVal = List.max list
        let minIdx = List.findIndex (fun x -> x = minVal) list
        let maxIdx = List.findIndex (fun x -> x = maxVal) list
        
        let startIdx, endIdx = 
            match minIdx < maxIdx with 
            | true -> minIdx, maxIdx 
            | false -> maxIdx, minIdx

        let startList = 
            match startIdx with
            | 0 -> []
            | _ -> list.[0..startIdx]
        
        let midList = 
            match endIdx - startIdx - 1 with
            | x when x <= 0 -> []
            | _ -> list.[startIdx+1..endIdx-1] |> List.rev
        
        let endList = 
            match endIdx with
            | x when x >= List.length list - 1 -> []
            | _ -> list.[endIdx..List.length list - 1]
        
        List.concat [startList; midList; endList]

let reverseBetweenMinMaxRecursion list =
    let rec findExtremumIndex compareFn index currentVal currentIndex = function
        | [] -> currentIndex
        | head::tail ->
            match compareFn head currentVal with
            | true -> findExtremumIndex compareFn (index+1) head index tail
            | false -> findExtremumIndex compareFn (index+1) currentVal currentIndex tail

    match list with
    | [] | [_] -> list
    | head::_ ->
        let minIndex = findExtremumIndex (<) 0 head 0 list
        let maxIndex = findExtremumIndex (>) 0 head 0 list  
        
        let startIndex, endIndex = 
            match minIndex < maxIndex with 
            | true -> minIndex, maxIndex 
            | false -> maxIndex, minIndex

        let rec reverseSection before middle after =
            match middle with
            | [] -> before @ after
            | head::tail -> reverseSection before tail (head::after)

        let rec splitList count acc = function
            | [] -> (List.rev acc, [])
            | tail when count = 0 -> (List.rev acc, tail)
            | head::tail -> splitList (count-1) (head::acc) tail

        let before, rest = splitList (startIndex+1) [] list
        let middle, after = splitList (endIndex-startIndex-1) [] rest

        reverseSection before middle after

let countMinInRangeRecursion a b list =
    let rec findMin current = function
        | [] -> current
        | head::tail when head < current -> findMin head tail
        | _::tail -> findMin current tail

    let rec loop idx cnt minVal = function
        | [] -> cnt
        | head::tail ->
            let inRange = idx >= a && idx <= b
            let isMin = head = minVal
            match inRange, isMin with
            | true, true -> loop (idx + 1) (cnt + 1) minVal tail
            | _, _       -> loop (idx + 1) cnt minVal tail

    match list with
    | [] -> 0
    | _ -> loop 0 0 (findMin System.Int32.MaxValue list) list

let countMinInRangeList a b list =
    match list with
    | [] -> 0
    | _ ->
        let minVal = List.min list
        list |> List.skip a |> List.take (b - a + 1) |> List.filter (fun x -> x = minVal) |> List.length

let countLocalMaxRecursion list =
    let rec loop prev curr = function
        | [] -> 0
        | next::tail when curr > prev && curr > next -> 1 + loop curr next tail
        | next::tail -> loop curr next tail
    
    match list with
    | x::y::tail -> loop x y tail
    | _ -> 0

let countLocalMaxList list =
    list |> List.windowed 3 |> List.filter (function
        | [a; b; c] -> b > a && b > c
        | _ -> false) |> List.length

let belowAverageRecursion list =
    let rec calculateSumAndCount sum count = function
        | [] -> (sum, count)
        | head::tail -> calculateSumAndCount (sum + head) (count + 1) tail

    let rec filterBelow avg acc = function
        | [] -> acc 
        | head::tail ->
            if float head < avg 
            then filterBelow avg (head::acc) tail
            else filterBelow avg acc tail

    match list with
    | [] -> []
    | _ ->
        let sum, count = calculateSumAndCount 0 0 list
        let avg = float sum / float count
        filterBelow avg [] list

let belowAverageList list =
    match list with
    | [] -> []
    | _ ->
        let avg = List.averageBy float list
        list |> List.filter (fun x -> float x < avg)

let primeFactorsRecursion n =
    let rec isPrime num div =
        match div * div > num with
        | true -> true
        | false -> 
            match num % div with
            | 0 -> false
            | _ -> isPrime num (div + 1)

    let rec factorize num p acc =
        match num with
        | 1 -> acc
        | _ ->
            match num % p with
            | 0 -> 
                match isPrime p 2 with
                | true -> factorize (num / p) p (p :: acc)
                | false -> factorize num (p + 1) acc
            | _ -> factorize num (p + 1) acc

    match n < 2 with
    | true -> []
    | false ->
        let factors = factorize n 2 []
        let rec sort lst =
            match lst with
            | [] -> []
            | head::tail -> 
                let left = sort (List.filter (fun x -> x < head) tail)
                let right = sort (List.filter (fun x -> x >= head) tail)
                left @ [head] @ right
        sort factors

let primeFactorsList n =
    let isPrime num =
        match { 2 .. int(sqrt(float num)) }
              |> Seq.tryFind (fun x -> num % x = 0) with
        | Some _ -> false
        | None -> true

    let rec getFactors num p =
        match num % p with
        | 0 -> p :: getFactors (num/p) p
        | _ -> []

    match n < 2 with
    | true -> []
    | false ->
        [2..n] |> List.filter isPrime |> List.collect (getFactors n) |> List.sort
    
let processList lst =
    let list1 = lst |> List.filter (fun x -> x % 2 = 0) |> List.map (fun x -> x / 2)
    let list2 = list1 |> List.filter (fun x -> x % 3 = 0) |> List.map (fun x -> x / 3)
    let list3 = list2 |> List.map (fun x -> x * x)
    let list4 = list3 |> List.filter (fun x -> List.contains x list1)
    let list5 = list2 @ list3 @ list4
    (list1, list2, list3, list4, list5)

let copyLastElement arrA arrB =
    Array.append arrA [|Array.last arrB|]

let isLowercaseSorted (str: string) =
    let rec checkOrder i prev =
        match i >= str.Length with
        | true -> true
        | false ->
            let current = Char.ToLower str.[i]
            match current >= prev with
            | true -> checkOrder (i + 1) current
            | false -> false

    match str with
    | null | "" -> true
    | _ -> checkOrder 1 (Char.ToLower str.[0])