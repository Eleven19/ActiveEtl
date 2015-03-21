using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eleven19.ActiveEtl.Data;
using Eleven19.ActiveEtl.Reflection;
using FluentAssertions;
using Insight.Database;
using Xunit;

namespace ActiveEtl.Tests.CSharp
{
    public class DataReaderTests
    {
        public class WhenTestingColumnInfo
        {
            public string GetTasksSql { get; private set; }
            public ConnectionStringSettings TestDb { get; set; }

            public WhenTestingColumnInfo()
            {
                var type = GetType();
                GetTasksSql = type.Assembly.ReadEmbeddedResourceTextInNamespace(type, "GetTasks.sql");
                TestDb = ConfigurationManager.ConnectionStrings["TestDb.SqlServer"];
            }

            [Fact]
            public void GetColumnInfoShouldReturnColumnInfoWithTheCorrectColumnNames()
            {
                var expectedColumnNames = new[] {"TaskId","Name","Description","Completed"};
                using (var db = TestDb.Open())
                {
                    var reader = db.GetReaderSql(GetTasksSql);
                    var columnInfos = reader.GetColumnInfo();
                    var columnNames = columnInfos.Select(x => x.ColumnName).ToArray();
                    Console.WriteLine("ColumnNames: {0}", String.Join(",",columnNames));
                    columnNames.Should()
                        .BeEquivalentTo(expectedColumnNames)
                        .And.ContainInOrder(expectedColumnNames)
                        .And.HaveCount(expectedColumnNames.Length);
                }
            }
        }
    }
}
