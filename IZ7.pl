dynamic writer/23.
question1(X1):-write("��� ������� ��������?").
write("1. ��"),nl,
write("0. ���"),nl,
read(X1).
question2(X2):-write("�� ����� �� ������?").
write("6. ������"),nl,
write("5. �������"),nl,
    write("4.��������"),nl,
    write("3. ���������"),nl,
    write("2. �������"),nl,
    write("1. ��������"),nl,
    write("0. �������"),nl,
read(X2).
question3(X3):-	write("� ����� ���� ������� ��������?"),nl,
				write("2. �� ����� 19-20"),nl,
                                write("1. 19"),nl,
				write("0. 20"),nl,
				read(X3).
question4(X4):-	write("� ����� �������� ���� �� �������?"),nl,
				write("3. � ������"),nl,
				write("2. �� ������"),nl,
				write("1. � �����"),nl,
				write("0. �� ����� �����"),nl,
				read(X4).
pr:-	question1(X1),question2(X2),question3(X3),question4(X4),


		write(X).



