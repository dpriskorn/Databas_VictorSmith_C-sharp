using System;
using System.Collections.Generic;
using System.Text;

namespace Databas_VictorSmith_C_sharp.Models
{
    public class Observer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public override string ToString()
        //{
        //    return string.Format("{1}, {0}", FirstName, LastName);
        //}
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
        public int Area_id { get; set; }
    }

    public class area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Country_Id { get; set; }

    }
    public class country
    {
        public int Id { get; set; }
        public string Country { get; set; }

    }

    
}
