namespace FireFightingRobot.Framework.Interface;

public interface IDeviceInput
{
    public double Temperature { get; }
    public double Humidity { get;  }
    public double Smoke { get;  }
    public double HeatIndex { get;  }
    public string FireDetected { get; set; }
}
