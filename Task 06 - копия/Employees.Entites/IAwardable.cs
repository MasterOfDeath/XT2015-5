namespace Employees.Entites
{
    using System.Collections.Generic;
    
    public interface IAwardable
    {
        IEnumerable<Award> Awards { get; }
        
        void AddAward(Award award);
    }
}
