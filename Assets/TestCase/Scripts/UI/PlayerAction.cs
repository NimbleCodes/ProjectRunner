using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] GameObject _getItemPanel;
    [SerializeField] GameObject[] _itemSlot;
    Sprite _sprite;
    public float _playerActionDistance;
    public bool active = false;
    int _arrIndex = 0;
    string _itemName;
    RaycastHit hit;
    void Update(){
        active = Physics.Raycast(_player.position,_player.TransformDirection(Vector3.forward.normalized), out hit, _playerActionDistance);
        if (active == true && hit.collider.CompareTag("Weapon"))
        {
            _getItemPanel.SetActive(true);
        }
        if(active == false)
        {
            _getItemPanel.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.F) && active == true){
            _itemName = hit.collider.name;
            Debug.Log(_itemName);
            _itemSlot[_arrIndex].GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/ItemIcons/" + _itemName);
        }

    }

    void checkSlot()
    {
        for(int i =0; i < _itemSlot.Length; i++)
        {
           
        }
    }
    
}
