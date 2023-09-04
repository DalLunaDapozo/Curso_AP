using UnityEngine;

public class _MovimientoClase4: MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public float eje_mov;

    public float maxJumpDuration = 1f;

    private float jumpTime;

    private Rigidbody rb;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Verificar si el personaje está en el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f);


        // Movimiento horizontal
        eje_mov = Input.GetAxis("Horizontal");
       

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpTime = Time.time; // Iniciar el contador de tiempo de salto
        }

        // Salto
        if (Input.GetButton("Jump") && !isGrounded && (Time.time - jumpTime) < maxJumpDuration)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Force);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(eje_mov, rb.velocity.y, rb.velocity.z) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
}