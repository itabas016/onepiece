using OnePiece.Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Data
{
    public class DataSetToModelTest
    {
        [Fact]
        public void should_convert()
        {
            var ds = new DataSet();
            var dt = new DataTable();
            ds.Tables.Add(dt);
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Birthday", typeof(DateTime));
            dt.Columns.Add("GraduateDate", typeof(DateTime));
            dt.Columns.Add("Times", typeof(long));
            dt.Columns.Add("NullTimes", typeof(long));
            dt.Columns.Add("Gender", typeof(bool));
            dt.Columns.Add("ModelType", typeof(int));
            dt.Columns.Add("IsMate", typeof(bool));
            dt.Columns.Add("Gold", typeof(int));

            var row = dt.NewRow();
            row["Id"] = "1";
            row["Name"] = "Allen";
            row["Age"] = 20;
            row["Birthday"] = new DateTime(1991, 1, 1);
            row["GraduateDate"] = new DateTime(2001, 1, 1);
            row["Times"] = 2;
            row["NullTimes"] = 3;
            row["Gender"] = 1;
            row["ModelType"] = 1;
            row["IsMate"] = 0;
            row["Gold"] = 30;

            dt.Rows.Add(row);

            // second row
            row = dt.NewRow();
            row["Id"] = DBNull.Value;
            row["Name"] = DBNull.Value;
            row["Age"] = DBNull.Value;
            row["Birthday"] = DBNull.Value;
            row["GraduateDate"] = DBNull.Value;
            row["Times"] = DBNull.Value;
            row["NullTimes"] = DBNull.Value;
            row["Gender"] = DBNull.Value;
            row["ModelType"] = DBNull.Value;
            row["IsMate"] = DBNull.Value;
            row["Gold"] = DBNull.Value;
            dt.Rows.Add(row);

            var model = ds.ToModel<TestModel>();

            Assert.True(model.Count == 2);

            var first = model.First();
            Assert.Equal(1, first.Id);
            Assert.Equal("Allen", first.Name);
            Assert.Equal(20, first.Age);
            Assert.Equal(new DateTime(1991, 1, 1), first.Birthday);
            Assert.Equal(new DateTime(2001, 1, 1), first.GraduateDate.GetValueOrDefault());
            Assert.Equal(2, first.Times);
            Assert.Equal(3, first.NullTimes);
            Assert.Equal(true, first.Gender);
            Assert.Equal(ModelType.Worker, first.ModelType);
            Assert.Equal(false, first.IsMate.GetValueOrDefault());

            var second = model.Skip(1).First();

            Assert.Equal(0, second.Id);
            Assert.Equal(null, second.Name);
            Assert.Equal(null, second.Age);
            Assert.Equal(new DateTime(1, 1, 1), second.Birthday);
            Assert.Equal(null, second.GraduateDate);
            Assert.Equal(0, second.Times);
            Assert.Equal(null, second.NullTimes);
            Assert.Equal(false, second.Gender);
            Assert.Equal(ModelType.Unknown, second.ModelType);
            Assert.Equal(null, second.IsMate);
        }

        public class TestModel
        {
            [SqlColumn]
            public int Id { get; set; }

            [SqlColumn]
            public string Name { get; set; }

            [SqlColumn]
            public int? Age { get; set; }

            [SqlColumn]
            public DateTime Birthday { get; set; }

            [SqlColumn]
            public DateTime? GraduateDate { get; set; }

            [SqlColumn]
            public long Times { get; set; }

            [SqlColumn]
            public long? NullTimes { get; set; }

            [SqlColumn]
            public bool Gender { get; set; }

            [SqlColumn]
            public ModelType ModelType { get; set; }

            [SqlColumn]
            public bool? IsMate { get; set; }

            [SqlColumn]
            public string Gold { get; set; }
        }

        public enum ModelType
        {
            Unknown = 0,
            Worker = 1,
            Student = 2
        }
    }
}
