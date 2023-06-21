﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedForSpeed
{
    public abstract class Car : Vehicle
    {
        private const double DefaultCarFuelConsumption = 3;
        public Car(int horsePower, double fuel) 
            : base(horsePower, fuel)
        {
        }

        public override double FuelConsumption
            => DefaultCarFuelConsumption;
        
    }
}
