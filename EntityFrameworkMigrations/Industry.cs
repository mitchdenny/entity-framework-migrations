using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkMigrations
{
    public class Industry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual Collection<Customer> Customers { get; set; }
    }
}
