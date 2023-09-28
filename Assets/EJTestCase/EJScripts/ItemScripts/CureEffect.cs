using UnityEngine;

public class CureEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem _cureEffect;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            _cureEffect.Play();
        }
    }
}
