using UnityEngine;
using Cinemachine;
using System.Collections;

public class CinematicPart1 : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _vcam1;
    [SerializeField] CinemachineVirtualCamera _vcam2;
    [SerializeField] CinemachineFreeLook _freeLook;
    [SerializeField] GameObject[] _cam1Enemies;
    [SerializeField] GameObject[] _cam2Enemies;
    [SerializeField] GameObject[] _allEnemiesInSection;
    [SerializeField] Animator[] _allDoorInSection;
    [SerializeField] Transform cinematicLookat;
    Transform _player, _playerObj;
    GameObject _cam;
    bool vcam1 = false, vcam2= false, freeLook=true;
    private void Start() {
        _player = GameObject.FindGameObjectWithTag("PlayerHolder").transform;
        _playerObj = GameObject.FindGameObjectWithTag("Player").transform;
        _cam = GameObject.Find("CamPoint");
    }
    private void Update() {
        SwitchCam();
    }
    void SwitchCam(){
        if(vcam1){
            _vcam1.Priority = 10;
            _vcam2.Priority = 0;
            _freeLook.Priority = 0;
        }else if(vcam2){
            _vcam1.Priority = 0;
            _vcam2.Priority = 10;
            _freeLook.Priority = 0;
        }else if(freeLook){
            _vcam1.Priority = 0;
            _vcam2.Priority = 0;
            _freeLook.Priority = 10;

        }
    }

    IEnumerator CinematicPartOne(){
        _player.GetComponent<MoveNRotate>().enabled = false;
        _cam.GetComponent<FreeCam>().enabled= false;
        freeLook = false;
        vcam1 = true;
        _playerObj.GetComponent<Animator>().SetFloat("X",0f);
        _playerObj.GetComponent<Animator>().SetFloat("Y",0f);
        _playerObj.LookAt(cinematicLookat);
        yield return new WaitForSeconds(2f);

        for(int i=0; i < _cam1Enemies.Length; i++){
            _cam1Enemies[i].transform.LookAt(_player);
        }
        yield return new WaitForSeconds(2f);

        vcam1 = false;
        vcam2 = true;
        
        yield return new WaitForSeconds(2f);

        for(int i=0; i< _cam2Enemies.Length; i++){
            _cam2Enemies[i].transform.LookAt(_player);
        }

        yield return new WaitForSeconds(3f);

        vcam2 = false;
        freeLook = true;
        // All Doors Open, All Enemies track Player
        for(int i =0; i < _allDoorInSection.Length; i++){
            _allDoorInSection[i].SetBool("isOpen", true);
        }
        for(int i =0; i < _allEnemiesInSection.Length; i++){
            _allEnemiesInSection[i].transform.LookAt(_player);
            _allEnemiesInSection[i].GetComponent<TrackPlayer>().SetCinematic();
        }

        yield return new WaitForSeconds(3f);

        _player.GetComponent<MoveNRotate>().enabled = true;
        _cam.GetComponent<FreeCam>().enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            StartCoroutine(CinematicPartOne());
        }
    }

}
