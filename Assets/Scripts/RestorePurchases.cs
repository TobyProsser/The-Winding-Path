using UnityEngine;

public class RestorePurchases : MonoBehaviour
{
    void Start()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer ||  //Disable this on Iphones
            Application.platform != RuntimePlatform.OSXPlayer)
        {
            gameObject.SetActive(false);
        }
    }

    public void ClickRestorePurchaseButton()
    {
        IAPManager.instance.RestorePurchases();
    }
}
