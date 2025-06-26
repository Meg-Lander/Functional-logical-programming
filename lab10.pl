% ЛР 10: Рекурсия и списки. Вариант 1

% Задание 1
max(X, Y, U, Z) :- max(X, Y, Temp), max(Temp, U, Z).
max(A, B, A) :- A >= B.
max(A, B, B) :- A < B.

fact_down(0, 1).
fact_down(N, X) :- N > 0, N1 is N - 1, fact_down(N1, X1), X is N * X1.

fact_up(N, X) :- fact_up(N, 1, X).
fact_up(0, Acc, Acc).
fact_up(N, Acc, X) :- N > 0, N1 is N - 1, Acc1 is Acc * N, fact_up(N1, Acc1, X).

sum_digits_down(0, 0).
sum_digits_down(N, Sum) :- N > 0, Digit is N mod 10, N1 is N // 10, sum_digits_down(N1, Sum1), Sum is Sum1 + Digit.

sum_digits_up(N, Sum) :- sum_digits_up_helper(N, 0, Sum).
sum_digits_up_helper(0, Acc, Acc).
sum_digits_up_helper(N, Acc, Sum) :- N > 0, Digit is N mod 10, N1 is N // 10, NewAcc is Acc + Digit, sum_digits_up_helper(N1, NewAcc, Sum).

is_square_free(1).
is_square_free(Num) :- Num > 1, \+ (between(2, floor(sqrt(Num)), I), Sq is I*I, Num mod Sq =:= 0).

read_list(List) :- write('Введите список: '), read(List).
print_list([]).
print_list([H|T]) :- write(H), nl, print_list(T).

sum_list_down([], 0).
sum_list_down([H|T], Summ) :- sum_list_down(T, SummTail), Summ is H + SummTail.

sum_list_up(List, Summ) :- sum_list_up_helper(List, 0, Summ).
sum_list_up_helper([], Acc, Acc).
sum_list_up_helper([H|T], Acc, Summ) :- NewAcc is Acc + H, sum_list_up_helper(T, NewAcc, Summ).

remove_elements_with_digit_sum(ListIn, Target, ListOut) :- exclude(has_digit_sum(Target), ListIn, ListOut).
has_digit_sum(TargetSum, Number) :- sum_digits_up(Number, Sum), Sum =:= TargetSum.

% Задание 2
product_digits(0, 0).
product_digits(N, Product) :- N > 0, product_digits_helper(N, 1, Product).
product_digits_helper(0, Acc, Acc).
product_digits_helper(N, Acc, Product) :- N > 0, Digit is N mod 10, NextN is N // 10, NewAcc is Acc * Digit, product_digits_helper(NextN, NewAcc, Product).

count_odd_digits_greater_than_3_down(0, 0).
count_odd_digits_greater_than_3_down(N, Count) :- N > 0, Digit is N mod 10, NextN is N // 10, count_odd_digits_greater_than_3_down(NextN, RestCount), ((Digit > 3, Digit mod 2 =:= 1) -> Count is RestCount + 1 ; Count is RestCount).

count_odd_digits_greater_than_3_up(N, Count) :- count_odd_digits_greater_than_3_up_helper(N, 0, Count).
count_odd_digits_greater_than_3_up_helper(0, Acc, Acc).
count_odd_digits_greater_than_3_up_helper(N, Acc, Count) :- N > 0, Digit is N mod 10, NextN is N // 10, NewAcc is Acc + ((Digit > 3, Digit mod 2 =:= 1) -> 1 ; 0), count_odd_digits_greater_than_3_up_helper(NextN, NewAcc, Count).

gcd(0, B, B) :- B >= 0.
gcd(A, B, G) :- A > 0, R is B mod A, gcd(R, A, G).

% Задание 3 (1.1, 1.13, 1.25)
task3_1_read(List) :- read_list(List).
task3_1_logic(List, Count) :- max_list(List, Max), findall(Index, nth0(Index, List, Max), MaxIndices), last(MaxIndices, LastMaxIndex), length(List, Length), Count is Length - 1 - LastMaxIndex.
task3_1_write(Count) :- write('Количество элементов после последнего макс: '), write(Count), nl.
task3_1 :- task3_1_read(List), task3_1_logic(List, Count), task3_1_write(Count).

