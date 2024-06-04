using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestOpen : MonoBehaviour
{
    
    [SerializeField] private Button Chest;
    [SerializeField] private TMP_Text text;
    [SerializeField]
    private GameObject Chest_Opening;
    [SerializeField] private ImageAnimation imageAnimation;
    [SerializeField] private BonusController _bonusManager;

    [SerializeField]
    internal bool isOpen;

    //[Header("FreeSpins Popup")]
    //[SerializeField]
    private GameObject FreeSpinPopup_Object;
    //[SerializeField]
    private TMP_Text Free_Text;
    private int FreeSpins;
   // [SerializeField]
    private Button FreeSpin_Button;
    //[SerializeField]
    private GameObject Bonus_Object;

    //[SerializeField]
    private SlotBehaviour slotManager;

    void Start()
    {
        if (Chest) Chest.onClick.RemoveAllListeners();
        if (Chest) Chest.onClick.AddListener(OpenCase);

        if (FreeSpin_Button) FreeSpin_Button.onClick.RemoveAllListeners();
        if (FreeSpin_Button) FreeSpin_Button.onClick.AddListener(delegate { StartFreeSpins(FreeSpins); });
    }

    internal void ResetCase()
    {
        isOpen = false;
        text.gameObject.SetActive(false);
    }

    void OpenCase()
    {
        if (isOpen)
            return;
        PopulateCase();
        Chest_Opening.SetActive(true);
        Chest.gameObject.SetActive(false);
        StartCoroutine(setCase());
    }

    void PopulateCase()
    {
        int value = _bonusManager.GetValue();
        if (value == -1)
        {
            text.text = "game over";
        }
        else
        {
            text.text = value.ToString();
        }
    }

    IEnumerator setCase()
    {
        yield return new WaitUntil(() => !imageAnimation.isplaying);
        yield return new WaitForSeconds(0.3f);
        text.gameObject.SetActive(true);
        isOpen = true;
        if (text.text == "game over")
        {
            yield return new WaitForSeconds(1f);
            _bonusManager.GameOver();
        }
    }

    private void StartFreeSpins(int spins)
    {
        if (Bonus_Object) Bonus_Object.SetActive(false);
        if (FreeSpinPopup_Object) FreeSpinPopup_Object.SetActive(false);
        slotManager.FreeSpin(spins);
    }

    internal void FreeSpinProcess(int spins)
    {
        FreeSpins = spins;
        if (FreeSpinPopup_Object) FreeSpinPopup_Object.SetActive(true);
        if (Free_Text) Free_Text.text = spins.ToString();
        if (Bonus_Object) Bonus_Object.SetActive(true);
    }
}