using UnityEngine;

public class _MovimientoTopDown : MonoBehaviour
{
    [SerializeField] private float velocidad_de_movimiento;

    private Rigidbody fisicas;
    private float eje_x;

    void Start()
    {
        fisicas = GetComponent<Rigidbody>();
    }

    void Update()
    {
        eje_x = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        MovimientoBasico();
    }

    private void MovimientoBasico()
    {
        fisicas.velocity = new Vector3(eje_x * velocidad_de_movimiento * Time.deltaTime, fisicas.velocity.y, fisicas.velocity.z);
    }
}
