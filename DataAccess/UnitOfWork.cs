using DataAccess.Repositories.ComposerRepository;
using DataAccess.Repositories.WorkRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private readonly DbEntitiesContext context;

        private WorkRepository workRepository;
        private ComposerRepository composerRepository;

        private bool disposed = false;

        public UnitOfWork()
            :this(new DbEntitiesContext())
        {
        }

        public UnitOfWork(DbEntitiesContext context)
        {
            this.context = context;
        }

        public WorkRepository WorkRepository
        {
            get
            {
                if(workRepository == null)
                {
                    workRepository = new WorkRepository(context);
                }
                return workRepository;
            }
        }

        public ComposerRepository ComposerRepository
        {
            get
            {
                if(composerRepository == null)
                {
                    composerRepository = new ComposerRepository(context);
                }
                return composerRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
