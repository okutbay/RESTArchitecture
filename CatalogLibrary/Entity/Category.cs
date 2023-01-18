﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLibrary.Entity;

public class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }
}
