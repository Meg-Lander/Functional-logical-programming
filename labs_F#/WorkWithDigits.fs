module WorkWithDigits
    let sum_digits_top number = 
        let rec sum_digit number cur_sum =
            if number = 0 then cur_sum
            else 
                let last_digit = number % 10
                sum_digit (number / 10) (cur_sum + last_digit)
        sum_digit number 0

    let sum_digits_down number = 
         let rec sum_digit number =
             if number = 0 then 0
             else 
                 let last_digit = number % 10
                 last_digit + sum_digit (number / 10)
         sum_digit number

    let factorial_top number =
        let rec sum_bef number =
            let proiz = number
            if number = 0 then 1
            else
                proiz * sum_bef (number - 1)
        sum_bef number

    let factorial_down number =
        let rec sum_bef number proiz =
            if number = 0 then proiz
            else
                let last = number - 1
                sum_bef (last) (number * proiz)
        sum_bef number 1

    let big_function flag =
        match flag with
            true -> sum_digits_top
            | false -> factorial_top

    let operation_on_digits_numbers number operation initialValue =
        let rec oper num acc =
            if num = 0 then acc
            else
                let lastDigit = num % 10
                oper (num / 10) (operation acc lastDigit)
        oper number initialValue

    let operation_on_digits_numbers_with_condition number operation accum condition =
        let rec oper num acc =
            if num = 0 then acc
            else
                let lastDigit = num % 10
                let newAcc = if condition lastDigit then operation acc lastDigit else acc
                oper (num / 10) newAcc
        oper number accum
         
    let isPrime n =
        let rec check i =
            match i * i > n with
            | true -> true
            | false ->
                match n % i = 0 with
                | true -> false
                | false -> check (i + 1)
        match n < 2 with
        | true -> false
        | false -> check 2

    let sumPrimeDivisors number =
        let rec loop i acc =
            match i > number with
            | true -> acc
            | false ->
                match (number % i = 0, isPrime i) with
                | (true, true) -> loop (i + 1) (acc + i)
                | _ -> loop (i + 1) acc
        loop 1 0

    let countOddDigitsGreaterThanThree number =
    let rec count num acc =
        match num with
        | 0 -> acc
        | _ ->
            let digit = num % 10
            let rest = num / 10
            match (digit % 2, digit > 3) with
            | (1, true) -> count rest (acc + 1)
            | _ -> count rest acc
    count number 0
