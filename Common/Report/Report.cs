using Puma.Prey.Common.Rest;
using System;
using System.Collections.Generic;
using unirest_net.http;

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

        public static Report GetReportFromProvider(Guid userId)
        {
            HttpResponse<Report> jsonResponse = Unirest.post("http://thirdparyservice.org/v1/report")
              .header("accept", "application/json")
              .field("userId", userId)
              .asJson<Report>();

            return jsonResponse.Body;
        }

        public static Report GetReport(string lanId)
        {
            return RestClient.Get<Report>($"report/{lanId}");
        }
    }
}
