using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EraRepository
{
    public class EraRepository : GenericRepository<Era>
    {
        private readonly DbEntitiesContext context;

        public EraRepository()
            : this(new DbEntitiesContext())
        {
        }

        public EraRepository(DbEntitiesContext context)
            : base(context)
        {
            this.context = context;
        }

        public Era GetEraByName(string name)
        {
            return context.Eras
                 .Where(g => g.Name.ToLower().Contains(name))
                 .FirstOrDefault();
        }


    }
}
