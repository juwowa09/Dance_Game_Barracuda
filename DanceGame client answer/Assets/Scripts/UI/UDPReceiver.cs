using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class UDPReceiver : MonoBehaviour
{
    UdpClient udpClient;
    public int listenPort = 9050;
    public RawImage rawImage;

    ConcurrentQueue<byte[]> receivedQueue = new ConcurrentQueue<byte[]>();
    Texture2D tex;

    void Start()
    {
        udpClient = new UdpClient(listenPort);
        udpClient.BeginReceive(OnReceived, null);
// 초기 텍스처 생성 시 고정 크기로 (예: 512x512)
        tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
    }

    void OnReceived(IAsyncResult ar)
    {
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
        byte[] received = udpClient.EndReceive(ar, ref remoteEP);

        receivedQueue.Enqueue(received); // 메인 스레드에서 처리하도록 큐에 저장

        udpClient.BeginReceive(OnReceived, null); // 다시 수신 대기
    }

    void Update()
    {
        if (receivedQueue.TryDequeue(out byte[] data))
        { 
            tex.LoadImage(data, false); // false로 하면 텍스처 크기 유지 → GC 줄임
            rawImage.texture = tex;
        }
    }

    void OnDestroy()
    {
        udpClient?.Close();
    }
}