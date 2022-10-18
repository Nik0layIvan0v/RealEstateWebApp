namespace RealEstate.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using RealEstate.Data;
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using RealEstate.Models;
    using System.Collections.Generic;

    public class EstatesController : AdminController
    {
        private readonly RealEstateDbContext dbContext;

        public EstatesController(RealEstateDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Estate> unPublishedEstates = await this.dbContext
                .Estates
                .Select(estate => new Estate()
                {
                    //TODO: TO MODEL!!!
                    //TODO: TO Service!!!
                    //TODO: Finish administration!!!
                    Id = estate.Id,
                    Broker = this.dbContext.Brokers.Where(broker => broker.Id == estate.BrokerId).FirstOrDefault(),
                    CreatedOn = estate.CreatedOn,
                    EditedOn = estate.EditedOn,
                    ArchivedOn = estate.ArchivedOn,
                    BannedOn = estate.BannedOn,
                    Reports = this.dbContext.Reports.Where(report => report.ReportedEstateId == estate.Id).ToList(),
                    Comments = this.dbContext.Comments.Where(comment => comment.EstateId == estate.Id).ToList(),
                    Notes = this.dbContext.Notes.Where(note => note.EstateId == estate.Id).ToList()
                })
                .ToListAsync();

            return this.View(unPublishedEstates);
        }
    }
}
