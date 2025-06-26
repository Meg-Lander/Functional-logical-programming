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
