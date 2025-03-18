using TMPro;
using UnityEngine;

public class WorldButtons : MonoBehaviour
{
    public EnvironmentStuff environmentStuff;
    public EditWorld editWorld;

    public GameObject worldSelectScreen;
    public GameObject worldCreateScreen;
    public GameObject worldEditor;
    
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject plusButton;

    public TMP_Text world1Name;
    public TMP_Text world2Name;
    public TMP_Text world3Name;
    public TMP_Text world4Name;
    public TMP_Text world5Name;

    public void RevealButtons()
    {
        Debug.Log("Start Reveaing Buttons! " + environmentStuff.UsersEnvironments.Count);


        if (environmentStuff.UsersEnvironments.Count >= 1)
        {
            Debug.Log("Button 1");
            button1.SetActive(true);
            world1Name.SetText(environmentStuff.UsersEnvironments[0].name);

            if(environmentStuff.UsersEnvironments.Count >= 2)
            {
                Debug.Log("Button 2");
                button2.SetActive(true);
                world2Name.SetText(environmentStuff.UsersEnvironments[1].name);

                if (environmentStuff.UsersEnvironments.Count >= 3)
                {
                    Debug.Log("Button 3");
                    button3.SetActive(true);
                    world3Name.SetText(environmentStuff.UsersEnvironments[2].name);

                    if (environmentStuff.UsersEnvironments.Count >= 4)
                    {
                        Debug.Log("Button 4");
                        button4.SetActive(true);
                        world4Name.SetText(environmentStuff.UsersEnvironments[3].name);

                        if (environmentStuff.UsersEnvironments.Count >= 5)
                        {
                            Debug.Log("Button 5");
                            button5.SetActive(true);
                            world5Name.SetText(environmentStuff.UsersEnvironments[4].name);
                        }
                        else
                        {
                            plusButton.transform.position = new Vector3(960, 440);
                            plusButton.SetActive(true);
                        }
                    }
                    else
                    {
                        plusButton.transform.position = new Vector3(360, 440);
                        plusButton.SetActive(true);
                    }
                }
                else
                {
                    plusButton.transform.position = new Vector3(1560, 840);
                    plusButton.SetActive(true);
                }
            }
            else
            {
                plusButton.transform.position = new Vector3(960, 840);
                plusButton.SetActive(true);
            }
        }
        else
        {
            plusButton.transform.position = new Vector3(360, 440);
            plusButton.SetActive(true);
        }

    }

    public void GoToEditor(int worldNumber)
    {
        editWorld.currentEnvironment = environmentStuff.UsersEnvironments[worldNumber];
        worldSelectScreen.SetActive(false);
        worldEditor.SetActive(true);
    }

    public void GoToWorldCreateScreen()
    {
        worldSelectScreen.SetActive(false);
        worldCreateScreen.SetActive(true);
    }
}
