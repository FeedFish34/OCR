using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace OCRSerialPort
{
    public class BiuTCPClientPort : BiuPort
    {
        /// <summary>
        /// 连接超时线程
        /// 当超时后，每隔 TimeoutSeconds 秒，再次连接
        /// 每次收到数据就重置这个时钟
        /// </summary>
        System.Threading.Timer TimeOutTimer = null;
        /// <summary>
        /// 超时秒数
        /// </summary>
        public int TimeoutSeconds = 10;

        /// <summary>
        /// 超时多少次后关闭当前连接释放资源
        /// </summary>
        public int TimeOutLimit = 1;

        /// <summary>
        /// 当前超时次数
        /// </summary>
        public int TimeOutCount = 0;

        /// <summary>
        /// 当前通讯Tcp客户端
        /// </summary>
        TcpClient client = null;

        public BiuTCPClientPort(string NetIP,int NetMidPort)
        {

            HostName = NetIP;
            Port = NetMidPort;
        }

        public virtual Encoding Encoder
        {
            get { return Encoding.UTF8; }
            set { }
        }
        public string HostName { get; set; }
        public int Port { get; set; }

        public void Open()
        {
            client = new TcpClient();


            client.BeginConnect(HostName, Port, ConnectCallback, null);
        }

        public void Close()
        {
            client.Close();
        }

        void KillTimeOutTimer()
        {
            TimeOutTimer.Change(Timeout.Infinite, 0);
        }

        void ResetTimeOutTimer()
        {
            TimeOutCount = 0;
            TimeOutTimer.Change(TimeoutSeconds * 1000, TimeoutSeconds * 1000);
        }

        /// <summary>
        /// 创建超时计时器
        /// </summary>
        /// <param name="seconds">秒</param>
        public void CreateTimeOutTimer(int seconds)
        {
            if (TimeOutTimer != null)
            {
                TimeOutTimer.Change(Timeout.Infinite, 0);
                TimeOutTimer.Dispose();
            }
            TimeOutTimer = new
            System.Threading.Timer(
            new TimerCallback(TimeOutTimerCallBack), null,
            seconds * 1000, seconds * 1000);


        }

        private void TimeOutTimerCallBack(object state)
        {
            try
            {
                if (client.Connected)
                {
                    TimeOutCount++;
                    if (TimeOutCount >= TimeOutLimit)
                    {
                        client.Close();
                        TimeOutCount = 0;
                    }
                }
                else
                {
                    try
                {
                    client.Close();
                }
                catch (Exception)
                {
                }
                client = new TcpClient();
                client.BeginConnect(HostName, Port, ConnectCallback, null);
                }
            }
            catch (Exception)
            {

            }

        }

        private void ConnectCallback(IAsyncResult result)
        {

            try
            {
                CreateTimeOutTimer(TimeoutSeconds);
                client.EndConnect(result);

                NetworkStream networkStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];
                networkStream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);


            }
            catch (Exception ex)
            {
                Lib.LogManager.Logger.LogException(ex);
            }
        }
        string curMsg = string.Empty;
        /// <summary>
        ///回写
        /// </summary>
        /// <param name="result"></param>
        private void ReadCallback(IAsyncResult result)
        {
            int read;
            NetworkStream networkStream;
            try
            {
                ResetTimeOutTimer();//重置超时次数
                networkStream = client.GetStream();
                if (networkStream == null)
                    return;

                read = networkStream.EndRead(result);
                if (read == 0)
                    return;
                byte[] buffer = result.AsyncState as byte[];
                byte[] Data = new byte[read];
                Buffer.BlockCopy(buffer, 0, Data, 0, read);
                networkStream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
                LogRevMsg.LogText("接收",Encoding.Default.GetString(Data));
                if (DataReceived != null)
                {

                    DataReceived(networkStream, Data);
                }

            }
            catch (Exception ex)
            {

                Lib.LogManager.Logger.LogException(ex);
            }
            finally
            {


            }


        }


        public void Send(byte[] messages)
        {
            IPEndPoint ipendpoint = client.Client.RemoteEndPoint as IPEndPoint;
            NetworkStream stream = client.GetStream();
            stream.Write(messages, 0, messages.Length);
            // stream.Close();
        }

        public void Receive()
        {

            IPEndPoint ipendpoint = client.Client.RemoteEndPoint as IPEndPoint;
            NetworkStream stream = client.GetStream();

            //2.接收状态,长度<1024字节
            byte[] bytes = new Byte[1024 * 20];
            //string data = string.Empty;
            stream.Read(bytes, 0, bytes.Length);
            LogRevMsg.LogText("接收", Encoding.Default.GetString(bytes));
            if (DataReceived != null)
                DataReceived(stream, bytes);
        }

        public override void Dispose()
        {
            Close();
            client = null;
        }

        public event ByteReceivedEventHandler DataReceived;

        public string SpcialItrID { get; set; }
    }
}
