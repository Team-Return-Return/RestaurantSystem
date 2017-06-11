﻿namespace RestaurantSystem.DataImporter.JsonImporter.Importers
{
    using RestaurantSystem.Data.Abstraction;
    using RestaurantSystem.DataImporter.JsonImporter.Abstraction;
    using RestaurantSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AddressImporter : IExecutable
    {
        private int saveChangesCount = 50;

        public int Order => 2;

        public Action<IRestaurantSystemData, IList<SupplyDocument>> Execute
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

                        if(city != null)
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

                        SaveChanges(i, db);
                    }

                    db.SaveChanges();
                };
            }
        }

        private void SaveChanges(int count, IRestaurantSystemData db)
        {
            if (count % saveChangesCount == 0)
            {
                db.SaveChanges();
            }
        }

        //private bool CheckIfCityExists(City city, IRestaurantSystemData db)
        //{
        //    var result = true;

        //    var dbCity = db.Cities
        //        .All()
        //        .Where(x => x.Name == city.Name)
        //        .FirstOrDefault();

        //    if (dbCity == null)
        //    {
        //        result = false;
        //    }

        //    return result;
        //}

        private List<Address> ExtractAddresses(IList<SupplyDocument> documents)
        {
            var result = new List<Address>();

            foreach (var doc in documents)
            {
                result.Add(doc.Supplier.Address);
            }

            return result;
        }
    }
}
