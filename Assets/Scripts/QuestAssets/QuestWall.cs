using UnityEngine;

public class QuestWall : QuestAsset
{
    [SerializeField]
    private GameObject _invisibleWall;

    [SerializeField]
    private ParticleSystem _vfx;

    public override void ResetAsset()
    {
    }

    public override void TriggerQuestBegin()
    {
        _invisibleWall.SetActive(true);
        _vfx.Play();
    }

    public override void TriggerQuestComplete()
    {
    }

    public void DeactivateWall()
    {
        _invisibleWall.SetActive(false);
        _vfx.Stop();
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        var boxCollider = _invisibleWall.GetComponent<BoxCollider>();

        Gizmos.color = new Color(1.0f, 0.0f, 1.0f, 0.3f);
        Gizmos.DrawCube(boxCollider.center, boxCollider.size);
    }
}
