using System.Collections;
using UnityEngine;

public class DropzoneObjective : QuestObjective
{
    [SerializeField]
    private GameObject _requiredObject;

    private Vector3 _requiredObjectPosition;
    private Quaternion _requiredObjectRotation;

    private bool _isDelivered = false;

    [SerializeField]
    private float _deliverTimeout = 3.0f;

    private Coroutine _deliverTimer;

    private void Start()
    {
        _requiredObjectPosition = _requiredObject.transform.position;
        _requiredObjectRotation = _requiredObject.transform.rotation;
    }

    public override void ResetObjective()
    {
        _requiredObject.transform.SetPositionAndRotation(_requiredObjectPosition, _requiredObjectRotation);
        StopDelivery();
        _isDelivered = false;
    }

    public override bool IsDone()
    {
        return _isDelivered;
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
        _deliverTimer = StartCoroutine(DeliveryTimer());
    }

    private void StopDelivery()
    {
        if (_deliverTimer != null)
        {
            StopCoroutine(_deliverTimer);
            _deliverTimer = null;
        }
    }

    private IEnumerator DeliveryTimer()
    {
        Debug.Log($"Starting to deliver {_requiredObject.name}");
        yield return new WaitForSeconds(_deliverTimeout);
        _isDelivered = true;
        _deliverTimer = null;
        Debug.Log($"Finished delivering {_requiredObject.name}");
        TriggerIsDone();
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

        if (_requiredObject != null)
        {
            var lineColor = Color.white;
            lineColor.a = 0.5f;
            Gizmos.color = lineColor;
            var requiredPosition = _requiredObject.transform.position;
            Gizmos.DrawLine(center, requiredPosition);
        }
    }
}
