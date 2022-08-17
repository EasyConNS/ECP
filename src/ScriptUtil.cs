using System.Text.RegularExpressions;

using System.Text;
namespace Scripter;

static class ScriptUtil
{
    public static void Build(string text)
    {
        Compiler.Scanners.Lexer lexer = new();
        ECP.Parser.ECParser.OnDefineLexer(lexer);
        text = compat(text);

        List<Compiler.Scanners.Lexeme> tokens = new();

        foreach(var t in lexer.Parse(text))
        {
            if(t.Tag == ECP.Parser.ECParser.K_IF)
            {
                Console.WriteLine($"is if:{t}");
            }
            if(t.Tag == ECP.Parser.ECParser.T_NEWLINE)
            {
                Console.WriteLine($"---------------------");
            }
            else
            {
                Console.WriteLine(t);
            }
        }
    }

    private static string compat(string text)
    {
        var builder = new StringBuilder();
        var lines = Regex.Split(text, "\r\n|\r|\n");
        foreach (var line in lines)
        {
            var _text = line.Trim();
            var m = Regex.Match(_text, @"(\s*#.*)$");
            if (m.Success)
            {
                var comment = m.Groups[1].Value;
                _text = line[..^comment.Length];
            }
            if(_text == string.Empty)continue;
            var mp = Regex.Match(_text, @"^print\s+(.*)$", RegexOptions.IgnoreCase);
            if (mp.Success)
            {
                builder.Append($"print \"{mp.Groups[1].Value}\"");
            }
            else
            {
                builder.Append(_text);
            }
            builder.Append("\r\n");
        }
        return builder.ToString();
    }
}