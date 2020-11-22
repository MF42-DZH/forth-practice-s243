\ lnot - logical not
: lnot ( flag -- !flag ) 0 = if -1 else 0 then ;

\ bottom - moves the TOS to the bottom, O(n)
: bottom ( n1 n2 .. nk-1 nk -- nk n1 n2 .. nk-1 )
  depth 1 ?do
  depth 1 - roll
  loop
;

\ reverse - reverses the stack, O(n)
: reverse ( n1 n2 .. nk-1 nk -- nk nk-1 .. n2 n1 )
  depth 0 ?do
  depth 1 - roll
  loop
;

\ squared - calculates the square of an int, O(1)
: squared ( n -- n ^ 2 ) dup * ;

\ rotright - rotates the top three items to the right, O(1)
: rotright ( n1 n2 n3 -- n3 n1 n2 ) rot rot ;

\ squaresin - prints all squares in the interval [1, k], O(n)
: squaresin ( k -- 1, 4, ..., n ^ 2 <= k )
  1 begin                 \ stk ( k it=1 )
    dup squared dup .     \ stk ( k it it^2 )
    2 pick                \ stk ( k it it^2 k )
    rot 1 + rotright      \ stk ( k it+1 it^2 k )
    >= until              \ stk ( k it+1 )
    2drop                 \ stk ( )
;

\ nextfib - calculates the next pair of fibonacci numbers, O(1)
: nextfib ( n1 n2 -- n2 n1+n2 ) tuck + ;

\ fibsin - calculates fibonacci numbers in the interval [0, k]
: fibsin ( k -- 0, 1, 1, 2 ..., f <= k )
  0 1 begin               \ stk ( k i=0 j=1 )
    2dup +                \ stk ( k i j i+j )
    3 pick                \ stk ( k i j i+j k )
    <= while              \ stk ( k i j )
    2dup                  \ stk ( k i j i j )
    nextfib               \ stk ( k i j j i+j )
    nip                   \ stk ( k i j i+j )
    rot .                 \ stk ( k j i+j )
    repeat                \ stk ( k j i+j )
    swap . . drop         \ stk ( )
;

\ NOTE - True is -1, False is 0, so we can do some manipulation
: fizz ( n -- flag ) 3 mod 0= ;
: buzz ( n -- flag ) 5 mod 0= ;
: fzbz ( n -- flag ) dup fizz swap buzz + -2 = ;
: evalfb ( n -- )
  space                   \ stk ( n )
  dup 2dup                \ stk ( n n n n )
  fzbz if                 \ stk ( n n n )
    ." fizzbuzz "
    drop drop drop        \ stk ( )
  else
    fizz if               \ stk ( n n )
      ." fizz "
      drop drop           \ stk ( )
    else
      buzz if             \ stk ( n )
        ." buzz "
        drop              \ stk ( )
      else
        .                 \ stk ( )
      then
    then
  then
;

\ fizzbuzz - does fizzbuzz for the range [1 .. n]
: fizzbuzz ( n -- ) 1 + 1 ?do i evalfb loop ;

\ star - prints a * to the console
: star ( -- ) 42 emit ;

\ stars - prints multiple *s to the console
: stars ( n -- ) 0 ?do star loop ;

\ pyramid - prints a left-hand star pyramid
: pyramid ( n -- ) cr 1 + 1 ?do i stars cr loop ;

\ underswap - swaps the two items under the TOS
\ ... I swear this name was taken by an undertale au
: underswap ( i j k -- j i k ) >r swap r> ;

\ xorswap - swaps the contents of two integer variables
\ works on the principle that:
\   a xor b xor b = a
: xorswap ( v1* v2* -- )
  3 0 ?do              \ stk ( v1* v2* )
    2dup @ swap @ swap \ stk ( v1* v2* v1 v2 )
    xor                \ stk ( v1* v2* v1-xor-v2 )
    i 1 = if
      over !           \ stk ( v1* v2* )
    else
      underswap over ! \ stk ( v2* v1* )
      swap             \ stk ( v1* v2* )
    then
  loop
  2drop                \ stk ( )
;
