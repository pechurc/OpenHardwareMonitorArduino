using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.WMI;

namespace OpenHardwareMonitor.Utilities {
  public class ArduinoReporter {
    private SerialPort serialPort;
    private string portName;
    private int baudRate;

    private readonly IComputer computer;

    private ISensor cpuTemperature;
    private ISensor cpuLoad;
    private ISensor ramLoad;
    private ISensor ramFree;
    private ISensor ramUsed;
    private ISensor gpuLoad;
    private ISensor gpuTemperature;
    private DateTime lastLoggedTime = DateTime.MinValue;

    public ArduinoReporter(IComputer computer) {
      this.computer = computer;
    }

    public ArduinoReporter(string portName, int baudRate) {

      this.portName = portName;
      this.baudRate = baudRate;
    }

    public void createSerialPort() {
      // Create a new SerialPort object with default settings.
      serialPort = new SerialPort();

      // Allow the user to set the appropriate properties.
      serialPort.PortName = portName;
      serialPort.BaudRate = baudRate;
      serialPort.Parity = Parity.None;
      serialPort.DataBits = 8;
      serialPort.StopBits = StopBits.One;
      serialPort.Handshake = Handshake.None;
    }

    public string PortName {
      get { return portName; }
      set { portName = value; }
    }
    public int BaudRate {
      get { return baudRate; }
      set { baudRate = value; }
    }

    public void Open() {
      IList<ISensor> list = new List<ISensor>();
      SensorVisitor visitor = new SensorVisitor(sensor => {
        list.Add(sensor);
      });
      visitor.VisitComputer(computer);

      IList<IHardware> hardwares = this.computer.Hardware;

      foreach(IHardware hardware in hardwares) {
        switch(hardware.HardwareType) {
          case HardwareType.CPU: {
              cpuTemperature = hardware.Sensors.First(x => x.Name == "CPU Package");
              cpuLoad = hardware.Sensors.First(x => x.Name == "CPU Total");
              break;
            }
          case HardwareType.RAM: {
              ramLoad = hardware.Sensors.First(x => x.SensorType == SensorType.Load);
              ramUsed = hardware.Sensors.First(x => x.Name == "Used Memory");
              ramFree = hardware.Sensors.First(x => x.Name == "Available Memory");
              break;
            }
          case HardwareType.GpuNvidia: {
              gpuLoad = hardware.Sensors.First(x => x.Name == "GPU Core");
              gpuTemperature = hardware.Sensors.First(x => x.SensorType == SensorType.Temperature);
              break;
            }
          default:
            break;
        }
      }
    }
    public TimeSpan LoggingInterval { get; set; }

    public void Log() {
      var now = DateTime.Now;

      if (lastLoggedTime + LoggingInterval - new TimeSpan(5000000) > now)
        return;

      Console.WriteLine(cpuTemperature.Value);

      lastLoggedTime = now;
    }

  }
}
