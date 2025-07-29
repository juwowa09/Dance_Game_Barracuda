using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UDPSender : MonoBehaviour
{
    [Header("UDP 설정")]
    public string remoteIP = "127.0.0.1"; // 수신 대상 IP
    public int remotePort = 9050;         // 수신 대상 포트

    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;

    void Start()
    {
        udpClient = new UdpClient();
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
    }

    public void Send(byte[] data)
    {
        try
        {
            udpClient.Send(data, data.Length, remoteEndPoint);
        }
        catch (SocketException e)
        {
            Debug.LogError("UDP 전송 실패: " + e.Message);
        }
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}