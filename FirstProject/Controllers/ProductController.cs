using FirstProject.DataAccess;
using FirstProject.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Controllers
{
    [Route("api/products")]
 
    public class ProductController : Controller
    {
        IProductDal _productDal;

        public ProductController(IProductDal productDal)
        {
            _productDal = productDal;

        }
        [HttpGet("")]//Tüm ürünleri çeker
        public IActionResult Get()
        {
            var products = _productDal.GetList();
            return Ok(products);
        }

        [HttpGet("{productId}")]//ürünleri ID göre çeker
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(p => p.ProductId == productId);
                if (product == null)
                {
                    return NotFound($"There İs No Product With Id={productId}");
                }
                return Ok(product);
            }
            catch { }
            return BadRequest();
        }
        public IActionResult Post(Product product)//Insert etmek için post edilir.
        {
            try
            {
                _productDal.Add(product);
                return new StatusCodeResult(201);
            }
            catch
            {

            }
            return BadRequest();
        }

        [HttpPut]//PUT İşlemi yapılacak ise [HttpPut] yazılması gerek.
        public IActionResult Put(Product product)
        {
            try
            {
                _productDal.Update(product);
                return Ok(product);
            }
            catch
            {

            }
            return BadRequest();
        }
        [HttpDelete("{productId}")]//silme işlemini Id yoluna göre siliyoruz.
        public IActionResult Delete(int productId)
        {
            try
            {
                _productDal.Delete(new Product { ProductId = productId });
                return Ok();
            }
            catch
            {

            }
            return BadRequest();
        }
        [HttpGet("GetProductDetails")] //ürünlerin detaylarını ceker
        public IActionResult GetProductsWithDetails()
        {
            try
            {
                var result = _productDal.GetProductWithDetails();
                return Ok(result);
            }
            catch
            {

            }
            return BadRequest();
        }
        [HttpGet("{productId}/GetProductDetails")]//ID ye göre detay çekme
        public IActionResult GetProductsWithDetails(int ProductId)
        {
            try
            {
                var result = _productDal.GetProductDetails(ProductId);
                return Ok(result);
            }
            catch
            {

            }
            return BadRequest();
        }


    }

}

