using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    [SerializeField] Transform _cam;
    [SerializeField] GameObject _getItemPanel;
    [SerializeField] GameObject[] _itemSlot;
    [SerializeField] Transform _Weapons; 
    public float _playerActionDistance;
    public bool active = false;
    RaycastHit hit;
    GameObject _holding, _objName;


    void Update()
    {
        FullItemSlot(); 
    }

    private void FullItemSlot()
    {
        active = Physics.Raycast(_cam.position, _cam.TransformDirection(Vector3.forward), out hit, _playerActionDistance);
        if (active == true && hit.collider.CompareTag("Weapon"))
        {
            _getItemPanel.SetActive(true);
        }
        if (active == false)
        {
            _getItemPanel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F) && active == true)
        {
            _itemSlot[0].gameObject.SetActive(true);
            _itemSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/ItemImages/" + hit.collider.name);
            _objName = hit.collider.gameObject; 
        }
    }

}
