﻿using System.Collections.Generic;

namespace RLS.Domain.Shared.Models
{
    public class CollectionResult<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Collection { get; set; }

        public int TotalCount { get; set; }
    }
}