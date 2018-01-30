using Puma.Prey.Common.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puma.Prey.Common
{
    public class Report
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Timestamp { get; set; }

        public Decimal Cost { get; set; }

        public int Size { get; set; }

        public Dictionary<string, string> MetaData;

        public static Report GetReport(Guid userId)
        {
            return RestClient.Get<Report>($"report/{userId}");
        }
    }
}
