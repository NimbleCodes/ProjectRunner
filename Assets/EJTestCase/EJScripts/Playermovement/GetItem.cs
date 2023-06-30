using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
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
        EatItem();
        UseItem(); 
    }

    private void EatItem()
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
            _itemSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/ItemIcons/" + hit.collider.name);
            _objName = hit.collider.gameObject; 
            Destroy(hit.collider.gameObject);
        }
    }

    private void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/" + _itemSlot[0].GetComponent<Image>().sprite.name);
            if(obj != null)
            {
                _holding = Instantiate(obj, _Weapons);
                _holding.transform.localPosition = _Weapons.transform.localPosition;
               
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
    }
}
