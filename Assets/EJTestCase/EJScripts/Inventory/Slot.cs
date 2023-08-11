using UnityEngine;

public class Slot : MonoBehaviour
{
    bool isItem = false;
    public bool isItemIn { get { return isItem; } set { isItem = value; } }
}
