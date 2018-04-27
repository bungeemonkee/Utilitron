using System;
using System.Data;
using System.Data.Common;

namespace Utilitron.Tests.Data.Profiling
{
    public class TestingCommand : DbCommand
    {
        private readonly DbDataReader reader;

        public override string CommandText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int CommandTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CommandType CommandType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override bool DesignTimeVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override UpdateRowSource UpdatedRowSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        protected override DbConnection DbConnection { get; set; }

        protected override DbParameterCollection DbParameterCollection => throw new NotImplementedException();

        protected override DbTransaction DbTransaction { get; set; }

        public TestingCommand(DbDataReader reader)
        {
            this.reader = reader;
        }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return reader;
        }
    }
}
