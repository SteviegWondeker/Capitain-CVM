using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDeNiveau : MonoBehaviour
{
    [SerializeField]
    private int _currLvl;
    [SerializeField]
    private string _nxtLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PlayerData.ClearedLevel(_currLvl);
            GameManager.Instance.SaveData();
            SceneManager.LoadScene(this._nxtLevel);
        }
    }
}
