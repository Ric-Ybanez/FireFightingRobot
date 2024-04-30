using FireFightingRobot.Framework.Interface;

namespace FireFightingRobot.Framework.Enums
{
    public class AlertLevel
    {
        public string Name { get; set; }
        public int Value { get; set; }

        private const int HEAT_INDEX_ALERT_LEVEL = 72;
        private const int SMOKE_ALERT_LEVEL = 20;
        private const int TEMPERATURE_ALERT_LEVEL = 38;
        private const int HUMIDITY_ALERT_LEVEL = 30;

        public static readonly AlertLevel Ok = new ("Okay", 0);
        public static readonly AlertLevel FireDetected = new("Fire Detected", 5);
        public static readonly AlertLevel HighHeatIndex = new("High Heat Index", 4);
        public static readonly AlertLevel HighSmoke = new("High Smoke", 3);
        public static readonly AlertLevel HighTemperature = new("High Temperature", 2);
        public static readonly AlertLevel LowHumidity = new("Low Humidity", 1);



        private AlertLevel(string name, int value) 
        {
            Name = name;
            Value = value;
        }

        public static AlertLevel FromInput(IDeviceInput input) 
        {
            if (!string.IsNullOrEmpty(input.FireDetected))
                return FireDetected;

            if (input.HeatIndex > HEAT_INDEX_ALERT_LEVEL)
                return HighHeatIndex;

            if (input.Smoke > SMOKE_ALERT_LEVEL)
                return HighSmoke;

            if (input.Temperature > TEMPERATURE_ALERT_LEVEL)
                return HighTemperature;

            if(input.Humidity < HUMIDITY_ALERT_LEVEL)
                return LowHumidity;

            return Ok;
        }
    }
}
