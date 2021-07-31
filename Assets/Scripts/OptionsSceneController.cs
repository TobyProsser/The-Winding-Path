using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsSceneController : MonoBehaviour
{
    public TextMeshProUGUI ScaryText;
    public TextMeshProUGUI MusicText;

    public TMP_InputField ScoreInput;
    private int JumpScore = 50;

    private SaveDataScript SaveData;

    void Start()
    {
        if (MenuController.Scary)
        {
            ScaryText.text = "ON";
        }
        else
        {
            ScaryText.text = "OFF";
        }

        if (MenuController.Music)
        {
            MusicText.text = "ON";
        }
        else
        {
            MusicText.text = "OFF";
        }
    }

    private void Update()
    {
        string input = ScoreInput.text;

        if (int.TryParse(input, out JumpScore))
        {
            JumpScore = int.Parse(ScoreInput.text);
            MenuController.JumpScareScore = JumpScore;
        }
    }

    public void ScaryButton()
    {
        AudioManager.instance.Play("Click");
        if (MenuController.Scary)
        {
            MenuController.Scary = false;
            ScaryText.text = "OFF";
        }
        else
        {
            MenuController.Scary = true;
            ScaryText.text = "ON";
        }
    }

    public void MusicButton()
    {
        AudioManager.instance.Play("Click");
        if (MenuController.Music)
        {
            MenuController.Music = false;
            MusicText.text = "OFF";
            AudioManager.instance.Stop("Music");
        }
        else
        {
            MenuController.Music = true;
            MusicText.text = "ON";
        }
    }

    public void Back()
    {
        AudioManager.instance.Play("Click");
        Save();
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
