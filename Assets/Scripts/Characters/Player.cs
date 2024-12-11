using NUnit.Framework;
using UnityEngine;

public class Player : Character
{
    [SerializeField] WeaponManager _weaponManager;
    [SerializeField] Animator _animator;
    [SerializeField] Transform weaponHolder;
    private float horizontal;
    private float vertical;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed;
        Move(horizontal);
        Jump();
        Attack();
        _animator.SetFloat("speed", Mathf.Abs(horizontal));

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.SpawnWeaponCollector();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 12;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 8;
        }

        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Sola bak
            weaponHolder.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Saða bak
            weaponHolder.localScale = new Vector3(1, 1, 1);
        }
    }


    public override void Move(float horizontal)
    {
        rb.linearVelocity = new Vector2(horizontal, rb.linearVelocity.y);
    }

    public override void Jump()
    {
        if (Input.GetButton("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    public override void Attack()
    {
        if (Input.GetMouseButton(0) && equippedWeapon != null)
        {
            equippedWeapon.Fire();
        }
    }

    protected override bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponCollecter"))
        {
            GameObject collecter = collision.gameObject;
            WeaponCollecter collecterScript = collecter.GetComponent<WeaponCollecter>();
            if (collecterScript == null) return;
            WeaponData _weaponData = collecterScript.getWeaponData();
            _weaponManager.SelectWeaponByName(_weaponData.weaponName);
            Destroy(collecter);
        }
    }
}