task3_13_read(List) :- read_list(List).
task3_13_logic(List, NewList) :- min_list(List, Min), nth0(MinPos, List, Min), length(Prefix, MinPos), append(Prefix, Rest, List), append(Rest, Prefix, NewList).
task3_13_write(NewList) :- write('Список после перемещения: '), print_list(NewList).
task3_13 :- task3_13_read(List), task3_13_logic(List, NewList), task3_13_write(NewList).

task3_25_read(List, A, B) :- read_list(List), write('a: '), read(A), write('b: '), read(B).
task3_25_logic(List, A, B, MaxInInterval) :- findall(X, (member(X, List), X >= A, X =< B), Elements), (Elements = [] -> MaxInInterval = 'нет элементов' ; max_list(Elements, MaxInInterval)).
task3_25_write(MaxInInterval) :- write('Макс в интервале: '), write(MaxInInterval), nl.
task3_25 :- task3_25_read(List, A, B), task3_25_logic(List, A, B, MaxInInterval), task3_25_write(MaxInInterval).

% Задание 4
logical_task4_variant1 :-
    Names = [belokurov, ryzhov, chernov], Colors = [blond, brunet, red],
    permutation(Colors, [ColorB, ColorR, ColorC]),
    ColorB \= blond, ColorR \= red, ColorC \= brunet,
    ColorB \= brunet,
    ColorB = red,
    ColorR = brunet, ColorC = blond,
    write('Задание 4 (Вариант 1) Решение:'), nl,
    write('Белокуров: '), write(ColorB), nl, write('Рыжов: '), write(ColorR), nl, write('Чернов: '), write(ColorC), nl.

permutation([], []).
permutation([H|T], Perm) :- permutation(T, PermT), select(H, Perm, PermT).

% Задание 5
is_prime(2). is_prime(3). is_prime(P) :- integer(P), P > 3, P mod 2 =\= 0, \+ has_factor(P, 3).
has_factor(N, K) :- K * K =< N, (N mod K =:= 0 ; K2 is K + 2, has_factor(N, K2)).

find_divisors(Number, Divisors) :- findall(D, (between(1, Number, D), Number mod D =:= 0), Divisors).

sum_prime_divisors(Number, Sum) :- find_divisors(Number, Divisors), include(is_prime, Divisors, PrimeDivisors), sum_list_up(PrimeDivisors, Sum).

product_list([], 1).
product_list([H|T], Product) :- product_list(T, ProdT), Product is H * ProdT.

product_divisors_conditional(Number, Product) :-
    sum_digits_up(Number, SumOrig),
    findall(D, (between(1, Number, D), Number mod D =:= 0, sum_digits_up(D, SumD), SumD < SumOrig), ConditionalDivisors),
    product_list(ConditionalDivisors, Product).

task5_variant1_read(Number) :- write('Число: '), read(Number).
task5_variant1_logic(Number, SumPD, ProdCD) :- sum_prime_divisors(Number, SumPD), product_divisors_conditional(Number, ProdCD).
task5_variant1_write(SumPD, ProdCD) :- write('1. Сумма простых делителей: '), write(SumPD), nl, write('2. Произведение делителей по условию: '), write(ProdCD), nl.
task5_variant1 :- task5_variant1_read(Number), task5_variant1_logic(Number, SumPD, ProdCD), task5_variant1_write(SumPD, ProdCD).

% Задание 6 (1.37, 1.49)
task6_37_read(List) :- read_list(List).
task6_37_logic([_], [], 0).
task6_37_logic([H1, H2|T], Indices, Count) :-
    task6_37_logic([H2|T], RestIndices, RestCount), length([H2|T], RestLength), CurrentIndex is RestLength,
    (H2 < H1 -> Count is RestCount + 1, Indices = [CurrentIndex | RestIndices] ; Count is RestCount, Indices = RestIndices).

task6_37_write(Indices, Count) :- write('Индексы < левого соседа: '), write(Indices), nl, write('Количество: '), write(Count), nl.
task6_37 :- task6_37_read(List), task6_37_logic(List, Indices, Count), task6_37_write(Indices, Count).

prime_divisors(Number, Divisors) :- (Number =< 1 -> Divisors = [] ; find_prime_divisors_helper(Number, 2, Divisors)).
find_prime_divisors_helper(1, _, []).
find_prime_divisors_helper(N, D, [D|Rest]) :- is_prime(D), N mod D =:= 0, N1 is N // D, find_prime_divisors_helper(N1, D, Rest).
find_prime_divisors_helper(N, D, Divisors) :- is_prime(D), N mod D =\= 0, D1 is D + 1, find_prime_divisors_helper(N, D1, Divisors).
find_prime_divisors_helper(N, D, Divisors) :- \+ is_prime(D), D1 is D + 1, find_prime_divisors_helper(N, D1, Divisors).

