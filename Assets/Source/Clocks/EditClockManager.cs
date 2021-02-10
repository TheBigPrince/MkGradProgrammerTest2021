using System;
using System.Collections;
using System.Collections.Generic;
using Protodroid.Clocks.Models;
using Protodroid.Clocks.ViewModels;
using Protodroid.Clocks.Views;
using Protodroid.MVVM;
using UnityEngine;

public class EditClockManager : MonoBehaviour
{
    #region Singleton
        
    public static EditClockManager instance = null;

    private void CreateSingleton()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    #endregion

    [SerializeField]
    private GameObject dialogueContainer = null;
    
    [SerializeField]
    private EditTimerView editTimerView;
    
    [SerializeField]
    private EditStopwatchView editStopwatchView;

    [SerializeField]
    private EditTimeDisplayView editTimeDisplayView;

    private IView activeEditClockView = null;

    private void Awake()
    {
        CreateSingleton();
    }

    public void EditClock(ClockModel model)
    {
        dialogueContainer.SetActive(true);
        
        switch (model)
        {
            case TimerModel timerModel:
                EditTimerViewModel editTimerVM = new EditTimerViewModel(timerModel);
                editTimerView.SetViewModel(editTimerVM);
                editTimerView.gameObject.SetActive(true);
                activeEditClockView = editTimerView;
                break;
            
            case StopwatchModel stopwatchModel:
                EditStopwatchViewModel editStopwatchVM = new EditStopwatchViewModel(stopwatchModel);
                editStopwatchView.SetViewModel(editStopwatchVM);
                editStopwatchView.gameObject.SetActive(true);
                activeEditClockView = editStopwatchView;
                break;
            
            case TimeDisplayModel timeDisplayModel:
                EditTimeDisplayViewModel editTimeViewModel = new EditTimeDisplayViewModel(timeDisplayModel);
                editTimeDisplayView.SetViewModel(editTimeViewModel);
                editTimeDisplayView.gameObject.SetActive(true);
                activeEditClockView = editTimeDisplayView;
                break;
        }
    }

    public void CloseAll()
    {
        dialogueContainer.SetActive(false);
        activeEditClockView.GameObject.SetActive(false);
        activeEditClockView = null;
    }
}
