using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ModifiedStepManager : MonoBehaviour
{
    [Serializable]
    private class Step
    {
        public GameObject StepObject;
        public string ButtonText;
    }

    [SerializeField]
    private TMP_Text _stepButtonTextField;

    [SerializeField]
    private List<Step> _stepList = new();

    public UnityEvent OnLastCardReached;

    private int _currentStepIndex;

    public void Next()
    {
        _stepList[_currentStepIndex].StepObject.SetActive(false);
        _currentStepIndex = (_currentStepIndex + 1) % _stepList.Count;
        _stepList[_currentStepIndex].StepObject.SetActive(true);

        _stepButtonTextField.text = _stepList[_currentStepIndex].ButtonText;

        if (_currentStepIndex == _stepList.Count - 1)
        {
            OnLastCardReached.Invoke();
        }
    }
}
