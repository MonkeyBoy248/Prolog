dynamic writer/23.
question1(X1):-write("Это русский писатель?").
write("1. Да"),nl,
write("0. Нет"),nl,
read(X1).
question2(X2):-write("Из какой он страны?").
write("6. Англия"),nl,
write("5. Америка"),nl,
    write("4.Ирландия"),nl,
    write("3. Шотландия"),nl,
    write("2. Австрия"),nl,
    write("1. Германия"),nl,
    write("0. Франция"),nl,
read(X2).
question3(X3):-	write("В каком веке работал писатель?"),nl,
				write("2. На стыке 19-20"),nl,
                                write("1. 19"),nl,
				write("0. 20"),nl,
				read(X3).
question4(X4):-	write("В какой половине века он работал?"),nl,
				write("3. В первой"),nl,
				write("2. Во второй"),nl,
				write("1. В обеих"),nl,
				write("0. На стыке веков"),nl,
				read(X4).
pr:-	question1(X1),question2(X2),question3(X3),question4(X4),


		write(X).



