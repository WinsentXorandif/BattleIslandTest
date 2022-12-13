using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField]
    private GamePlay game;

    [SerializeField]
    private TMP_Text TextFinal;
    [SerializeField]
    private Button ButtonStart;
    [SerializeField]
    private Button ButtonPause;
    [SerializeField]
    private Button ButtonReStart;
    [SerializeField]
    private Button ButtonQuit;

    private bool pause;

    private void Awake()
    {
        pause = false;
        ButtonPause.gameObject.SetActive(false);
        ButtonReStart.gameObject.SetActive(false);
    }

    public void OnPauseButton()
    {
        if (!pause)
        {
            pause = true;
            Time.timeScale = 0f;
        }
        else
        {
            pause = false;
            Time.timeScale = 1.0f;
        }

    }

    public void OnReStartButton()
    {
        Time.timeScale = 0f;
        //DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnStartButton()
    {
        Time.timeScale = 1.0f;
        game.StartGameOn();
        ButtonStart.gameObject.SetActive(false);
        ButtonPause.gameObject.SetActive(true);
        ButtonReStart.gameObject.SetActive(true);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnVictory()
    {
        TextFinal.color = Color.red;
        TextFinal.text = "* VICTORY *";
    }

    public void OnLoose()
    {
        TextFinal.color = Color.blue;
        TextFinal.text = "You Loose...";
    }

    private void OnEnable()
    {
        game.OnWin += OnVictory;
        game.OnLoose += OnLoose;
    }

    private void OnDisable()
    {
        game.OnWin -= OnVictory;
        game.OnLoose -= OnLoose;
    }

}
