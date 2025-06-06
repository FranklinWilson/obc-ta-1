using CsvHelper.Configuration;
using System;

public class DataMap : ClassMap<Data>
{
    public DataMap()
    {
        Map(m => m.RecordingId).Name("RECORDING_ID");
        Map(m => m.AssetName).Name("ASSET_NAME");
        Map(m => m.UnixTime).Name("UNIX_TIME");
        Map(m => m.PositionYards).Name("POSITION_YARDS");
        Map(m => m.Lattitude).Name("LATITUDE");
        Map(m => m.Longitude).Name("LONGITUDE");
        Map(m => m.Score).Name("SCORE");
        Map(m => m.Time)
            .Convert(args =>
            {
                var unixTime = args.Row.GetField<int>("UNIX_TIME");
                return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
            });
    }
}