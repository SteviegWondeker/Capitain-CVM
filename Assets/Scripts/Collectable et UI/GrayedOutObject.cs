using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrayedOutObject : MonoBehaviour
{

    [SerializeField]
    private int _lvl;
    [SerializeField]
    private string _chest;
    [SerializeField]
    private Button _btn;

    void Start()
    {
        if (this._lvl > 1)
        {
            if (GameManager.Instance.PlayerData != null
                && GameManager.Instance.PlayerData.ListeNiveauxCompletes[this._lvl - 2] == 1)
                this._btn.interactable = true;
        }
        else if (this._chest != "")
        {
            if (Array.Exists(GameManager.Instance.PlayerData.ListeCoffreOuvert, element => element == this._chest))
            {
                this.gameObject.GetComponent<ChestInteraction>().EstOuvert = true;
            }
        }
    }

}
