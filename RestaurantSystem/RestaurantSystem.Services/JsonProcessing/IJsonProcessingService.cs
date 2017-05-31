﻿namespace RestaurantSystem.Services.Abstraction
{
    using RestaurantSystem.Data.Abstraction;
    using RestaurantSystem.Infrastructure.Enumerations;
    using RestaurantSystem.JsonManaging;
    using RestaurantSystem.Services.JsonProcessing;

    public interface IJsonProcessingService
    {
        JsonProcessingResult ImportDocument(ImportingType importing,
             IRestaurantSystemData data, IJsonManager jsonManager, byte[] document);
    }
}
