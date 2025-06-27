using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class udpReceiver : MonoBehaviour
{
    public Avatar avatar;
    private UdpClient receiver;
    private IPEndPoint endPoint;

    public int port = 8001;
    private string buffer = "";
    private bool open;

    void Start()
    {
        buffer = "";
        receiver = new UdpClient(port);
        endPoint = new IPEndPoint(IPAddress.Any, port);
        
        Thread t = new Thread(new ThreadStart(OnReceive));
        t.IsBackground = true;
        t.Start();
    }
    public void Connect()
    {
        open = true;
    }
    public void Disconnect()
    {
        receiver.Close();
        open = false;
    }

    private void OnReceive()
    {
        open = true;

        while (open)
        {
            try
            {
                byte[] data = receiver.Receive(ref endPoint);
                string message = Encoding.UTF8.GetString(data);
                buffer += message;

                if (buffer.Contains("EOM"))
                {
                    string[] lines = buffer.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string line in lines)
                    {
                        if (line == "EOM") break;

                        string[] tokens = line.Split('|');
                        if (tokens.Length == 4 && int.TryParse(tokens[0], out int index) &&
                            float.TryParse(tokens[1], out float x) &&
                            float.TryParse(tokens[2], out float y) &&
                            float.TryParse(tokens[3], out float z))
                        {
                            if (index >= 0 && index < avatar.jointPoints.Length)
                            {
                                avatar.jointPoints[index].pos3D = new Vector3(x, y, z);
                            }
                        }
                    }

                    buffer = "";
                }
            }
            catch (SocketException ex)
            {
                Debug.LogWarning("Socket closed or failed: " + ex.Message);
            }
            catch (Exception e)
            {
                Debug.LogError("Receiver Error: " + e);
            }
        }
    }

    void OnApplicationQuit()
    {
        Disconnect();
    }
}