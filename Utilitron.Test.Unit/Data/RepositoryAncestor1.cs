﻿using Utilitron.Data;

namespace Utilitron.Test.Unit.Data
{
    public class RepositoryAncestor1 : Repository
    {
        public RepositoryAncestor1(IRepositoryConfiguration configuration) : base(configuration)
        {
        }

        public string QueryTest()
        {
            return GetQuery();
        }
    }
}