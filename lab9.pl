% Лабораторная работа № 9: Введение в пролог, дерево семьи, Вариант 1

% База фактов: пол
man(anatoliy). man(dimitriy). man(vlad). man(kirill). man(mefodiy). man(petr). man(oleg). man(sergey). man(igor).
woman(vladina). woman(galya). woman(sveta). woman(zoya). woman(katrin). woman(irina). woman(elena). woman(anna).

% База фактов: родитель-ребенок (dite(Ребенок, Родитель))
dite(dimitriy, anatoliy). dite(dimitriy, galya).
dite(vladina, anatoliy). dite(vladina, galya).
dite(kirill, dimitriy). dite(kirill, sveta).
dite(mefodiy, dimitriy). dite(mefodiy, sveta).
dite(zoya, vlad). dite(zoya, vladina).
dite(katrin, vlad). dite(katrin, vladina).
dite(anatoliy, petr). dite(anatoliy, irina).
dite(sergey, petr). dite(sergey, irina).
dite(galya, oleg). dite(galya, elena).
dite(igor, kirill). dite(igor, anna).


% 1: Основные предикаты семьи
men :- man(X), write(X), nl, fail. men.
women :- woman(X), write(X), nl, fail. women.

mother(M, X) :- woman(M), dite(X, M).
mother(X) :- mother(M, X), write('Мама '), write(X), write(': '), write(M), nl.

father(F, X) :- man(F), dite(X, F).
father(X) :- father(F, X), write('Отец '), write(X), write(': '), write(F), nl.

children(X) :- dite(Child, X), write('Ребенок '), write(X), write(': '), write(Child), nl, fail. children(_).

sibling(X, Y) :- dite(X, P1), dite(Y, P1), dite(X, P2), dite(Y, P2), P1 \= P2, X \= Y.

brother(X, Y) :- man(X), sibling(X, Y).
brothers(X) :- brother(Brother, X), write('Брат '), write(X), write(': '), write(Brother), nl, fail. brothers(_).

sister(X, Y) :- woman(X), sibling(X, Y).
sisters(X) :- sister(Sister, X), write('Сестра '), write(X), write(': '), write(Sister), nl, fail. sisters(_).

b_s(X, Y) :- sibling(X, Y).
b_s(X) :- b_s(Bs, X), write('Сиблинг '), write(X), write(': '), write(Bs), nl, fail. b_s(_).


% 3 : Более сложные отношения

% grand_pa(X, Y): X является дедушкой Y
grand_pa_facts(X, Y) :- man(X), dite(Parent, Y), dite(X, Parent).
grand_pa_preds(X, Y) :- man(X), (father(Parent, Y) ; mother(Parent, Y)), (father(X, Parent) ; mother(X, Parent)).

grand_pas(X) :- write('Дедушки '), write(X), write(':'), nl, grand_pa_facts(GP, X), write(GP), nl, fail. grand_pas(_).

% grand_son_of_pa(GS, GP): GS является внуком GP (по линии дедушки)
grand_son_of_pa_facts(GS, GP) :- man(GS), grand_pa_facts(GP, GS).
grand_son_of_pa_preds(GS, GP) :- man(GS), grand_pa_preds(GP, GS).

% grand_pa_and_son(X, Y): X и Y дедушка и внук или наоборот
grand_pa_and_son_facts(X, Y) :- (grand_pa_facts(X, Y), man(Y)); (grand_pa_facts(Y, X), man(X)).
grand_pa_and_son_preds(X, Y) :- (grand_pa_preds(X, Y), man(Y)); (grand_pa_preds(Y, X), man(X)).

% uncle(X, Y): X является дядей Y
uncle_facts(X, Y) :- man(X), sibling(X, P), dite(P, Y).
uncle_preds(X, Y) :- man(X), b_s(X, P), (father(P, Y) ; mother(P, Y)).

uncles(X) :- write('Дядя '), write(X), write(': '), write(U), nl, fail. uncles(_).
