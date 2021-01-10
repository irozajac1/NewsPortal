using Microsoft.EntityFrameworkCore;
using NewsPortal.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Framework.Data.Context
{
    public class NewsPortalContext : DbContext
    {
        public NewsPortalContext(DbContextOptions<NewsPortalContext>options) : base(options)
        {

        }

        DbSet<News>News { get; set; }
    }
}
