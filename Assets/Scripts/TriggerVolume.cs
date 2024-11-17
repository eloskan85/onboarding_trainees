using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour
{
    public UnityEvent OnEnterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        OnEnterTrigger.Invoke();
    }
}
