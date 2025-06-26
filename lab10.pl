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
