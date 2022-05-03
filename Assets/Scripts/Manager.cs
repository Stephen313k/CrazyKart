using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

   
	public static Manager Instance { set; get; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;

    }
    //changing menu to game scene
    public int currentLevel = 0; 
    //game to menu
    public int menuFocus = 0; 
}
