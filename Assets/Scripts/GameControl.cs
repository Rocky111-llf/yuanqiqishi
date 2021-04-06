using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

interface BeAttack
{
    void BeAttack(float value);
}


public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public Transform WeaponRecycle;
    public Button restart;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public void GameOver()
    {
        
        restart.onClick.AddListener(() => { SceneManager.LoadScene("SampleScene"); });
    }
}
