namespace Employees.DAL.Contract
{
    using System;
    
    public interface IUserStoreCreator
    {
        IUserStore CreateInstance();
    }
}
