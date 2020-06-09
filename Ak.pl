
:- dynamic great/2, country/2, genre/2, time/2, break/1.

write_list([]):-!.
write_list([H|T]):- write(H), write(" "), write_list(T).

write_str([]):-!.
write_str([H|T]):- write(H), write(" "), write_str(T).

read_str(A):-get0(_),get0(X1),r_str(X1,A,[]).
r_str(10,A,A):-!.
r_str(X,A,B):-append(B,[X],B1),get0(X1),r_str(X1,A,B1).


game:- clear_DB,see('iz7.txt'), getWriter, seen,!, q1(Ans1), q2(Ans2),
q3(Ans3), q4(Ans4),answer_check(Ans1,Ans2,Ans3,Ans4),write('Желаете
продолжить игру?
(0.-Нет,1.-Да)'),read(Continue),continue(Continue),clear_DB.


% Получение персонажей и их сведений
getWriter:- readln(Writer), Writer \=[], readln(Info), main_funct(Writer, Info),getWriter.
getWriter:-!.

% Заполнение базы данных
main_funct(_, []):-!.
main_funct(Writer,[H|T]):-asserta(great(Writer,H)),country_funct(Writer,T),!.

country_funct(_, []):- !.
country_funct(Writer,[H|T]):- asserta(country(Writer,H)), genre_funct(Writer, T),!.

genre_funct(_, []):- !.
genre_funct(Writer, [H|T]):- asserta(genre(Writer, H)), time_funct(Writer, T),!.

time_funct(_,[]):- !.
time_funct(Writer,[H|_]):- asserta(time(Writer,H)),!.


% Вопросы
q1(Ans):-write('\n\'Автор - "наше все"?'),nl,write('0. Да.'),nl,write('1. Нет'),nl,read(Ans).
q2(Ans):- write('\n\'Из какой страны этот писатель?'), nl, write('0.Англия.'), nl, write('1. Америка'),nl, write('2. Ирландия'),nl, write('3. Франция'),nl, write('4. Шотландия'),nl,write('5. Австрия'),nl,write('6. Германия'),nl,write('7. Россия'),nl,read(Ans).
q3(Ans):- write('\n\nВ каком жанре (в какой форме) писал автор?'), nl, write('0. Научная фантастика.'), nl, write('1. Фантастика.'),nl,write('2. Роман.'),nl,write('3. Повесть.'),nl,write('4. Фантастика.'),nl,write('5. Стих.'),nl,write('6. Поэма.'),nl,write('7. Рассказ.'),nl,write('8. Готический роман.'),nl,write('9.Роман-эпопея.'),nl,write('10. Детектив.'),nl,write('11. Антиутопия.'),nl,read(Ans).
q4(Ans):- write('\n\nВ какой период работал писатель?'), nl, write('0.Первая половина 19 века.'), nl, write('1.Вторая половина 19 века.'),nl,write('2.Конец 19 века.'),nl,write('3.Первая половина 20 века.'),nl,write('4.Вторая половина 20 века.'),nl,read(Ans).




% Проверки
answer_check(Ans1,Ans2,Ans3,Ans4):-asserta(break(1)),findWriter(Ans1,Ans2,Ans3,Ans4,Writer),
    forall((findWriter(Ans1,Ans2,Ans3,Ans4,Writer)),(write('\nВаш писатель - '), write_str(Writer),nl,write('0. Нет.'),nl,write('1.Да'),nl,read(Answer),
   answer(Answer))),fail.
answer_check(_,_,_,_):-checkBreak0.
answer_check(Ans1,Ans2,Ans3,Ans4):-write('Писатель не был найден в базе данных\nЖелаете добавить писателя в базу данных?\n0. Нет.\n1. Да.\n'),nl, read(Answer),
    add_Writer(Answer,[Ans1,Ans2,Ans3,Ans4]).

answer(Answer):-Answer=1,asserta(break(0)),!.
answer(_):-asserta(break(1)).

findWriter(Ans1,Ans2,Ans3,Ans4,Writer):-great(Writer,Ans1),country(Writer,Ans2), genre(Writer,Ans3), time(Writer,Ans4),checkBreak1.
findWriter(_,_,_,_,_):-fail.

continue(0):-!.
continue(1):-clear_DB,game.
clear_DB:-retractall(country(_,_)),retractall(genre(_,_)),retractall(time(_,_)), retractall(break(_)).

checkBreak1:-break(X),!,X=1.
checkBreak0:-break(X),!,X=0.

add_Writer(1, List):-write('Введите имя писателя -- '), read_str(Name), name(Writer, Name), add_Writer_to_DB(Writer, List), !.
add_Writer(0,_):- !.

add_Writer_to_DB(CharName, List):- append('iz7.txt'), nl, write(CharName), nl, write_list(List), told.



