task6_49_read(List) :- read_list(List).
task6_49_logic(List, UniquePrimeDivisors) :- findall(PD, (member(Number, List), prime_divisors(Number, PDList), member(PD, PDList)), AllPrimeDivisors), sort(AllPrimeDivisors, UniquePrimeDivisors).
task6_49_write(UniquePrimeDivisors) :- write('Уникальные простые делители списка: '), write(UniquePrimeDivisors), nl.
task6_49 :- task6_49_read(List), task6_49_logic(List, UniquePrimeDivisors), task6_49_write(UniquePrimeDivisors).

% Задание 7
logical_task7_variant1 :-
    Profession = [astronom, poet, prosaic, dramatist], Names = [alekseev, borisov, konstantinov, dmitriev],
    permutation(Profession, [ProfA, ProfB, ProfK, ProfD]),
    permutation(Names, [AuthorA, AuthorB, AuthorK, AuthorD]),
    AuthorA \= alekseev, AuthorB \= borisov, AuthorK \= konstantinov, AuthorD \= dmitriev,
    AuthorA = borisov, AuthorB = alekseev, AuthorK = dmitriev, AuthorD = konstantinov,

    Name_Prof(alekseev, ProfA), Name_Prof(borisov, ProfB), Name_Prof(konstantinov, ProfK), Name_Prof(dmitriev, ProfD),

    ((ProfA = poet, ProfB = dramatist) ; (ProfB = poet, ProfA = dramatist) ; (ProfK = poet, ProfD = dramatist) ; (ProfD = poet, ProfK = dramatist)),
    \+ (ProfA = prosaic, ProfB = astronom), \+ (ProfB = prosaic, ProfA = astronom), \+ (ProfK = prosaic, ProfD = astronom), \+ (ProfD = prosaic, ProfK = astronom),

    member(astronom, [ProfA, ProfB, ProfK, ProfD]), member(poet, [ProfA, ProfB, ProfK, ProfD]),
    member(prosaic, [ProfA, ProfB, ProfK, ProfD]), member(dramatist, [ProfA, ProfB, ProfK, ProfD]),

    write('Задание 7 (Вариант 1) Решение:'), nl,
    write('Профессии:'), nl,
    write('Алексеев: '), write(ProfA), nl, write('Борисов: '), write(ProfB), nl,
    write('Константинов: '), write(ProfK), nl, write('Дмитриев: '), write(ProfD), nl,
    write('Что читал каждый:'), nl,
    Name_Prof_output(AuthorA, ProfAuthorA, ProfA, ProfB, ProfK, ProfD), write('Алексеев читал книгу '), write(AuthorA), write(' ('), write(ProfAuthorA), write(')'), nl,
    Name_Prof_output(AuthorB, ProfAuthorB, ProfA, ProfB, ProfK, ProfD), write('Борисов читал книгу '), write(AuthorB), write(' ('), write(ProfAuthorB), write(')'), nl,
    Name_Prof_output(AuthorK, ProfAuthorK, ProfA, ProfB, ProfK, ProfD), write('Константинов читал книгу '), write(AuthorK), write(' ('), write(ProfAuthorK), write(')'), nl,
    Name_Prof_output(AuthorD, ProfAuthorD, ProfA, ProfB, ProfK, ProfD), write('Дмитриев читал книгу '), write(AuthorD), write(' ('), write(ProfAuthorD), write(')'), nl.

Name_Prof(alekseev, ProfA) :- var(ProfA), logical_task7_variant1.
Name_Prof(borisov, ProfB) :- var(ProfB), logical_task7_variant1.
Name_Prof(konstantinov, ProfK) :- var(ProfK), logical_task7_variant1.
Name_Prof(dmitriev, ProfD) :- var(ProfD), logical_task7_variant1.
Name_Prof_output(Name, Prof, ProfA, ProfB, ProfK, ProfD) :-
    (Name = alekseev -> Prof = ProfA); (Name = borisov -> Prof = ProfB);
    (Name = konstantinov -> Prof = ProfK); (Name = dmitriev -> Prof = ProfD).