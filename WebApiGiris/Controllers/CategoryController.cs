using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGiris.Models;
using WebApiGiris.ViewModels;

namespace WebApiGiris.Controllers
{
    public class CategoryController : ApiController
    {
        // GET: api/Category
        public List<CategoryViewModel> Get()
        {
            return new NorthModel().Categories.Select(x => new CategoryViewModel()
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName,
                Description = x.Description,
                Picture = x.Picture
            }).ToList();
        }

        // GET: api/Category/5
        public CategoryViewModel Get(int? id)
        {
            if (id == null)
                return null;
            var category = new NorthModel().Categories.Find(id.Value);
            if (category == null)
                return null;
            var model = new CategoryViewModel()
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Picture = category.Picture
            };
            return model;
        }

        // POST: api/Category
        public object Post([FromBody]CategoryViewModel value)
        {
            using (NorthModel db = new NorthModel())
            {
                try
                {
                    db.Categories.Add(new Categories()
                    {
                        CategoryName = value.CategoryName,
                        Description = value.Description,
                        Picture = value.Picture
                    });
                    db.SaveChanges();
                    return new
                    {
                        message =$"{value.CategoryName} isimli kategori ekleme işlemi başarılı"
                    };
                }
                catch (Exception ex)
                {
                    return new
                    {
                        message = $"Kategori ekleme işlemi sırasında bir hata oluştu",
                        detail = ex.Message
                    };
                }
            }
        }

        // PUT: api/Category/5
        public object Put(int id, [FromBody]CategoryViewModel value)
        {
            using (NorthModel db = new NorthModel())
            {
                var category = db.Categories.Find(value.CategoryID);
                if (category == null)
                    return new
                    {
                        message = $"Kategori Bulunamadı"
                    };
                try
                {
                    category.CategoryName = value.CategoryName;
                    category.Description = value.Description;
                    category.Picture = value.Picture;
                    db.SaveChanges();
                    return new
                    {
                        message = "Kategori güncelleme işlemi başarılı"
                    };
                }
                catch (Exception ex)
                {
                    return new
                    {
                        message = $"Kategori güncelleme sırasında bir hata oluştu",
                        detail = ex.Message
                    };
                }
            }
        }

        // DELETE: api/Category/5
        public object Delete(int id)
        {
            if (id == null)
                return null;
            
            using (NorthModel db = new NorthModel())
            {
                var category = db.Categories.Find(id);
                if(category==null)
                    return new
                    {
                        message = "Silinecek kategori bulunamadı"
                    };
                try
                {
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    return new
                    {
                        message = "Kategori silme işlemi başarılı"
                    };
                }
                catch (Exception ex)
                {
                    return new
                    {
                        message = "Kategori silme işlemi başarılı",
                        detail= ex.Message
                    };
                }
            }
        }
    }
}
