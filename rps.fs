\ Imports random functions
s" prng-msws.fs" included

\ move delaration
0 constant rock
1 constant paper
2 constant scissors

\ wflag declarations
-1 constant tie
0 constant plhuman
1 constant plforth

\ land - logical and
: land ( flag flag -- flag )
  2dup = if
    true = if
      drop
      true
    else
      drop
      false
    then
  else
    2drop
    false
  then
;

\ checkwin - checks who won
: checkwin ( pmove fmove -- wflag )
  2dup = if 2drop tie else
    2dup rock     = swap paper    = land if 2drop plhuman exit then
    2dup paper    = swap scissors = land if 2drop plhuman exit then
    2dup scissors = swap rock     = land if 2drop plhuman exit then
    2dup rock     = swap scissors = land if 2drop plforth exit then
    2dup paper    = swap rock     = land if 2drop plforth exit then
    2dup scissors = swap paper    = land if 2drop plforth exit then
  then
;

\ forthmove - forth's move
: forthmove ( -- u ) 3 nextint ;

\ legal - checks if a player's move is legal
: legal ( n -- n flag ) dup 0 >= swap 3 < land ;

\ scores
variable humanscore
variable forthscore
variable goalscore

\ printscore - prints the current scores
: printscore ( -- )
  ." You " humanscore ?
  ." -" space
  forthscore ? ." Forth"
  cr
;

\ printmove - prints a move
: printmove ( pl move -- )
  swap plhuman = if ." You threw " else ." Forth threw " then
  dup rock = if ." Rock!" drop cr exit then
  dup paper = if ." Paper!" drop cr exit then
  dup scissors = if ." Scissors!" drop cr exit then
;

\ printstatus - prints the outcome of a pair of moves
: printstatus ( wflag -- )
  dup dup tie = if ." It's a tie!" else
    plhuman = if
      ." You won this round!"
    else
      ." Forth won this round!"
    then
  then
  cr
;

\ printresult
: printresult ( -- )
  humanscore @ goalscore @ >= if
    ." You won the game!" cr exit
  then
  forthscore @ goalscore @ >= if
    ." Forth won the game!" cr exit
  then
;

\ startgame - starts a new game until one person reaches n points
: startgame ( n -- )
  goalscore !
  0 humanscore !
  0 forthscore !

  init

  cr ." Playing a first to " goalscore ? ." !" cr
;

\ != ... is it not obvious?
: != ( n n -- flag ) = if false else true then ;

\ domove - game logic
: domove ( n -- )
  cr
  plhuman over printmove
  forthmove plforth over printmove
  checkwin
  dup printstatus

  dup dup tie != if
    plhuman = if
      humanscore @ 1 + humanscore !
    else
      forthscore @ 1 + forthscore !
    then
  else
    drop
  then

  printscore
  printresult
;
