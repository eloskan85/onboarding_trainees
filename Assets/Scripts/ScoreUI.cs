using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private const string ScoreTemplate = "Aktueller Punktestand\n{0}";

    public void SetScore(int newScore)
    {
        _text.text = string.Format(ScoreTemplate, newScore);
    }
}
