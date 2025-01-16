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

public partial class PlayerMy : Schema
{
#if UNITY_5_3_OR_NEWER
    [Preserve]
#endif
    public PlayerMy() { }
    [Type(0, "string")]
    public string playerName = default(string);

    [Type(1, "string")]
    public string sessionID = default(string);

    [Type(2, "int32")]
    public int weaponIndex = default(int);

    [Type(3, "ref", typeof(VectorS))]
    public VectorS position = new VectorS();

    [Type(4, "ref", typeof(VectorS))]
    public VectorS velocity = new VectorS();

    /*
	 * Support for individual property change callbacks below...
	 */

    protected event PropertyChangeHandler<string> __playerNameChange;
    public Action OnPlayerNameChange(PropertyChangeHandler<string> __handler, bool __immediate = true)
    {
        if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
        __callbacks.AddPropertyCallback(nameof(this.playerName));
        __playerNameChange += __handler;
        if (__immediate && this.playerName != default(string)) { __handler(this.playerName, default(string)); }
        return () => {
            __callbacks.RemovePropertyCallback(nameof(playerName));
            __playerNameChange -= __handler;
        };
    }

    protected event PropertyChangeHandler<string> __sessionIDChange;
    public Action OnSessionIDChange(PropertyChangeHandler<string> __handler, bool __immediate = true)
    {
        if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
        __callbacks.AddPropertyCallback(nameof(this.sessionID));
        __sessionIDChange += __handler;
        if (__immediate && this.sessionID != default(string)) { __handler(this.sessionID, default(string)); }
        return () => {
            __callbacks.RemovePropertyCallback(nameof(sessionID));
            __sessionIDChange -= __handler;
        };
    }

    protected event PropertyChangeHandler<int> __weaponIndexChange;
    public Action OnWeaponIndexChange(PropertyChangeHandler<int> __handler, bool __immediate = true)
    {
        if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
        __callbacks.AddPropertyCallback(nameof(this.weaponIndex));
        __weaponIndexChange += __handler;
        if (__immediate && this.weaponIndex != default(int)) { __handler(this.weaponIndex, default(int)); }
        return () => {
            __callbacks.RemovePropertyCallback(nameof(weaponIndex));
            __weaponIndexChange -= __handler;
        };
    }

    protected event PropertyChangeHandler<VectorS> __positionChange;
    public Action OnPositionChange(PropertyChangeHandler<VectorS> __handler, bool __immediate = true)
    {
        if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
        __callbacks.AddPropertyCallback(nameof(this.position));
        __positionChange += __handler;
        if (__immediate && this.position != null) { __handler(this.position, null); }
        return () => {
            __callbacks.RemovePropertyCallback(nameof(position));
            __positionChange -= __handler;
        };
    }

    protected event PropertyChangeHandler<VectorS> __velocityChange;
    public Action OnVelocityChange(PropertyChangeHandler<VectorS> __handler, bool __immediate = true)
    {
        if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
        __callbacks.AddPropertyCallback(nameof(this.velocity));
        __velocityChange += __handler;
        if (__immediate && this.velocity != null) { __handler(this.velocity, null); }
        return () => {
            __callbacks.RemovePropertyCallback(nameof(velocity));
            __velocityChange -= __handler;
        };
    }

    protected override void TriggerFieldChange(DataChange change)
    {
        switch (change.Field)
        {
            case nameof(playerName): __playerNameChange?.Invoke((string)change.Value, (string)change.PreviousValue); break;
            case nameof(sessionID): __sessionIDChange?.Invoke((string)change.Value, (string)change.PreviousValue); break;
            case nameof(weaponIndex): __weaponIndexChange?.Invoke((int)change.Value, (int)change.PreviousValue); break;
            case nameof(position): __positionChange?.Invoke((VectorS)change.Value, (VectorS)change.PreviousValue); break;
            case nameof(velocity): __velocityChange?.Invoke((VectorS)change.Value, (VectorS)change.PreviousValue); break;
            default: break;
        }
    }
}

