﻿using System;
using System.Net;
using System.Net.Sockets;
using SockLibNG.Domain.Sockets;
using SockLibNG.Sockets;
using Buffer = SockLibNG.Buffers.Buffer;

namespace SockLibNG.Tests.AcceptanceTests
{
    class TestClientTcpAsync
    {
        private Socket _server;

        private readonly Buffer _receiveBuffer;
        private readonly Buffer _sendBuffer;

        public TestClientTcpAsync()
        {
            _receiveBuffer = Buffer.New();
            _sendBuffer = Buffer.New();

            SockLib.TcpConnect("127.0.0.1", 14804, SocketCommunicationTypes.NonBlocking, TcpConnected);
            while (true)
            {
                //Here so the main thread runs continuously.
            }
        }

        private void TcpConnected(Socket socket)
        {
            _server = socket;
            SockLib.ReceiveMessage(_server, _receiveBuffer, SocketCommunicationTypes.NonBlocking, MessageReceived);
        }

        private void MessageReceived(int bytesReceived, EndPoint remoteEndpoint)
        {
            Console.WriteLine(string.Format("Received message from server. Size is {0}. Details are as follows: {1} (int)\n{2} (float)\n{3} (double)\n{4} (char)\n{5} (string)\n{6} (byte)", bytesReceived,
                                                                                                                                                                                Buffer.Get<int>(_receiveBuffer),
                                                                                                                                                                                Buffer.Get<float>(_receiveBuffer),
                                                                                                                                                                                Buffer.Get<double>(_receiveBuffer),
                                                                                                                                                                                Buffer.Get<char>(_receiveBuffer),
                                                                                                                                                                                Buffer.Get<string>(_receiveBuffer),
                                                                                                                                                                                Buffer.Get<byte>(_receiveBuffer)));
            SendTestResponse();
        }

        private void SendTestResponse()
        {
            Console.WriteLine("Sending response to server");
            Buffer.ClearBuffer(_sendBuffer);
            Buffer.Add(_sendBuffer, 20);
            Buffer.Add(_sendBuffer, 40.0F);
            Buffer.Add(_sendBuffer, 80.0);
            Buffer.Add(_sendBuffer, 'B');
            Buffer.Add(_sendBuffer, "Giggity gigity goo!!!");
            Buffer.Add(_sendBuffer, (byte)127);
            Buffer.FinalizeBuffer(_sendBuffer);

            SockLib.SendMessage(_server, _sendBuffer);
        }
    }
}
