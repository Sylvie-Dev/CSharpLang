using System.Collections;
using System.Text.RegularExpressions;
using Voxelicious.Lexer.Token;
using Voxelicious.Util.List;
using Voxelicious.Lexer.Error;

namespace Voxelicious.Lexer
{
    public interface ILexer
    {
        IEnumerable<LangToken> Tokenize(string input);
    }

    public class Lexer : ILexer
    {
        private ErrorLevel level;

        public Lexer(ErrorLevel level)
        {
            this.level = level;
        }

        public Lexer() : this(ErrorLevel.Fatal) {}

        public IEnumerable<LangToken> Tokenize(string input)
        {
            List<LangToken> tokens = new List<LangToken>();
            ShiftList<char> src = new ShiftList<char>(input.ToCharArray());
            int line, column;
            line = 0;
            column = 0;

            while(src.Count > 0)
            {
                char c = src.Shift();
                if (c == '\n')
                {
                    line++;
                    column = 0;
                }
                else
                {
                    column++;
                }
                string cstring = c.ToString();
                // check for comments
                if (c == '/' && src.Count > 0 && src[0] == '/')
                {
                    while (src.Count > 0 && src[0] != '\n') src.Shift();
                    continue;
                }
                else if (c == '/' && src.Count > 0 && src[0] == '*')
                {
                    while (src.Count > 0 && !(src[0] == '*' && src[1] == '/')) src.Shift();
                    src.ShiftMany(2);
                    continue;
                }

                bool found = false;
                for (int i = 0; i < TkType.Tokens.Count; i++)
                {
                    if (TkType.Tokens[i].Value == cstring)
                    {
                        found = true;
                        tokens.Add(NewToken(TkType.Tokens[i].Type, cstring));
                        break;
                    }
                }
                if (!found)
                {
                    if (cstring == "\"")
                    {
                        string str = "";
                        while (src.Count > 0 && src[0] != '\"')
                        {
                            str += src.Shift();
                            column++;
                        }
                        if (src.Count == 0) LogAndExit(new UnexpectedTokenException(line, column, "EOF"));
                        src.Shift();
                        column++;
                        tokens.Add(NewToken(TokenType.String, str));
                    }
                    else if (cstring == "\'")
                    {
                        string str = "";
                        while (src.Count > 0 && src[0] != '\'')
                        {
                            str += src.Shift();
                            column++;
                        }
                        if (src.Count == 0) LogAndExit(new UnexpectedTokenException(line, column, "EOF"));
                        src.Shift();
                        column++;
                        tokens.Add(NewToken(TokenType.Char, str));
                    }
                    else if (IsNumber(cstring))
                    {
                        string number = cstring;
                        while (src.Count > 0 && (IsNumber(src[0].ToString()) 
                            || src[0] == '.'
                            || src[0] == 'f'
                            || src[0] == 'F'
                            || src[0] == 'd'
                            || src[0] == 'D'
                            || src[0] == 'l'
                            || src[0] == 'L'))
                        {
                            number += src.Shift();
                            column++;
                        }
                        
                        if (IsInteger(number)) tokens.Add(NewToken(TokenType.Integer, number));
                        else if (IsLong(number)) tokens.Add(NewToken(TokenType.Long, number));
                        else if (IsFloatingPoint(number)) tokens.Add(NewToken(TokenType.Float, number));
                        else if (IsDouble(number)) tokens.Add(NewToken(TokenType.Double, number));
                        else LogAndExit(new UnexpectedTokenException(line, column, number));
                    }
                    else if (IsAlphabetic(cstring))
                    {
                        string identifier = cstring;
                        while (src.Count > 0 && IsAlphanumeric(src[0].ToString()))
                        {
                            identifier += src.Shift();
                            column++;
                        }

                        if (KeywordToken.IsReserved(identifier))
                        {

                            tokens.Add(NewToken(KeywordToken.GetKeyword(identifier).Type, identifier));
                        }
                        else
                        {
                            tokens.Add(NewToken(TokenType.Identifier, identifier));
                        }
                    }
                    else if (ShouldSkip(cstring)) {}
                    else LogAndExit(new UnexpectedTokenException(line, column, cstring));
                }
            }
            tokens.Add(NewToken(TokenType.EOF, "EndOfFile"));
            return tokens;
        }

        public bool CanLog(ErrorLevel level, LexerException e) => level switch {
            ErrorLevel.Fatal => true,
            ErrorLevel.Error => e.Level == ErrorLevel.Error || e.Level == ErrorLevel.Fatal,
            ErrorLevel.Warn => e.Level == ErrorLevel.Error || e.Level == ErrorLevel.Fatal || e.Level == ErrorLevel.Warn,
            _ => false
        };

        public void Log(LexerException e)
        {
            if (CanLog(level, e))
            {
                System.Console.WriteLine("[Lexer Error] ({0}:{1}) [{2}] {3}", e.Line, e.Column, e.Level, e.Message);
            } 
        }

        public void LogAndExit(LexerException e)
        {
            Log(e);
            System.Environment.Exit(1);
        }

        public bool IsString(string source) => Regex.IsMatch(source, @"^"".*""$");
        public bool IsChar(string source) => Regex.IsMatch(source, @"^'.*'$");
        public bool IsAlphabetic(string source) => Regex.IsMatch(source, @"^[a-zA-Z]+$");
        public bool IsNumeric(string source) => Regex.IsMatch(source, @"^[0-9]+$");
        public bool IsAlphanumeric(string source) => Regex.IsMatch(source, @"^[a-zA-Z0-9]+$");
        public bool IsInteger(string source) => Regex.IsMatch(source, @"^[0-9]+(_[0-9]+)*$");
        public bool IsLong(string source) => Regex.IsMatch(source, @"^[0-9]+(L|l)$");
        public bool IsFloatingPoint(string source) => Regex.IsMatch(source, @"^[0-9]+\.[0-9]+(f|F)?$");
        public bool IsDouble(string source) => Regex.IsMatch(source, @"^[0-9]+\.[0-9]+(d|D)$");
        public bool IsNumber(string source) => IsInteger(source) || IsLong(source) || IsFloatingPoint(source) || IsDouble(source);
        public bool ShouldSkip(string str) => Regex.IsMatch(str, @"^\s+$");


        public LangToken NewToken(TokenType type, string value) => new ImplLangToken(type, value);
    }

      

}