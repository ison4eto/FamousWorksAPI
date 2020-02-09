using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ComposerRepository;
using DatabaseStructure.Entities;

namespace Business.Services.ComposerService
{
    public class ComposerService : GenericService<Composer, IGenericRepository<Composer>>
    {
        private readonly ComposerRepository composerRepository;

        public ComposerService()
            : base(new DbEntitiesContext())
        {
            composerRepository = new ComposerRepository();
        }

        public ComposerService(DbEntitiesContext context)
        {
            composerRepository = new ComposerRepository(context);
        }

        public Composer GetComposerByName(string name)
        {
            return composerRepository.GetComposerByName(name);
        }
    }
}
