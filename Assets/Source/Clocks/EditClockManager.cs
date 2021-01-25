﻿using System;
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
                EditTimerViewModel vm = new EditTimerViewModel(timerModel);
                editTimerView.SetViewModel(vm);
                editTimerView.gameObject.SetActive(true);
                break;
            
            case StopwatchModel stopwatchModel:

                break;
            
            case TimeDisplayModel timeDisplayModel:

                break;
        }
    }

    public void CloseAll()
    {
        dialogueContainer.SetActive(false);
        editTimerView.gameObject.SetActive(false);
    }
}