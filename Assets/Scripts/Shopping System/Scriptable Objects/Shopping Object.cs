using UnityEngine;

[CreateAssetMenu(fileName = "ShoppingObject", menuName = "Scriptable Objects/ShoppingObject")]
public class ShoppingObject : ScriptableObject
{
    [Header("Object Info")]
    public float itemPrice;
    public float itemWeight;
}
