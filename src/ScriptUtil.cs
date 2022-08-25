using System.Text.RegularExpressions;
using VBF.Compilers;
using System.Text;
namespace Scripter;

static class ScriptUtil
{
    public static void Build(string text)
    {
        CompilationErrorManager errorManager = new CompilationErrorManager();
        CompilationErrorList errorList = errorManager.CreateErrorList();
        var scripter = new ECP.VBFECScript(errorManager);
        scripter.Initialize();
        var ast = scripter.Parse(text, errorList);
        foreach(var s in ast.Statements)
        {
            Console.Writeline(s);
        }
    }

    private static string compat(string text)
    {
        var builder = new StringBuilder();
        var lines = Regex.Split(text, "\r\n|\r|\n");
        foreach (var line in lines)
        {
            var _text = line.Trim();
            //if(_text == string.Empty)continue;
            var mp = Regex.Match(_text, @"^print\s+(.*)$", RegexOptions.IgnoreCase);
            if (mp.Success)
            {
                builder.Append($"PRINT \"{mp.Groups[1].Value}\"");
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