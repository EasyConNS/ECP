// See https://aka.ms/new-console-template for more information

using Injection;
using Compiler;

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

//PointerUtil.GetPointerAddress("[[main+427C470]+1F0]+68");

text = "_孵蛋数 = 101\n wait 500\nfor\na\nnext\r\n $3=5\nif $3 >= 5\n a\nendif\nprint \"有什么，、·能够 & $test & 阻挡test\"\n\n";

Scanner.Build(text);