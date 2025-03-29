using UnityEngine;
using UnityEngine.InputSystem;

public class MueveAccionChabelito : MonoBehaviour
{
    //InputAction
    [SerializeField] 
    private InputAction leftAction; // Tecla izquierda
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
   // Movimiento en las 4 direcciones
    [SerializeField]
   private InputAction moveAction;

   private float SPEED = 10.0f;
    void Start()
    {
        leftAction.Enable();
        moveAction.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       Vector2 move = moveAction.ReadValue<Vector2>();
       transform.position = (Vector2)transform.position + move * SPEED * Time.deltaTime;
    }
}
