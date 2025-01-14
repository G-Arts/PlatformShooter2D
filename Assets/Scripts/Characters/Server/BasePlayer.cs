using UnityEngine;

public interface IPlayerBase
{
    public void SetCharacter(PlayerMy player, string sessionId);
    public void DestroyCharacter();
    public GameObject GetGameObject();
}
