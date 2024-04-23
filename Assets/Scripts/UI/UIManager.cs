using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;


public class UIManager : MonoBehaviour
{

    [Header("Menu UI")]
    [SerializeField]
    private Button Info_Button;

    [Header("Popus UI")]
    [SerializeField]
    private GameObject MainPopup_Object;

    [Header("Paytable Popup")]
    [SerializeField]
    private GameObject PaytablePopup_Object;
    [SerializeField]
    private Button PaytableExit_Button;
    [SerializeField]
    private Image Info_Image;

    [Header("Card Bonus Game")]
    [SerializeField]
    private Button DoubleBet_Button;
    [SerializeField]
    private GameObject BonusPanel;
    [SerializeField]
    private GameObject CardGame_Panel;
    [SerializeField]
    private GameObject CoffinGame_Panel;

    [SerializeField] private AudioController audioController;


    private void Start()
    {
        if (PaytableExit_Button) PaytableExit_Button.onClick.RemoveAllListeners();
        if (PaytableExit_Button) PaytableExit_Button.onClick.AddListener(delegate { ClosePopup(PaytablePopup_Object); });

        if (Info_Button) Info_Button.onClick.RemoveAllListeners();
        if (Info_Button) Info_Button.onClick.AddListener(delegate { OpenPopup(PaytablePopup_Object); });

        if (DoubleBet_Button) DoubleBet_Button.onClick.RemoveAllListeners();
        if (DoubleBet_Button) DoubleBet_Button.onClick.AddListener(delegate { OpenBonusGame(true); });
    }

    private void OpenBonusGame(bool type)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (type)
        {
            if (CardGame_Panel) CardGame_Panel.SetActive(true);
            if (CoffinGame_Panel) CoffinGame_Panel.SetActive(false);
        }
        else
        {
            if (CardGame_Panel) CardGame_Panel.SetActive(false);
            if (CoffinGame_Panel) CoffinGame_Panel.SetActive(true);
        }
        if (BonusPanel) BonusPanel.SetActive(true);
    }

    private void OpenPopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
    }

    private void ClosePopup(GameObject Popup)
    {

        if (audioController) audioController.PlayButtonAudio();

        if (Popup) Popup.SetActive(false);
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
    }
}
