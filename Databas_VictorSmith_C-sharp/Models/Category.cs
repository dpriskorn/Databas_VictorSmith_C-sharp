using System;
using System.Collections.Generic;
using System.Text;

namespace Databas_VictorSmith_C_sharp.Models
{
    public class Category
    {
        public int Id { get; set; }
        //public int Basecategory_Id { get; set; }
        //public int Unit_Id { get; set; }
        public string Category_Name { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", Category_Name);
        }
    }
}