using Riptide;
using Riptide.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _singleton;

    public static UIManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(UIManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    [SerializeField] private string stringMessage;
    [SerializeField] private int intMessage;
    [SerializeField] private float floatMessage;
    [SerializeField] private bool boolMessage;

    private void Awake()
    {
        Singleton = this;
    }

    [SerializeField] 

    public void SendStringMessageClicked()
    {
        ServerMessageManager.Singleton.SendStringMessagesToClient(ServerToClientId.stringMessage, stringMessage);
    }
    public void SendIntMessageClicked()
    {
        ServerMessageManager.Singleton.SendIntMessagesToClient(ServerToClientId.intMessage, intMessage);
    }
    public void SendFloatMessageClicked()
    {
        ServerMessageManager.Singleton.SendFloatMessagesToClient(ServerToClientId.floatMessage, floatMessage);
    }
    public void SendBoolMessageClicked()
    {
        ServerMessageManager.Singleton.SendBoolMessagesToClient(ServerToClientId.boolMessage, boolMessage);
    }
}
