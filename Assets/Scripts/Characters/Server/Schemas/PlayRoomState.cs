// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 2.0.36
// 

using Colyseus.Schema;
using Action = System.Action;
#if UNITY_5_3_OR_NEWER
using UnityEngine.Scripting;
#endif

public partial class PlayRoomState : Schema
{
#if UNITY_5_3_OR_NEWER
    [Preserve]
#endif
    public PlayRoomState() { }
    [Type(0, "string")]
    public string serverName = default(string);

    [Type(1, "map", typeof(MapSchema<PlayerMy>))]
    public MapSchema<PlayerMy> serverPlayers = new MapSchema<PlayerMy>();

    /*
	 * Support for individual property change callbacks below...
	 */

    protected event PropertyChangeHandler<string> __serverNameChange;
    public Action OnServerNameChange(PropertyChangeHandler<string> __handler, bool __immediate = true)
    {
        if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
        __callbacks.AddPropertyCallback(nameof(this.serverName));
        __serverNameChange += __handler;
        if (__immediate && this.serverName != default(string)) { __handler(this.serverName, default(string)); }
        return () => {
            __callbacks.RemovePropertyCallback(nameof(serverName));
            __serverNameChange -= __handler;
        };
    }

    protected event PropertyChangeHandler<MapSchema<PlayerMy>> __serverPlayersChange;
    public Action OnServerPlayersChange(PropertyChangeHandler<MapSchema<PlayerMy>> __handler, bool __immediate = true)
    {
        if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
        __callbacks.AddPropertyCallback(nameof(this.serverPlayers));
        __serverPlayersChange += __handler;
        if (__immediate && this.serverPlayers != null) { __handler(this.serverPlayers, null); }
        return () => {
            __callbacks.RemovePropertyCallback(nameof(serverPlayers));
            __serverPlayersChange -= __handler;
        };
    }

    protected override void TriggerFieldChange(DataChange change)
    {
        switch (change.Field)
        {
            case nameof(serverName): __serverNameChange?.Invoke((string)change.Value, (string)change.PreviousValue); break;
            case nameof(serverPlayers): __serverPlayersChange?.Invoke((MapSchema<PlayerMy>)change.Value, (MapSchema<PlayerMy>)change.PreviousValue); break;
            default: break;
        }
    }
}

