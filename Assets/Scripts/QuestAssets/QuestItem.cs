using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class QuestItem : QuestAsset
{
    [SerializeField]
    private XRGrabInteractable _interactable;

    [SerializeField]
    private ParticleSystem _vfxHint;

    private bool _isQuestActive = false;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Start()
    {
        _interactable.selectEntered.AddListener(Interactable_IsSelected);
        _interactable.selectExited.AddListener(Interactable_IsDeselected);

        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void OnDestroy()
    {
        _interactable.selectEntered.RemoveListener(Interactable_IsSelected);
        _interactable.selectExited.RemoveListener(Interactable_IsDeselected);
    }

    public override void ResetAsset()
    {
        transform.SetPositionAndRotation(_startPosition, _startRotation);
        _vfxHint.Stop();
    }

    public override void TriggerQuestBegin()
    {
        _isQuestActive = true;
        _vfxHint.Play();
    }

    public override void TriggerQuestComplete()
    {
        _isQuestActive = false;
        _vfxHint.Stop();
    }

    private void Interactable_IsSelected(SelectEnterEventArgs args)
    {
        _vfxHint.Stop();
    }

    private void Interactable_IsDeselected(SelectExitEventArgs args)
    {
        if (_isQuestActive)
        {
            _vfxHint.Play();
        }
    }
}
