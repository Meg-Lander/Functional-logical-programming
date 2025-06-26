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

% Задания 4, 5, 6: Акинатор

% 5: Предметная область (школьные предметы)
subject('Алгебра'). subject('Геометрия'). subject('Математика'). subject('Физика'). subject('Химия'). subject('Биология'). subject('География'). subject('История'). subject('Литература'). subject('Русский язык'). subject('Английский язык'). subject('Французский язык'). subject('Немецкий язык'). subject('Информатика'). subject('Искусство'). subject('Музыка'). subject('Физкультура'). subject('Экономика'). subject('Право'). subject('Технология'). subject('Экология'). subject('Астрономия'). subject('Обществознание'). subject('ОБЖ'). subject('Черчение').

% Характеристики предметов
difficult('Алгебра', 1). difficult('Геометрия', 1). difficult('Математика', 1). difficult('Физика', 1). difficult('Химия', 1). difficult('Биология', 0). difficult('География', 0). difficult('История', 0). difficult('Литература', 0). difficult('Русский язык', 1). difficult('Английский язык', 0). difficult('Французский язык', 0). difficult('Немецкий язык', 0). difficult('Информатика', 1). difficult('Искусство', 0). difficult('Музыка', 0). difficult('Физкультура', 0). difficult('Экономика', 0). difficult('Право', 0). difficult('Технология', 0). difficult('Экология', 0). difficult('Астрономия', 0). difficult('Обществознание', 0). difficult('ОБЖ', 0). difficult('Черчение', 1).

written_work('Алгебра', 1). written_work('Геометрия', 1). written_work('Математика', 1). written_work('Физика', 1). written_work('Химия', 1). written_work('Биология', 1). written_work('География', 1). written_work('История', 1). written_work('Литература', 1). written_work('Русский язык', 1). written_work('Английский язык', 1). written_work('Французский язык', 1). written_work('Немецкий язык', 1). written_work('Информатика', 1). written_work('Искусство', 0). written_work('Музыка', 1). written_work('Физкультура', 0). written_work('Экономика', 1). written_work('Право', 1). written_work('Технология', 1). written_work('Экология', 1). written_work('Астрономия', 1). written_work('Обществознание', 1). written_work('ОБЖ', 0). written_work('Черчение', 1).

lab_work('Алгебра', 0). lab_work('Геометрия', 0). lab_work('Математика', 0). lab_work('Физика', 1). lab_work('Химия', 1). lab_work('Биология', 1). lab_work('География', 0). lab_work('История', 0). lab_work('Литература', 0). lab_work('Русский язык', 0). lab_work('Английский язык', 0). lab_work('Французский язык', 0). lab_work('Немецкий язык', 0). lab_work('Информатика', 1). lab_work('Искусство', 0). lab_work('Музыка', 0). lab_work('Физкультура', 0). lab_work('Экономика', 0). lab_work('Право', 0). lab_work('Технология', 1). lab_work('Экология', 0). lab_work('Астрономия', 0). lab_work('Обществознание', 0). lab_work('ОБЖ', 1). lab_work('Черчение', 0).

creative('Алгебра', 0). creative('Геометрия', 0). creative('Математика', 0). creative('Физика', 0). creative('Химия', 0). creative('Биология', 0). creative('География', 0). creative('История', 0). creative('Литература', 1). creative('Русский язык', 1). creative('Английский язык', 0). creative('Французский язык', 0). creative('Немецкий язык', 0). creative('Информатика', 0). creative('Искусство', 1). creative('Музыка', 1). creative('Физкультура', 0). creative('Экономика', 0). creative('Право', 0). creative('Технология', 0). creative('Экология', 0). creative('Астрономия', 0). creative('Обществознание', 0). creative('ОБЖ', 0). creative('Черчение', 0).

has_exam('Алгебра', 1). has_exam('Геометрия', 1). has_exam('Математика', 1). has_exam('Физика', 1). has_exam('Химия', 1). has_exam('Биология', 1). has_exam('География', 0). has_exam('История', 1). has_exam('Литература', 1). has_exam('Русский язык', 1). has_exam('Английский язык', 1). has_exam('Французский язык', 0). has_exam('Немецкий язык', 0). has_exam('Информатика', 0). has_exam('Искусство', 0). has_exam('Музыка', 0). has_exam('Физкультура', 0). has_exam('Экономика', 0). has_exam('Право', 0). has_exam('Технология', 0). has_exam('Экология', 0). has_exam('Астрономия', 0). has_exam('Обществознание', 1). has_exam('ОБЖ', 0). has_exam('Черчение', 0).

% 6: Программа "Акинатор"
q1(X1) :- write('Это сложный предмет? (1 - да, 0 - нет): '), nl, read(X1).
q2(X2) :- write('Есть письменные работы? (1 - да, 0 - нет): '), nl, read(X2).
q3(X3) :- write('Есть лабораторные работы? (1 - да, 0 - нет): '), nl, read(X3).
q4(X4) :- write('Это творческий предмет? (1 - да, 0 - нет): '), nl, read(X4).
q5(X5) :- write('По этому предмету есть экзамен? (1 - да, 0 - нет): '), nl, read(X5).

pr :-
    write('Загадайте школьный предмет.'), nl,
    write('Я попробую его угадать, задавая вопросы.'), nl,
    q1(X1), q2(X2), q3(X3), q4(X4), q5(X5),
    subject(Subj),
    difficult(Subj, X1), written_work(Subj, X2), lab_work(Subj, X3), creative(Subj, X4), has_exam(Subj, X5),
    write('Вы загадали: '), write(Subj), nl.
pr :-
    write('Кажется, я не могу угадать этот предмет.'), nl,
    write('Возможно, его нет в моей базе знаний или вы ошиблись в ответах.'), nl.
