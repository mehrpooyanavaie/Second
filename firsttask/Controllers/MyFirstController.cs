using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using firsttask.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions;
using firsttask.Data;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using AutoMapper;
using firsttask.Repository;
using Microsoft.AspNetCore.Authorization;
using firsttask.Controllers;
using System.Security.Claims;
using System;

namespace firsttask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyFirstController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public MyFirstController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("itemswithpagination")]
        public async Task<ActionResult<List<ProductViewModel>>> GetAllProductsAsync(int page = 1, int pagesize = 10)
        {
            IEnumerable<Product> products = await _unitOfWork.ProductRepository.GetAllAsync();
            var myreturndata = _mapper.Map<List<ProductViewModel>>(products);
            var totalcount = myreturndata.Count();
            var totalpages = (int)Math.Ceiling((decimal)totalcount / pagesize);
            var productsperpage = myreturndata.Skip((page - 1) * pagesize).Take(pagesize).ToList();
            return Ok(productsperpage);
        }
        [HttpGet("categorieswithpagination")]
        public async Task<ActionResult<List<CategoryViewModel>>> GetAllCategoriesAsync(int page = 1, int pagesize = 10)
        {
            IEnumerable<Category> categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var myreturndata = _mapper.Map<List<CategoryViewModel>>(categories);
            var totalcount = myreturndata.Count();
            var totalpages = (int)Math.Ceiling((decimal)totalcount / pagesize);
            var categoriesperpage = myreturndata.Skip((page - 1) * pagesize).Take(pagesize).ToList();
            return Ok(categoriesperpage);
        }
        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductViewModel>> GetByIdProductAsync(int id)
        {
            var myproduct = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            var myreturndata = _mapper.Map<ProductViewModel>(myproduct);
            if (myreturndata == null)
                return NotFound();
            return Ok(myreturndata);
        }
        [HttpGet("category/{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetByIdCategoryAsync(int id)
        {
            var mycategory = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            var myreturndata = _mapper.Map<CategoryViewModel>(mycategory);
            if (myreturndata == null)
                return NotFound();
            return Ok(myreturndata);
        }
        [HttpPost("addproduct")]
        //[Authorize(Roles = "Maneger")]
        public async Task<ActionResult<int>> AddProductAsync(PostProductModel product)
        {
            if (!ModelState.IsValid)
                return BadRequest(-1);//return -1 if an error accures
            var tosave = _mapper.Map<Product>(product);
            var tosaveid = await _unitOfWork.ProductRepository.AddAsync(tosave);
            if (tosaveid == -1)
                return BadRequest(-1);
            await _unitOfWork.SaveAsync();
            return Ok(tosaveid);
        }
        [HttpPost("addcategory")]
        //[Authorize(Roles = "Maneger")]
        public async Task<ActionResult<int>> AddCategoryAsync(PostCategoryModel category)
        {
            if (!ModelState.IsValid)
                return BadRequest(-1);//return -1 if an error accures
            var tosave = _mapper.Map<Category>(category);
            if (tosave == null)
                return -1;//return -1 if an error accures
            var tosaveid = await _unitOfWork.CategoryRepository.AddAsync(tosave);
            await _unitOfWork.SaveAsync();
            return Ok(tosaveid);
        }
        [HttpDelete("deleteproduct/{id}")]
        //[Authorize(Roles = "Maneger")]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            bool existornot = await _unitOfWork.ProductRepository.DeleteByIdAsync(id);
            if (!existornot)
                return NotFound();
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpDelete("deletecategory/{id}")]
        //[Authorize(Roles = "Maneger")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            var mycategory = await _unitOfWork.CategoryRepository.DeleteByIdAsync(id);
            if (!mycategory)
                return NotFound();
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpPut("editproduct/{id}")]
        //[Authorize(Roles = "Maneger")]
        public async Task<ActionResult<int>> EditProductAsync(int id, PostProductModel product)
        {
            if (ModelState.IsValid)
            {
                var tosave = _mapper.Map<Product>(product);
                tosave.Id = id;
                await _unitOfWork.ProductRepository.UpdateAsync(tosave);
                await _unitOfWork.SaveAsync();
                return Ok(id);
            }
            return BadRequest(-1);//return -1 if an error accures
        }
        [HttpPut("editcategory/{id}")]
        //[Authorize(Roles = "Maneger")]
        public async Task<ActionResult<int>> EditCategoryAsync(int id, PostCategoryModel category)
        {
            if (ModelState.IsValid)
            {
                var tosave = _mapper.Map<Category>(category);
                tosave.Id = id;
                await _unitOfWork.CategoryRepository.UpdateAsync(tosave);
                await _unitOfWork.SaveAsync();
                return Ok(id);
            }
            return BadRequest(-1);//return -1 if an error accures
        }
        [HttpPost("addresid")]
        //[Authorize(Roles = "Maneger")]
        public async Task<ActionResult<int>> AddResidAsync(ResidViewModel residViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(-1);//return -1 if an error accures
            int residId = await _unitOfWork.ResidRepository.AddResidAsync(residViewModel);
            await _unitOfWork.SaveAsync();
            return Ok(residId);
        }
        [HttpPost("addhavale")]
        public async Task<ActionResult<int>> AddHavaleAsync(HavaleViewModel havaleViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(-1);//return -1 if an error accures
            int havaleId = await _unitOfWork.HavaleRepository.AddHavaleAsync(havaleViewModel);
            if (havaleId == -1)
                return BadRequest(havaleId);
            await _unitOfWork.SaveAsync();
            return Ok(havaleId);
        }
        [HttpGet("get-all-products-reports")]
        //[Authorize(Roles = "Maneger")]
        public async Task<ActionResult<List<ProductReport>>> GetAllProductsReportsAsync(int page = 1, int pagesize = 10)
        {
            var productsReport = await _unitOfWork.ProductRepository.ReportAllAsync();
            var totalcount = productsReport.Count();
            var totalpages = (int)Math.Ceiling((decimal)totalcount / pagesize);
            var productsperpage = productsReport.Skip((page - 1) * pagesize).Take(pagesize).ToList();
            return Ok(productsperpage);
        }
        [HttpGet("get-one-product-report/{id}")]
        //[Authorize(Roles = "Maneger")]
        public async Task<ActionResult<ProductReport>> GetOneProductReportById(int id)
        {
            var productReport = await _unitOfWork.ProductRepository.ReportOneAsync(id);
            return Ok(productReport);
        }
    }
}
