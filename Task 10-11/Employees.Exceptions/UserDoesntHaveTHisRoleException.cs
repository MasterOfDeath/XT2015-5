namespace Employees.Exceptions
{
    using System;

    public class UserDoesntHaveThisRoleException : Exception
    {
        public UserDoesntHaveThisRoleException(string message)
            : base(message)
        {
        }

        public UserDoesntHaveThisRoleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
