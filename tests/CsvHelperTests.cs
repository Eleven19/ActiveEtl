using System;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using CsvHelper;
using FluentAssertions;
using Insight.Database;
using Xunit;

namespace Eleven19.ActiveEtl.Writers
{
    public class CsvHelperTests
    {
        public ConnectionStringSettings TestDb { get; private set; }
        public CsvHelperTests()
        {
            TestDb = ConfigurationManager.ConnectionStrings["LocalTestDb"];
        }

        [Fact]
        public void CanConnectToLocalDb()
        {
            var connectSettings = TestDb;
            connectSettings.Invoking(settings =>
            {
                using (settings.Open())
                {
                }
            }).ShouldNotThrow();
        }

        [Fact]
        public void CanWriteCsvFromDataReader()
        {
            using (var conn = TestDb.Open())
            {
                using (var reader = Db.getDateDetailsDataReader(conn, DateTime.Parse("2013/06/30"),
                        DateTime.Parse("2014/12/31")))
                {
                    using (var stringWriter = new StringWriter())
                    {
                        using (var writer = new CsvFactory().CreateWriter(stringWriter))
                        {                            
                            writer.WriteRecords(reader);
                            var text = stringWriter.ToString();
                            Debug.WriteLine(text);
                            text.Should().NotBeEmpty();
                        }
                    }
                }
            }
        }
    }
}
