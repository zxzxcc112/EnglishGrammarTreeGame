using System;
using System.Text.RegularExpressions;

public class MessageHandler
{
    public static string GetTextInParentheses(string text)
    {
        // �w�q�ΨӤǰt�A�����e�����W��F��
        string pattern = @"\((.*?)\)";

        // �إߥ��W��F����H
        Regex regex = new Regex(pattern);

        // �M��Ĥ@�Ӥǰt��
        Match match = regex.Match(text);

        // �p�G���ǰt��
        if (match.Success)
        {
            // ��^�A�����e
            return match.Groups[1].Value;
        }
        else
        {
            // �S�����ǰt���A��^��Ŧ�
            return text;
        }
    }
}
