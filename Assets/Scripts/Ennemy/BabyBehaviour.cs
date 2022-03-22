using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BabyBehaviour : MonoBehaviour
{
    /// <summary>
    /// Point de vie du personnage
    /// </summary>
    [SerializeField]
    private int _pv = 1;
    /// <summary>
    /// Angle de tolérange pour le calcul du saut sur la tête
    /// </summary>
    [SerializeField]
    private GameObject _projectile;
    /// <summary>
    /// Décrit la durée de l'invulnaribilité
    /// </summary>
    public const float DelaisInvulnerabilite = 1f;
    /// <summary>
    /// Décrit si l'entité est invulnérable
    /// </summary>
    private bool _invulnerable = false;
    /// <summary>
    /// Réfère à l'animator du GO
    /// </summary>
    private Animator _animator;
    /// <summary>
    /// Représente le moment où l'invulnaribilité a commencé
    /// </summary>
    private float _tempsDebutInvulnerabilite;
    /// <summary>
    /// Représente la durée entre deux lancers de projectile
    /// </summary>
    private float _deltaTimeProjectile = 2f;
    /// <summary>
    /// Représente le temps où le dernier projectile a été lancé
    /// </summary>
    private float _timeDernierProjectile;
    /// <summary>
    /// Nombre de points octroyer lors de la destruction
    /// </summary>
    [SerializeField]
    private int _pointDestruction = 5;
    /// <summary>
    /// Défini si l'objet est en cours de destruction
    /// </summary>
    private bool _destructionEnCours = false;
    /// <summary>
    /// Indique le sens du bébé / la direction du projectile
    /// </summary>
    [SerializeField]
    private int _direction = 1;

    private void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
        _timeDernierProjectile = Time.fixedTime;
    }

    private void Update()
    {
        if (this._pv <= 0 && !this._destructionEnCours)
        {
            _animator.SetTrigger("Destruction");
            GameManager.Instance.PlayerData.IncrScore(this._pointDestruction);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<BabyPatrol>().enabled = false;
            GameObject.Destroy(this.transform.parent.gameObject, 0.5f);
            this._destructionEnCours = true;
        }

        if (Time.fixedTime > _tempsDebutInvulnerabilite + DelaisInvulnerabilite)
            _invulnerable = false;

        if (Time.fixedTime > _timeDernierProjectile + _deltaTimeProjectile)
        {
            this.sendProjectile();
            _timeDernierProjectile = Time.fixedTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !_invulnerable)
        {
            PlayerBehaviour pb = collision.gameObject.GetComponent<PlayerBehaviour>();
            if (pb != null)
                pb.CallEnnemyCollision();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (!_invulnerable)
            {
                this._pv--;
                _animator.SetTrigger("DegatActif");
                _tempsDebutInvulnerabilite = Time.fixedTime;
                _invulnerable = true;
            }
        }
    }

    public void sendProjectile()
    {
        //Debug.Log("Projectile shot by baby!");
        Vector3 pos = this.transform.position;
        GameObject p = Instantiate(_projectile, this.transform.position, Quaternion.identity);
        p.GetComponent<ProjectileBehaviour>().setDirection(this._direction);
    }

    public int getDirection()
    {
        return this._direction;
    }
}
