using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Vakantieverhuur.CORE.Entities
{
    public class Rental
    {
        public Residence HollidayResidence { get; set; }
        public Tenant HollidayTenant { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsDeposidPaid { get; set; }
        public decimal Paid { get; set; }
        public decimal ToPay { get; set; }

    }
    public class RentalCaravan : Rental
    {
        public Caravan Caravan { get; set; }

    }
    public class RentalHollidayHome : Rental
    {
        public HolidayHome HolidayHome { get; set; }
    }
}
