using Colyseus.Schema;
using NUnit.Framework;
using System;
using UnityEngine;

public class ServerPlayer : Character, IPlayerBase
{
    [SerializeField] WeaponManager _weaponManager;
    [SerializeField] Animator _animator;
    [SerializeField] Transform weaponHolder;
    [SerializeField] float sprintSpeed = 12f;
    private float horizontal;
    private float vertical;

    PlayerMy serverPlayer;
    private Rigidbody2D rigid;

    private Action playerPositionActionX;
    private Action playerPositionActionY;
    private Action weaponChangeAction;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        /*
        horizontal = Input.GetAxisRaw("Horizontal") * speed;
        Move(horizontal);
        Jump();
        Attack();
        _animator.SetFloat("speed", Mathf.Abs(horizontal));

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.SpawnWeaponCollector();
        }

        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded() && speed != sprintSpeed)
        {
            speed = sprintSpeed;
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

        if (rb.linearVelocityY < 0)
        {
            rb.gravityScale = 8;
        }

        else
        {
            rb.gravityScale = 4;
        }
        */


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
        if (Input.GetButton("Fire1") && equippedWeapon != null)
        {
            equippedWeapon.Fire(gameObject);
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

    public void SetCharacter(PlayerMy player, string sessionId)
    {
        if(player == null)
        {
            Destroy(gameObject);
            return;
        }

        serverPlayer = player;

        playerPositionActionX = serverPlayer.position.OnXChange(OnPositionChange);
        playerPositionActionY = serverPlayer.position.OnXChange(OnPositionChange);

        weaponChangeAction = serverPlayer.OnWeaponIndexChange(weaponChange);


    }

    private void weaponChange(int currentValue, int previousValue)
    {
        this._weaponManager.EquipWeaponWIndex(currentValue);
    }

    private void FixedUpdate()
    {
        rigid.linearVelocity = new Vector2(serverPlayer.velocity.x, serverPlayer.velocity.y);
        _animator.SetFloat("speed", Mathf.Abs(rigid.linearVelocityX));

        if (rigid.linearVelocityX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Sola bak
        }
        else if(rigid.linearVelocityX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Saða bak
        }
    }

    public void OnPositionChange(float currentValue, float previousValue)
    {
        rigid.position = new Vector2(serverPlayer.position.x, serverPlayer.position.y);
    }

    public void DestroyCharacter()
    {
        if (playerPositionActionX != null) playerPositionActionX();

        if (playerPositionActionY != null) playerPositionActionY();

        //Destroy(gameObject);
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
