\ This is a PRNG implementation based on the Middle Square Weyl Sequence
\ PRNG algorithm. This is a 64-bit implementation (QWORD).

\ Variables
variable xterm
variable wterm
variable seed

: sqre ( n -- n*n ) dup * ;

\ Initialise the PRNG
: init ( -- )
  0 xterm !
  0 wterm !
  time&date * * + + + sqre seed !
;

\ Generate a new x term
: randomise ( -- u )
  xterm @ sqre xterm !
  wterm @ seed @ + wterm !
  xterm @ wterm @ + xterm !
  xterm @ 32 rshift xterm @ 32 lshift or dup xterm !
;

\ Next random integer within the interval 0 <= n < k; k > 1
: nextint ( k -- n ) randomise swap mod ;

\ Next m random integers within the interval 0 <= n < k; k > 1; m >= 1
: nextints ( k m -- n1 n2 .. nm )
  0 ?do
    dup
    nextint
    swap
  loop
    drop
;

