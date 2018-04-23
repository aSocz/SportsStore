using SportsStore.Infrastructure.Repositories;

namespace SportsStore.UnitTests.Helpers
{
    public class FakeUnitOfWork : UnitOfWork
    {
        public FakeUnitOfWork() : base(TestDataProvider.GetDbContextMock().Object)
        {
        }
    }
}