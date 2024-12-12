using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    [SerializeField] private Vector2 parallaxEffectMultiplier;

    private Transform cameraTransfrom;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransfrom = Camera.main.transform;
        lastCameraPosition = cameraTransfrom.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransfrom.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y, deltaMovement.z);
        lastCameraPosition = cameraTransfrom.position;
    }
}
