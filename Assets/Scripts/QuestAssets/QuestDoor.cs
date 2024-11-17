using System;
using System.Collections;
using UnityEngine;

public class QuestDoor : QuestAsset
{
    [SerializeField]
    private float _openAngle = 135.0f;

    [SerializeField]
    private float _time = 5.0f;

    private float _animationTime = 0.0f;

    [SerializeField]
    private AnimationCurve _animationCurve;

    [SerializeField]
    private Transform _doorPivot;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _doorOpenSfx;

    private Coroutine _coroutine;

    public override void ResetAsset()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _doorPivot.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    public override void TriggerQuestBegin()
    {
    }

    public override void TriggerQuestComplete()
    {
        _coroutine = StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        Debug.Log($"Opening Door '{name}'...");
        if (_time <= 0.0f)
            _time = 0.001f;

        _audioSource.PlayOneShot(_doorOpenSfx);

        while (_animationTime <= _time)
        {
            var t = _animationTime / _time;
            var angle = _animationCurve.Evaluate(t) * _openAngle;
            _doorPivot.localRotation = Quaternion.Euler(0.0f, angle, 0.0f);

            yield return null;

            _animationTime += Time.deltaTime;
        }
        Debug.Log($"Opened Door '{name}'!");
    }
}
