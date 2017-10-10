using Buildit.Data.Contracts;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BuilditDbContext context;

        public UnitOfWork(BuilditDbContext context)
        {
            Guard.WhenArgument(context, "DbContext").IsNull().Throw();

            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}