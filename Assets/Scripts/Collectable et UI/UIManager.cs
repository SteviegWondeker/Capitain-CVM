using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Référence au UIManager
    /// </summary>
    private static UIManager _instance;
    /// <summary>
    /// Permet d'accès à l'instance en cours du UIManager
    /// </summary>
    public static UIManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            _instance.gameObject.SetActive(false);
        }
        else
        {
            _instance.gameObject.SetActive(true);
        }
    }

}
