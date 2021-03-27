using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Databas_VictorSmith_C_sharp.Models
{
    public class Observer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return string.Format("{1}, {0}", FirstName, LastName);
        }
    }

    public class Observation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Observer_Id { get; set; }
        public int Geolocation_Id { get; set; }
    }

    public class Geolocation
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Area_Id { get; set; }
    }

    public class Area
    {
        public int Id { get; set; }
        public string Area_Name { get; set; }
        public int Country_Id { get; set; }
    }
    public class Country
    {
        public int Id { get; set; }
        public string Country_Name { get; set; }
    }
    public class Measurement
    {
        public int Id { get; set; }
        public int Category_Id { get; set; }
        public int Observation_Id { get; set; }
        public string Value { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public int Basecategory_Id { get; set; }
        public int Unit_Id { get; set; }
        public string Category_Name { get; set; }
    }
    public class Unit
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Abbreviation { get; set; }
    }
}
