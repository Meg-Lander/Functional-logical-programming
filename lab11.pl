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

% Задание 2 : Размещения без повторений, перестановки

% Размещения без повторений
arrangements_norep([], _, []).
arrangements_norep(List, K, [H|Arr]) :-
    K > 0, select(H, List, Rest), K1 is K - 1, arrangements_norep(Rest, K1, Arr).

% Перестановки
permutation([], []).
permutation(List, [H|Perm]) :- select(H, List, Rest), permutation(Rest, Perm).

% Задание 4 : Генерация в файл

% Записать размещения без повторений в файл
write_arrangements_norep_to_file(List, K, File) :-
    tell(File), write('Размещения без повторений из '), write(List), write(' по '), write(K), write(':'), nl,
    forall(arrangements_norep(List, K, Arr), (write(Arr), nl)), told.

% Записать перестановки в файл
write_permutations_to_file(List, File) :-
    tell(File), write('Перестановки списка '), write(List), write(':'), nl,
    forall(permutation(List, Perm), (write(Perm), nl)), told.

% Задание 5: Остовное дерево (неориентированный связный граф)

% Пример неориентированного графа: u_edge(Узел1, Узел2). Ребро в обе стороны.
% u_edge(a, b). u_edge(b, a).
% u_edge(a, c). u_edge(c, a).
% u_edge(b, d). u_edge(d, b).
% u_edge(c, d). u_edge(d, c).
% u_edge(d, e). u_edge(e, d).

% Построение остовного дерева (BFS обход)
spanning_tree(StartNode, TreeEdges) :-
    spanning_tree_bfs([StartNode], [StartNode], [], TreeEdges).

spanning_tree_bfs([], _, Edges, Edges).
spanning_tree_bfs([Node|Queue], Visited, CurrentEdges, TreeEdges) :-
    findall(Neighbor, (u_edge(Node, Neighbor), \+ member(Neighbor, Visited)), NewNeighbors),
    append(Visited, NewNeighbors, NewVisited),
    findall(edge(Node, Neighbor), member(Neighbor, NewNeighbors), NewEdges),
    append(CurrentEdges, NewEdges, UpdatedEdges),
    append(Queue, NewNeighbors, NextQueue),
    spanning_tree_bfs(NextQueue, NewVisited, UpdatedEdges, TreeEdges).

% Задание 6: Обход в глубину ориентированный граф

% Пример ориентированного графа: d_edge(От, К).
% d_edge(1, 2).
% d_edge(1, 3).
% d_edge(2, 4).
% d_3(4, 1). % Цикл

% Обход в глубину
dfs(StartNode, Path) :-
    dfs_helper(StartNode, [StartNode], Path).

dfs_helper(Node, Visited, [Node|Path]) :-
    findall(Neighbor, (d_edge(Node, Neighbor), \+ member(Neighbor, Visited)), UnvisitedNeighbors),
    dfs_neighbors(UnvisitedNeighbors, [Node|Visited], Path).

dfs_neighbors([], _, []).
dfs_neighbors([Neighbor|Rest], Visited, Path) :-
    dfs_helper(Neighbor, Visited, PathSegment),
    dfs_neighbors(Rest, Visited, RestPath),
    append(PathSegment, RestPath, Path).