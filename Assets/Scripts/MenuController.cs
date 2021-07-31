using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuController : MonoBehaviour
{
    public static int HighScore = 0;
    public static bool Scary = true;
    public static bool Music = true;
    public static bool Paid = false;
    public static int TimesPlayed = 0;

    public static int JumpScareScore = 50;

    public TextMeshProUGUI ScoreText;

    private void Start()
    {
        print(JumpScareScore);
        LoadData();
        ScoreText.text = HighScore.ToString();

        if (Music)
        {
            AudioManager.instance.Play("Music");
        }
    }
    public void Play()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene("GameScene");
    }

    public void Options()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene("OptionsScene");

        if (!Paid)
            AdController.AdInstance.ShowBannerAd("banner");
    }

    public void Store()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene("StoreScene");
    }

    public void Exit()
    {
        AudioManager.instance.Play("Click");
        Application.Quit();
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + "/player.scores";
        //File.Delete(path);
        if (File.Exists(path))
        {
            PlayerData data = SaveSystem.LoadPlayer();

            if (data != null)
            {
                HighScore = data.HighestSaveScore;

                TimesPlayed = data.TimesPlayed2;

                Paid = data.paid;
                Music = data.MusicSound;
                Scary = data.scary;
            }
            else
            {
                Debug.Log("No Saved Data");
            }
        }
    }
}
