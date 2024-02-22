using AutoMapper;
using firsttask.Data;
using firsttask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace firsttask.Repository
{
    public class ResidRepository : IResidRepository
    {
        public ResidRepository(IMapper mapper, MyFirstContext myFirstContext)
        {
            _mapper = mapper;
            _myFirstContext = myFirstContext;
        }
        private readonly IMapper _mapper;
        private readonly MyFirstContext _myFirstContext;
        public async Task<int> AddResidAsync(ResidViewModel residViewModel)
        {
            var tosave = _mapper.Map<Resid>(residViewModel);
            var myitem = await _myFirstContext.Products.SingleOrDefaultAsync(x => x.Id == residViewModel.ProductId);
            tosave.PriceOfAll = tosave.Quantity * myitem.Price;
            tosave.PriceOfOne = myitem.Price;
            tosave.ResidTime = DateTime.Now;
            await _myFirstContext.Resids.AddAsync(tosave);
            return tosave.Id;
        }
    }
}
