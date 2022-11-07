using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class toggleAudioListener : MonoBehaviour
{
    public Material notSky;
    public Material Sky;
    Camera camera;
    /*[SerializeField] private LayerMask everyLayer;
    LayerMask smellLayer;*/
    /*[SerializeField]*/ private bool muted;
    /*[SerializeField]*/ private bool congested;
    /*[SerializeField]*/ private bool blind;
    private int SmellsLayer;
    private int VisibleLayer;
    private float mouseY;
    private float mouseX;
    public float angle;
    public SC_FPSController controller;

    void Start()
    {
        //ears = GetComponent<AudioListener>();
        camera = GetComponent<Camera>();
        print(AudioListener.volume);
        blind = false;
        muted = false;
        congested = false;
        SmellsLayer = 1 << LayerMask.NameToLayer("Smells");
        VisibleLayer = (1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("WhatIsGround"));
        Debug.Log(LayerMask.NameToLayer("Default"));
        /*everyLayer = LayerMask.NameToLayer("Smells");
        Debug.Log(everyLayer + "hi");
        smellLayer = LayerMask.NameToLayer("Smells");*/
    }

    void Update()
    {

        if (Input.GetKey("q"))
        {
            controller.canMove = false;
            SetSence();
        }
        else
        {
            controller.canMove = true;
        }

        /* if (Input.GetKeyDown("g") && muted)
         {
             AudioListener.volume = 1;
             muted = false;
             print("Volume set to " + AudioListener.volume);
         }
        */
    }
    private void SetSence()
    {
        //Debug.Log("here");
        controller.canMove = false;
        mouseY = Input.GetAxis("Mouse Y");
        mouseX = Input.GetAxis("Mouse X");
        if (mouseY != 0 || mouseX != 0)
        {
            angle = Mathf.Atan2(mouseY, mouseX) / Mathf.PI;
            //to optimize make this(vvv) a state machine
            if (angle >= (2f / 3f))
            {
                setSight(true);
                setHearing(false);
                setSmell(false);
            }
            else if (angle >= (1f / 3f))
            {
                setSight(false);
                setHearing(true);
                setSmell(false);
            }
            else if (angle >= 0)
            {
                setSight(false);
                setHearing(false);
                setSmell(true);
            }
        }
    }

    private void setSight(bool canSee)
    {
        if (canSee)
        {
            blind = false;
            RenderSettings.skybox = Sky;
            camera.cullingMask = VisibleLayer | camera.cullingMask;
        }
        else
        {
            blind = true;
            RenderSettings.skybox = notSky;
            camera.cullingMask = ~VisibleLayer & camera.cullingMask;
        }
    }

    private void setHearing(bool canHear)
    {
        if (canHear)
        {
            muted = false;
            AudioListener.volume = 1;
            Debug.Log("Volume set to " + AudioListener.volume);
        }
        else
        {            
            muted = true;
            AudioListener.volume = 0;
            Debug.Log("Volume set to " + AudioListener.volume);
        }
    }

    private void setSmell(bool canSmell)
    {
        if (canSmell)
        {
            congested = false;
            camera.cullingMask = SmellsLayer | camera.cullingMask;
        }
        else
        {
            congested = true;
            camera.cullingMask = ~SmellsLayer & camera.cullingMask;
        }
    }
}
