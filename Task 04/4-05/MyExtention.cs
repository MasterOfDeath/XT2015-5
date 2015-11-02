namespace ExtensionMethods
{
    using System;

    public static class MyExtention
    {
        public static bool IsPositiveNumber(this string str)
        {
            str = str.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            int indexE = str.IndexOf('e');

            if (indexE < 0)
            {
                return CheckNoExpNotation(str);
            }
            else if (indexE != str.Length - 1 && str.IndexOf('e', indexE + 1) < 0)
            {
                return CheckExpNotation(str);
            }
            else
            {
                return false;
            }
        }

        private static bool CheckNoExpNotation(string str)
        {
            if (!char.IsDigit(str[0]) && str[0] != '+')
            {
                return false;
            }

            for (var i = 1; i < str.Length; i++)
            {
                if (!char.IsDigit(str[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckExpNotation(string str)
        {
            string[] res = str.Split(new char[] { 'e' }, StringSplitOptions.RemoveEmptyEntries);

            if (res.Length != 2)
            {
                return false;
            }

            string mantissa = res[0];
            string grade = res[1];

            if (!IsMantissaValid(mantissa) || !IsGradeValid(grade))
            {
                return false;
            }
            
            int gradeNum = (int)StrToDouble(grade);

            int indexPoint = mantissa.IndexOf(".");
            if (indexPoint < 0)
            {
                indexPoint = mantissa.Length;
            }

            int newIndexPoint = indexPoint + gradeNum;

            if (newIndexPoint < 0)
            {
                return false;
            }
            else if (newIndexPoint < (mantissa.Length - 1))
            {
                string rest = mantissa.Substring(newIndexPoint + 1);

                foreach (var item in rest)
                {
                    if (item != '0')
                    {
                        return false;
                    }   
                }
            }
            
            return true;
        }

        private static bool IsMantissaValid(string mantissa)
        {
            if (!char.IsDigit(mantissa[0]) && mantissa[0] != '+')
            {
                return false;
            }

            int isPoint = 0;

            for (var i = 1; i < mantissa.Length; i++)
            {
                if (mantissa[i] == '.')
                {
                    isPoint++;

                    if (isPoint > 1)
                    {
                        return false;
                    }
                }
                else if (!char.IsDigit(mantissa[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsGradeValid(string grade)
        {
            if (!char.IsDigit(grade[0]) && grade[0] != '+' && grade[0] != '-')
            {
                return false;
            }

            for (var i = 1; i < grade.Length; i++)
            {
                if (!char.IsDigit(grade[i]))
                {
                    return false;
                }
            }

            return true;
        }
        
        // Only for valided values, like: "-123" or "12.3"
        private static double StrToDouble(string strToDouble)
        {
            string str = strToDouble.Clone().ToString();

            int negative = 1;
            if (str[0] == '-')
            {
                str = str.Substring(1);
                negative = -1;
            }
            else if (str[0] == '+')
            {
                str = str.Substring(1);
            }

            double result = 0;
            int grade = 1;

            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i] != '.')
                {
                    result += (str[i] - '0') * grade;
                    grade *= 10;
                }
            }

            int indexPoint = str.IndexOf('.');
            if (indexPoint > 0)
            {
                result /= Math.Pow(10, str.Length - 1 - indexPoint);
            }

            return result * negative;
        }
    }
}