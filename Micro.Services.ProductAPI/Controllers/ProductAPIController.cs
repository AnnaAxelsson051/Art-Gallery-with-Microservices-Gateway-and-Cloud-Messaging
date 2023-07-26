using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Micro.Services.ProductAPI.Data;
using Micro.Services.ProductAPI.Models;
using Micro.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPIController : Controller
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public ProductAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        //Get list of products
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _db.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //Get product by id
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product obj = _db.Products.First(u => u.ProductId == id);
                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        //Create a product
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productDto);
                _db.Products.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //Update product
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] ProductDto productDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productDto);
                _db.Products.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //Delete a product
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product obj = _db.Products.First(u => u.ProductId == id);
                _db.Products.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}

