using UnityEngine;

public class Gameflow : MonoBehaviour
{
    public static int orderValue = 12101; //lowbun patty bacon cheese topbun
    public static int plateValue = 00000;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            orderValue = 00000;
            plateValue = 00000;
        }
    }
}
