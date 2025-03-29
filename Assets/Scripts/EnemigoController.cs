using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SaludPersonaje.instance.vidas--;
            HUDController.instance.ActualizarVidas();
            if (SaludPersonaje.instance.vidas == 0)
            {
                Destroy(collision.gameObject, 0.1f);
            }
        }
    }
}
