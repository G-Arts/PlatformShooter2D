using UnityEngine;

public class CharacterAlignment : MonoBehaviour
{
    public Transform characterBody; // Karakterin hizalanacak k�sm� (�rne�in Sprite Renderer'�n oldu�u obje)
    public float rayLength = 1f; // Raycast'in uzunlu�u
    public LayerMask groundLayer;
    public BoxCollider2D collider2d;
    public Rigidbody2D rg2d;

    private void Start()
    {
        rayLength = collider2d.bounds.size.y / 2 + 1;
    }

    void Update()
    {
        AlignToGround();
    }

    void AlignToGround()
    {
        // Karakterin alt�na do�ru bir ray g�nder
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        if (hit.collider != null)
        {
            // Zeminin normalini al
            Vector2 groundNormal = hit.normal;

            // Normal vekt�r�n�n a��s�n� hesapla
            float angle = Mathf.Atan2(groundNormal.y, groundNormal.x) * Mathf.Rad2Deg;

            // Karakteri bu a��ya g�re d�nd�r
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
        else
        {
            // E�er karakter havadaysa varsay�lan hizalama
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
