using System;
using System.Collections.Generic;
using System.Globalization;
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

    public ArduinoReporter(IComputer computer, string portName, int baudRate) {
      this.computer = computer;
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

    public void Close() {
      if (serialPort != null && serialPort.IsOpen) {
        serialPort.Close();
      }
    }
    public void Open() {

      if (serialPort == null) {
        createSerialPort();
      }

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
          case HardwareType.GpuNvidia:
          case HardwareType.GpuAti: {
              gpuLoad = hardware.Sensors.First(x => x.SensorType == SensorType.Load & x.Name == "GPU Core");
              gpuTemperature = hardware.Sensors.First(x => x.SensorType == SensorType.Temperature);
              break;
            }
          default:
            break;
        }
      }

      serialPort.Open();
    }
    public TimeSpan LoggingInterval { get; set; }

    public void Log() {
      var now = DateTime.Now;

      if (lastLoggedTime + LoggingInterval - new TimeSpan(5000000) > now)
        return;

      string tmp = "";

      tmp += string.Format("{0,2}" + (","), (int) cpuTemperature.Value);
      tmp += string.Format("{0,3}" + (","), (int) cpuLoad.Value);
      tmp += string.Format("{0,2}" + (","), (int) gpuTemperature.Value);
      tmp += string.Format("{0,3}" + (","), (int) gpuLoad.Value);
      tmp += string.Format("{0,2}" + (","), Decimal.Round((decimal) ramFree.Value, 1));
      tmp += string.Format("{0,2}" + (","), Decimal.Round((decimal) ramUsed.Value, 1));
      tmp += string.Format("{0,2}" + (","), (int) ramLoad.Value);
      tmp += "\n";

      write(Encoding.ASCII.GetBytes(tmp));
     
      lastLoggedTime = now;
    }

    private void write(Byte[] data) {
      serialPort.Write(data, 0, data.Length);
    }

  }
}
