namespace Employees.BLL.Contract
{
    using System;
    
    public interface IUserLogicCreator
    {
        IUserLogic CreateInstance();
    }
}
