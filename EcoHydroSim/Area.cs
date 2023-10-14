using EcoHydroSim;
using System;
using System.Collections.Generic;
using System.IO;

namespace EcoHydroSim
{

     public abstract class Area
    {
        public string Owner { get; set; }
        public string Type { get; set; }
        public double WaterStored { get; set; }

        public Area(string owner, double waterStored)
        {
            Owner = owner;
            WaterStored = waterStored;
        }
        public abstract void UpdateWeather(ref IWeather weather);

        public override string ToString()
        {
            return $"{Owner} {Type} {WaterStored}";
        }
    }

    public class Plain : Area
    {
        public Plain(string owner, double waterStored) : base(owner, waterStored)
        {
            Type = "P";
        }

        public override void UpdateWeather(ref IWeather weather)
        {
           weather=weather.CalculateWeatherForPlain(this);
        }
    }

    public class Grassland : Area
    {
        public Grassland(string owner, double waterStored) : base(owner, waterStored)
        {
            Type = "G";
        }

        public override void UpdateWeather(ref IWeather weather)
        {
           weather =  weather.CalculateWeatherForGrassland(this);
        }
    }

    public class LakesRegion : Area
    {
        public LakesRegion(string owner, double waterStored) : base(owner, waterStored)
        {
            Type= "L";
        }

        public override void UpdateWeather(ref IWeather weather)
        {
           weather = weather.CalculateWeatherForLakesRegion(this);
        }
    }
}