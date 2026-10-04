    using UnityEngine;

public class ClickPlace : MonoBehaviour
{
    public Transform cloneObj;
    public int foodValue;

    private void OnMouseDown()
    {
        if(gameObject.name == "BunButtom")
        {
            Instantiate(cloneObj, new Vector3(0, 0.1f, 0), cloneObj.rotation);
        }
        if (gameObject.name == "BunTop")
        {
            Instantiate(cloneObj, new Vector3(0, 0.6f, 0), cloneObj.rotation);
        }
        if (gameObject.name == "Cheese")
        {
            Instantiate(cloneObj, new Vector3(0, 0.65f, -0.5f), cloneObj.rotation);
        }
        if (gameObject.name == "Bacon") //BACON NORMALLY HAS 2 ON BURGERS
        {
            Instantiate(cloneObj, new Vector3(-0.1f, 0.10f, 0), cloneObj.rotation);
            Instantiate(cloneObj, new Vector3( 0.1f, 0.10f, 0), cloneObj.rotation);
        }
        
        Gameflow.plateValue += foodValue;
        Debug.Log(Gameflow.plateValue+ "" + Gameflow.orderValue);
    }
}
