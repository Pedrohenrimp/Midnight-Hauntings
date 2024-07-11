using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField]
    private Button PlayAgainButton;

    [SerializeField]
    private Button GoToMenuButton;

    [SerializeField]
    private List<Image> stars;

    private void OnEnable()
    {
        PlayAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        GoToMenuButton.onClick.AddListener(OnGoToMenuButtonClicked);

        SetupStars();
    }

    private void OnDisable()
    {
        PlayAgainButton.onClick.RemoveListener(OnPlayAgainButtonClicked);
        GoToMenuButton.onClick.RemoveListener(OnGoToMenuButtonClicked);
    }

    private void OnPlayAgainButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Phase2");
    }

    private void OnGoToMenuButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    private async void SetupStars()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            await Task.Delay(500);
            stars[i].transform.DOShakeScale(0.5f, 0.1f, 1);
        }
    }
}
