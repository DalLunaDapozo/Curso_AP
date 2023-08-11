using UnityEngine;

public class _MovimientoTopDown : MonoBehaviour
{
    [SerializeField] private float velocidad_de_movimiento;

    private Rigidbody fisicas;
    private Vector3 ejes;

    void Start()
    {
        fisicas = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ejes = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        MovimientoBasico();
    }

    private void MovimientoBasico()
    {
        fisicas.velocity = ejes * velocidad_de_movimiento * Time.deltaTime;
    }
}
