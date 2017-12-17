using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Model
{
    public class Wish
    {
        private string title;
        private string description;
        private double price; 
        //picture  
        private User buyer; 
        private Category category;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public User User
        {
            get { return buyer; }
            set { buyer = value; }
        }

        public Wish(Category category)
        {
            this.category = category; 
        }

        public Wish(Category category, string title, string description, double price)
        {
            this.category = category;
            this.title = title;
            this.description = description;
            this.price = price; 
        }
        public string getCategoryName() {
            return this.category.Name; 
        }
    }
}
