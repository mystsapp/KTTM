using Data.Interfaces;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface ITT621Repository : IRepository<TT621>
    {

    }
    public class TT621Repository : Repository_KTTM<TT621>, ITT621Repository
    {
        public TT621Repository(KTTMDbContext context) : base(context)
        {
        }
    }
}
