using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChestOpen : MonoBehaviour
{
    [SerializeField]
    private Button Chest_Button;
    [SerializeField]
    private GameObject Closed_Chest;
    [SerializeField]
    private GameObject Open_Chest;
    [SerializeField]
    private RectTransform Prize_RT;
    [SerializeField]
    private Image Prize_Image;

    private void Start()
    {
        if (Chest_Button) Chest_Button.onClick.RemoveAllListeners();        
        if (Chest_Button) Chest_Button.onClick.AddListener(ChestClick);
    }

    private void ChestClick()
    {
        if (Open_Chest) Open_Chest.SetActive(true);
        if (Closed_Chest) Closed_Chest.SetActive(false);
        if (Prize_RT) Prize_RT.DOScale(1, 1);
    }
}
