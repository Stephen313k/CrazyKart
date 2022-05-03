using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public static SaveManager Instance{set; get;}
    //accessing script
    public SaveState state;
	// Use this for initialization

	private void Awake () {
        ResetSave();
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();

        Debug.Log(IsColourOwned(4));
        UnlockColour(4);
        Debug.Log(IsColourOwned(4));

    }

    //save the state script to player preference
    public void Save()
    {
        //send the state to the string using serializer
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    //load previous save state
    public void Load()
    {
        //read save if there is one
        if(PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("no file foudn");
        }
    }

    //check if color is owned
    public bool IsColourOwned(int index)
    {
        //check if the bit is set, colour is owned if so
        return (state.colourOwned & (1 << index)) != 0 ;
    }

    //check if trail is owned
    public bool IsTrailOwned(int index)
    {
        //check if the bit is set, colour is owned if so
        return (state.trailOwned & (1 << index)) != 0;
    }

    //player buying a colour
    public bool BuyColour(int index, int cost)
    {
        if(state.gold >= cost)
        {
            //if user has enough gold buy the colour
            state.gold -= cost;
            UnlockColour(index);

            Save();

            return true;
       }
        else
        {
            //not enough gold
            return false;
        }
    }

    //player buying a trail
    public bool BuyTrail(int index, int cost)
    {
        if (state.gold >= cost)
        {
            //if user has enough gold buy the colour
            state.gold -= cost;
            UnlockTrail(index);

            Save();

            return true;
        }
        else
        {
            //not enough gold
            return false;
        }
    }

    //unlock a colour
    public void UnlockColour(int index)
    {
        //toggle the bit at the index
        state.colourOwned |= 1 << index;
    }

    //unlock a trail
    public void UnlockTrail(int index)
    {
        //toggle the bit at the index
        state.trailOwned |= 1 << index;
    }

    //complete level
    public void CompleteLevel(int index)
    {
        //if its the active level
        if(state.completedLevel == index)
        {   
            //level completetion counter
            state.completedLevel++;
            Save();
        }
    }
    //resets save file
    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
    }
}
