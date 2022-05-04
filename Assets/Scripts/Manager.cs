using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

   
	public static Manager Instance { set; get; }

    public Material playerMaterial;
    public Color[] playerColours = new Color[6];
    public GameObject[] playerTrails = new GameObject[6];

    //for touch
    private Dictionary<int, Vector2> activeTouches = new Dictionary<int, Vector2>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;

    }

  // private void Update() { transform.Translate(Input.acceleration.x, 0, 0); }
    //changing menu to game scene
    public int currentLevel = 0; 
    //game to menu
    public int menuFocus = 0;


    public Vector3 GetPlayerInput()
    {
            Vector3 a = Input.acceleration;
          //  a.y = a.z; // replace y axis 
            return a;
    }
    /*  //using the accelerometer
 //    if(SaveManager.Instance.state.usingAccelerometer)
     // {
      //    Vector3 a = Input.acceleration;
       //   a.y = a.z; // replace y axis 
      //    return a;
   //   }

       //screen touch control, read all touches
       Vector3 r = Vector3.zero;
       foreach(Touch touch in Input.touches)
       {
           //register first screen touch
           if(touch.phase == TouchPhase.Began)
           {
               activeTouches.Add(touch.fingerId, touch.position);
           }
           //register removing finger from screen
           else if(touch.phase == TouchPhase.Ended)
           {
               if(activeTouches.ContainsKey(touch.fingerId))
                   activeTouches.Remove(touch.fingerId);
           }
           //use delta if finger is sationary or moving
           else
           {
               float mag = 0;
               r = (touch.position - activeTouches[touch.fingerId] );
               //find magnitude player drags finger across 
               mag = r.magnitude / 100;
               r = r.normalized * mag;
           }
       }

       return r;

  }*/
}
