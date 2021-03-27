using System;
using System.Collections.Generic;
using System.Text;

namespace Databas_VictorSmith_C_sharp.Models
{
    public class Observation
    {
		public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Observer_Id { get; set; }
        public int Geolocation_Id { get; set; }
        public override string ToString()
        {
            return string.Format("{0}: {1}", Id, Date.ToShortDateString());
        }
    }
}
