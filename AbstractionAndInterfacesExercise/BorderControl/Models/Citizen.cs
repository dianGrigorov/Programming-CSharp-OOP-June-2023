﻿
using BorderControl.Models.Interfaces;

namespace BorderControl.Models;

public class Citizen : IIdentifiable
{

    public Citizen( string name, int age, string id)
    {
        Name = name;
        Age = age;
        Id = id;
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }
}
