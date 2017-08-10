using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetim.Model.Entities;

namespace Yonetim.DAL
{
  public class MyContext: DbContext
    {
        public MyContext()
            :base("name=HaberCon")
        {
           // this.Database.CreateIfNotExists();
        }
        public virtual DbSet<Haber> Haberler { get; set; }
        public virtual DbSet<Kategori> Kategoriler { get; set; }
    }
}
