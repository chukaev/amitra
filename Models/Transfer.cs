using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Models
{
    public class Transfer
    {
        public Transfer()
        {
        }

        public Transfer(JToken jToken)
        {
            foreach (var outwardToken in jToken["transportationsBlocks"]["outward"])
            {
                var block = new TransportationsBlock();
                var blockPart = outwardToken["arrival"];
                if (blockPart != null)
                {
                    block.Arrival = blockPart["departureCity"]?["label"].Value<string>();
                    block.ArrivalDateTime = blockPart["time"] != null
                        ? blockPart["time"]?.Value<DateTime?>()
                        : new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                            .AddSeconds(outwardToken["date"].Value<int>()).ToLocalTime();
                }
                blockPart = outwardToken["departure"];
                if (blockPart != null)
                {
                    block.Departure = blockPart["departureCity"]?["label"].Value<string>();
                    block.DepartureDateTime = blockPart["time"] != null
                        ? blockPart["time"]?.Value<DateTime?>()
                        : new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                            .AddSeconds(outwardToken["date"].Value<int>()).ToLocalTime();
                }

                Outward.Add(block);
            }

            foreach (var returnToken in jToken["transportationsBlocks"]["return"])
            {
                var block = new TransportationsBlock();
                var blockPart = returnToken["arrival"];
                if (blockPart != null)
                {
                    block.Arrival = blockPart["departureCity"]?["label"].Value<string>();
                    block.ArrivalDateTime = blockPart["time"] != null
                        ? blockPart["time"]?.Value<DateTime?>()
                        : new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                            .AddSeconds(returnToken["date"].Value<int>()).ToLocalTime();
                }
                blockPart = returnToken["departure"];
                if (blockPart != null)
                {
                    block.Departure = blockPart["departureCity"]?["label"].Value<string>();
                    block.DepartureDateTime = blockPart["time"] != null
                        ? blockPart["time"]?.Value<DateTime?>()
                        : new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                            .AddSeconds(returnToken["date"].Value<int>()).ToLocalTime();
                }

                Return.Add(block);
            }
        }

        public int Price { get; set; }

        public List<TransportationsBlock> Outward { get; set; } = new List<TransportationsBlock>();

        public List<TransportationsBlock> Return { get; set; }=new List<TransportationsBlock>();

        public List<TransportationsBlock> AllTransportationsBlocks => Outward.Concat(Return).ToList();
    }
}