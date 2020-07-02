using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenHardwareMonitor.GUI {
  public partial class ArduinoConfiguration : Form {
    private MainForm parent;
    public ArduinoConfiguration(MainForm m) {
      InitializeComponent();
      parent = m;

      populateBaudRateCombobox();
      populateSerialPortsCombobox();
    }

    private void ArduinoConfiguration_Load(object sender, EventArgs e) {

    }

    private void populateBaudRateCombobox() {
      baudRate.Items.AddRange(new object[] {
                        300,
                        600,
                        1200,
                        2400,
                        4800,
                        9600,
                        14400,
                        19200,
                        28800,
                        38400,
                        57600,
                        115200
      });
      baudRate.SelectedItem = this.parent.ArduinoReporter.BaudRate;
    }
    private void populateSerialPortsCombobox() {
      // Get a list of serial port names.
      string[] ports = SerialPort.GetPortNames();

      // Display each port name to the console.
      foreach (string port in ports) {
        portCOM.Items.Add(port);
      }

      portCOM.SelectedItem = this.parent.ArduinoReporter.PortName;
    }

    private void baudRate_SelectedIndexChanged(object sender, EventArgs e) {
      this.parent.ArduinoReporter.BaudRate = (int) baudRate.SelectedItem;
    }

    private void portCOM_SelectedIndexChanged(object sender, EventArgs e) {
      this.parent.ArduinoReporter.PortName = (string) portCOM.SelectedItem;
    }
  }
}
