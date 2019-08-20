using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace OCRSerialPort

{
    public partial class Form1 : Form
    {
        OCRSerialDevice ocr = new OCRSerialDevice();
        BiuTCPClientPort ClientPort = null;
        public Form1()
        {
            InitializeComponent();
        }
        string picName = string.Empty;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        public int selectedDeviceIndex = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string IP = txtIp.Text;
                int Port = Convert.ToInt32(txtPort.Text);
                ClientPort = new BiuTCPClientPort(IP, Port);
                ClientPort.Open();
                // 枚举所有视频输入设备
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                foreach (FilterInfo device in videoDevices)
                {
                    tscbxCameras.Items.Add(device.Name);
                }

                tscbxCameras.SelectedIndex = 0;

            }
            catch (ApplicationException)
            {
                tscbxCameras.Items.Add("No local capture devices");
                videoDevices = null;
            }
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[tscbxCameras.SelectedIndex].MonikerString);
            videoSource.DesiredFrameSize = new System.Drawing.Size(320, 240);
            videoSource.DesiredFrameRate = 1;
            videoSourcePlayer1.Visible = true;
            videoSourcePlayer1.VideoSource = videoSource;
            videoSourcePlayer1.Start();
        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.WaitForStop();

            videoSourcePlayer1.Visible = false;
        }

        private string GetImagePath()
        {
            string personImgPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)
                         + Path.DirectorySeparatorChar.ToString() + "PersonImg";
            if (!Directory.Exists(personImgPath))
            {
                Directory.CreateDirectory(personImgPath);
            }

            return personImgPath;
        }

        private void btnPhoto_Click(object sender, EventArgs e)
        {
            btnPhoto.Enabled = false;
            try
            {
                //picName = GetImagePath() + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                picName = GetImagePath() + "\\" + "xiaosy.jpg";
                #region 拍照生成图片
                //if (videoSourcePlayer1.IsRunning)
                //{
                //    BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                //                    videoSourcePlayer1.GetCurrentVideoFrame().GetHbitmap(),
                //                    IntPtr.Zero,
                //                     Int32Rect.Empty,
                //                    BitmapSizeOptions.FromEmptyOptions());
                //    PngBitmapEncoder pE = new PngBitmapEncoder();
                //    pE.Frames.Add(BitmapFrame.Create(bitmapSource));
                //    using (Stream stream = File.Create(picName))
                //    {
                //        pE.Save(stream);
                //    }
                //    //拍照完成后关摄像头并刷新同时关窗体
                //    if (videoSourcePlayer1 != null && videoSourcePlayer1.IsRunning)
                //    {
                //        videoSourcePlayer1.SignalToStop();
                //        videoSourcePlayer1.WaitForStop();
                //        videoSourcePlayer1.Visible = false;
                //    }

                //}
                #endregion

                #region 根据图片获取图片的文字
                AspriseOCR1.GetOCRpart(picName);
                string ORCString = AspriseOCR1.ORCResult;

                //string ORCString = GetVeryfyCode(picName);

                //Bitmap pic = new Bitmap(picName);
                //int width = pic.Size.Width; // 图片的宽度
                //int height = pic.Size.Height; // 图片的高度
                //string ORCString = Marshal.PtrToStringAnsi(OCRpart(picName, -1, 0, 0, width, height));
                string OCRNum = System.Text.RegularExpressions.Regex.Replace(ORCString, @"[^0-9]+", "");
                if (!string.IsNullOrEmpty(OCRNum))
                {
                    string Msg = ASTMCommon.cENQ_5.ToString() + ASTMCommon.cSTX_2.ToString() + OCRNum + ASTMCommon.cETX_3.ToString() + ASTMCommon.cEOT_4.ToString();
                    byte[] SendMsg = Encoding.Default.GetBytes(Msg);
                    LogRevMsg.LogText("发送", Msg);
                    ClientPort.Send(SendMsg);
                    //ocr.AnswerList.Add(ASTMCommon.cSTX_2.ToString() + OCRNum+ASTMCommon.cETX_3.ToString());
                    //ocr.AnswerList.Add(ASTMCommon.cEOT_4.ToString());
                    //ocr.SendEnqCommand(100);
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("摄像头异常：" + ex.Message);
            }
            btnPhoto.Enabled = true;
        }

        private string GetVeryfyCode(string _imgPath)
        {
            string veryfyCode = string.Empty;
            if (File.Exists(_imgPath))//ok now?
            {
                try
                {
                  //veryfyCode = craboOCR(_imgPath, -1);   //将返回string,并以"/r/n"结尾!!

                }
                catch (Exception e)
                {
                }
            }
            return veryfyCode;
        }
    }
}
