﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class ChestInteraction : BaseInteraction
{
    /// <summary>
    /// Représente le nombre de points qui sera ajouter à
    /// l'ouverture
    /// </summary>
    [SerializeField, Range(0, 25)]
    private int _scoreBonus;

    /// <summary>
    /// Détermine si le coffre est ouvert
    /// </summary>
    [SerializeField]
    private bool _estOuvert = false;
    public bool EstOuvert { get { return _estOuvert; } set { _estOuvert = value; } }

    /// <summary>
    /// Sprite à ouvrir quand le coffre est ouvert
    /// </summary>
    [SerializeField]
    private Sprite _coffreOuvert;

    /// <summary>
    /// Nom du coffre utiliser dans le fichier de sauvegarde
    /// Le nom est autogénéré dans la méthode Start
    /// </summary>
    [SerializeField]
    private string _name;

    private void Start()
    {
        this._estOuvert = GameManager.Instance.PlayerData.AvoirOuvertureCoffre(_name);

        if (_estOuvert)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = _coffreOuvert;
    }

    public override void DoAction()
    {
        if(!_estOuvert)
        {
            GameManager.Instance.PlayerData.IncrScore(this._scoreBonus);
            _estOuvert = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = _coffreOuvert;
            this.ArreterInteraction();
            GameManager.Instance.PlayerData.AjouterCoffreOuvert(_name);
        }
    }
}
