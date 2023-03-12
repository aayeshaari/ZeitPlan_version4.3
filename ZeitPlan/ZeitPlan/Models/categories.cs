using SQLite;
using System;

namespace ZeitPlan.Models
{
    public class categories
    {
        [PrimaryKey, AutoIncrement]
        public int CatId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }

        //public static implicit operator categories(categories v)
       //{
           //throw new NotImplementedException();
        //}
    }
}
