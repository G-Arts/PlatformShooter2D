using Unity.Cinemachine;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    private CinemachineCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineCamera>();
        ColyseusManager.instance.onPlayerSpawned += OnPlayerSpawned;
    }

    private void OnPlayerSpawned()
    {
        virtualCamera.Follow = ColyseusManager.instance.activeCharacter.GetGameObject().transform;
    }
}
