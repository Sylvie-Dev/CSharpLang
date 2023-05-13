
using Voxelicious.Lexer.Token;

namespace Voxelicious.Access
{
    public enum AccessModifier
    {
        Public,
        Private,
        Protected
    }

    public static class AccessModifierExtensions
    {
        public static string ToStr(this AccessModifier modifier) => modifier switch
        {
            AccessModifier.Public => "public",
            AccessModifier.Private => "private",
            AccessModifier.Protected => "protected",
            _ => "public"
        };

        public static AccessModifier FromToken(TokenType type) => type switch
        {
            TokenType.Public => AccessModifier.Public,
            TokenType.Private => AccessModifier.Private,
            TokenType.Protected => AccessModifier.Protected,
            _ => AccessModifier.Public
        };

        public static AccessModifier FromStr(string str) => str switch
        {
            "public" => AccessModifier.Public,
            "private" => AccessModifier.Private,
            "protected" => AccessModifier.Protected,
            _ => AccessModifier.Public
        };

        public static string ToStr(this AccessModifier modifier, bool isConstant) => modifier switch
        {
            AccessModifier.Public => isConstant ? "pub const" : "pub",
            AccessModifier.Private => isConstant ? "priv const" : "priv",
            AccessModifier.Protected => isConstant ? "prot const" : "prot",
            _ => "pub"
        };
    }
}