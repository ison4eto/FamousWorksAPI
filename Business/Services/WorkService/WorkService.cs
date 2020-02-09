using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.WorkRepository;
using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.WorkService
{
    public class WorkService : GenericService<Work, IGenericRepository<Work>>
    {
        private readonly WorkRepository workRepository;

        public WorkService()
            : base()
        {
            workRepository = new WorkRepository();
        }

        public WorkService(DbEntitiesContext context)
        {
            workRepository = new WorkRepository(context);
        }


        public Work GetWorkByTitle(string title)
        {
            return workRepository.GetWorkByTitle(title);
        }

        public List<Work> GetAllWorksForComposer(int id)
        {
            return workRepository.GetAllWorksForComposer(id);
        }
    }
}
