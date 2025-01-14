using Colyseus;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameRoomClient
{
    private const string serverAddress = "http://localhost:2567";

    private ColyseusClient _client;

    public GameRoomClient()
    {
        if (_client == null)
            _client = new ColyseusClient(serverAddress);
    }

    public void JoinOrCreate(Action<ColyseusRoom<PlayRoomState>> onConnected)
    {
        JoinOrCreateToServer(onConnected);
    }

    private async void JoinOrCreateToServer(Action<ColyseusRoom<PlayRoomState>> onConnected)
    {
        Dictionary<string, object> options = new Dictionary<string, object>();

        var room = await _client.JoinOrCreate<PlayRoomState>("my_room");

        onConnected(room);
    }
}
