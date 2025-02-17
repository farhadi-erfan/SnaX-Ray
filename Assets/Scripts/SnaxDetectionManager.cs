using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SnaxDetectionManager : MonoBehaviour
{

    public GameObject snaxContainer;
    public TMP_Text snackNameTMP;
    public TMP_Text sodiumTMP;
    public TMP_Text fatTMP;
    public TMP_Text energyTMP;
    public TMP_Text sugarTMP;


    public Image customerAvatar;
    public TMP_Text orderText;

    private CharacterData currentCustomer;
    private CharacterManager characterManager;
    private SnackData currentSnack;
    private SnackManager snackManager;


    void Start()
    {
        characterManager = FindFirstObjectByType<CharacterManager>();
        snackManager = FindFirstObjectByType<SnackManager>();

        string currentCustomerName = PlayerPrefs.GetString("CurrentCustomer");
        Debug.Log("Current Customer Name: " + currentCustomerName);
        currentCustomer = characterManager.GetCharacterByName(currentCustomerName);
        orderText.text = currentCustomer.order;
        customerAvatar.sprite = currentCustomer.normalFace;
    }

    public void inspectClicked()
    {
        Debug.Log("Inspect Clicked");
        PlayerPrefs.SetString("CurrentCustomer", currentCustomer.characterName);
        PlayerPrefs.SetString("InspectingSnackBarcode", currentSnack.barcode);
        SceneManager.LoadScene("InspectScene");
    }

    public void buyClicked()
    {
        Debug.Log("Buy Clicked");
        PlayerPrefs.SetString("CurrentCustomer", currentCustomer.characterName);
        PlayerPrefs.SetString("CurrentSnackBarcode", currentSnack.barcode);
        SceneManager.LoadScene("RewardScene");
    }

    public void pauseClicked()
    {
        SceneManager.LoadScene("CustomersList");
    }

    public void barcodeDetected(string barcode)
    {
        currentSnack = snackManager.GetSnackByBarcode(barcode);
        if (currentSnack == null)
        {
            return;
        }
        snackNameTMP.text = currentSnack.snackName;
        sodiumTMP.text = currentSnack.sodium + "mg";
        fatTMP.text = currentSnack.fat + "g";
        energyTMP.text = currentSnack.energy + "Cal";
        sugarTMP.text = currentSnack.sugar + "g";
        snaxContainer.SetActive(true);
    }

    public void noBarcodeSeen()
    {
        snaxContainer.SetActive(false);
    }
}
