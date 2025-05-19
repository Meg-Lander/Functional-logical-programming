open System

let rec powerPositive (baseVal: int64) (power: int) : int64 =
    if power < 0 then
        invalidArg "power" "Показатель не может быть отрицательным."
    elif power = 0 then
        1L
    else
        baseVal * powerPositive baseVal (power - 1)


let calculateUn (n: int) : int64 =
    let n64 = int64 n
    if n64 = 0L then
         1L
    else
        let rec sumTermsIter (i: int) (currentSum: int64) : int64 =
            if i > 10 then
                currentSum
            else
                let term = powerPositive n64 i
                let signedTerm = if i % 2 = 0 then term else -term
                sumTermsIter (i + 1) (currentSum + signedTerm)
        sumTermsIter 0 0L

let rec generateDifferences (terms: list<int64>) : list<int64> =
    match terms with
    | [] | [_] -> []
    | h1 :: h2 :: tail ->
        let diff = h2 - h1
        diff :: generateDifferences (h2 :: tail)

let rec predictNextTerm (terms: list<int64>) : int64 =
    match terms with
    | [] -> failwith "Невозможно предсказать для пустого списка."
    | [singleTerm] -> singleTerm
    | _ ->
        let diffs = generateDifferences terms
        let predictedNextDiff = predictNextTerm diffs
        let lastTerm = List.last terms
        lastTerm + predictedNextDiff

let rec findSumOfFits (k: int) (maxK: int) (accumulatedSum: int64) : int64 =
    if k > maxK then
        printfn "\n--- Расчет завершен ---"
        accumulatedSum
    else
        printfn "\n--- Проверка для k = %d ---" k

        let rec generateTerms (n: int) (currentList: list<int64>) : list<int64> =
            if n > k then
                List.rev currentList
            else
                generateTerms (n + 1) (calculateUn n :: currentList)

        let termsUk = generateTerms 1 []
        printfn "Первые %d членов (u_1..u_%d): %A" k k termsUk

        let predictedValue = predictNextTerm termsUk
        printfn "Предсказанное значение OP(%d, %d): %d" k (k+1) predictedValue

        let actualValue = calculateUn (k + 1)
        printfn "Фактическое значение u_%d: %d" (k+1) actualValue

        let newSum =
            if predictedValue <> actualValue then
                printfn "Результат: BOP найден! FIT = %d" (accumulatedSum + predictedValue)
                accumulatedSum + predictedValue
            else
                 printfn "Результат: Предсказание верное."
                 accumulatedSum

        findSumOfFits (k + 1) maxK newSum

let maxDegree = 12
let totalFitSum = findSumOfFits 1 maxDegree 0L
printfn "\nОбщая сумма FIT для всех BOP: %d" totalFitSum
