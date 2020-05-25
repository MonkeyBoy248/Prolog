read_list(A,N):-r_1([],A,0,N).
r_1(A,A,N,N):-!.
r_1(B,A,I,N):-I1 is I+1,read(X),append(B,[X],B1),r_1(B1,A,I1,N).

insertion_sort:-read(N),read_list(A,N),insertion_sort(A,B),write(B).

insert(Element, [], [Element]):-!.
insert(Element, [Head|Tail], [Element, Head|Tail]):-
  Element < Head, !.
insert(Element, [Head|Tail], [Head|InsertTail]):-
  insert(Element, Tail, InsertTail).

insertion_sort(List, SortedList):-
  insertion_sort(List, [], SortedList).
insertion_sort([], SortedList, SortedList):-!.
insertion_sort([Head|Tail], SortedPart, SortedList):-
  insert(Head, SortedPart, SortedPartWithHead),
  insertion_sort(Tail, SortedPartWithHead, SortedList).