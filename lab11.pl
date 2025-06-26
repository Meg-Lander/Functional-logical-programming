% ЛР 11: Решение комбинаторных задач. Вариант 1

% Задание 1: Генерация комбинаторных объектов

% Размещения с повторениями
arrangements_rep(_, 0, []).
arrangements_rep(Alphabet, K, [H|T]) :-
    K > 0, member(H, Alphabet), K1 is K - 1, arrangements_rep(Alphabet, K1, T).

% Сочетания без повторений
combinations_norep([], 0, []).
combinations_norep([H|T], K, [H|Comb]) :- K > 0, K1 is K - 1, combinations_norep(T, K1, Comb).
combinations_norep([_|T], K, Comb) :- K >= 0, combinations_norep(T, K, Comb).

% Слова длины K с 3 буквами 'a'
words_k_3a(Alphabet, K, Word) :- words_k_3a_helper(Alphabet, K, 3, Word).
words_k_3a_helper(_, 0, 0, []).
words_k_3a_helper(Alphabet, K, CountA, [H|T]) :-
    K > 0, member(H, Alphabet), K1 is K - 1,
    (H = a -> CountA1 is CountA - 1 ; CountA1 is CountA),
    CountA1 >= 0, words_k_3a_helper(Alphabet, K1, CountA1, T).

