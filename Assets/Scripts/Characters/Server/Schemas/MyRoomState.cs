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

public partial class MyRoomState : Schema
{
#if UNITY_5_3_OR_NEWER
    [Preserve]
#endif
    public MyRoomState() { }
    [Type(0, "map", typeof(MapSchema<PlayerMy>))]
    public MapSchema<PlayerMy> players = new MapSchema<PlayerMy>();

    /*
	 * Support for individual property change callbacks below...
	 */

    protected event PropertyChangeHandler<MapSchema<PlayerMy>> __playersChange;
    public Action OnPlayersChange(PropertyChangeHandler<MapSchema<PlayerMy>> __handler, bool __immediate = true)
    {
        if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
        __callbacks.AddPropertyCallback(nameof(this.players));
        __playersChange += __handler;
        if (__immediate && this.players != null) { __handler(this.players, null); }
        return () => {
            __callbacks.RemovePropertyCallback(nameof(players));
            __playersChange -= __handler;
        };
    }

    protected override void TriggerFieldChange(DataChange change)
    {
        switch (change.Field)
        {
            case nameof(players): __playersChange?.Invoke((MapSchema<PlayerMy>)change.Value, (MapSchema<PlayerMy>)change.PreviousValue); break;
            default: break;
        }
    }
}

