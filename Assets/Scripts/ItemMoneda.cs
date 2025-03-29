using UnityEngine;

public class ItemMoneda : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Prende explosion
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //Apaga la moneda
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //Destruye la moneda y el objeto
            Destroy(gameObject, 0.3f);
        }
    }
}
