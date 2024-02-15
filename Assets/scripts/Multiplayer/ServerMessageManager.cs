using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ServerMessageManager : MonoBehaviour
{
    private static ServerMessageManager _singleton;

    public static ServerMessageManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(ServerMessageManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        Singleton = this;
    }
    public PlayerState playerState;

    #region SEND MESSAGE TYPES FROM SERVER TO CLIENT
    public void SendStringMessagesToClient(ServerToClientId messageId, string messageContent)
    {
        Message message = Message.Create(MessageSendMode.reliable, (ushort)messageId);
        message.AddString(messageContent);
        NetworkManager.Singleton.Server.SendToAll(message);
        Debug.Log($"{messageContent} was sent to the client");
    }

    public void SendFloatMessagesToClient(ServerToClientId messageId, float messageContent)
    {
        Message message = Message.Create(MessageSendMode.reliable, (ushort)messageId);
        message.AddFloat(messageContent);
        NetworkManager.Singleton.Server.SendToAll(message);
        Debug.Log($"{messageContent} was sent to the client");
    }

    public void SendIntMessagesToClient(ServerToClientId messageId, int messageContent)
    {
        Message message = Message.Create(MessageSendMode.reliable, (ushort)messageId);
        message.AddInt(messageContent);
        NetworkManager.Singleton.Server.SendToAll(message);
        Debug.Log($"{messageContent} was sent to the client");
    }

    public void SendBoolMessagesToClient(ServerToClientId messageId, bool messageContent)
    {
        Message message = Message.Create(MessageSendMode.reliable, (ushort)messageId);
        message.AddBool(messageContent);
        NetworkManager.Singleton.Server.SendToAll(message);
        Debug.Log($"{messageContent} was sent to the client");
    }
    #endregion

    #region HANDLE MESSAGES FROM CLIENT
    [MessageHandler((ushort)ClientToServerId.stringMessage)]
    private static void OnStringMessageReceived(ushort a, Message message)
    {
        string content = message.GetString();
        Debug.Log($"{content} was received by the client");
    }

    [MessageHandler((ushort)ClientToServerId.intMessage)]
    private static void OnInMessageReceived(ushort a, Message message)
    {
        int content = message.GetInt();
        Debug.Log($"{content} was received by the client");
    }

    [MessageHandler((ushort)ClientToServerId.floatMessage)]
    private static void OnFloatMessageReceived(ushort a, Message message)
    {
        float content = message.GetFloat();
        Debug.Log($"{content} was received by the client");
    }

    [MessageHandler((ushort)ClientToServerId.boolMessage)]
    private static void OnBoolMessageReceived(ushort a, Message message)
    {
        bool content = message.GetBool();
        Debug.Log($"{content} was received by the client");
        GameObject.Find("networkManager").GetComponent<ServerMessageManager>().playerState.PickupPhone(content);
    }
    #endregion

}
