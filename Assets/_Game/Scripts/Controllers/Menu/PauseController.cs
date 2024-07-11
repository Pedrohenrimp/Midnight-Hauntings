using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private Button ResumeButton;

    [SerializeField]
    private Button GoToMenuButton;

    [SerializeField]
    private Toggle FPSToggle;

    [SerializeField]
    private GameObject FPSContent;

    [SerializeField]
    private Toggle DinamicLODToggle;

    [SerializeField]
    private TMP_InputField BaseFPSInput;

    private void OnEnable()
    {
        ResumeButton.onClick.AddListener(OnResumeButtonClicked);
        GoToMenuButton.onClick.AddListener(OnGoToMenuButtonClicked);

        FPSToggle.onValueChanged.AddListener(SetupFPSContent);

    }

    private void OnDisable()
    {
        ResumeButton.onClick.RemoveListener(OnResumeButtonClicked);
        GoToMenuButton.onClick.RemoveListener(OnGoToMenuButtonClicked);
    }

    private void OnResumeButtonClicked()
    {
        SetupBaseFPS();
    }

    private void OnGoToMenuButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

    private void SetupFPSContent(bool isOn)
    {
        FPSContent.SetActive(isOn);
    }

    private void SetupDinamicLODContent(bool isOn)
    {
        FPSContent.SetActive(isOn);
    }

    private void SetupBaseFPS()
    {
        if(DinamicLODToggle.isOn != LODManager.UseDinamicLOD)
        {
            var fps = BaseFPSInput.text != string.Empty ? int.Parse(BaseFPSInput.text) : 120;
            LODManager.MinFPS = fps;
            LODManager.OnUseDinamicLODValueChanged?.Invoke(DinamicLODToggle.isOn);
        }
    }
}
