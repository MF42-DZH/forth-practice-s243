\ This is a rudimentary implementation of the LCG algorithm for generating
\ random numbers.

\ Current term
variable ctrm

\ Modulus
variable modl

\ Multiplier
variable mult

\ Increment
variable incr

\ init - Initialise all the variables using current time
\ note that the multiplier must be odd otherwise infinite sequences of even
\ numbers can occur
: init ( -- ) 
  time&date + + + + + ctrm !                       \ Init term
  time&date * * + + + modl !                       \ Init modulus
  time&date + * + - + modl 1 - mod 1 + 1 or mult ! \ Init multiplier
  time&date * + - + - modl mod incr !              \ Init increment
;

\ sinit - Initialise all the variables using a seed
: sinit ( n -- )
  dup dup dup             \ stk ( n n n n )
  dup * ctrm !            \ stk ( n n n )
  65535 mod modl !        \ stk ( n n )
  modl 1 - mod 1 + mult ! \ stk ( n )
  dup + 64 /mod * incr !  \ stk ( )
;

\ randomise - Generate a random integer between the min & max integer bounds
: randomise ( -- n ) mult @ ctrm @ * incr @ + modl @ mod dup 1 xor ctrm ! ;

\ nextint - Get the next int within the interval 0 <= n < k; k > 1
: nextint ( k -- n ) randomise swap mod ;

\ nextInts - Get the next m integers within the interval
\ 0 <= n < k; m>= 1, k > 1
: nextints ( k m -- n1 n2 .. nm )
  0 ?do     \ stk ( k )
    dup     \ stk ( k k )
    nextint \ stk ( k n1 )
    swap    \ stk ( n1 k )
  loop
    drop    \ stk ( n1 .. nm )
;
