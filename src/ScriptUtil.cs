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

        foreach(var t in lexer.Parse(text))
        {
            Console.WriteLine(t);
        }
    }

    private static string compat(string text)
    {
        var builder = new StringBuilder();
        var lines = Regex.Split(text, "\r\n|\r|\n");
        foreach (var line in lines)
        {
            text = line.Trim();
            if(text == string.Empty)continue;
            var mp = Regex.Match(text, @"^print\s+(.*)$", RegexOptions.IgnoreCase);
            if (mp.Success)
            {
                builder.Append($"print \"{mp.Groups[1].Value}\"");
            }
            else
            {
                builder.Append(text);
            }
            builder.Append("\r\n");
        }
        return builder.ToString();
    }
}