using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenHardwareMonitor.Utilities {
  public class ArduinoReporter {
    private SerialPort serialPort;
    private string portName;
    private int baudRate;
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
  }
}
