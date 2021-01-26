using System;
using TMPro;
using UnityEngine.UI;

namespace Protodroid.Helper
{
    public static class Validate
    {
        public static double Between(this TMP_InputField input, double from, double to)
        {
            double num = Convert.ToDouble(input.text);

            if (num < from) num = from;
            else if (num > to) num = to;

            input.text = num.ToString();

            return num;
        }
        
        public static bool IsNumber(this TMP_InputField input)
        {
            try
            {
                double num = Convert.ToDouble(input.text);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}