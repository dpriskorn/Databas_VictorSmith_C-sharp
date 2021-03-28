using System;
using System.Collections.Generic;
using System.Text;

namespace Databas_VictorSmith_C_sharp.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Abbreviation { get; set; }
        public override string ToString()
        {
            return string.Format("{0}: {1}", Type, Abbreviation);
        }
    }
}