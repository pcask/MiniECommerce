using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.WebApi.Configurations.Serilog.ColumnWriters
{
    public class UserEmailColumnWriter : ColumnWriterBase
    {
        public UserEmailColumnWriter() : base(NpgsqlDbType.Varchar, columnLength: 100)
        {
        }

        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            logEvent.Properties.TryGetValue("user_email", out var email);

            return email?.ToString() ?? null;
        }
    }
}
