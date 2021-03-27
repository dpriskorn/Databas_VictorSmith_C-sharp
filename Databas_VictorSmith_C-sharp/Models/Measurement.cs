using System;
using System.Collections.Generic;
using System.Text;

namespace Databas_VictorSmith_C_sharp.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public int Category_Id { get; set; }
        public int Observation_Id { get; set; }
        public string Value { get; set; }
    }
}