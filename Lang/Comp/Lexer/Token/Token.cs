
namespace Voxelicious.Lexer.Token 
{
    public enum TokenType
    {
        // Literal Types
        Integer,
        Long,
        Short,
        Float,
        Double,
        String,
        Char,
        Boolean,
        Null,
        Byte,

        
        Identifier,

        // Operator stuff
        BinaryOperator,
        Equals,

        // Keywords
        Let,
        Complex,
        Return,
        Object,
        For,
        While,
        If,
        Else,
        Fn,

        Break,
        Continue,
        
        
        // Access Modifiers
        Public,
        Private,
        Protected,


        And, // &
        Or, // |
        Comma, // ,
        Colon, // :
        Semicolon, // ;

    
        

        OpenParen, // (
        CloseParen, // )

        OpenBrace, // {
        CloseBrace, // }
        OpenBracket, // [
        CloseBracket, // ]


        LeftArrow, // <
        RightArrow, // >
        Exclamation, // !

        Dot, // .
        Grave, // `
        Tilde, // ~
        


        EOF
    }

    record TkType(TokenType Type, string Value) {
        public static TkType NewToken(TokenType type, string value) => new TkType(type, value);

        public static List<TkType> Tokens = new List<TkType> {
            new TkType(TokenType.OpenParen, "("),
            new TkType(TokenType.CloseParen, ")"),
            new TkType(TokenType.OpenBrace, "{"),
            new TkType(TokenType.CloseBrace, "}"),
            new TkType(TokenType.OpenBracket, "["),
            new TkType(TokenType.CloseBracket, "]"),
            new TkType(TokenType.BinaryOperator, "+"),
            new TkType(TokenType.BinaryOperator, "-"),
            new TkType(TokenType.BinaryOperator, "*"),
            new TkType(TokenType.BinaryOperator, "/"),
            new TkType(TokenType.BinaryOperator, "%"),
            new TkType(TokenType.Equals, "="),
            new TkType(TokenType.And, "&"),
            new TkType(TokenType.Comma, ","),
            new TkType(TokenType.Colon, ":"),
            new TkType(TokenType.Semicolon, ";"),
            new TkType(TokenType.LeftArrow, "<"),
            new TkType(TokenType.RightArrow, ">"),
            new TkType(TokenType.Exclamation, "!"),
            new TkType(TokenType.Dot, "."),
            new TkType(TokenType.Grave, "`"),
            new TkType(TokenType.Tilde, "~"),
            new TkType(TokenType.Or, "|"),
            
            

            
        };
    }

    record KeywordToken(string Keyword, TokenType Type) {
        public static List<KeywordToken> Keywords = new List<KeywordToken> {
            new KeywordToken("let", TokenType.Let),
            new KeywordToken("pub", TokenType.Public),
            new KeywordToken("priv", TokenType.Private),
            new KeywordToken("prot", TokenType.Protected),
            new KeywordToken("complex", TokenType.Complex),
            new KeywordToken("return", TokenType.Return),
            new KeywordToken("obj", TokenType.Object),
            new KeywordToken("for", TokenType.For),
            new KeywordToken("break", TokenType.Break),
            new KeywordToken("continue", TokenType.Continue),
            new KeywordToken("while", TokenType.While),
            new KeywordToken("if", TokenType.If),
            new KeywordToken("else", TokenType.Else),
            new KeywordToken("fn", TokenType.Fn),

            new KeywordToken("int", TokenType.Integer),
            new KeywordToken("long", TokenType.Long),
            new KeywordToken("short", TokenType.Short),
            new KeywordToken("float", TokenType.Float),
            new KeywordToken("double", TokenType.Double),
            new KeywordToken("string", TokenType.String),
            new KeywordToken("char", TokenType.Char),
            new KeywordToken("boolean", TokenType.Boolean),
            new KeywordToken("true", TokenType.Boolean),
            new KeywordToken("false", TokenType.Boolean),
            new KeywordToken("null", TokenType.Null),
            new KeywordToken("byte", TokenType.Byte),
        };

        public static bool IsReserved(string identifier) => Keywords.Any(k => k.Keyword == identifier);
        public static KeywordToken GetKeyword(string identifier) => Keywords.First(k => k.Keyword == identifier);
    }

    public interface LangToken 
    {
        TokenType Type { get; }
        string Value { get; }
    }

    enum BinaryOperator
    {
        Plus,
        Minus,
        Multiply,
        Divide,
        Modulo
    }


    class BinaryOp
    {
        public static BinaryOperator GetOperator(string op) => op switch {
            "+" => BinaryOperator.Plus,
            "-" => BinaryOperator.Minus,
            "*" => BinaryOperator.Multiply,
            "/" => BinaryOperator.Divide,
            "%" => BinaryOperator.Modulo,
            _ => throw new System.Exception("Invalid operator")
        };
    }


    public class ImplLangToken : LangToken
    {
        public TokenType Type { get; }
        public string Value { get; }

        public ImplLangToken(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString() => $"{{ value: \"{Value}\", type: {Type} }}";
    }
}