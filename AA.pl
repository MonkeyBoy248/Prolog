man(anatoliy).
man(zahar).
man(dimitry).
man(vladimir).
man(artem).
woman(lida).
woman(vera).
woman(fjokla).
woman(dasha).
woman(liza).
child(dimitry,anatoliy).
child(dimitry,vera).
child(vladimir,anatoliy).
child(vladimir,vera).
child(zahar,dimitry).
child(zahar,fjokla).
child(dasha,dimitry).
child(dasha,fjokla).
child(liza,vladimir).
child(liza,lida).
child(artem,vladimir).
child(artem,lida).
son(X,Y):-child(X,Y),man(X).
doch(X,Y):-child(X,Y),woman(X).
bro(X,Y):-child(X,Z),!,child(Y,Z),X\=Y,man(Y),!.
sister(X,Y):-child(X,Z),!,child(Y,Z),X\=Y,woman(Y),!.
br_s(X,Y):-child(X,Z),!,child(Y,Z),X\=Y,!.
brak(X,Y):-child(Z,X),!,child(Z,Y),X\=Y,!.
uncle(X,Y):-child(X,Z),!,bro(Z,Y),!.
uncle(X,Y):-child(X,Z),!,sister(Z,P),brak(P,Y),!.
grandw(X,Y):-woman(X),!,child(X,Z),!,child(Z,Y),!.
grandm(X,Y):-man(X),!,child(X,Z),!,child(Z,Y),!.
