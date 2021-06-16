using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Threading;

public class Client : MonoBehaviour
{
    public GameObject chatContainer;
    public GameObject messagePrefab;
    public GameObject alertPanel;
    public TextMeshProUGUI notifText;
    public GameObject ChatBox;
    public GameObject ChatMsg;
    public GameObject loginPanel;
    public GameObject gamePanel;
    public TextMeshProUGUI modelCar_UI, speedCar_UI, hp_UI, kill_UI;
    public int localTime;

    [SerializeField] private string clientName;
    [SerializeField] private string clientPw;

    private bool socketConnected;
    private bool loginSuccess;
    private bool dataAvailable;
    private bool buttonSuccessClicked;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    private string textBuffer;
    public int clientID;

    void Awake()
    {
        UnityThread.initUnityThread();
    }
    public void Start()
    {
        gamePanel.SetActive(false);
        alertPanel.SetActive(false);
        ConnectToServer();
        StartCoroutine(LoginChecker());
        localTime = 0;
    }

    IEnumerator LoginChecker()
    {
        yield return new WaitUntil((() => loginSuccess));
    }

    public void ConnectToServer()
    {
        if (socketConnected)
            return;

        string hostValue = "127.0.0.1";
        int portValue = 1828;

        var hostInput = GameObject.Find("IP").GetComponent<TMP_InputField>().text;
        if (hostInput != null)
            hostValue = hostInput;

        var portInput = GameObject.Find("Port").GetComponent<TMP_InputField>().text;
        if (portInput != null)
            portInput = portValue.ToString();

        try
        {
            socket = new TcpClient(hostValue, portValue);
            stream = socket.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);

            Thread thread = new Thread(g => ReceiveData((TcpClient)g));
            thread.Start(socket);
            socketConnected = true;

        }
        catch (Exception e)
        {
            Debug.Log("Socket error : " + e.Message);
        }
    }

    void Update()
    {
        if (stream.DataAvailable && !loginSuccess)
        {
            string data = reader.ReadLine();
            if (data != null)
            {
                OnIncomingData(data);
            }
        }
        if (loginSuccess && dataAvailable)
        {
            GameObject message = Instantiate(messagePrefab, chatContainer.transform) as GameObject;
            message.GetComponentInChildren<TMP_Text>().text = textBuffer;
        }
    }

    public void ReceiveData(TcpClient client)
    {
        while (true)
        {
            if (loginSuccess)
            {
                string data = reader.ReadLine();
                
                if (data == "CD")
                {
                    NetworkStream ns = client.GetStream();
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Binder = new CustomizedBinder();
                    Car carData = (Car)formatter.Deserialize(stream);
                    UnityThread.executeInUpdate(() =>
                    {
                        ShowCarData(carData);
                        ShowBroadcast("Car Data : " + carData.model + " " + carData.speed);
                    });

                }

                else if (data == "2")
                {
                    NetworkStream ns = client.GetStream();
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Binder = new CustomizedBinder();
                    PlayerAttribute playerAttribute = (PlayerAttribute)formatter.Deserialize(stream);

                    UnityThread.executeInUpdate(() =>
                    {
                        ShowPlayerData(playerAttribute);
                        print("Sekarang ada di t" + localTime);
                        ShowBroadcast("Player Data : " + playerAttribute.hp + " " + playerAttribute.kill);
                        
                    });
                }

                else if (data == "H")
                {
                    NetworkStream ns = client.GetStream();
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Binder = new CustomizedBinder();
                    PlayerAction newAction = (PlayerAction)formatter.Deserialize(stream);
                    newAction.counter += 1;
                    UnityThread.executeInUpdate(() =>
                    {
                        localTime = newAction.counter;
                        print("Sekarang ada di t" + localTime);
                        ShowBroadcast(newAction.command);

                    });
                }

                else if (data == "R")
                {
                    NetworkStream ns = client.GetStream();
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Binder = new CustomizedBinder();
                    PlayerAction newAction = (PlayerAction)formatter.Deserialize(stream);
                    newAction.counter += 1;
                    UnityThread.executeInUpdate(() =>
                    {
                        localTime = newAction.counter;
                        print("Sekarang ada di t" + localTime);
                        ShowBroadcast(newAction.command);

                    });
                }

                else if (data == "N")
                {
                    NetworkStream ns = client.GetStream();
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Binder = new CustomizedBinder();
                    PlayerAction newAction = (PlayerAction)formatter.Deserialize(stream);
                    newAction.counter += 1;
                    UnityThread.executeInUpdate(() =>
                    {
                        localTime = newAction.counter;
                        ShowBroadcast(newAction.command);

                    });
                }
            }
        }
    }

    public void ShowBroadcast(string data)
    {
        GameObject message = Instantiate(messagePrefab, chatContainer.transform) as GameObject;
        message.GetComponentInChildren<TMP_Text>().text = data;

        if (buttonSuccessClicked)
        {
            Canvas.ForceUpdateCanvases();
            GameObject.Find("Chat List").GetComponent<ScrollRect>().velocity = new Vector2(0f, 1000f);
            Canvas.ForceUpdateCanvases();
        }
    }

    public void ShowCarData(Car carData)
    {
        modelCar_UI.text = carData.model.ToString();
        speedCar_UI.text = carData.speed.ToString();
    }

    public void ShowPlayerData(PlayerAttribute playerAttribute)
    {
        hp_UI.text = playerAttribute.hp.ToString();
        kill_UI.text = playerAttribute.kill.ToString();
    }

    public void HitButton()
    {
        localTime += 1;
        string command = "H";
        int timeStamp = localTime;
        SendHitAction(command, timeStamp);
        Debug.Log("Send Hit Action at t" + localTime);
    }

    public void SendHitAction(string cmd, int timeStamp)
    {
        writer.WriteLine("H");
        writer.Flush();

        PlayerAction newAction = new PlayerAction(cmd, timeStamp,clientID);
        IFormatter formatter = new BinaryFormatter();
        stream = socket.GetStream();
        formatter.Binder = new CustomizedBinder();
        formatter.Serialize(stream, newAction);
    }

    public void RunButton()
    {
        localTime += 1;
        string command = "R";
        int timeStamp = localTime;
        SendRunAction(command, timeStamp);
        Debug.Log("Send Run Action at t" + localTime);
    }

    public void SendRunAction(string command, int timeStamp)
    {
        writer.WriteLine("R");
        writer.Flush();

        PlayerAction newAction = new PlayerAction(command, timeStamp, clientID);
        IFormatter formatter = new BinaryFormatter();
        stream = socket.GetStream();
        formatter.Binder = new CustomizedBinder();
        formatter.Serialize(stream, newAction);
    }

    public void NitroButton()
    {
        localTime += 1;
        string command = "N";
        int timeStamp = localTime;
        SendNitroAction(command, timeStamp);
        Debug.Log("Send Nitro Action at t" + localTime);
    }

    public void SendNitroAction(string command, int timeStamp)
    {
        writer.WriteLine("N");
        writer.Flush();

        PlayerAction newAction = new PlayerAction(command, timeStamp, clientID);
        IFormatter formatter = new BinaryFormatter();
        stream = socket.GetStream();
        formatter.Binder = new CustomizedBinder();
        formatter.Serialize(stream, newAction);
    }

    public void SendCarData()
    {
        try
        {
            writer.WriteLine("CD");
            writer.Flush();

            int carModel = 0;
            float speedType = 0f;

            Car carData;
            carData = new Car(carModel, speedType);
            IFormatter formatter = new BinaryFormatter();
            stream = socket.GetStream();
            formatter.Binder = new CustomizedBinder();
            formatter.Serialize(stream, carData);

        }
        catch (Exception e)
        {
            Debug.Log("Error : " + e.Message);
        }
    }

    public void SendPlayerAttrib()
    {
        try
        {
            writer.WriteLine("2");
            writer.Flush();

            int hp_player = 0;
            int kill_player = 0;

            PlayerAttribute playerAttribute;
            playerAttribute = new PlayerAttribute(hp_player, kill_player);
            IFormatter formatter = new BinaryFormatter();
            stream = socket.GetStream();
            formatter.Binder = new CustomizedBinder();
            formatter.Serialize(stream, playerAttribute);
        }
        catch (Exception e)
        {
            Debug.Log("Error : " + e.Message);
        }
    }

    public void GetData()
    {
        clientName = GameObject.Find("Username").GetComponent<TMP_InputField>().text;
        clientPw = GameObject.Find("Password").GetComponent<TMP_InputField>().text;

        if (socketConnected)
        {
            SendLoginData(clientName, clientPw);
        }
    }

    public void SendLoginData(string uname, string pw)
    {
        User userLogin = new User(uname, pw);
        IFormatter formatter = new BinaryFormatter();
        stream = socket.GetStream();
        formatter.Binder = new CustomizedBinder();
        formatter.Serialize(stream, userLogin);
    }

    private void OnIncomingData(string data)
    {
        if (data != null)
        {
            print(data);
        }

        if (data == "%SUKSES")
        {
            clientID = Convert.ToInt32(reader.ReadLine());

            writer.WriteLine(clientID.ToString());
            writer.Flush();

            print("Client : Sukses");
            notifText.text = "Login Sukses";
            alertPanel.SetActive(true);
            loginSuccess = true;
            return;
        }

        if (data == "%GAGAL")
        {
            print("Client : GAGAL");
            notifText.text = "Login Gagal";
            alertPanel.SetActive(true);
            return;
        }
    }

    private void SendData(string data)
    {
        if (!socketConnected)
            return;

        writer.WriteLine(data);
        writer.Flush();
    }

    public void ButtonSucces()
    {
        if (loginSuccess)
        {
            //ChatBox.SetActive(true);
            ChatMsg.SetActive(true);
            gamePanel.SetActive(true);
            buttonSuccessClicked = true;
            loginPanel.SetActive(false);
        }
        else
        {
            ChatBox.SetActive(false);
            ChatMsg.SetActive(false);
            buttonSuccessClicked = false;
            loginPanel.SetActive(true);
        }
    }

    public void SendButton()
    {
        string message = GameObject.Find("SendInput").GetComponent<TMP_InputField>().text;
        SendData(message);
    }

    public void ConnectButton()
    {
        string _clientName = clientName;
        string _clientPw = clientPw;

        SendLoginData(_clientName, _clientPw);
    }

    private void CloseSocket()
    {
        if (!socketConnected)
            return;

        writer.Close();
        reader.Close();
        socket.Close();
        socketConnected = false;
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    private void OnDisable()
    {
        CloseSocket();
    }
}
