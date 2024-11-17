using System.Collections;
using UnityEngine;

public class DropzoneObjective : QuestObjective
{
    [SerializeField]
    private GameObject _requiredObject;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private ParticleSystem _vfxDropzoneHint;

    [SerializeField]
    private ParticleSystem _vfxDeliveryDoneHint;

    private bool _isDelivered = false;

    [SerializeField]
    private float _deliverTimeout = 3.0f;

    private Coroutine _deliverTimer;

    [SerializeField]
    private Color _hintStartColor;

    [SerializeField]
    private Color _hintDeliveringColor;

    public override void ResetObjective()
    {
        StopDelivery();
        _isDelivered = false;
    }

    public override bool IsDone()
    {
        return _isDelivered;
    }

    public override void BeginObjective()
    {
        _vfxDropzoneHint.Play();

        SetHintColor(_hintStartColor);
    }

    private void SetHintColor(Color hintColor)
    {
        var mainComponent = _vfxDropzoneHint.main;
        mainComponent.startColor = hintColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _requiredObject)
        {
            return;
        }

        StartDelivery();
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject != _requiredObject)
        {
            return;
        }

        StopDelivery();
    }

    private void StartDelivery()
    {
        SetHintColor(_hintDeliveringColor);
        _deliverTimer = StartCoroutine(DeliveryTimer());
    }

    private void StopDelivery()
    {
        if (_deliverTimer != null)
        {
            StopCoroutine(_deliverTimer);
            _deliverTimer = null;
            SetHintColor(_hintStartColor);
        }
    }

    private IEnumerator DeliveryTimer()
    {
        yield return new WaitForSeconds(_deliverTimeout);
        
        EndDelivery();

        _deliverTimer = null;
    }

    private void EndDelivery()
    {
        _audioSource.Play();
        _isDelivered = true;
        TriggerIsDone();
        _vfxDropzoneHint.Stop();
        _vfxDeliveryDoneHint.Play();
    }

    private void OnDrawGizmos()
    {
        var color = Color.green;
        color.a = 0.125f;

        var boxCollider = GetComponent<BoxCollider>();

        var center = boxCollider.center + transform.position;
        var size = boxCollider.size;

        Gizmos.color = color;
        Gizmos.DrawCube(center, size);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);

        //if (_requiredObject != null)
        //{
        //    var lineColor = Color.white;
        //    lineColor.a = 0.5f;
        //    Gizmos.color = lineColor;
        //    var requiredPosition = _requiredObject.transform.position;
        //    Gizmos.DrawLine(center, requiredPosition);
        //}
    }
}
