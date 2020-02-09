using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.EraRepository;
using DatabaseStructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.EraService
{
    public class EraService : GenericService<Era, IGenericRepository<Era>>
    {
        private readonly EraRepository eraRepository;
        public EraService()
            :base()
        {
            eraRepository = new EraRepository();
        }

        public EraService(DbEntitiesContext context)
        {
            eraRepository = new EraRepository(context);
        }

        public Era GetEraByName(string name)
        {
            return eraRepository.GetEraByName(name);
        }
    }
}
