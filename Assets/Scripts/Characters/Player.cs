using System.Globalization;
using UnityEngine;

public class Player : Character, IPlayerBase
{
    [SerializeField] WeaponManager _weaponManager;
    [SerializeField] Animator _animator;
    [SerializeField] Transform weaponHolder;
    [SerializeField] float sprintSpeed = 12f;
    private float horizontal;
    private float vertical;

    PlayerMy clientPlayer;

    private float serverPositionSendTime = 0;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

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

        if(rb.linearVelocityY < 0)
        {
            rb.gravityScale = 8;
        }

        else
        {
            rb.gravityScale = 4;
        }

        serverPositionSendTime += Time.deltaTime;

        if (serverPositionSendTime > .1f)
        {
            ColyseusManager.instance.ServerMessageSend("playerPosition", "{\"x\":" + transform.position.x.ToString(CultureInfo.InvariantCulture) + ",\"y\":" + transform.position.y.ToString(CultureInfo.InvariantCulture) + "}");
            serverPositionSendTime = 0;
            
        }

        ColyseusManager.instance.ServerMessageSend("playerVelocity", "{\"x\":" + rigid.linearVelocity.x.ToString(CultureInfo.InvariantCulture) + ",\"y\":" + rigid.linearVelocity.y.ToString(CultureInfo.InvariantCulture) + "}");
        ColyseusManager.instance.ServerMessageSend("weaponRotX", this._weaponManager.getWeaponRot().x.ToString());
        ColyseusManager.instance.ServerMessageSend("weaponRotY", this._weaponManager.getWeaponRot().y.ToString());
        ColyseusManager.instance.ServerMessageSend("weaponRotZ", this._weaponManager.getWeaponRot().z.ToString());
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
        clientPlayer = player;
    }

    public void DestroyCharacter()
    {
        Destroy(gameObject);
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
