using System.Text;
using Compiler.Scanners;

namespace Compiler;

static class Scanner
{
    const string comment = "#" + "[^\u000D\u000A\u0085\u2028\u2029]*";

    public static void Build(string text)
    {
        var lexer = new Lexer();
        lexer.DefineToken("A|B|X|Y|PLUS|HOME","KEY");
        lexer.DefineToken("for","KEY_FOR");
        lexer.DefineToken("next","KEY_NEXT");
        lexer.DefineToken("\\${1,2}[\\p{L}\\d_]+","VARS");
        lexer.DefineToken("_[\\p{L}\\d_]+","COUNSTS");
        lexer.DefineToken(@"\d+","INT");
        lexer.DefineToken("[\\+\\-\\*/=<>]=?","SYMBOL");
        lexer.DefineToken("[\\p{L}\\d_]+","IDENT");

        foreach(var t in lexer.Parse(text))
        {
            Console.WriteLine(t);
        }
    }
}