using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class toggleAudioListener : MonoBehaviour
{
    public bool muted;
    
    void Start()
    {
        //ears = GetComponent<AudioListener>();
        print(AudioListener.volume);
        muted = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            if (!muted)
            {
                muted = !muted;
                AudioListener.volume = 0;
                print("Volume set to " + AudioListener.volume);

            }
            else
            {
                muted = !muted;
                AudioListener.volume = 1;
                print("Volume set to " + AudioListener.volume);
            }
        }
        
       /* if (Input.GetKeyDown("g") && muted)
        {
            AudioListener.volume = 1;
            muted = false;
            print("Volume set to " + AudioListener.volume);
        }
       */
    }
}
