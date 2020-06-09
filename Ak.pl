
:- dynamic great/2, country/2, genre/2, time/2, break/1.

write_list([]):-!.
write_list([H|T]):- write(H), write(" "), write_list(T).

write_str([]):-!.
write_str([H|T]):- write(H), write(" "), write_str(T).

read_str(A):-get0(_),get0(X1),r_str(X1,A,[]).
r_str(10,A,A):-!.
r_str(X,A,B):-append(B,[X],B1),get0(X1),r_str(X1,A,B1).


game:- clear_DB,see('iz7.txt'), getWriter, seen,!, q1(Ans1), q2(Ans2),
q3(Ans3), q4(Ans4),answer_check(Ans1,Ans2,Ans3,Ans4),write('�������
���������� ����?
(0.-���,1.-��)'),read(Continue),continue(Continue),clear_DB.


% ��������� ���������� � �� ��������
getWriter:- readln(Writer), Writer \=[], readln(Info), main_funct(Writer, Info),getWriter.
getWriter:-!.

% ���������� ���� ������
main_funct(_, []):-!.
main_funct(Writer,[H|T]):-asserta(great(Writer,H)),country_funct(Writer,T),!.

country_funct(_, []):- !.
country_funct(Writer,[H|T]):- asserta(country(Writer,H)), genre_funct(Writer, T),!.

genre_funct(_, []):- !.
genre_funct(Writer, [H|T]):- asserta(genre(Writer, H)), time_funct(Writer, T),!.

time_funct(_,[]):- !.
time_funct(Writer,[H|_]):- asserta(time(Writer,H)),!.


% �������
q1(Ans):-write('\n\'����� - "���� ���"?'),nl,write('0. ��.'),nl,write('1. ���'),nl,read(Ans).
q2(Ans):- write('\n\'�� ����� ������ ���� ��������?'), nl, write('0.������.'), nl, write('1. �������'),nl, write('2. ��������'),nl, write('3. �������'),nl, write('4. ���������'),nl,write('5. �������'),nl,write('6. ��������'),nl,write('7. ������'),nl,read(Ans).
q3(Ans):- write('\n\n� ����� ����� (� ����� �����) ����� �����?'), nl, write('0. ������� ����������.'), nl, write('1. ����������.'),nl,write('2. �����.'),nl,write('3. �������.'),nl,write('4. ����������.'),nl,write('5. ����.'),nl,write('6. �����.'),nl,write('7. �������.'),nl,write('8. ���������� �����.'),nl,write('9.�����-������.'),nl,write('10. ��������.'),nl,write('11. ����������.'),nl,read(Ans).
q4(Ans):- write('\n\n� ����� ������ ������� ��������?'), nl, write('0.������ �������� 19 ����.'), nl, write('1.������ �������� 19 ����.'),nl,write('2.����� 19 ����.'),nl,write('3.������ �������� 20 ����.'),nl,write('4.������ �������� 20 ����.'),nl,read(Ans).




% ��������
answer_check(Ans1,Ans2,Ans3,Ans4):-asserta(break(1)),findWriter(Ans1,Ans2,Ans3,Ans4,Writer),
    forall((findWriter(Ans1,Ans2,Ans3,Ans4,Writer)),(write('\n��� �������� - '), write_str(Writer),nl,write('0. ���.'),nl,write('1.��'),nl,read(Answer),
   answer(Answer))),fail.
answer_check(_,_,_,_):-checkBreak0.
answer_check(Ans1,Ans2,Ans3,Ans4):-write('�������� �� ��� ������ � ���� ������\n������� �������� �������� � ���� ������?\n0. ���.\n1. ��.\n'),nl, read(Answer),
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

add_Writer(1, List):-write('������� ��� �������� -- '), read_str(Name), name(Writer, Name), add_Writer_to_DB(Writer, List), !.
add_Writer(0,_):- !.

add_Writer_to_DB(CharName, List):- append('iz7.txt'), nl, write(CharName), nl, write_list(List), told.



















