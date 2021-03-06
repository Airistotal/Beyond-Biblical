﻿namespace BB.Infrastructure.Tests
{
    using System;
    using BB.Infrastructure.Context;
    using Microsoft.EntityFrameworkCore;

    public class DbInMemoryTest
    {
        public DbInMemoryTest()
        {
            this.ConfigureContext();
            this.FillTestData();
        }

        public BibleContext BibleContext { get; internal set; }

        private void ConfigureContext()
        {
            var dbname = "BE_" + Guid.NewGuid();
            var options = new DbContextOptionsBuilder<BibleContext>()
                .UseInMemoryDatabase(databaseName: dbname)
                .Options;

            this.BibleContext = new BibleContext(options);
        }

        private void FillTestData()
        {
        }
    }
}