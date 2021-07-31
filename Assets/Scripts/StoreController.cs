using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreController : MonoBehaviour
{
    public GameObject PurchaseButton;

    void Start()
    {
        if (MenuController.Paid)
        {
            PurchaseButton.SetActive(false);
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
