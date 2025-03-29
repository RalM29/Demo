using System;
using UnityEngine;
using UnityEngine.SceneManagement; //Escena
using UnityEngine.UIElements;
public class MenuControlador : MonoBehaviour
{
    private UIDocument menu; //Objeto en la escena

    private Button botonA; //Componente de la UI
    private Button botonB;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;
        botonA = root.Q<Button>("BotonJuegoA");
        botonB = root.Q<Button>("BotonJuegoB");
        //Callbacks
        botonA.RegisterCallback<ClickEvent, String>(IniciarJuego, "SampleScene");
        botonB.RegisterCallback<ClickEvent, String>(IniciarJuego, "EscenaMapa");
    }
    private void IniciarJuego(ClickEvent evt, String escena)
    {
        SceneManager.LoadScene(escena);
    }
}

    /*
    private void IniciarJuegoA(ClickEvent evt)
    {
        SceneManager.LoadScene("SampleScene");

    }
    private void IniciarJuegoB(ClickEvent evt)
    {
        SceneManager.LoadScene("EscenaMapa");

    }


}

    */