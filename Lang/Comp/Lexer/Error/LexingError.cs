

namespace Voxelicious.Lexer.Error
{

    public class UnexpectedTokenException : LexerException
    {
        public UnexpectedTokenException(int line, int column, string token) : base(line, column, ErrorMessage.New("Unexpected token '{0}'", token), ErrorLevel.Error) {}
    }
}