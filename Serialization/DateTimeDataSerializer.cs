using System;

namespace SpecFirst.Core.Serialization
{
    public class DateTimeDataSerializer
    {
        public string Serialize(object data)
        {
            DateTime date = (DateTime) data;

            return $"new DateTime({date.Year}, {date.Month}, {date.Day}, {date.Hour}, {date.Minute}, {date.Second}, {date.Millisecond})";
        }
    }
}
