using System.Collections;
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

    public Vector3 StartPosition;

    public Quaternion StartRotation;

    private Coroutine _resetQuestItemTimer;

    [SerializeField]
    private float _resetQuestItemTimeout = 10.0f;

    [SerializeField]
    private Color _questItemColorDefault = Color.green;

    [SerializeField]
    private Color _questItemColorReset = Color.red;

    [SerializeField]
    private AudioSource _sfxQuestItemAudioSource = null;

    private bool isInDeliveryProcess = false;

    private void Start()
    {
        _interactable.selectEntered.AddListener(Interactable_IsSelected);
        _interactable.selectExited.AddListener(Interactable_IsDeselected);

        SetHintColor(_questItemColorDefault);
    }

    private void OnDestroy()
    {
        _interactable.selectEntered.RemoveListener(Interactable_IsSelected);
        _interactable.selectExited.RemoveListener(Interactable_IsDeselected);
    }

    private void SetHintColor(Color hintColor)
    {
        var mainComponent = _vfxHint.main;
        mainComponent.startColor = hintColor;
    }

    public override void ResetAsset()
    {
        transform.SetPositionAndRotation(StartPosition, StartRotation);
        _vfxHint.Stop();
        SetHintColor(_questItemColorDefault);
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

        StopResetQuestTimer();
    }

    private void Interactable_IsDeselected(SelectExitEventArgs args)
    {
        if (_isQuestActive)
        {
            StartResetQuestTimer();
        }
    }

    private void StartResetQuestTimer()
    {
        if (isInDeliveryProcess)
        {
            return;
        }

        _resetQuestItemTimer = StartCoroutine(ResetQuestTimer_Coroutine());
    }

    private void StopResetQuestTimer()
    {
        if (_resetQuestItemTimer != null)
        {
            StopCoroutine(_resetQuestItemTimer);
            _vfxHint.Stop();
        }
    }

    private IEnumerator ResetQuestTimer_Coroutine()
    {
        SetHintColor(_questItemColorReset);
        _vfxHint.Play();
        yield return new WaitForSeconds(_resetQuestItemTimeout);

        _sfxQuestItemAudioSource.Play();
        ResetAsset();
        _vfxHint.Play();

        _resetQuestItemTimer = null;
    }

    public void StartDelivery()
    {
        isInDeliveryProcess = true; 
        StopResetQuestTimer();
    }

    public void StopDelivery()
    {
        isInDeliveryProcess = false;
    }
}
