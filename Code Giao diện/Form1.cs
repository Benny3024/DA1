using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ZedGraph;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Đồ_án
{
    public partial class Form1 : Form
    {
        int tickStart = 0;
        int load = 0;
       
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            string[] ports = SerialPort.GetPortNames();
            cbport.Items.AddRange(ports);
            btndisconnect.Enabled = false;
            //**************************************************************************************************************************
           
            GraphPane myPane = zedGraphControl1.GraphPane; // Khai báo sử dụng Graph loại GraphPane;
            // Các thông tin cho đồ thị của mình
            myPane.Title.Text = "Control motor by PID";
            myPane.XAxis.Title.Text = "Thời gian t(s)";
            myPane.YAxis.Title.Text = "Position";

            // Định nghĩa list để vẽ đồ thị. Để các bạn hiểu rõ cơ chế làm việc ở đây khai báo 2 list điểm <=> 2 đường đồ thị
            RollingPointPairList list1 = new RollingPointPairList(12000);
            RollingPointPairList list2 = new RollingPointPairList(12000);

            // dòng dưới là định nghĩa curve để vẽ.
            LineItem duongline1 = myPane.AddCurve("Set Point", list1, Color.Red,SymbolType.None); // Color màu đỏ, đặc trưng cho đường 1
            // SymbolType là kiểu biểu thị đồ thị : điểm, đường tròn, tamgiác....
            LineItem duongline2 = myPane.AddCurve("Position", list2, Color.Blue,SymbolType.None); // Color màu Xanh, đặc trưng cho đường 2

            // Định hiện thị cho trục thời gian (Trục X)
            myPane.XAxis.Scale.Min = 0; // Min = 0;
            myPane.XAxis.Scale.Max = 4; // Mã = 30;
            myPane.XAxis.Scale.MinorStep = 0.5; // Đơn vị chia nhỏ nhất 
            myPane.XAxis.Scale.MajorStep = 1; // Đơn vị chia lớn 
            // Định hiện thị cho trục thời gian (Trục Y)
            myPane.YAxis.Scale.Min = 0; // Min = 0;
            myPane.YAxis.Scale.Max = 6000; // Mã = 30;
            myPane.YAxis.Scale.MinorStep = 100; // Đơn vị chia nhỏ nhất 
            myPane.YAxis.Scale.MajorStep = 1000; // Đơn vị chia lớn 
            // Gọi hàm xác định cỡ trục
            zedGraphControl1.AxisChange();
            
            

        }
        private void Draw(double setpoint1, double setpoint2)
        {
           
            
            LineItem duongline1 = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
            LineItem duongline2 = zedGraphControl1.GraphPane.CurveList[1] as LineItem;
            if (duongline1 == null)
                return;
            if (duongline2 == null)
                return;
            // list chứa các điểm.
            // Get the PointPairList
            IPointListEdit list1 = duongline1.Points as IPointListEdit;
            IPointListEdit list2 = duongline2.Points as IPointListEdit;
            if (list1 == null)
                return;
            if (list2 == null)
                return;
            // Time được tính bằng s
            double time = (Environment.TickCount - tickStart) / 1000.0;
            // Tính toán giá trị hiện thị
            // Muốn hiện thị cái gì thì chỉ việc thay vào setpointx
            list1.Add(time, setpoint1);
            list2.Add(time, setpoint2);
            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = 0;

            }

            // Vẽ đồ thị
            zedGraphControl1.AxisChange();
            // Force a redraw
            zedGraphControl1.Invalidate();
            
        }
        private void btnconnect_Click(object sender, EventArgs e)
        {
            btnconnect.Enabled = false;
            btndisconnect.Enabled = true;
            try
            {
                serialPort1.PortName = cbport.Text;
                serialPort1.BaudRate =Convert.ToInt32(cbbaudrate.Text);
                progressBar1.Value = 100;
                serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndisconnect_Click(object sender, EventArgs e)
        {
            btnconnect.Enabled = true;
            btndisconnect.Enabled = false;
            try
            {
                serialPort1.Close();
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
            

        private void cbcontrolmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbcontrolmode.Text == "Position")
            {
                zedGraphControl1.GraphPane.YAxis.Title.Text = "Position";
                zedGraphControl1.GraphPane.YAxis.Scale.Min = 0; // Min = 0;
                zedGraphControl1.GraphPane.YAxis.Scale.Max = 6000; // Mã = 30;
                zedGraphControl1.GraphPane.YAxis.Scale.MinorStep = 500; // Đơn vị chia nhỏ nhất 
                zedGraphControl1.GraphPane.YAxis.Scale.MajorStep = 1000; // Đơn vị chia lớn 
                                                   
                zedGraphControl1.AxisChange();
               
            }
            else
            {
                zedGraphControl1.GraphPane.YAxis.Title.Text = "Velocity";
                zedGraphControl1.GraphPane.YAxis.Scale.Min = 0; // Min = 0;
                zedGraphControl1.GraphPane.YAxis.Scale.Max = 500; // Mã = 30;
                zedGraphControl1.GraphPane.YAxis.Scale.MinorStep = 50; // Đơn vị chia nhỏ nhất 
                zedGraphControl1.GraphPane.YAxis.Scale.MajorStep = 100; // Đơn vị chia lớn 

                zedGraphControl1.AxisChange();
            }
            zedGraphControl1.Invalidate();
        }

        
        private void btnrun_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                timer1.Enabled = true;
                tickStart = Environment.TickCount;
                if (btnrun.Text == "RUN")
                {
                    
                    btnrun.Text = "STOP";
                    if (cbcontrolmode.Text == "Velocity")
                    {
                        byte[] frame = new byte[13];
                        byte[] commandVel = new byte[] { 0x56, 0x45, 0x4C };
                        int setpoint = int.Parse(txtsetpoint.Text);
                       
                        // Chuyển đổi thành 2 bytes
                        byte[] Data = new byte[10];
                        // Chuyển đổi số thực     
                        double kp = double.Parse((txtKp.Text).Trim())*10000;
                        double ki = double.Parse((txtKi.Text).Trim())*10000;
                        double kd = double.Parse((txtKd.Text).Trim())*10000;
                        ////Chuyển đổi sang int
                        int intKp = Convert.ToInt16(kp);
                        int intKi = Convert.ToInt16(ki);
                        int intKd = Convert.ToInt16(kd);
                        Data[0] = (byte)(load & 0x01);
                        Data[1] = 0;
                        Data[2] = (byte)((setpoint >> 8) & 0xFF);
                        Data[3] = (byte)(setpoint & 0xFF);
                        Data[4] = (byte)((intKp >> 8) & 0xFF);
                        Data[5] = (byte)(intKp & 0xFF);
                        Data[6] = (byte)((intKi >> 8) & 0xFF);
                        Data[7] = (byte)(intKi & 0xFF);
                        Data[8] = (byte)((intKd >> 8) & 0xFF);
                        Data[9] = (byte)(intKd & 0xFF);

                        Array.Copy(commandVel, 0, frame, 0, 3); // Copy 3 byte COMMAND vào frame
                        Array.Copy(Data, 0, frame, 3, 10); // Copy 10 byte data vào frame

                        serialPort1.Write(frame, 0, 13);
                    }
                    else
                    {
                        byte[] frame = new byte[13];
                        byte[] commandPos = new byte[] { 0x50, 0x4f, 0x53 };  //'p'; 'o'' 's'
                        int setpoint = int.Parse(txtsetpoint.Text);

                        // Chuyển đổi thành 2 bytes
                        byte[] Data = new byte[10];
                        // Chuyển đổi số thực     
                        double kp = double.Parse((txtKp.Text).Trim()) * 10000;
                        double ki = double.Parse((txtKi.Text).Trim()) * 10000;
                        double kd = double.Parse((txtKd.Text).Trim()) * 10000;
                        ////Chuyển đổi sang int
                        int intKp = Convert.ToInt16(kp);
                        int intKi = Convert.ToInt16(ki);
                        int intKd = Convert.ToInt16(kd);
                        Data[0] = (byte)(load & 0x01);
                        Data[1] = 0;
                        Data[2] = (byte)((setpoint >> 8) & 0xFF);
                        Data[3] = (byte)(setpoint & 0xFF);
                        Data[4] = (byte)((intKp >> 8) & 0xFF);
                        Data[5] = (byte)(intKp & 0xFF);
                        Data[6] = (byte)((intKi >> 8) & 0xFF);
                        Data[7] = (byte)(intKi & 0xFF);
                        Data[8] = (byte)((intKd >> 8) & 0xFF);
                        Data[9] = (byte)(intKd & 0xFF);

                        Array.Copy(commandPos, 0, frame, 0, 3); // Copy 3 byte COMMAND vào frame
                        Array.Copy(Data, 0, frame, 3, 10); // Copy 5 byte data vào frame

                        serialPort1.Write(frame, 0, 13);
                    }

                }
                else
                {
                    btnrun.Text = "RUN";
                    timer1.Stop();
                    byte[] frame = new byte[13];
                    byte[] commandStop = new byte[] { 0x53, 0x54, 0x4f };  //'s'; 't'; 'o'; 'p'                   
                    Array.Copy(commandStop, 0, frame, 0, 3); // Copy 3 byte COMMAND vào frame
                    serialPort1.Write(frame, 0, 13);           
                }
            }    
            
        }

       

        private void btnreset_Click(object sender, EventArgs e)
        {
            timer1.Stop();
             RPM = 0;
             SET = 0;
            
            // Xóa textbox nhận data
            recievedata.Text = "";
            zedGraphControl1.GraphPane.CurveList.Clear(); // Xóa đường
            zedGraphControl1.GraphPane.GraphObjList.Clear(); // Xóa đối tượng
            // Xóa dữ liệu đồ thị và khởi tạo lại
            zedGraphControl1.Invalidate();
            GraphPane myPane = zedGraphControl1.GraphPane; 
            myPane.Title.Text = "Control motor by PID";
            myPane.XAxis.Title.Text = "Thời gian t(s)";
            myPane.YAxis.Title.Text = null;
            RollingPointPairList list1 = new RollingPointPairList(12000);
            RollingPointPairList list2 = new RollingPointPairList(12000);
            LineItem duongline1 = myPane.AddCurve("Set Point", list1, Color.Red, SymbolType.None); 
            LineItem duongline2 = myPane.AddCurve("Position", list2, Color.Blue, SymbolType.None);
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 4;
            myPane.XAxis.Scale.MinorStep = 0.5;
            myPane.XAxis.Scale.MajorStep = 1;
            if (cbcontrolmode.Text == "Position")
            {
                myPane.YAxis.Title.Text = "Position";
                myPane.YAxis.Scale.Min = 0;
                myPane.YAxis.Scale.Max = 6000;
                myPane.YAxis.Scale.MinorStep = 100; // Đơn vị chia nhỏ nhất 
                myPane.YAxis.Scale.MajorStep = 1000;
            }
            else {
                myPane.YAxis.Title.Text = "Velocity";
                myPane.YAxis.Scale.Min = 0;
                myPane.YAxis.Scale.Max = 500;
                myPane.YAxis.Scale.MinorStep = 20; // Đơn vị chia nhỏ nhất 
                myPane.YAxis.Scale.MajorStep = 100; // Đơn vị chia lớn
                  }
            zedGraphControl1.AxisChange();
        }

        Int16 RPM = 0;
        Int16 SET = 0;
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                byte[] Rxbuffer = new byte[13];
                serialPort1.Read(Rxbuffer, 0, Rxbuffer.Length);
                byte[] dataBytes = { Rxbuffer[3], Rxbuffer[4] };
                byte[] dataSet = { Rxbuffer[5], Rxbuffer[6] };
                RPM = BitConverter.ToInt16(dataBytes, 0);
                SET = BitConverter.ToInt16 (dataSet, 0);
                string RPMString = RPM.ToString();
                string frame = BitConverter.ToString(Rxbuffer);
                this.Invoke(new Action(() =>
                {
                 // Hiển thị dữ liệu nhận được dưới dạng chuỗi hex trên TextBox
                    recievedata.AppendText(" FARME: "  + frame + Environment.NewLine + " DATA: " + RPMString + Environment.NewLine);
                }));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                
                Draw((double)SET, (double)RPM);
               
            }
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void cbport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if(comboBox1.Text == "No Load")
            {
                load = 0;
            }
            else
            {
                load = 1;
            }
        }
    }
}
