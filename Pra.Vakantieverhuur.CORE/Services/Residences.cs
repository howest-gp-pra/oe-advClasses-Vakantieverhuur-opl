using System;
using System.Collections.Generic;
using System.Text;
using Pra.Vakantieverhuur.CORE.Entities;

namespace Pra.Vakantieverhuur.CORE.Services
{
    public class Residences
    {
        private List<Residence> allResidences;
        private List<HolidayHome> allHolidayHomes;
        private List<Caravan> allCaravans;

        public List<Residence> AllResidences
        {
            get { return allResidences; }
        }
        public List<HolidayHome> AllHolidayHomes
        {
            get { return allHolidayHomes; }
        }
        public List<Caravan> AllCaravans
        {
            get { return allCaravans; }
        }
        public Residences()
        {
            allResidences = new List<Residence>();

            if (allHolidayHomes == null || allCaravans == null)
            {

                allResidences.Add(new HolidayHome() { StreetAndNumber = "Klaverstraat 1", PostalCode = 8000, Town = "Brugge", ResidenceName = "'t Eeuwig leven", MaxPersons = 2, BasePrice = 70M, DaysForReduction = 7, ReducedPrice = 65M, Deposit = 140M, Microwave = true, TV = true, DishWasher = false, WashingMachine = false, WoodStove = false });
                allResidences.Add(new HolidayHome() { StreetAndNumber = "Steenstraat 123/7", PostalCode = 8000, Town = "Brugge", ResidenceName = "Kiekekot", MaxPersons = 4, BasePrice = 120M, DaysForReduction = 7, ReducedPrice = 110M, Deposit = 240M, Microwave = true, TV = true, DishWasher = true, WashingMachine = true, WoodStove = false });
                allResidences.Add(new HolidayHome() { StreetAndNumber = "Stoofstraat 99", PostalCode = 8000, Town = "Brugge", ResidenceName = "Zwaluwnest", MaxPersons = 2, BasePrice = 85M, DaysForReduction = 7, ReducedPrice = 75M, Deposit = 170M, Microwave = true, TV = true, DishWasher = true, WashingMachine = true, WoodStove = true });
                allResidences.Add(new Caravan() { StreetAndNumber = "Veltemweg 109 - P57", PostalCode = 8310, Town = "Brugge", ResidenceName = "Krot & Co", MaxPersons = 3, BasePrice = 45M, DaysForReduction = 7, ReducedPrice = 40M, Deposit = 90M, Microwave = false, TV = true, PrivateSanitaryBlock = false });

                allHolidayHomes = new List<HolidayHome>();
                allCaravans = new List<Caravan>();

                foreach (Residence residence in allResidences)
                {
                    if (residence is HolidayHome)
                        allHolidayHomes.Add((HolidayHome)residence);
                    if (residence is Caravan)
                        allCaravans.Add((Caravan)residence);
                }

            }
            else
            {
                foreach(Caravan caravan in allCaravans)
                {
                    allResidences.Add(caravan);
                }
                foreach(HolidayHome holidayHome in allHolidayHomes)
                {
                    allResidences.Add(holidayHome);
                }
            }

        }
    }
}
