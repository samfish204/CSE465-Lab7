using System;
using System.Collections.Generic;

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
 *      LPAREN, RPAREN, EQUALS:  (, ), and = literals
 *      OP_PLUS, OP_MINUS: + and - literals
 *      OP_MULTIPLY, OP_DIVIDE:  * and / literals
 *      OP_EXPONENT: ** literal (x**2 is "x squared"). 
 *Grammar:
 *      <stmt> ::= <assign> | <expr>
 *      <assign> ::= ID = <expr>
 *      <expr> ::= <term> | <term> + <expr> | <term> - <expr>
 *      <term> ::= <factor> | <factor> * <term> | <factor> / <term>
 *      <factor> ::= <base>**<factor> | <base>
 *      <base> := ID | NUM |  (<expr>)
 */
public class ExpressionParser {
    
    public enum Symbol
    {
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
    public class Node{
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

    
    }

    public static void Main(string[] args){
        try {
            foreach (Node n in tokenize(new System.IO.StreamReader(Console.OpenStandardInput()))){
                Console.WriteLine($"{Enum.GetName(typeof(Symbol), n.symbol),-15}\t{n.text}");
            }
        } catch (Exception e){
            Console.WriteLine(e.Message);
        }
    }
}
