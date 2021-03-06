﻿namespace RestaurantSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Data.Abstraction;
    using RestaurantSystem.DataImporter.SupplyDocumentImporter.Abstraction;
    using RestaurantSystem.Infrastructure.Enumerations;
    using RestaurantSystem.JsonManaging;
    using RestaurantSystem.Services.Abstraction;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class JsonImporterController : BaseController
    {
        private readonly IJsonProcessingService jsonProcessingService;
        private readonly IJsonManager jsonManager;
        private readonly ISupplyDocumentDataSeeder seeder;

        public JsonImporterController(IRestaurantSystemData data, IJsonProcessingService jsonProcessingService,
            IJsonManager jsonManager, ISupplyDocumentDataSeeder seeder) : base(data)
        {
            this.jsonProcessingService = jsonProcessingService;
            this.jsonManager = jsonManager;
            this.seeder = seeder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("JsonImporter")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            if (files.Count > 0)
            {
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(stream);

                            this.jsonProcessingService.ImportDocument(ImportingType.Products,
                                this.Data, this.jsonManager, stream.ToArray(), seeder);
                        }

                        ViewData["Message"] = "Successful Importing!";
                    }
                }
            }
            
            return View("Index");
        }
    }
}
