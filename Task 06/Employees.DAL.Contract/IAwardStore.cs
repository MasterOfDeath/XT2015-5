﻿namespace Employees.DAL.Contract
{
    using System.Collections.Generic;
    using Entites;

    public interface IAwardStore
    {
        ICollection<Award> Awards { get; }

        int AddAward(Award award);

        ICollection<Award> ListAllAwards();

        Award GetAwardByTitle(string titleStr);

        Award GetAwardById(int id);
    }
}