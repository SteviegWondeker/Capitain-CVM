﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformMovement : MonoBehaviour
{
    /// <summary>
    /// Vitesse de l'objet en patrouille
    /// </summary>
    [SerializeField]
    private float _vitesse = 7f;
    /// <summary>
    /// Liste de GO représentant les points à atteindre
    /// </summary>
    [SerializeField]
    private Transform[] _points;
    /// <summary>
    /// Référence vers la cible actuelle de l'objet
    /// </summary>
    private Transform _cible = null;
    /// <summary>
    /// Permet de connaître la position actuelle de la cible dans le tableau
    /// </summary>
    private int _indexPoint;
    /// <summary>
    /// Défini le sens de la direction (true => vers la droite)
    /// </summary>
    private bool _directionDroite = true;
    /// <summary>
    /// Seuil où l'objet change de cible de déplacement
    /// </summary>
    private float _distanceSeuil = 0.3f;
    /// <summary>
    /// Représente le collider du joueur
    /// </summary>
    [SerializeField]
    private Transform _playerTransform;

    private void Start()
    {
        _indexPoint = 0;
        _cible = _points[_indexPoint];
        _playerTransform = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = _cible.position - this.transform.position;
        this.transform.Translate(direction.normalized * _vitesse * Time.deltaTime, Space.World);
        
        if (Vector3.Distance(this.transform.position, _cible.position) < _distanceSeuil)
        {
            if (_indexPoint == 0)
                _directionDroite = true;
            else if (_indexPoint == _points.Length - 1)
                _directionDroite = false;

            _indexPoint += (_directionDroite ? 1 : -1);
            _cible = _points[_indexPoint];
        }

        if (_playerTransform != null)
            _playerTransform.Translate(direction.normalized * _vitesse * Time.deltaTime, Space.World);
    }

    private void FixedUpdate()
    {
        Collider2D[] _hitColliders = Physics2D.OverlapBoxAll(this.transform.position, this.transform.localScale * 1.2f, 0);
        Transform playerTrouve = null;
        for (int i = 0; i < _hitColliders.Length; i++)
            if (_hitColliders[i].CompareTag("Player"))
            {
                playerTrouve = _hitColliders[i].gameObject.transform;
            }

        _playerTransform = playerTrouve;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Ligne entre les cibles
        for (int i = 0; i < _points.Length - 1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_points[i].position,
                _points[i + 1].position);
        }

        // Ligne entre l'ennemi et la cible
        if (_cible != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(this.transform.position, _cible.position);
        }
    }
#endif
}
