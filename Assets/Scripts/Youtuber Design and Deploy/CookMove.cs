using System.Collections;
using UnityEngine;

public class CookMove : MonoBehaviour
{
    private int foodValue = 0;
    private MeshRenderer meatMat;
    private bool stillCooking = true;

    void Start()
    {
        meatMat = GetComponent<MeshRenderer>();
        StartCoroutine(CookTimer());
    }

    private void OnMouseDown()
    {
        GetComponent<Transform>().position = new Vector3(0, .66f, 0);
        Gameflow.plateValue += foodValue;
        stillCooking = false;
    }

    IEnumerator CookTimer()
    {
        yield return new WaitForSeconds(10);
        foodValue = 1000;
        if(stillCooking == true)
        {
            meatMat.material.color = new Color(0.3f, 0.3f, 0.3f);   //14:50
        }
    }
}
