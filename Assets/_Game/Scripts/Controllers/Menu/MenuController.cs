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

    private void OnEnable()
    {
        PlayButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveListener(OnPlayButtonClicked);
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Phase1");
    }
}
