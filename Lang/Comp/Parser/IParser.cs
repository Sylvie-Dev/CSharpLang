using System.Collections.Generic;

using Voxelicious.Ast;
using Voxelicious.Lexer;
using Voxelicious.Lexer.Token;
using Voxelicious.Util.List;
using Voxelicious.Ast.Statement;
using Voxelicious.Ast.Expression;
using Voxelicious.Ast.LiteralExpression;

namespace Voxelicious.Parser
{

    public interface IParser
    {
        ShiftList<LangToken> Tokens { get; set; }
        
        ProgramStatement ProduceAST(string src, ILexer lexer);

        LangToken At() => Tokens[0];
        LangToken Eat() => Tokens.Shift();
        LangToken[] EatMany(int count) => Tokens.ShiftMany(count);
        LangToken? Peek() {
            if (Tokens.Count < 2) return null;
            return Tokens[1];
        }

        LangToken Expect(TokenType token, string message)
        {
            if (At().Type != token) throw new System.Exception("[Parser Exception] -> " + message + " -> " + At().ToString());
            return Eat();
        }

        bool NotEof() => At().Type != TokenType.EOF;
    }


    public abstract class ParserBase : IParser
    {


        ShiftList<LangToken> IParser.Tokens { get; set; } = new ShiftList<LangToken>(); 

    
        public abstract ProgramStatement ProduceAST(string src, ILexer lexer);

        public IParser GetParser() => (IParser) this;

        public void SetTokens(params LangToken[] tokens) => GetParser().Tokens = new ShiftList<LangToken>(tokens);
        public void SetTokens(IEnumerable<LangToken> tokens) => GetParser().Tokens = new ShiftList<LangToken>(tokens);



        public LangToken At() => GetParser().Tokens[0];
        public LangToken Eat() => GetParser().Tokens.Shift();

        public LangToken[] EatMany(int count) => GetParser().Tokens.ShiftMany(count);
        
        public LangToken? Peek() {
            if (GetParser().Tokens.Count < 2) return null;
            return GetParser().Tokens[1];
        }

        public LangToken Expect(TokenType token, string message)
        {
            if (At().Type != token) throw new System.Exception("[Parser Exception] -> " + message + " -> " + At().ToString());
            return Eat();
        }

        public LangToken Expect(string message, TokenType token, string value)
        {
            if (At().Type != token || At().Value != value) throw new System.Exception("[Parser Exception] -> " + message + " -> " + At().ToString());
            return Eat();
        }

        public LangToken ExpectMultiple(string message, params TokenType[] tokens)
        {
            if (!tokens.Contains(At().Type)) throw new System.Exception("[Parser Exception] -> " + message + " -> " + At().ToString());
            return Eat();
        }

        public LangToken? AdvanceIf(TokenType token)
        {
            if (At().Type == token) return Eat();
            return null;
        }

        public LangToken AdvanceIf(params TokenType[] tokens)
        {
            if (tokens.Contains(At().Type)) return Eat();
            throw new System.Exception("[Parser Exception] -> Expected one of the following tokens: " + string.Join(", ", tokens) + " -> " + At().ToString());
        }

        public bool NotEof() => At().Type != TokenType.EOF;

        public string CurrentValue() => At().Value;
        
        public TokenType CurrentType() => At().Type;

        public bool IsValue(string value) => CurrentValue() == value;

        public bool IsValue(params string[] values) => values.Contains(CurrentValue());
        
        public bool IsType(TokenType type) => CurrentType() == type;

        public bool IsType(params TokenType[] types) => types.Contains(CurrentType());
    }
}