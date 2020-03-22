max_2(X,Y,X):-X>Y,!.
max_2(_,Y,Y).
max_3(X,Y,U,Z):-max_2(X,Y,T),max_2(T,U,Z).
fact(X,_):-X<0,write("NE MOZHNO"),!,fail.
fact(0,1):-!.
fact(N,X):-N1 is N-1,fact(N1,X1),X is N*X1.
fact1(X,N):-fact1(X,0,1,N).
fact1(X,X,F,F):-!.
fact1(X,K,F,N):-K1 is K+1,F1 is F*K1,fact1(X,K1,F1,N).
fib(1,1):-!.
fib(2,1):-!.
fib(N,X):-N1 is N-1, N2 is N-2,fib(N1,X1),fib(N2,X2), X is X1+X2.
fibo(N,X):-fibo(1,1,2,N,X).
fibo(_,F,N,N,F):-!.
fibo(A,B,K,N,X):-C is A+B, K1 is K+1,fibo(B,C,K1,N,X).
prost(2):-!.
prost(X):-prost(2,X).
prost(X,X):-!.
prost(K,X):-Ost is X mod K,Ost=0,!,fail.
prost(K,X):-K1 is K+1,prost(K1,X).
delit(X,N):-delit(X,X,N).
delit(X,K,K):-Ost is X mod K,Ost=0,prost(K),!.
delit(X,K,N):-K1 is K-1, delit(X,K1,N).



