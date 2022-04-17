using System;
using System.Collections.Generic;

// Edited By: Samuel Fisher
// CSE 465
// Dr. Femiani
// Lab 07
// Due April 18th

/**
 * This lab is the FIRST part of a parser lab.  This week we focus on lexing. 
 * Eventually, we will build a parser for a simple language that supports assignments
 *     var = expr
 * Typeing an expression by itself will display the value of the expression
 *
 * Tokens:
 *      ID:  Any letter followed be a sequence of letters and numbers
 *      REAL: An optional sign followed by a sequence of digits, optionally with single decimal point. 
 *      WS:  Whitespace (no tokens generated, this is skipped)
 *      LIT: (, ), +, -, *, /, =
 *Grammar:
 *   <stmt> ::= <assign> | <expr>
 *   <assign> ::= ID = <expr> 
 *   <expr> ::= <term> {(+|-) <term>}
 *   <term> ::= <factor> { (*|/) <factor> }
 *   <factor> ::= <base>[**<factor>]
 *   <base> := ID | NUM |  (<expr>)

 */
public class ExpressionParser {
    
    public enum Symbol {
		  INVALID,
		  ID,
		  LITERAL,
		  NUM,
		  WS
    }

    /**
     * Represents a node in a parse tree. 
     * - Should keep track of the 'text' of the node (the substring under the node)
     * - Should keep track of the line and column where the node begins. 
     * - Should keep track of the children of the node in the parse tree
     * - should keep track of the Symbol (see the enum) corresponding to the node
     * - Tokens are leaf nodes (the array of children should be null)
     * - Needs a constructor with symbol, text, line, and column
     **/
    public class Node {
		public string Text;
		public int Line;
		public int Column;
    public Symbol Token;
		private List<Node> children = new List<Node>();
		
		public Node(Symbol token, string text, int line, int column) {
		  Text = text;
			Token = token;
			Line = line;
			Column = column;
			children = new List<Node>();
		}
  }

    /**
     * Generator for tokens. 
     * Use 'yield return' to return tokens one at a time.
     **/
    public static IEnumerable<Node> tokenize(System.IO.StreamReader src) 
    {
      int line = 1;
      int column = 1;
      System.Text.StringBuilder lexeme = new System.Text.StringBuilder();
      Symbol token = Symbol.WS;
		  int state = 0;
		
		  while (!src.EndOfStream) {
        char current = (char)src.Peek();
			  switch (state) {
				  case 0:
					  if (current == '\n') {
              state = 0;
							token = Symbol.WS;
              src.Read();
							line ++;
							column = 1;
            } else if (char.IsWhiteSpace(current)) {
              token = Symbol.WS;
              state = 0;
              src.Read();
              column++;
            } else if (current == '+' || current == '-') {
              token = Symbol.LITERAL;
              state = 1;
              lexeme.Append(current);
              src.Read();
              column++;
            } else if (char.IsDigit(current)) {
              token = Symbol.NUM;
              state = 2;
              lexeme.Append(current);
              src.Read();
              column++;
            } else if (current == '*') {
              token = Symbol.LITERAL;
              state = 4;
              lexeme.Append(current);
              src.Read();
              column++;
            } else if (current == '_' || char.IsLetter(current)) {
              token = Symbol.ID;
              state = 6;
              lexeme.Append(current);
              src.Read();
              column++;
            } else if (current == '=' || current == '(' || current == ')') {
              token = Symbol.LITERAL;
              state = 5;
              lexeme.Append(current);
              src.Read();
              column++;
            } else {
              throw new Exception($"Invalid character '{current}' at line {line} column {column}");
            }
					break;
				  case 1:
            if (char.IsDigit(current)) {
              token = Symbol.NUM;
              state = 2;
              lexeme.Append(current);
              src.Read();
              column++;
            } else {
              yield return new Node(token, lexeme.ToString(), line, column);
              lexeme.Clear();
              token = Symbol.INVALID;
              state = 0;
            }
					break;
          case 2: 
            if (char.IsDigit(current)) {
              token = Symbol.NUM;
              state = 2;
              lexeme.Append(current);
              src.Read();
              column++;
            } else if (current == '.') {
              token = Symbol.NUM;
              state = 3;
              lexeme.Append(current);
              src.Read();
              column++;
            } else {
              yield return new Node(token, lexeme.ToString(), line, column);
              lexeme.Clear();
              token = Symbol.INVALID;
              state = 0;
            }
          break;
          case 3:
            if (char.IsDigit(current)) {
              token = Symbol.NUM;
              state = 3;
              lexeme.Append(current);
              src.Read();
              column++;
            } else {
              yield return new Node(token, lexeme.ToString(), line, column);
              lexeme.Clear();
              token = Symbol.INVALID;
              state = 0;
            }
          break;
          case 4:
            if (current == '*') {
              token = Symbol.LITERAL;
              state = 5;
              lexeme.Append(current);
              src.Read();
              column++;
            } else {
              yield return new Node(token, lexeme.ToString(), line, column);
              lexeme.Clear();
              token = Symbol.INVALID;
              state = 0;
            }
          break;
          case 5: 
            yield return new Node(token, lexeme.ToString(), line, column);
            lexeme.Clear();
            token = Symbol.INVALID;
            state = 0;
            break;
          case 6:
            if (char.IsLetterOrDigit(current) || current == '_') {
              token = Symbol.ID;
              state = 6;
              lexeme.Append(current);
              src.Read();
              column++;
            } else {
              yield return new Node(token, lexeme.ToString(), line, column);
              lexeme.Clear();
              token = Symbol.INVALID;
              state = 0;
            }
          break;
			  }
		  }
		
      if (token == Symbol.INVALID) {
        throw new Exception("End of stream before finishing token {token}");
      } else if (token != Symbol.WS) {
        yield return new Node(token, lexeme.ToString(), line, column);
      }
    }

    public static void Main(string[] args) {
      try {
        System.IO.Stream stream;
        bool testing = false;
        if (testing) {
          string src = "a + b";
          stream = new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(src));
        } else {
          stream = Console.OpenStandardInput();
        }
			// set stream = Console.OpenStandardInput(); when submitting instead of the two lines above ^
        foreach (Node n in tokenize(new System.IO.StreamReader(stream))){
          Console.WriteLine($"{n.Token, -15}\t{n.Text}");
        }
        } catch (Exception e){
            Console.WriteLine(e.Message);
        }
    }
}
