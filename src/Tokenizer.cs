using System.Text;

namespace Token;

static class Tokenizer
{
    public static void Parse(ReadOnlySpan<char> s)
    {
        foreach(var l in s.EnumerateLines())
        {
            Console.WriteLine("newline:");
            var state = State.IDLE;
            StringBuilder sb = new();
            var enumRunes = l.EnumerateRunes();
            while(enumRunes.MoveNext())
            {   
                var cur = enumRunes.Current;
                switch (state)
                {
                    case State.IDLE:
                        sb.Clear();
                        sb.Append(cur);
                        if (cur.IsIdent())
                        {
                            state = State.IDENT;
                            break;
                        };
                        if (cur.IsComment())
                        {
                            state = State.COMNT;
                            break;
                        };
                        if (cur.IsDigit() || cur.Value == '-')
                        {
                            state = State.NUMS;
                            break;
                        };
                        if (cur.Value == '$' || cur.Value == '_' || cur.Value == '@')
                        {
                            state = State.SYBL;
                            break;
                        };
                    break;
                    case State.IDENT:
                    sb.Append(cur);
                    Console.WriteLine($"ident:{sb}");
                    state = State.IDLE;
                    break;
                    case State.NUMS:
                    sb.Append(cur);
                    Console.WriteLine($"num:{sb}");
                    state = State.IDLE;
                    break;
                    case State.COMNT:
                    state = State.IDLE;
                    break;
                    case State.SYBL:
                    state = State.IDLE;
                    break;
                };
                
            }
        }
    }
}

static class RuneExt
{
    public static bool IsIdent(this  System.Text.Rune b)
    {
        if (!b.IsAscii) return true;
        return b.IsLetter();
    }
    public static bool IsLetter(this System.Text.Rune b)
    {
        return b.Value >= 65 && b.Value <= 122;
    }

    public static bool IsDigit(this System.Text.Rune b)
    {
        return b.Value >= 48 && b.Value <= 57;
    }

    public static bool IsComment(this System.Text.Rune b)
    {
        return b.Value == 35; // '#'
    }
}

enum State: byte
{
    IDLE,
    IDENT,
    NUMS,
    COMNT,
    SYBL,
}