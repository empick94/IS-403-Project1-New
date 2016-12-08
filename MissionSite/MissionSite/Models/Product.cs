using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MissionSite.Models
{
    public class Product
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public Product() { }

        public Product(string id, string name, decimal price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }
    }
}