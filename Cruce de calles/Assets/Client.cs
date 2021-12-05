using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour
{
    const int BUFFER_SISE = 1024;
    byte[] readBuff = new byte[BUFFER_SISE];
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    String str = "cat";
    // Start is called before the first frame update
    void Start()
    {
        socket.Connect("127.0.0.1", 1234);
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        socket.Send(bytes);
    }

    // Update is called once per frame
    void Update()
    {
        pregunta();
    }
    void pregunta()
    {
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        socket.Send(bytes);
        int count = socket.Receive(readBuff);
        str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);
        Debug.Log(str);
        socket.Close();
    }
}
