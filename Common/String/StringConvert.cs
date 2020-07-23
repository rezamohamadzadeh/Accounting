using System;
using System.Collections.Generic;
using System.Text;


public static class StringConvert
{
    public static string[] stringToArray(this string input)
    {
        if (input.Contains(","))
        {
            return input.Split(',');
        }
        string[] s = { input };
        return s;
    }
    public static string stringToArraySelectIndex(this string[] input, int select)
    {
        if (input.Length >= select)
        {
            return input[select];
        }
        return null;
    }
    public static string DeleteSelectedString(this string input, string selected)
    {
        if (input.Contains(selected))
        {
            return input.Replace(selected, "");
        }
        return input;
    }
}
