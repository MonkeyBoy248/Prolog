let rec prime_check (num:int) = function
    n when (num > 1 && n < num && ((num % n) = 0)) -> false
    | n when num <= 1 -> false
    | n when n = num -> true
    | n -> prime_check num (n+1)

let rec check_left (num:int) = function
    n when (n >= 10 && (prime_check (num % n) 2 = false)) -> false
    | n when n < 10 -> true
    | n -> check_left num (n/10)

let rec check_right (num:int) = function
    n when (n < num && (prime_check ((int)(System.Math.Truncate((float)(num/n)))) 2) = false) -> false
    | n when n >= num -> true
    | n -> check_right num (n*10)

let rec num_div num = function
    n when n < num -> num_div num (n*10)
    | n -> (n/10)

let rec prime_crit_nums num = function
    n when (num < 1000000 && (prime_check num 2) = true && (check_left num (num_div num 1)) = true && (check_right num 10) = true) -> prime_crit_nums (num+1) (n+num)
    | n when num > 1000000 -> n
    | n -> prime_crit_nums (num+1) n

[<EntryPoint>]
let main argv = 
System.Console.WriteLine((prime_crit_nums 10 0).ToString())
System.Console.ReadKey()
0
