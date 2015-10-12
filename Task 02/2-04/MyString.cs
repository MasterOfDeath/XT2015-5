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
        private char[] buffer;
        
        public MyString(string buffer)
        {
            this.buffer = new char[buffer.Length];
            buffer.ToCharArray().CopyTo(this.buffer, 0);
        }
        
        public MyString(char[] buffer)
        {
            this.buffer = new char[buffer.Length];
            buffer.CopyTo(this.buffer, 0);
        }
        
        public int Size
        {
            get { return this.buffer.Length; }
        }
        
        public char this[int id]
        {
            get { return this.buffer[id]; }
            set { this.buffer[id] = value; }
        }
        
        public static MyString operator +(MyString object1, MyString object2)
        {
            var tmp = new char[object1.Size + object2.Size];
            object1.buffer.CopyTo(tmp, 0);
            object2.buffer.CopyTo(tmp, object1.Size);

            return new MyString(tmp);
        }
        
        public static bool operator <(MyString object1, MyString object2)
        {
            return object1.Size < object2.Size;
        }
        
        public static bool operator >(MyString object1, MyString object2)
        {
            return object1.Size > object2.Size;
        }

        public static implicit operator string(MyString object1)
        {
            return new string(object1.buffer);
        }
        
        public static explicit operator char[](MyString object1)
        {
            return object1.buffer;
        }

        public static explicit operator MyString(char[] object1)
        {
            return new MyString(object1);
        }

        public static explicit operator MyString(string object1)
        {
            return new MyString(object1);
        }
    }
}
