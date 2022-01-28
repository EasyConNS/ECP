// See https://aka.ms/new-console-template for more information
var text = "";
if(args.Length > 1)
{
    text = File.ReadAllText(args[1]);
}

const string Ident = @"[\d\p{L}_]+";
const string Instant = @"-?\d+";
const string Constant = @"_"+Ident;
const string VarStant = @"\${1,2}"+Ident;
const string ExtVar = @"@"+Ident;

Console.WriteLine($"code: <{text}>");
