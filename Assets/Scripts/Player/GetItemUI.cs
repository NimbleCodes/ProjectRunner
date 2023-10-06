using UnityEngine;

public class GetItemUI : MonoBehaviour
{
    [SerializeField] LayerMask _LWeapon;
    public GameObject _getItemUI;

    bool _isItemInRange = false;

    private void Start() 
    {
        _getItemUI = GameObject.FindGameObjectWithTag("GetItemPanel");
        //_getItemUI.SetActive(false);
    }

    private void Update() 
    {
        if(_isItemInRange = Physics.CheckSphere(transform.position, 1.8f, _LWeapon)){
            _getItemUI.SetActive(true);
            
        }
        else {
            _getItemUI.SetActive(false);
        }
        
    }
}
