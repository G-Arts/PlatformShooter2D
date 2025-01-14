using Colyseus;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[DefaultExecutionOrder(-100)]
public class ColyseusManager : MonoBehaviour
{
    public static ColyseusManager instance;

    private ColyseusRoom<PlayRoomState> roomClient;

    private Dictionary<string, IPlayerBase> characterObjects = new Dictionary<string, IPlayerBase>();

    public Dictionary<string, IPlayerBase> characters => characterObjects;
    public IPlayerBase activeCharacter => characterObjects[roomClient.SessionId];

    private Queue<string> characterJoinQueue = new();

    public event Action onPlayerSpawned = delegate { };

    private Action OnPlayerJoinAction;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        var client = new GameRoomClient();

        client.JoinOrCreate(onConnected);
    }


    private void OnApplicationQuit()
    {
        roomClient.Leave(true);
    }

    private void onConnected(ColyseusRoom<PlayRoomState> room)
    {
        roomClient = room;

        OnPlayerJoinAction = roomClient.State.OnChange(OnActivePlayerJoin);
    }

    private void OnActivePlayerJoin()
    {
        if (OnPlayerJoinAction != null)
            OnPlayerJoinAction();

        foreach (string item in roomClient.State.serverPlayers.Keys)
        {
            Debug.Log("char: " + roomClient.State.serverPlayers[item].playerName);
            OnServerPlayersAdd(item, roomClient.State.serverPlayers[item]);
        }

        roomClient.State.serverPlayers.OnAdd(OnServerPlayersAdd);
        roomClient.State.serverPlayers.OnRemove(OnServerPlayersRemove);

    }


    private void OnServerPlayersRemove(string key, PlayerMy value)
    {
        if (characterObjects.ContainsKey(key))
        {
            characterObjects[key].DestroyCharacter();
        }
    }

    private void OnServerPlayersAdd(string key, PlayerMy value)
    {
        if (characterObjects.ContainsKey(key)) return;

        bool isClient = key.Equals(roomClient.SessionId);

        SpawnCharacter(isClient, value, key);
    }

    private void SpawnCharacter(bool IsClient, PlayerMy data, string sessionId)
    {
        if (characterJoinQueue.Contains(sessionId)) return;

        characterJoinQueue.Enqueue(sessionId);

        Addressables.LoadAssetAsync<GameObject>(IsClient ? "ClientPlayer" : "ServerPlayer").Completed += (cb) =>
        {
            if (cb.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                var obj = Instantiate(cb.Result);

                obj.GetComponent<IPlayerBase>().SetCharacter(data, sessionId);

                characterObjects.Add(sessionId, obj.GetComponent<IPlayerBase>());
                characterJoinQueue.Dequeue();

                if (IsClient)
                {
                    onPlayerSpawned.Invoke();
                }
            }
        };
    }

    public void ServerMessageSend(string type, string message)
    {
        roomClient.Send(type, message);
    }
}
