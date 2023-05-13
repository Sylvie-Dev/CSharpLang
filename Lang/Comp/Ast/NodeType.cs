
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Voxelicious.Ast
{
    public enum NodeType
    {
        Program,
        VariableDeclaration,
        ReturnStatement,
        IfStatement,
        ElseStatement,
        ForStatement,
        WhileStatement,
        BreakStatement,
        ContinueStatement,
        BlockStatement,
        EmptyStatement,
        

        // Expressions
        AssignmentExpr,
        LogicalExpr,
        ComparisonExpr,
        BinaryExpr,
        UnaryExpr,
        ComplexExpr,
        MemberExpr,
        CallExpr,


        // Literals
        PropertyLiteral,
        ObjectLiteralExpr,
        ObjectLiteral,
        ArrayLiteralExpr,
        
        IntegerLiteral,
        LongLiteral,
        FloatLiteral,
        DoubleLiteral,
        ShortLiteral,
        ByteLiteral,
        CharLiteral,
        
        
        StringLiteral,
        BooleanLiteral,
        NullLiteral,
        Identifier,

        EmptyExpr
    }
}