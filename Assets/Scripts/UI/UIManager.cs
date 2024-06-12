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
    [SerializeField]
    private TMP_Text[] SymbolsText;

    [Header("Win Popup")]
    [SerializeField]
    private GameObject WinPopup_Object;
    [SerializeField]
    private TMP_Text Win_Text;
    [Header("Win Popup")]
    [SerializeField]
    private Sprite MegaWin_Sprite;
    [SerializeField]
    private Image Win_Image;

    [SerializeField] private AudioController audioController;
    [SerializeField]
    private SlotBehaviour slotManager;

    [SerializeField]
    private Button GameExit_Button;



    private void Start()
    {
        if (PaytableExit_Button) PaytableExit_Button.onClick.RemoveAllListeners();
        if (PaytableExit_Button) PaytableExit_Button.onClick.AddListener(delegate { ClosePopup(PaytablePopup_Object); });

        if (Info_Button) Info_Button.onClick.RemoveAllListeners();
        if (Info_Button) Info_Button.onClick.AddListener(delegate { OpenPopup(PaytablePopup_Object); });

        if (GameExit_Button) GameExit_Button.onClick.RemoveAllListeners();
        if (GameExit_Button) GameExit_Button.onClick.AddListener(CallOnExitFunction);


    }

    internal void PopulateWin(int value, double amount)
    {
        switch (value)
        {
            case 1:
                if (Win_Image) Win_Image.sprite = MegaWin_Sprite;
                break;
            
        }

        StartPopupAnim(amount);
    }

    internal void PopulateWin(double amount)
    {
        int initAmount = 0;
        if (WinPopup_Object) WinPopup_Object.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);

        DOTween.To(() => initAmount, (val) => initAmount = val, (int)amount, 5f).OnUpdate(() =>
        {
            if (Win_Text) Win_Text.text = initAmount.ToString();
        });

        DOVirtual.DelayedCall(6f, () =>
        {
            if (WinPopup_Object) WinPopup_Object.SetActive(false);
            if (MainPopup_Object) MainPopup_Object.SetActive(false);
        });
    }

   

    private void StartPopupAnim(double amount)
    {
        int initAmount = 0;
        if (WinPopup_Object) WinPopup_Object.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);

        DOTween.To(() => initAmount, (val) => initAmount = val, (int)amount, 5f).OnUpdate(() =>
        {
            if (Win_Text) Win_Text.text = initAmount.ToString();
        });

        DOVirtual.DelayedCall(6f, () =>
        {
            if (WinPopup_Object) WinPopup_Object.SetActive(false);
            if (MainPopup_Object) MainPopup_Object.SetActive(false);
            slotManager.CheckBonusGame();
        });
    }

    
    private void CallOnExitFunction()
    {
        slotManager.CallCloseSocket();
        Application.ExternalCall("window.parent.postMessage", "onExit", "*");
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

    internal void InitialiseUIData(string SupportUrl, string AbtImgUrl, string TermsUrl, string PrivacyUrl, Paylines symbolsText)
    {
        PopulateSymbolsPayout(symbolsText);
    }


    private void PopulateSymbolsPayout(Paylines paylines)
    {
        for (int i = 0; i < paylines.symbols.Count; i++)
        {
            if (i < SymbolsText.Length)
            {
                string text = null;
                if (paylines.symbols[i].multiplier._5x != 0)
                {
                    text += paylines.symbols[i].multiplier._5x;
                }
                if (paylines.symbols[i].multiplier._4x != 0)
                {
                    text += "\n" + paylines.symbols[i].multiplier._4x;
                }
                if (paylines.symbols[i].multiplier._3x != 0)
                {
                    text += "\n" + paylines.symbols[i].multiplier._3x;
                }
                if (paylines.symbols[i].multiplier._2x != 0)
                {
                    text += "\n" + paylines.symbols[i].multiplier._2x;
                }
                if (SymbolsText[i]) SymbolsText[i].text = text;
            }
        }
    }

    
}
