using System;
using System.Collections.Generic;
using System.Text;

namespace Databas_VictorSmith_C_sharp.Models
{
    public class Geolocation
    {
		public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        //public int Area_Id { get; set; }
        public string Area_Name { get; set; }
        public override string ToString()
        {
            return string.Format("{0}: {1},{2}", Area_Name, Latitude, Longitude);
        }
    }
}
