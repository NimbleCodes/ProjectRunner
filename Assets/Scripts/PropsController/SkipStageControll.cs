using UnityEngine;

public class SkipStageControll : MonoBehaviour
{
    
    [SerializeField] Transform _player;
    [SerializeField] Transform _part1;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Z)){
            _player.position = transform.position;
        }
        if(Input.GetKeyDown(KeyCode.C)){
            _player.position = _part1.position;
        }
    }
}
