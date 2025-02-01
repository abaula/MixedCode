using EFCoreRepositoryUnitOfWork.Repositories;

namespace EFCoreRepositoryUnitOfWork.Works
{
    public class FalseWork : IFalseWork
    {
        private readonly IEditRepository _falseRepository;

        public FalseWork(IEditRepository falseRepository)
        {
            _falseRepository = falseRepository;
        }

        public void DoWork()
        {
            // Контекст используемый репозиторием не будет зарегистрирован в scope, и метод сохранения изменений не будет вызван.
            // Эта запись не попадёт в базу! Либо попадёт если этот же экземпляр TContext будет правильно использован в другом месте!
            // Никогда так не делаем!
            _falseRepository.AddValue("Эта запись не попадёт в базу!");
        }
    }
}
