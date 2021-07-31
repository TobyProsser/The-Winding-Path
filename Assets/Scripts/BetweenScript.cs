using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BetweenScript : MonoBehaviour
{
    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI LastScore;

    private SaveDataScript SaveData;

    // Start is called before the first frame update
    void Start()
    {
        HighScore.text = MenuController.HighScore.ToString();
        LastScore.text = GameControllerScript.LastScore.ToString();

        MenuController.TimesPlayed++;
        Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuController.TimesPlayed >= 9)
        {
            if (!MenuController.Paid)
                AdController.AdInstance.ShowAd("video");
            MenuController.TimesPlayed = 0;
        }
    }

    public void PlayAgain()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene("GameScene");
    }
    public void Upgrades()
    {
        AudioManager.instance.Play("Click");
    }

    public void MainMenu()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene("MainMenuScene");
    }

    private void Save()
    {
        SaveData = GameObject.Find("SaveObject").GetComponent<SaveDataScript>();

        SaveData.HighestScore1 = MenuController.HighScore;
        SaveData.TimesPlayed1 = MenuController.TimesPlayed;
        SaveData.MusicSound1 = MenuController.Music;
        SaveData.Paid1 = MenuController.Paid;
        SaveData.Scary1 = MenuController.Scary;

        SaveSystem.SavePlayer(SaveData);
    }
}
