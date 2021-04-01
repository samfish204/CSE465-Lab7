

all: lex.exe

check: check-example1

check-example1: example1.output
	cmp example1.ouput example1.expected

example1.output: lex.exe example1.input
	mono lex < example1.input > example1.output

lex.exe: lex.cs
	csc lex.cs
