using System;
using Controller;
using UnityEngine;
using UnityEngine.UI;


public class UiFunctionView : MonoBehaviour
{
    [SerializeField] private MainController _mainController;
    [SerializeField] private GameObject _fpc;
    [SerializeField] private Toggle _toggleFpc;
    [SerializeField] private Toggle _togglePostProcess;

    private void Update()
    {
        // _fpc.SetActive(_toggleFpc.isOn);
        // _mainController._services.ThirdCameraController.SetActivePostProcessing(_togglePostProcess.isOn);
    }

    public void TogglePostProcess(bool value)
    {
        
    }
}
