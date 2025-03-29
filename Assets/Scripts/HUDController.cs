using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;
    private VisualElement vida_1;
    private VisualElement vida_2;
    private VisualElement vida_3;
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        vida_1 = root.Q<VisualElement>("Vida_1");
        vida_2 = root.Q<VisualElement>("Vida_2");
        vida_3 = root.Q<VisualElement>("Vida_3");
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void ActualizarVidas()
    {
        int vidas = SaludPersonaje.instance.vidas;
        if (vidas == 0)
        {
            vida_1.style.visibility = Visibility.Hidden;
        }
        else if (vidas == 1)
        {
            vida_2.style.visibility = Visibility.Hidden;
        }
        else if (vidas == 2)
        {
            vida_3.style.visibility = Visibility.Hidden;
        }
    }
}
