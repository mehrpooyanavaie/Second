namespace firsttask.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public bool IsDisposed { get; }
        public Task SaveAsync();
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IResidRepository ResidRepository { get; }
        IHavaleRepository HavaleRepository { get; }
    }
}
