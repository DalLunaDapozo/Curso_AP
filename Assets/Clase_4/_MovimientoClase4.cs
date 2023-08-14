using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad_de_movimiento = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verificar si el personaje está en el suelo
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Ground"));

        // Movimiento horizontal
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * velocidad_de_movimiento, rb.velocity.y);

        // Salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}