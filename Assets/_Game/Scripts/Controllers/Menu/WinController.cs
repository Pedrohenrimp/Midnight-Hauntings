using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    [SerializeField]
    private Button PlayAgainButton;

    [SerializeField]
    private Button GoToMenuButton;


    [SerializeField]
    private Sprite filedStar;

    [SerializeField]
    private Sprite emptyStar;

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
        for(int i = 0; i < stars.Count; i++)
        {
            if(i < PlayerController.Life)
            {
                await Task.Delay(500);
                stars[i].sprite = filedStar;
                stars[i].transform.DOShakeScale(0.5f, 0.1f, 1);
            }
            else
            {
                stars[i].sprite = emptyStar;
            }
        }
    }
}
