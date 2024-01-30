using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    private static audioManager _instance;
    private AudioSource PlayerSwipeSFX;

    private static bool instance {  get { return _instance; } }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        void Start()
        {


            PlayerSwipeSFX = GameObject.Find("PlayerSFX").GetComponent<AudioSource>();

            DontDestroyOnLoad(gameObject);
        }
    }
}
