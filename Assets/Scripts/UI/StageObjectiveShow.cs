using System.Collections;
using UnityEngine;

public class StageObjectiveShow : MonoBehaviour
{
    [SerializeField] Animator _anim;

    private void Start() {
        StartCoroutine(ShowObjective());
    }

    IEnumerator ShowObjective(){
        _anim.SetBool("SetActive", true);

        yield return new WaitForSeconds(4.0f);

        _anim.SetBool("SetActive", false);
        Invoke("TurnOffObjective", 2f);
    }

    void TurnOffObjective(){
        gameObject.SetActive(false);
    }
}
