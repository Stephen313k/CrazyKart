using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;

    public RectTransform menuContainer;
    public Transform levelPanel;

    public Transform colourPanel;
    public Transform trailPanel;

    public Text colourBuySetText;
    public Text trailBuySetText;
    public Text goldText;

    private MenuCamera menuCam;
   
    //5 items for colours and trails. setting the cost
    private int[] colourCost = new int[] { 0, 5, 5, 10, 15, 20, 25};
    private int[] trailCost = new int[] { 0, 10, 15, 20, 25, 30 };
    private int selectedColourIndex;
    private int selectedTrailIndex;

    private int activeColorIndex;
    private int activeTrailIndex;

    private Vector3 desiredMenuPosition;

    //variables menu animation
    public AnimationCurve enteringLevelZoomCurve;
    private bool isEnteringLevel = false;
    private float zoomDuration = 3.0f;
    private float zoomTransition;

    private void Start()
    {
        SaveManager.Instance.state.gold = 100; ////////////////

        //find menu camera and assign it
        menuCam = FindObjectOfType<MenuCamera>();

        //position to focused menu
        SetCameraTo(Manager.Instance.menuFocus);
        //display the gold amount
        UpdateGoldText();
        //grab the canvas group
        fadeGroup = FindObjectOfType<CanvasGroup>();
        //coloured screen
        fadeGroup.alpha = 1;

        //button events below
        InitShop();

        InitLevel();

        //player preferences trail and colour
        OnColourSelect(SaveManager.Instance.state.activeColour);
        SetColour(SaveManager.Instance.state.activeColour);

        OnTrailSelect(SaveManager.Instance.state.activeTrail);
        SetTrail(SaveManager.Instance.state.activeTrail);

        //make butttons bigger for default trail and colour

        trailPanel.GetChild(SaveManager.Instance.state.activeTrail).GetComponent<RectTransform>().localScale = Vector3.one * 1.15f;
        colourPanel.GetChild(SaveManager.Instance.state.activeColour).GetComponent<RectTransform>().localScale = Vector3.one * 1.15f;

    }

    private void Update()
    {
        //fade in
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
        //menu navigation
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
        //entering level aninmation
        if(isEnteringLevel)
        {
            //add zoom transition
            zoomTransition += (1 / zoomDuration) * Time.deltaTime;
            //change scale follow animation
            menuContainer.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 5, enteringLevelZoomCurve.Evaluate(zoomTransition));
            //position of canvas desired postion, zooms in the center
            Vector3 newDesiredPosition = desiredMenuPosition * 5;
            //specific level of canvas is selected 
            RectTransform rt = levelPanel.GetChild(Manager.Instance.currentLevel).GetComponent<RectTransform>();
            newDesiredPosition -= rt.anchoredPosition3D * 5;

            //override previous position
            menuContainer.anchoredPosition3D = Vector3.Lerp(desiredMenuPosition, newDesiredPosition, enteringLevelZoomCurve.Evaluate(zoomTransition));

            //fade white screen
            fadeGroup.alpha = zoomTransition;
            
            //is the animation complete
            if(zoomTransition >= 1)
            {
                //level is entered
                SceneManager.LoadScene("Game");
            }
        }
    }


    //picking level
    private void InitLevel()
    {
        //for all children under colour pannel, find button and onclick
        int i = 0;
        foreach (Transform t in levelPanel)
        {
            int currentIndex = i;
            //get button
            Button b = t.GetComponent<Button>();
            //use index
            b.onClick.AddListener(() => OnLevelSelect(currentIndex));
            Image img = t.GetComponent<Image>();
            //make sure level is unlocked
            if (i <= SaveManager.Instance.state.completedLevel)
            {
                //unlocked
                if (i == SaveManager.Instance.state.completedLevel)
                {
                    //not completed
                    img.color = Color.white;
                }
                else
                {
                    //level is completed
                    img.color = Color.green;
                }
            }
            else
            {
                //not unlocked
                b.interactable = false; //cant click
                img.color = Color.grey;
            }

            i++;
        }
        //reset index and do the same for trail button
        i = 0;
    }

  

    private void InitShop()
    {
        //for all children under colour pannel, find button and onclick
        int i = 0;
        foreach (Transform t in colourPanel)
        {
            int currentIndex = i;
            //get button
            Button b = t.GetComponent<Button>();
            //use index
            b.onClick.AddListener(() => OnColourSelect(currentIndex));
            //if colour is owned its clear
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsColourOwned(i) ? Color.white : new Color(0.5f, 0.5f, 0.5f);
            i++;
        }
        //reset index and do the same for trail button
        i = 0;
    
        foreach (Transform t in trailPanel)
        {
            int currentIndex = i;
            //get button
            Button b = t.GetComponent<Button>();
            //use index
            b.onClick.AddListener(() => OnTrailSelect(currentIndex));

            //if trail is owned its clear in the shop pannel
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsTrailOwned(i) ? Color.white : new Color(0.5f, 0.5f, 0.5f);

            i++;
        }
    }

    //setting what menu player is directed to
    public void SetCameraTo(int menuIndex)
    {
        NavigateTo(menuIndex);
        menuContainer.anchoredPosition3D = desiredMenuPosition;
    }

    //for transitioning the menus
    private void NavigateTo(int menuIndex)
    {
        switch (menuIndex)
        {   
            // 0 & default is main menu
            default:
            case 0:
                desiredMenuPosition = Vector3.zero;
                break;
            // 1 is play menu
            case 1:
                desiredMenuPosition = Vector3.right * 1280;
                //menu camera
                menuCam.MoveToLevel();
                break;
            //2 is shop menu
            case 2:
                desiredMenuPosition = Vector3.left * 1280;
                menuCam.MoveToShop();
                break;
        }
    }

    //equiping colour
    private void SetColour(int index)
    {
        //set active colour to index
        activeColorIndex = index;
        SaveManager.Instance.state.activeColour = index;
        //change color on player model

        //change buy/set button text
        colourBuySetText.text = "Current";

        //save the colour for when player restarts
        SaveManager.Instance.Save();

    }
    //equiping trail
    private void SetTrail(int index)
    {
        //set active trail to index
        activeTrailIndex = index;
        SaveManager.Instance.state.activeTrail = index;

        //change color on player model

        //change buy/set button text
        trailBuySetText.text = "Current";

        //save the colour for when player restarts
        SaveManager.Instance.Save();
    }

    private void UpdateGoldText()
    {
        //getting the amount of gold
        goldText.text = SaveManager.Instance.state.gold.ToString();
    }
    //BUTTONS    

    //selecting level
    private void OnLevelSelect(int currentIndex)
    {
        //load correct level using index
        Manager.Instance.currentLevel = currentIndex;
        //play animation
        isEnteringLevel = true;
        // level is loaded and displayed
        SceneManager.LoadScene("Game");

    }
    //selecting colour
    private void OnColourSelect(int currentIndex)
    {   //if is button clicked it's selected, exit
        if (selectedColourIndex == currentIndex)
            return;
        //make the scale bigger
        colourPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.15f;
        //put previous one on normal scale
        colourPanel.GetChild(selectedColourIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        selectedColourIndex = currentIndex;

        // change the state of the colour, buy/set button
        if (SaveManager.Instance.IsColourOwned(currentIndex))
        {
            //colour is owned
            //is it users current colour
            if(activeColorIndex == currentIndex)
            {
                colourBuySetText.text = "Current";
            }
            else
            {
                colourBuySetText.text = "Select";
            }
        }
        else
        {
            //colour is not owned
            colourBuySetText.text = "Buy: " + colourCost[currentIndex].ToString(); //return the colour using index

        }
    }
    //selecting trail
    private void OnTrailSelect(int currentIndex)
    {//if is button clicked it's selected, exit
        if (selectedTrailIndex == currentIndex)
            return;
        //make the scale bigger
        trailPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.15f;
        //put previous one on normal scale
        trailPanel.GetChild(selectedTrailIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        selectedTrailIndex = currentIndex;

        // change the state of the trail, buy/set button
        if (SaveManager.Instance.IsTrailOwned(currentIndex))
        {
            //trail is owned
            //is it users current trail
            if (activeTrailIndex == currentIndex)
            {
                trailBuySetText.text = "Current";
            }
            else
            {
                trailBuySetText.text = "Select";
            }
        }
        else
        {
            //trail is not owned
            trailBuySetText.text = "Buy: " + trailCost[currentIndex].ToString(); //return the trail using index

        }

    }

    public void OnBackClick()
    {
        NavigateTo(0);
        Debug.Log("backbutton");
    }
  
    public void OnPlayClick()
    {
        NavigateTo(1);
        Debug.Log("Play button");
    }

    public void OnShopClick()
    {
        NavigateTo(2);
        Debug.Log("Other button");
    }
    //when player buys from the shop
    public void OnColourBuySet()
    {
        //is selected colour owned
        if(SaveManager.Instance.IsColourOwned(selectedColourIndex))
        {
            //set the colour
            SetColour(selectedColourIndex);

        }
        else
        {
            //buy the colour
            if(SaveManager.Instance.BuyColour(selectedColourIndex, colourCost[selectedColourIndex]))
            {
                // bought, sets new colour
                SetColour(selectedColourIndex);


                //change colour of button in the pannel when bought
                colourPanel.GetChild(selectedColourIndex).GetComponent<Image>().color = Color.white;

                //update gold amount
                UpdateGoldText();

            }
            else
            {
                //not enough gold
            }
        }
    }

    public void OnTrailBuySet()
    {
        //is selected trail owned
        if (SaveManager.Instance.IsTrailOwned(selectedTrailIndex))
        {
            //set the trail
            SetTrail(selectedTrailIndex);

        }
        else
        {
            //buy the trail
            if (SaveManager.Instance.BuyTrail(selectedTrailIndex, trailCost[selectedTrailIndex]))
            {
                // bought, sets new trail
                SetTrail(selectedTrailIndex);

                //change colour of button in the pannel when bought
                trailPanel.GetChild(selectedTrailIndex).GetComponent<Image>().color = Color.white;

                //update gold amount
                UpdateGoldText();

            }
            else
            {
                //not enough gold
            }
        }
    }

}



