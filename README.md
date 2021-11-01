# Lab 7

 # Description
 This lab is the FIRST part of a parser lab.  This week we focus on lexing. 
 Eventually, we will build a parser for a simple language that supports assignments
 ```
      var = expr
 ```
 Typeing an expression by itself will display the value of the expression  

 Tokens:
 - `ID`:  Any letter followed be a sequence of letters and numbers  
 - `REAL`: An optional sign followed by a sequence of digits, optionally with single decimal point.   
 - `WS`:  Whitespace (no tokens generated, this is skipped)  
 - `LPAREN, RPAREN, EQUALS`:  (, ), and = literals  
 - `OP_PLUS, OP_MINUS`: + and - literals  
 - `OP_MULTIPLY, OP_DIVIDE`:  * and / literals  
 - `OP_EXPONENT`: ** literal (x**2 is "x squared").  

 Grammar:
 ```
      <stmt> ::= <assign> | <expr>
      <assign> ::= ID = <expr>
      <expr> ::= <term> | <term> + <expr> | <term> - <expr>
      <term> ::= <factor> | <factor> * <term> | <factor> / <term>
      <factor> ::= <base>**<factor> | <base>
      <base> := ID | NUM |  (<expr>)
 ```

The lexer uses a state diagram indicated in this image:
![State Diagram](state-diagram.png)

Watch the lecture for a description, I will do the whole lab and you just need to follow along. 

