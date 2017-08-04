using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace MSPOSBACKOFFICE
{
    class ChkClass:Form
    {
        private SerialPort _serialPort;         //<-- declares a SerialPort Variable to be used throughout the form
        private const int BaudRate = 9600;  
       
        public void button1_Click()
        {
            if (_serialPort != null && _serialPort.IsOpen)
                _serialPort.Close();
            if (_serialPort != null)
                _serialPort.Dispose();
            //<-- End of Block

            _serialPort = new SerialPort("COM5", BaudRate, Parity.None, 8, StopBits.One);       //<-- Creates new SerialPort using the name selected in the combobox
            _serialPort.DataReceived += SerialPortOnDataReceived;       //<-- this event happens everytime when new data is received by the ComPort
            _serialPort.Open();     //<-- make the comport listen
            //textBox1.Text = "Listening on " + _serialPort.PortName + "...\r\n";
        }
        public string tREad = "";
        private delegate void Closure();
        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {

            if (InvokeRequired)     //<-- Makes sure the function is invoked to work properly in the UI-Thread
                BeginInvoke(new Closure(() => { SerialPortOnDataReceived(sender, serialDataReceivedEventArgs); }));     //<-- Function invokes itself
            else
            {
                while (_serialPort.BytesToRead > 0) //<-- repeats until the In-Buffer is empty
                {
                  tREad=string.Format("{0:X2} ", _serialPort.ReadByte());
                    //<-- bytewise adds inbuffer to textbox
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ChkClass
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "ChkClass";
            this.Load += new System.EventHandler(this.ChkClass_Load);
            this.ResumeLayout(false);

        }

        private void ChkClass_Load(object sender, EventArgs e)
        {

        }

    }
}
