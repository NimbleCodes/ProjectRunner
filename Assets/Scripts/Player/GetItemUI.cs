using UnityEngine;

public class GetItemUI : MonoBehaviour
{
    [SerializeField] LayerMask _LWeapon;
    GameObject _getItemUI;

    bool _isItemInRange;

    private void Start() 
    {
        _getItemUI = GameObject.FindGameObjectWithTag("GetItemPanel");
    }

    private void Update() 
    {
        if(_isItemInRange = Physics.CheckSphere(transform.position, 1.8f, _LWeapon) && gameObject.GetComponent<PickUpItem>()._FpannelOn == true)
        {
            _getItemUI.SetActive(true);
        }
        else if(gameObject.GetComponent<PickUpItem>()._FpannelOn == false)
        {
            _getItemUI.SetActive(false);
        }
        
    }
}
