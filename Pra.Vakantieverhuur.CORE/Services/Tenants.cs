using System;
using System.Collections.Generic;
using System.Text;
using Pra.Vakantieverhuur.CORE.Entities;

namespace Pra.Vakantieverhuur.CORE.Services
{
    public class Tenants
    {
        private List<Tenant> allTenants;

        public List<Tenant> AllTenants
        {
            get { return allTenants; }
        }
        public Tenants()
        {
                allTenants = new List<Tenant>();

                allTenants.Add(new Tenant() { Name = "Jovi", Firstname = "Bon", Address = "Buckinhamstreet 1", Town = "LND001 London", Country = "UK", Phone = "", Email = "bon.jovi@doedelzakplayers.co.uk" });
                allTenants.Add(new Tenant() { Name = "Jolie", Firstname = "Angelina", Address = "Holywoodroad 1", Town = "SF123 San Fransisco", Country = "USA", Phone = "", Email = "angie.jolie@botoxpromotors.com" });
                allTenants.Add(new Tenant() { Name = "Cas", Firstname = "Goossens", Address = "Billiardplein 123", Town = "1000 Brussel", Country = "België", Phone = "0497695847", Email = "cas.goossens@exbrt.be" });

        }
    }
}
