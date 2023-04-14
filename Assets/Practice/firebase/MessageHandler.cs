using System;
using System.Text.RegularExpressions;

public class MessageHandler
{
    public static string GetTextInParentheses(string text)
    {
        // 定義用來匹配括號內容的正規表達式
        string pattern = @"\((.*?)\)";

        // 建立正規表達式對象
        Regex regex = new Regex(pattern);

        // 尋找第一個匹配項
        Match match = regex.Match(text);

        // 如果找到匹配項
        if (match.Success)
        {
            // 返回括號內容
            return match.Groups[1].Value;
        }
        else
        {
            // 沒有找到匹配項，返回原符串
            return text;
        }
    }
}
