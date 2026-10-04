using UnityEngine;

public class MeatCon : MonoBehaviour
{
    public Transform cloneObj;

    private void OnMouseDown()
    {
        if(gameObject.name == "Cutlet")
        {
            Instantiate(cloneObj, new Vector3(-3, 0.1f, 0), cloneObj.rotation);
        }
    }
}
