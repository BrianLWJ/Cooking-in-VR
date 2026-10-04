using UnityEngine;
using UnityEngine.UIElements;

public class CustomButtonEvent : MonoBehaviour
{
    public Renderer renderer;
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button button1 = root.Q<Button>("Button1");
        Button button2 = root.Q<Button>("Button2");
        Button button3 = root.Q<Button>("Button3");

        button1.clicked += () => SetColor(Color.red);
        button2.clicked += () => SetColor(Color.green);
        button3.clicked += () => SetColor(Color.blue);
    }

    public void SetColor(Color color)
    {
        renderer.material.color = color;
    }
}
