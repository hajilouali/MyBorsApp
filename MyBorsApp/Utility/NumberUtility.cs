using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBorsApp.Utility
{
    public static class NumberUtility
    {
        public static int ReternMax(int num1, int num2)
        {
            if (num1>num2)
            {
                return num1;
            }

            if (num2>num1)
            {
                return num2;
            }

            return num1;
        }
        public static int ReternMin(int num1, int num2)
        {
            if (num1 < num2)
            {
                return num1;
            }

            if (num2 < num1)
            {
                return num2;
            }

            return num1;
        }
    }
}