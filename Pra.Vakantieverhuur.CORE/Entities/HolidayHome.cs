using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Vakantieverhuur.CORE.Entities
{
    public class HolidayHome : Residence
    {
        public bool? DishWasher { get; set; }
        public bool? WashingMachine { get; set; }
        public bool? WoodStove { get; set; }

        public override string ToString()
        {
            return $"H - {ResidenceName} - {Town}";
        }
    }
}
