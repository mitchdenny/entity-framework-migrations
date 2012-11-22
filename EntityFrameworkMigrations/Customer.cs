﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkMigrations
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public CustomerStatus Status { get; set; }
        public virtual Industry Industry { get; set; }
    }
}
