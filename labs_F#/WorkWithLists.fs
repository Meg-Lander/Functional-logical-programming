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
