module WorkWithDigits
    let sum_digits_top number =
        let rec sum_digit number =
            match number with
            | 0 -> 0
            | _ -> (number % 10) + sum_digit (number / 10)
        sum_digit number

    let sum_digits_down number =
        let rec sum_digit_tail number cur_sum =
            match number with
            | 0 -> cur_sum
            | _ -> sum_digit_tail (number / 10) (cur_sum + (number % 10))
        sum_digit_tail number 0

    let factorial_top number =
        let rec factorial number =
            match number with
            | 0 -> 1
            | _ -> number * factorial (number - 1)
        factorial number

    let factorial_down number =
        let rec factorial_tail number acc =
            match number with
            | 0 -> acc
            | _ -> factorial_tail (number - 1) (number * acc)
        factorial_tail number 1

    let big_function flag =
        match flag with
        | true -> sum_digits_top
        | false -> factorial_top

    let operation_on_digits_numbers number operation initialValue =
        let rec oper num acc =
            match num with
            | 0 -> acc
            | _ -> oper (num / 10) (operation acc (num % 10))
        oper number initialValue

    let operation_on_digits_numbers_with_condition number operation accum condition =
        let rec oper num acc =
            match num with
            | 0 -> acc
            | _ ->
                let lastDigit = num % 10
                match condition lastDigit with
                | true -> oper (num / 10) (operation acc lastDigit)
                | false -> oper (num / 10) acc
        oper number accum

    let isPrime n =
        let rec check i =
            match n < 2 with
            | true -> false
            | false ->
                match i * i > n with
                | true -> true
                | false ->
                    match n % i = 0 with
                    | true -> false
                    | false -> check (i + 1)
        check 2

    let sumPrimeDivisors number =
        let rec loop i acc =
            match i with
            | _ when i > number -> acc
            | _ when number % i = 0 && isPrime i -> loop (i + 1) (acc + i)
            | _ -> loop (i + 1) acc
        loop 1 0

    let countOddDigitsGreaterThanThree number =
        let rec count num acc =
            match num with
            | 0 -> acc
            | _ ->
                let digit = num % 10
                let rest = num / 10
                match digit with
                | _ when digit > 3 && digit % 2 <> 0 -> count rest (acc + 1)
                | _ -> count rest acc
        count number 0

    let productDivisorsWithSmallerDigitSum number =
        let targetSum = sum_digits_top number

        let rec loop i acc =
            match i with
            | _ when i > number -> acc
            | _ when number % i = 0 && sum_digits_top i < targetSum -> loop (i + 1) (acc * i)
            | _ -> loop (i + 1) acc
        loop 1 1

    let rec nod a b =
        match b with
        | 0 -> a
        | _ -> nod b (a % b)

    let obhodProstComp number operation initial =
        let rec oper num acc =
            match num >= number with
            | true -> acc
            | false ->
                match nod num number = 1 with
                | true -> oper (num + 1) (operation acc num)
                | false -> oper (num + 1) acc
        oper 1 initial

    let obhodProstCompWithCondition number operation initial condition =
        let rec oper num acc =
            match num >= number with
            | true -> acc
            | false ->
                match nod num number = 1 && condition num with
                | true -> oper (num + 1) (operation acc num)
                | false -> oper (num + 1) acc
        oper 1 initial

    let eulerNumber n =
        obhodProstComp n (fun acc _ -> acc + 1) 0

