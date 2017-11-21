using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.Model
{
    class Category
    {
        private string name;

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public Category(string name) {
            Name = name; 
        }
    }
}
