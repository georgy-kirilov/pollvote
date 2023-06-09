﻿namespace MyWebApp.Data;

public sealed class Product
{
    public int Id { get; private set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required decimal Price { get; set; }
}
