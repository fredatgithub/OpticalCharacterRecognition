/*
The MIT License(MIT)
Copyright(c) 2015 Freddy Juhel
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;

namespace OpticalCharacterRecognition
{
  public static class Punctuation
  {
    public const string Comma = ",";
    public const string Colon = ":";
    public const string OneSpace = " ";
    public const string Dash = "-";
    public const string UnderScore = "_";
    public const string SignAt = "@";
    public const string Ampersand = "&";
    public const string SignSharp = "#";
    public const string Period = ".";
    public const string Backslash = "\\";
    public const string Slash = "/";
    public const string OpenParenthesis = "(";
    public const string CloseParenthesis = ")";
    public const string OpenCurlyBrace = "{";
    public const string CloseCurlyBrace = "}";
    public const string OpenSquareBracket = "[";
    public const string CloseSquareBracket = "]";
    public const string LessThan = "<";
    public const string GreaterThan = ">";
    public const string DoubleQuote = "\"";
    public const string SimpleQuote = "'";
    public const string Tilde = "~";
    public const string Pipe = "|";
    public const string Plus = "+";
    public const string Minus = "-";
    public const string Multiply = "*";
    public const string Divide = "/";
    public const string Dollar = "$";
    public const string Pound = "£";
    public const string Percent = "%";
    public const string QuestionMark = "?";
    public const string ExclamationPoint = "!";
    public const string Chapter = "§";
    public const string Micro = "µ";
    public static string CrLf = Environment.NewLine;

    public static string Tabulate(ushort numberOfTabulation = 1)
    {
      string result = string.Empty;
      for (int number = 0; number < numberOfTabulation; number++)
      {
        result += " ";
      }

      return result;
    }
  }
}