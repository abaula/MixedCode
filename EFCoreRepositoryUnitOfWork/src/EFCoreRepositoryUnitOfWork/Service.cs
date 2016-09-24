using EFCoreUnitOfWork.Interfaces;
using System;
using System.Collections.Generic;

namespace EFCoreRepositoryUnitOfWork
{
    public class Service
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public Service(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void CheckDbContextInstancesAndThrow()
        {
            int editRepositoryCtxHashCode1;
            int readOnlyRepositoryCtxHashCode1;
            int editRepositoryCtxHashCode2;

            using (var scope = _unitOfWorkFactory.Create())
            {
                // Оба репозитория получат один и тот же экземпляр контекста.
                var editRepository = scope.GetRepository<IEditRepository>();
                var readOnlyRepository = scope.GetRepository<IReadOnlyRepository>();

                editRepositoryCtxHashCode1 = editRepository.DbContextForTestPurpose.GetHashCode();
                readOnlyRepositoryCtxHashCode1 = readOnlyRepository.DbContextForTestPurpose.GetHashCode();

                if (editRepositoryCtxHashCode1 != readOnlyRepositoryCtxHashCode1)
                    throw new InvalidOperationException("Репозитарии внутри одного UnitOfWork получили разные экземпляры контекста.");
            }

            using (var scope = _unitOfWorkFactory.Create())
            {
                // Репозиторий в новом UnitOfWork получит новый экземпляр контекста.
                var editRepository = scope.GetRepository<IEditRepository>();
                editRepositoryCtxHashCode2 = editRepository.DbContextForTestPurpose.GetHashCode();

                if (editRepositoryCtxHashCode2 == editRepositoryCtxHashCode1)
                    throw new InvalidOperationException("Репозитарии в разных UnitOfWork получили одинаковые экземпляры контекста.");
            }
        }

        public void DoSomeCommitedWork()
        {
            using (var scope = _unitOfWorkFactory.Create())
            {
                var editRepository = scope.GetRepository<IEditRepository>();
                editRepository.AddValue("Сохраняем в транзакции 1");
                editRepository.AddValue("Сохраняем в транзакции 2");
                scope.Commit();
            }
        }

        public void DoSomeNotCommitedWork()
        {
            using (var scope = _unitOfWorkFactory.Create())
            {
                var repository = scope.GetRepository<IEditRepository>();
                repository.AddValue("Не сохраниться 1");
                repository.AddValue("Не сохраниться 2");
            }
        }

        public void DoSomeInterruptedWork()
        {
            using (var scope = _unitOfWorkFactory.Create())
            {
                try
                {
                    var repository = scope.GetRepository<IEditRepository>();
                    repository.AddValue("Не сохраниться 3");
                    repository.AddValue("Не сохраниться 4");
                    throw new Exception();
                    scope.Commit();
                }
                catch
                {
                }
            }
        }

        public IEnumerable<string> GetAllValues()
        {
            using (var scope = _unitOfWorkFactory.Create())
            {
                var repository = scope.GetRepository<IReadOnlyRepository>();
                return repository.GetAllValues();
            }
        }

        public void DeleteAll()
        {
            using (var scope = _unitOfWorkFactory.Create())
            {
                var repository = scope.GetRepository<IEditRepository>();
                // Выполняется через ExecuteSqlCommand. scope.Commit() не требуется.
                // Это недостаток текущей реализации, завязанной на DbContext.SaveChanges().
                repository.DeleteAll();
            }
        }
    }
}
