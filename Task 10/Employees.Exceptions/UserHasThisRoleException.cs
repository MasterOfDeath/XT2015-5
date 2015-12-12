namespace Employees.Exceptions
{
    using System;

    public class UserHasThisRoleException : Exception
    {
        public UserHasThisRoleException(string message)
            : base(message)
        {
        }

        public UserHasThisRoleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
