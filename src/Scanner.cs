using System.Text;
using Compiler.Scanners;
using ECP.Parser;

namespace Compiler;

static class Scanner
{
    public static void Build(string text)
    {
        var lexer = new Lexer();
        ECParser.OnDefineLexer(lexer);
        foreach(var t in lexer.GetTokens())
        {
            Console.WriteLine(t);
        }

        foreach(var t in lexer.Parse(text))
        {
            Console.WriteLine(t);
        }
    }
}