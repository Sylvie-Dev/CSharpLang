

namespace Voxelicious.Lexer.Error
{
    public enum ErrorLevel
    {
        None,
        Warn,
        Error,
        Severe,
        Fatal
    }
    
    public class ErrorMessage
    {
        private string message;
        private List<object> args;
        
        public ErrorMessage(string message, params object[] args)
        {
            this.message = message;
            this.args = args.ToList();
        }

        public override string ToString()
        {
            return string.Format(message, args.ToArray());
        }

        public static ErrorMessage New(string message, params object[] args) => new ErrorMessage(message, args);

        public string Error => ToString();
        public string Message => message;
        public List<object> Args => args;
    }


    public class LexerException
    {
        private int line, column;
        private ErrorMessage message;
        private ErrorLevel level;

        public LexerException(int line, int column, ErrorMessage message, ErrorLevel level)
        {
            this.line = line;
            this.column = column;
            this.message = message;
            this.level = level;
        }

        public override string ToString()
        {
            return $"LexerException: {message} at line {line}, column {column}";
        }

        public static LexerException New(int line, int column, ErrorMessage message, ErrorLevel level) => new LexerException(line, column, message, level);

        public int Line => line;
        public int Column => column;
        public ErrorMessage Message => message;
        public ErrorLevel Level => level;
    }

}