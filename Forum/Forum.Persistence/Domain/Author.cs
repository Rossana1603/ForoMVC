﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Persistence.Domain
{
    public class Author : Entidad
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
