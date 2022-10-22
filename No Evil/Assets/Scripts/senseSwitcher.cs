using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class senseSwitcher : MonoBehaviour
{
    public bool deaf;
    public bool blind;
    public bool canSmell;
    public Image img;

    void Start()    
    {
        print(AudioListener.volume);
        deaf = false;
        blind = false;
        GameObject canvas = GameObject.Find("Canvas");
        img = canvas.GetComponent<Image>();
    }

    void Update()
    {
        //deafens the character when key "g" is pressed
        if (Input.GetKeyDown("g"))
        {
            if (!deaf)
            {
                deaf = !deaf;
                AudioListener.volume = 0;
                print("Volume set to " + AudioListener.volume);

            }
            else
            {
                deaf = !deaf;
                AudioListener.volume = 1;
                print("Volume set to " + AudioListener.volume);
            }
        }

        //blinds the character when key "y" is pressed
        if (Input.GetKeyDown("y"))
        {
            blind = !blind;
            img.enabled = !img.enabled;
        }
    }


}

