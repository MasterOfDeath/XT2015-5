//-----------------------------------------------------------------------
// <copyright file="MyString.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _2_04
{
    using System;

    /// <summary>
    /// My own string class
    /// </summary>
    public class MyString
    {
        private char[] str;
        
        public MyString(string str)
        {
            this.str = new char[str.Length];
            str.ToCharArray().CopyTo(this.str, 0);
        }
        
        public MyString(char[] str)
        {
            this.str = new char[str.Length];
            str.CopyTo(this.str, 0);
        }
        
        public int Size
        {
            get { return this.str.Length; }
        }
        
        public char this[int id]
        {
            get { return this.str[id]; }
            set { this.str[id] = value; }
        }
        
        public static MyString operator +(MyString obj1, MyString obj2)
        {
            var tmp = new char[obj1.Size + obj2.Size];
            obj1.str.CopyTo(tmp, 0);
            obj2.str.CopyTo(tmp, obj1.Size);

            return new MyString(tmp);
        }
        
        public static bool operator <(MyString obj1, MyString obj2)
        {
            return obj1.Size < obj2.Size;
        }
        
        public static bool operator >(MyString obj1, MyString obj2)
        {
            return obj1.Size > obj2.Size;
        }

        public static implicit operator string(MyString obj1)
        {
            return new string(obj1.str);
        }
        
        public static explicit operator char[](MyString obj1)
        {
            return obj1.str;
        }

        public static explicit operator MyString(char[] obj1)
        {
            return new MyString(obj1);
        }

        public static explicit operator MyString(string obj1)
        {
            return new MyString(obj1);
        }
    }
}
