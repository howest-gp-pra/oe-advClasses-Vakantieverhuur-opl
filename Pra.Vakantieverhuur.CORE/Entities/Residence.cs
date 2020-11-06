using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Vakantieverhuur.CORE.Entities
{
    public class Residence
    {
		private string id;
		private int maxPersons;
		private int postalCode;
		private decimal basePrice;
		private decimal reducedPrice;
		private byte daysForReduction;
		private decimal deposit;

		public string ID
		{
			get { return id; }
		}
		public decimal BasePrice
		{
			get { return basePrice; }
			set
			{
				if (value < 0) value = 0;
				basePrice = value;
			}
		}
		public decimal ReducedPrice
		{
			get { return reducedPrice; }
			set
			{
				if (value < 0) value = 0;
				reducedPrice = value;
			}
		}
		public byte DaysForReduction
		{
			get { return daysForReduction; }
			set
			{
				if (value > 100) value = 100;
				daysForReduction = value;
			}
		}
		public decimal Deposit
		{
			get { return deposit; }
			set
			{
				if (value < 0) value = 0;
				deposit = value;
			}
		}
		public int MaxPersons
		{
			get { return maxPersons; }
			set
			{
				if (value < 1) value = 1;
				if (value > 20) value = 20;
				maxPersons = value;
			}
		}
		public string StreetAndNumber { get; set; }
		public string ResidenceName { get; set; }
		public string Town { get; set; }
		public int PostalCode
		{
			get { return postalCode; }
			set
			{
				if (value < 1000) value = 1000;
				if (value > 9999) value = 9999;
				postalCode = value;
			}
		}
		public bool? Microwave { get; set; }
		public bool? TV { get; set; }
		public bool IsRentable { get; set; } = true;
		public Residence()
		{
			id = Guid.NewGuid().ToString();
		}
	}
}
