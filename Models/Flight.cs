using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFinal.Models
{
    public class Flights
    {
        public int ID { get; set; }
        public String Plane_model { get; set; }
        public String Cities { get; set; }
        public int peoples { get; set; }
        public Type F_Class { get; set; }
        public float Price { get; set; }
        public String F_Image { get; set; }
        public String Schedule { get; set; }
        public bool F_Available { get; set; }


    }

    public enum Type
    {
        Business,
        Economy
    }

}
