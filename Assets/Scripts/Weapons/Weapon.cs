using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float lastFireTime = 0;

    [SerializeField] protected float _weaponRange = 20f;
    [SerializeField] protected float _fireRate = 0.1f;
    [SerializeField] protected float _weaponDamage = 10f;
    [SerializeField] protected float _weaponKnockBack = 3f;

    public abstract void Fire();

    protected void flip()
    {
        if(Mathf.Abs(transform.rotation.z) > 0.5f)
        {
            transform.Rotate(new Vector3(180,0,0));
        }
    }

    protected void FollowMouse()
    {
        transform.rotateToTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
