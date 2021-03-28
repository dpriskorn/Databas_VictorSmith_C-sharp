using System;
using System.Collections.Generic;
using System.Text;

namespace Databas_VictorSmith_C_sharp.Models
{
    public class Area
    {
		public int Id { get; set; }
        public string Area_Name { get; set; }
        //public int Country_Id { get; set; }
        public string Country_Name { get; set; }
        public override string ToString()
        {
            return string.Format("{0}: {1}", Country_Name, Area_Name);
        }
    }
}
