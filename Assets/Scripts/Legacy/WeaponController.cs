using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class WeaponController : MonoBehaviour
{
    private bool flip = false;

    private float lastFireTime = 0f;

    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _bulletTrail;
    [SerializeField] private float _weaponRange = 100f;
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private float _weaponDamage = 10f;
    [SerializeField] private float _weaponKnockBack = 3f;

    void Update()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouseWorldPosition.y - transform.position.y, mouseWorldPosition.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = targetRotation;

        if (Input.GetMouseButton(0) && Time.time - lastFireTime > _fireRate)
        {
            FireBullet();
            lastFireTime = Time.time;
        }
    }

    public void Flip()
    {
        flip = !flip;
    }

    private void FireBullet()
    {
        var hit = Physics2D.Raycast(
            _gunPoint.position,
            transform.right,
            _weaponRange);

        var trail = Instantiate(
            _bulletTrail,
            _gunPoint.position,
            transform.rotation
            );

        var trailScript = trail.GetComponent<BulletTrail>();

        if(hit.collider != null)
        {
            trailScript.SetTargetPosition(hit.point);
            var target = hit.collider.GetComponent<Target>();
            target?.getDamage(_weaponDamage);

            Vector3 difference = (new Vector3(hit.point.x, hit.point.y, -1) - transform.position).normalized;
            Vector3 force = difference * _weaponKnockBack;
            target?.knockBack(force);
        }
        else
        {
            var endPosition = _gunPoint.position + transform.right * _weaponRange;
            trailScript.SetTargetPosition(endPosition);
        }
    }
}
