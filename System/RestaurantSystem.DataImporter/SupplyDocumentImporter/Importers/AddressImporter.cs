﻿namespace RestaurantSystem.DataImporter.SupplyDocumentImporter.Importers
{
    using RestaurantSystem.Data.Abstraction;
    using RestaurantSystem.DataImporter.SupplyDocumentImporter.Abstraction;
    using RestaurantSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AddressImporter : BaseImporter, IImporter
    {
        public int Order => 2;

        public Action<IRestaurantSystemData, IList<SupplyDocument>> Import
        {
            get
            {
                return (db, documents) =>
                {
                    var addresses = ExtractAddresses(documents);

                    for (int i = 0; i < addresses.Count; i++)
                    {
                        var name = addresses[i].City.Name;

                        var city = db.Cities
                            .All()
                            .Where(x => x.Name == name)
                            .FirstOrDefault();

                        if (city != null)
                        {
                            city.Addresses.Add(new Address
                            {
                                ContactName = addresses[i].ContactName,
                                PhoneNumber = addresses[i].PhoneNumber,
                                PostCode = addresses[i].PostCode,
                                Street = addresses[i].Street
                            });

                            db.Cities.Update(city);
                        }

                        this.SaveChanges(i, db);
                    }

                    db.SaveChanges();
                };
            }
        }

        private List<Address> ExtractAddresses(IList<SupplyDocument> documents)
        {
            var result = new List<Address>();

            foreach (var doc in documents)
            {
                result.Add(doc.Supplier.Address);
            }
            result.Add(documents[0].RestaurantBranch.Address);

            return result;
        }
    }
}
