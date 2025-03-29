using UnityEngine;

/**
    Para saber si el personaje esta en el piso o no
    Autor: Raúl Maldonado Pineda
*/
public class EstadoPersonaje : MonoBehaviour
{

    public static bool enPiso {get; private set; } // Propiedades

    void Start()
    {
        enPiso = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        enPiso = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        enPiso = false;
    }
}