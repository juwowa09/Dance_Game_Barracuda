using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class udpServer : MonoBehaviour
{
    private UdpClient server;
    private IPEndPoint endPoint;
    
    public string ip = "127.0.0.1";  // 수신자 IP (예: 데스크탑 IP)
    public int port = 8001; 
    
    // Start is called before the first frame update
    void Start()
    {
        server = new UdpClient();
        endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
    }
    
    public void SendMessage(string message)
    {
        message += "EOM";
        byte[] data = Encoding.UTF8.GetBytes(message);
        server.Send(data, data.Length, endPoint);
        // Debug.Log($"[UDP Client] Sent: {message}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
