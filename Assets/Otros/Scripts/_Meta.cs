using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//ESTE SCRIPT HACE MAS COSAS DE LAS QUE DEBERIA Y NO ES LO IDEAL PARA JUEGOS "SERIOS"
//PORQUE ESTAMOS CONTROLANDO MAS ELEMENTOS DE LOS QUE DEBERIA
//IDEALMENTE USARIAMOS UN CONTROLADOR PARA HACER TODO LO QUE HACE ESTE SCRIPT

public class _Meta : MonoBehaviour
{

    //VARIABLE DE LAS LETRAS QUE APARECEN EN PANTALLA 
    [SerializeField] private GameObject cartel_victoria;
    //ESTA IMAGEN EN NEGRO LA VAMOS A UTILIZAR PARA HACER QUE SE VEA MAS BONITO LA TRANSICION
    [SerializeField] private Image fondo_negro;

    //EL AUTO DEL JUGADOR, TENEMOS QUE REFERENCIARLO PARA PODER VOLVERLO A SU LUGAR DE ORIGEN
    [SerializeField] private GameObject auto;
    //POSICION A LA QUE VA A VOLVER EL JUGADOR
    [SerializeField] private Transform posicion_inicial;

    private void Start()
    {
        //try Y catch SON INTENTOS DE LOGRAR ALGO DENTRO DEL CÓDIGO QUE PUEDE SUPONER UN ERROR SI SALEN MAL, CON ESTE MÉTODO EVITAMOS QUE EL JUEGO SE ROMPA
        //BASICAMENTE ESTAMOS REFERENCIADO ALGUNOS OBJETOS EN try, Y SI FALLA, EN catch VAMOS A TIRAR UN ERROR PARA SABER QUE ES LO QUE ANDA MAL
        //ESTO ES BASTANTE INNECESARIO EN ESTA SITUACIÓN SIMPLE, PERO ES UNA BUENA PRÁCTICA TENER EL MAYOR CONTROL POSIBLE DE NUESTROS CÓDIGOS
        
        try
        {
            auto = GameObject.Find("Auto");
            posicion_inicial = GameObject.Find("PosicionInicio").transform;
        }
        catch
        {
            Debug.LogError("OBJETOS NO ENCONTRADOS");
        }
        
    }
    //FUNCION DE COLISION, SI ESTRAMOS EN CONTACTO CON UN OBJETO QUE TENGA EL TAG PLAYER VAMOS A INICIAR UN RUTINA
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SecuenciaDeVictoria());
        }
    }

    //LAS CORRUTINAS EN C# SE LLAMAN IENUMERATOR, SE PUEDEN LLAMAR SI TENEMOS LA BIBLIOTECA DE LA LINEA 3 (System.Collections)
    //LAS CORRUTINAS SON FUNCIONES QUE TIENEN LA CAPACIDAD DE PAUSAR UN TIEMPO ESTABLECIDO EN VEZ DE HACER TODO A LA VEZ ESTO NOS PERMITE
    //CREAR SECUENCIAS MUY ELABORADAS 

    //DECLARAMOS LA CORRUTINA CON UN IENUMERATOR
    private IEnumerator SecuenciaDeVictoria()
    {
        //ACTIVAMOS EL CARTEL DE VICTORIA
        cartel_victoria.gameObject.SetActive(true);

        //CON YIELD, PODEMOS DETENER LA CORRUTINA UN TIEMPO ESTABLECIDO, EN ESTE CASO ESPERAMOS 2 SEGUNDOS GRACIAS AL
        //WAITFORSECONDS(2f)
        yield return new WaitForSeconds(2f);

        //CREAMOS UNA VARIABLE LOCAL PARA ALMACENAR TIEMPO
        float time = 0;
        //Y OTRA PARA LA DURACION DE LA TRANSICION
        float duration = 1f;

        //ABRIMOS UN WHILE, EN UNITY, LOS WHILE NO SE USAN EN CASI NINGUNA OCASION, EXCEPTO DENTRO DE CORRUTINAS :D 
        //BASICAMENTE ABRIMOS UN BUCLE QUE SE REPITE HASTA QUE NO SE CUMPLA LA CONDICION QUE ESTÁ ENTRE PARÉNTESIS
        //EN ESTE CASO: QUE EL TIEMPO QUE TRANSCURRE SEA MENOR A LA DURACION (QUE EN ESTE CASO VALOR 1)
        while (time < duration)
        {
            //ACA MOVEMOS LA POSICION EN X DE LA PANTALLA EN NEGRO Y USAMOS UN LERP QUE ES UNA FUNCION MATEMATICA PARA TRANSPOLAR DOS VALORES DE FORMA GRADUAL
            //ESO LE DA EL EFECTO DE DESLIZAMIENTO A LA PANTALLA
            fondo_negro.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(800, 0, time / duration), 0, 0);
            //CADA FRAME LE SUMAMOS UN DELTATIME A NUESTRA VARIABLE DE TIEMPO PARA PODER ROMPER EL LOOP EN ALGUN MOMENTO
            time += Time.deltaTime;

            yield return null;
        }

        //CUANDO SE TAPA LA PANTALLA, DESACTIVAMOS LAS LETRAS
        cartel_victoria.gameObject.SetActive(false);
        //Y MOVEMOS EL AUTO A SU POSICION INICIAL
        auto.transform.position = posicion_inicial.position;
        //REINICIAMOS LA VARIABLE TIME PARA REUTILIZARLA
        time = 0;
        //HACEMOS LO MISMO QUE ANTES PERO A LA INVERSA PARA VOLVER LA PANTALLA A LA NORMALIDAD
        while (time < duration)
        {
            fondo_negro.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(0, 800, time / duration), 0, 0);
            time += Time.deltaTime;

            yield return null;
        }
    }

}
