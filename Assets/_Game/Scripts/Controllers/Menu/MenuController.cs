using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Button PlayButton;

    [SerializeField]
    private Button SettingsButton;

    [SerializeField]
    private Button QuitButton;

    private void OnEnable()
    {
        PlayButton.onClick.AddListener(OnPlayButtonClicked);
        QuitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveListener(OnPlayButtonClicked);
        QuitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Phase2");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
