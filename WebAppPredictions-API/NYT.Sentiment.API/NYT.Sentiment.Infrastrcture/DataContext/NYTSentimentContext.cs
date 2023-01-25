using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SentimentRazor.Models
{
    public class NYTSentimentContext: DbContext
    {

        public NYTSentimentContext(DbContextOptions<NYTSentimentContext> options) : base(options)
        {

        }

        public DbSet<SentimentPredictionActualResults> SavePrediction { get; set; }

    }
}
