using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ComposerRepository
{
    public class ComposerRepository : GenericRepository<Composer>
    {
        private readonly DbEntitiesContext context;

        public ComposerRepository()
            :this(new DbEntitiesContext())
        {
        }

        public ComposerRepository(DbEntitiesContext context)
            : base(context)
        {
            this.context = context;
        }

        public Composer GetComposerByName(string name)
        {
            return context.Composers.
                Where(d => name.Contains(d.FirstName) || name.Contains(d.LastName))
                .FirstOrDefault();
        }
    }
}
