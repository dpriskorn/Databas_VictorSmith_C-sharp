using System;
using System.Collections.Generic;
using System.Text;

namespace Databas_VictorSmith_C_sharp.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
        public string Unit_Abbreviation { get; set; }
        public int Observation_Id { get; set; }
        public double Value { get; set; }
        public override string ToString()
        {
            return string.Format("{0}: {1} {2}", Category_Name, Math.Round(Value, 2).ToString(), Unit_Abbreviation);
        }
    }
}