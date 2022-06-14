using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Compiler.Scanners;

public class Lexer
{
    private ICollection<TokenInfo> rules = new List<TokenInfo>();
    private Regex ignores = new Regex(@"[ \f\t]");// whitespace
    private Regex linebreak = new Regex(@"\r\n|\r|\n");// linebreak

    private StringBuilder output = new();

    public ICollection<TokenInfo> Get()
    {
        return rules;
    }

    public TokenInfo DefineToken(string definition, string description)
    {
        var reg = new Regex("^" + definition);
        var tag = new Token(description, rules.Count);
        var token = new TokenInfo(reg, tag);
        rules.Add(token);
        return token;
    }

    public IEnumerable<Lexeme> Parse(string text)
    {
        var lines = Regex.Split(text, "\r\n|\r|\n");
        int row = 0;
        foreach(var line in lines)
        {
            row += 1;
            int position = 0;
            int col = 0;

            while(true)
            {
                while(position < line.Length && ignores.Match(line[position].ToString()).Success)
                {
                    position += 1;
                    col += 1;
                }
                if(position >= line.Length)
                {
                    break;
                }
                output.Clear();
                Token token = null;
                foreach(var rule in rules)
                {
                    var m = rule.Match(line[position..]);
                    if (m.Success)
                    {
                        output.Append(m.Value);
                        token = rule.Tag;
                        break;
                    }
                }
                if (token != null)
                {
                    var s = output.ToString();
                    position += s.Length;
                    col += s.EnumerateRunes().Count();
                    yield return new Lexeme(s, token, this, position, col, row);
                }
                else
                {
                    break;
                }
            }
            yield return new Lexeme("NEW_LINE", new Token("-newline-", -1), this, -1, -1, row);
        }  
    }
}
