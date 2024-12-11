using UnityEngine;

public static class RotateToTarget
{
    public static void rotateToTarget(this Transform transform, Vector3 target)
    {
        Vector2 mouseWorldPosition = target;
        float angle = Mathf.Atan2(mouseWorldPosition.y - transform.position.y, mouseWorldPosition.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, angle));
        transform.rotation = targetRotation;
    }
}
