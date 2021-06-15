//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Net.Sockets;
//using System.IO;
//using System.Net;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Threading;


////Use for when client connected to the server
//public class ConnectedClient
//{
//    public TcpClient client;
//    public string clientName;
//    public int numClient;
//    public bool isLogin;
//    public ConnectedClient(TcpClient clientSocket)
//    {
//        clientName = "Guest";
//        client = clientSocket;
//        isLogin = false;
//    }
//}


//public class OldServer : MonoBehaviour
//{
//    public int port = 1828;
//    private List<ConnectedClient> clients;
//    private List<ConnectedClient> disconnectLists;

//    private TcpListener serverListener;
//    private bool serverStarted;


//    byte[] loginData = new byte[1024];
//    private NetworkStream stream;
//    private StreamReader reader;
//    private StreamWriter writer;
//    private TcpClient tcpClient;


//    void Start()
//    {
        
//        //clients = new List<ConnectedClient>();
//        //disconnectLists = new List<ConnectedClient>();

//        //Start Listen to Any Client 
//        try
//        {
//            serverListener = new TcpListener(IPAddress.Any, port);
//            serverListener.Start();

//            tcpClient = serverListener.AcceptTcpClient();
//            reader = new StreamReader(tcpClient.GetStream());
//            writer = new StreamWriter(tcpClient.GetStream());

            
//            serverStarted = true;

//            //while (serverStarted)
//            //{
//            //    Thread thread = new Thread(ClientProcess);
//            //    thread.Start();
//            //}
           

//            //StartListening();
//            Debug.Log("Server has started " + port.ToString());
//        }

//        catch (Exception e)
//        {
//            print("Socket Error : " + e.Message);
//        }
//    }

//    public void ClientProcess()
//    {
//        while (true)
//        {
//            CheckLoginData(stream);
            
//        }

//    }
//    public void CheckLoginData(NetworkStream stream)
//    {
//        IFormatter formatter = new BinaryFormatter();
//        formatter.Binder = new CustomizedBinder();
//        User loginUser = (User)formatter.Deserialize(stream);

//        if (loginUser.uname == "admin" && loginUser.password == "admin")
//        {
//            print("Deserialize Sukses");
//            //SendDataBack("%SUKSES");
//        }

//        else
//        {
//            print("Data ndak ada di databes");
//            //SendDataBack("%SUKSES");
//        }
//    }

//    //void Update()
//    //{
//    //    if (!serverStarted)
//    //        return;

//    //    foreach (ConnectedClient cli in clients.ToList())
//    //    {

//    //        if(IsConnected(cli.client))
//    //        {
//    //            stream = cli.client.GetStream();
//    //            CheckLoginData(stream);
//    //            //StartCoroutine(SuccessLogin());
//    //            //if (CheckLoginData(stream))
//    //            //{
//    //            //    writer.WriteLine("%SUKSES");
//    //            //    writer.Flush();
//    //            //    //cli.isLogin = true;
//    //            //}
//    //            //else
//    //            //{
//    //            //    writer.WriteLine("%GAGAL");
//    //            //    writer.Flush();
//    //            //    //cli.client.Close();
//    //            //}

//    //            //if (stream.DataAvailable)
//    //            //{
//    //            //    reader = new StreamReader(stream, true);
//    //            //    string data = reader.ReadLine();

//    //            //    if (data != null)
//    //            //    {
//    //            //        ClientData(cli, data);
//    //            //    }
//    //            //}
//    //        }
//    //        else
//    //        {
//    //            cli.client.Close();
//    //            disconnectLists.Add(cli);
//    //            continue;
//    //        }
//    //    }

//    //    for (int i = 0; i < disconnectLists.Count - 1; i++)
//    //    {
//    //        //Broadcast(disconnectLists[i].clientName + " has disconnected", clients);
//    //        clients.Remove(disconnectLists[i]);
//    //        disconnectLists.RemoveAt(i);
//    //    }
//    //}

//    //public IEnumerator SuccessLogin()
//    //{
//    //    if (!isLogin)
//    //    {
//    //        CheckLoginData(stream);
//    //        StartCoroutine(LoginSucc());
//    //        yield return null;
//    //    }
//    //}

//    //public IEnumerator LoginSucc()
//    //{
//    //    if (loginSuccess)
//    //    {
//    //        writer.WriteLine("%SUKSES");
//    //        writer.Flush();
//    //        yield return null;
//    //    }
//    //    else
//    //    {
//    //        print("you dont have done yet" + " & " + loginSuccess);
//    //        writer.WriteLine("%GAGAL");
//    //        writer.Flush();
//    //        yield return null;
//    //    }
//    //}



//    private void ClientData(ConnectedClient client, string data)
//    {
//        if (data.Contains("&NAME"))
//        {
//            client.clientName = data.Split('|')[1];
//            //Broadcast(client.clientName + " has connected!", clients);
//            return;
//        }
//        //Broadcast(client.clientName + " : " + data, clients);

//    }

//    private void SendDataBack(string data)
//    {
//        writer.WriteLine(data);
//        writer.Flush();
//    }

//    //private bool IsConnected(TcpClient cli)
//    //{
//    //    try
//    //    {
//    //        if (cli != null && cli.Client != null && cli.Client.Connected)
//    //        {
//    //            if (cli.Client.Poll(0, SelectMode.SelectRead))
//    //                return !(cli.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
//    //            return true;
//    //        }
//    //        else
//    //        {
//    //            return false;
//    //        }
//    //    }
//    //    catch
//    //    {
//    //        return false;
//    //    }
//    //}

//    //Start Listening to client asynchronously
//    private void StartListening()
//    {
//        serverListener.BeginAcceptTcpClient(AcceptClient, serverListener);
//    }

//    private void AcceptClient(IAsyncResult result)
//    {
//        TcpListener listener = (TcpListener)result.AsyncState;

//        //clients.Add(new ConnectedClient(listener.EndAcceptTcpClient(result)));
//        Thread thread = new Thread(ClientProcess);
//        thread.Start();

//        StartListening();

//        //Broadcast message to everyone who connected to server
//        //Broadcast("%NAME", new List<ConnectedClient>() { clients[clients.Count - 1] });
//    }

    
//    private void Broadcast(string data, List<ConnectedClient> cli)
//    {
//        foreach (ConnectedClient client in cli)
//        {
//            try
//            {
//                writer = new StreamWriter(client.client.GetStream());
//                writer.WriteLine(data);
//                writer.Flush();
//            }
//            catch (Exception e)
//            {
//                Debug.Log("Write Error : " + e.Message + " to " + client.clientName);
//            }
//        }
//    }
//}
