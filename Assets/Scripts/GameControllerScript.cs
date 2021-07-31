using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public int Score = 0;
    public static int LastScore = 0;

    public GameObject Player;

    public TextMeshProUGUI ScoreText;

    private int TimeTillJump = 60;
    private bool ScareJumpStarted = false;
    private bool ScaryMusicStarted = false;

    public GameObject ScareObject;
    private Animator ScareAnim;

    public Material PaidColor;
    public Material FreeColor;
    public Material ThreeMat;
    public Material FiveMat;
    public Material ThousandMat;

    public GameObject HowToPanel;

    void Start()
    {
        if (MenuController.TimesPlayed == 0)
        {
            StartCoroutine(TutorialPanel());
        }
        else
        {
            HowToPanel.SetActive(false);
        }
        Score = 0;
        ScareJumpStarted = false;
        ScaryMusicStarted = false;
        ScareAnim = ScareObject.GetComponent<Animator>();

        if (MenuController.Paid)
        {
            Player.GetComponent<Renderer>().material = PaidColor;
        }
        else if(MenuController.HighScore < 300)
        {
            Player.GetComponent<Renderer>().material = FreeColor;
        }
        else if (MenuController.HighScore >= 300 && MenuController.HighScore < 500)
        {
            Player.GetComponent<Renderer>().material = ThreeMat;
        }
        else if (MenuController.HighScore >= 500 && MenuController.HighScore < 1000)
        {
            Player.GetComponent<Renderer>().material = FiveMat;
        }
        else if (MenuController.HighScore >= 1000)
        {
            Player.GetComponent<Renderer>().material = ThousandMat;
        }
    }

    void LateUpdate()
    {
        Score = Mathf.RoundToInt(Player.transform.position.x);
        ScoreText.text = Score.ToString();

        if (Score > MenuController.JumpScareScore - 15 && !ScaryMusicStarted && MenuController.Scary && MenuController.Music)
        {
            ScaryMusicStarted = true;
            AudioManager.instance.Stop("Music");
            AudioManager.instance.Play("ScaryMusic");
        }
        if (Score > MenuController.JumpScareScore && !ScareJumpStarted && MenuController.Scary)
        {
            ScareJumpStarted = true;
            StartCoroutine(ScareJump());
        }

        if (Player.transform.position.y < -5)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator ScareJump()
    {
        while (true)
        {
            Scare();
            float JumpTime = Random.Range(25, TimeTillJump);
            yield return new WaitForSeconds(JumpTime);
        }
    }

    private IEnumerator Death()
    {
        LastScore = Score;
        if(Score > MenuController.HighScore)
        {
            MenuController.HighScore = Score;
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("BetweenGamesScene");
    }

    private void Scare()
    {
        StartCoroutine(StopMoving());
        AudioManager.instance.Play("Scream1");

        ScareAnim.ResetTrigger("Scare");
        ScareAnim.SetTrigger("Scare");
    }

    private IEnumerator StopMoving()
    {
        Player.GetComponent<PlayerController>().CanMove = false;
        yield return new WaitForSeconds(4);
        Player.GetComponent<PlayerController>().CanMove = true;

        if (MenuController.Music)
        {
            AudioManager.instance.Stop("ScaryMusic");
            AudioManager.instance.Play("Music");
        }
    }

    private IEnumerator TutorialPanel()
    {
        HowToPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        HowToPanel.SetActive(false);
    }
}
