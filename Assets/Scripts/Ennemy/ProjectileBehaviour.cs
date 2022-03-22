using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    /// <summary>
    /// Indique le sens du bébé / la direction du projectile
    /// </summary>
    private int _direction;
    /// <summary>
    /// Intervalle de rotation
    /// </summary>
    [SerializeField]
    private float _deltaRotation = 1f;
    /// <summary>
    /// Vitesse du projectile
    /// </summary>
    [SerializeField]
    private float _vitesse = 1f;
    /// <summary>
    /// Défini si l'objet est en cours de destruction
    /// </summary>
    private bool _destructionEnCours = false;
    /// <summary>
    /// Défini si le projectile a touché sa cible
    /// </summary>
    private bool _success = false;
    /// <summary>
    /// Temps de création du projectile
    /// </summary>
    private float _initTime;
    /// <summary>
    /// Durée de vie du projectile
    /// </summary>
    private float _durability = 3f;

    private void Start()
    {
        _initTime = Time.fixedTime;
    }

    void Update()
    {
        this.transform.Rotate(0f, 0f, this._deltaRotation, Space.Self);
        this.transform.position = transform.position + new Vector3(this._direction * this._vitesse * Time.deltaTime, 0, 0);
        if (this._success && !this._destructionEnCours)
        {
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            GameObject.Destroy(this.gameObject);
            this._destructionEnCours = true;
        }
        if (Time.fixedTime > _initTime + _durability)
        {
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            GameObject.Destroy(this.gameObject);
            this._destructionEnCours = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerBehaviour pb = collision.gameObject.GetComponent<PlayerBehaviour>();
            if (pb != null)
            {
                pb.CallEnnemyCollision();
                this._success = true;
            }
        }
    }

    public void setDirection(int d)
    {
        this._direction = d;
    }
}
