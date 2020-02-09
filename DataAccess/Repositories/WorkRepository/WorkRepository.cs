using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.WorkRepository
{
    public class WorkRepository : GenericRepository<Work>
    {
        private readonly DbEntitiesContext context;

        public WorkRepository()
            : this(new DbEntitiesContext())
        {
        }

        public WorkRepository(DbEntitiesContext context)
            : base(context)
        {
            this.context = context;
        }

        public Work GetWorkByTitle(string title)
        {
            return context.Works
                .Where(m => m.Title.Contains(title))
                .FirstOrDefault();
        }

        public List<Work> GetAllWorksForComposer(int id)
        {
            return context.Works
                .Where(c => c.ComposerID.Equals(id))
                .OrderBy(c => c.Title)
                .ToList();
        }
    }
}
