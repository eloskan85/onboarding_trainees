using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class ScoreChangedEvent : UnityEvent<int>
    {
    }

    [SerializeField]
    private List<Quest> _quests;

    private int _currentQuestIndex = 0;
    private Quest _currentQuest;

    public bool IsGameFinished => _currentQuestIndex == _quests.Count;

    private int _score;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChanged.Invoke(_score);
        }
    }

    public ScoreChangedEvent OnScoreChanged;

    private void Start()
    {
        if (IsGameFinished)
        {
            return;
        }

        BeginGame();
    }

    private void BeginGame()
    {
        Score = 0;

        _currentQuest = _quests[_currentQuestIndex];
        _currentQuest.OnQuestComplete += OnCurrentQuestComplete;
        _currentQuest.StartQuest();
    }

    private void OnCurrentQuestComplete()
    {
        _currentQuest.OnQuestComplete -= OnCurrentQuestComplete;
        _currentQuestIndex++;
        Debug.Log($"Quest {_currentQuestIndex} is completed!");

        Score++;

        if (IsGameFinished)
        {
            return;
        }

        _currentQuest = _quests[_currentQuestIndex];
        if (_currentQuest == null)
        {
            throw new NullReferenceException($"{name}: quests at index {_currentQuestIndex} is null. You should set the quest.");
        }

        _currentQuest.OnQuestComplete += OnCurrentQuestComplete;
        _currentQuest.StartQuest();
    }

    public void ShutdownApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
