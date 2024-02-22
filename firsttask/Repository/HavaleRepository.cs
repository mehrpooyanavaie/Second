using AutoMapper;
using firsttask.Data;
using firsttask.Models;
using Microsoft.EntityFrameworkCore;

namespace firsttask.Repository
{
    public class HavaleRepository : IHavaleRepository
    {
        public HavaleRepository(IMapper mapper, MyFirstContext myFirstContext)
        {
            _mapper = mapper;
            _myFirstContext = myFirstContext;
        }
        private readonly IMapper _mapper;
        private readonly MyFirstContext _myFirstContext;
        public async Task<int> AddHavaleAsync(HavaleViewModel havaleViewModel)
        {
            var q = await _myFirstContext.Resids.Where(x => x.ProductId == havaleViewModel.ProductId).ToListAsync();
            var c = await _myFirstContext.Havales.Where(x => x.ProductId == havaleViewModel.ProductId).ToListAsync();
            if(q.Count == 0)
                return -1;
            int qsum = 0;
            int csum = havaleViewModel.Quantity;
            foreach (var x in q)
            {
                qsum += x.Quantity;
            }
            foreach (var x in c)
            {
                csum += x.Quantity;
            }
            if (csum > qsum)
                return -1;
            var itemPrice = q[0].PriceOfOne;
            var tosave = _mapper.Map<Havale>(havaleViewModel);
            tosave.PriceOfOne = itemPrice;
            tosave.PriceOfAll = tosave.Quantity * itemPrice;
            tosave.HavaleTime = DateTime.Now;
            await _myFirstContext.Havales.AddAsync(tosave);
            return tosave.Id;
        }
    }
}
