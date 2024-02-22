using AutoMapper;
using firsttask.Data;
using Microsoft.Extensions.Options;

namespace firsttask.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly IMapper _mapper;
        public UnitOfWork(MyFirstContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }
        private MyFirstContext _databaseContext;
        public bool IsDisposed { get; protected set; }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }
            if (disposing)
            {

                if (_databaseContext != null)
                {
                    _databaseContext.Dispose();
                    _databaseContext = null;
                }
            }
            IsDisposed = true;
        }
        public async Task SaveAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IResidRepository _residRepository;
        private IHavaleRepository _havaleRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_databaseContext);
                }

                return _productRepository;
            }
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_databaseContext);
                }
                return _categoryRepository;
            }
        }

        public IResidRepository ResidRepository 
        {
            get
            {
                if (_residRepository == null)
                {
                    _residRepository = new ResidRepository(_mapper,_databaseContext);
                }
                return _residRepository;
            }
        }
        public IHavaleRepository HavaleRepository
        {
            get
            {
                if (_havaleRepository == null)
                {
                    _havaleRepository = new HavaleRepository(_mapper, _databaseContext);
                }
                return _havaleRepository;
            }
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
