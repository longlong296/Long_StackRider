using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    Text coinText;
    public Slider tutorialSlider;
    private bool isSliding=true;

    // Start is called before the first frame update
    void Start()
    {
      coinText=  this.GetComponentInChildren<Text>();
       
            
    }

    // Update is called once per frame
    void LateUpdate()
    {
        coinText.text=PlayerController.score.ToString();
        
        if (tutorialSlider != null)
        {
            if (PlayerController.going == false) { 
                tutorialSlider.enabled=false;
                isSliding = false;
            }
            else
            {
                isSliding = true;
                if (isSliding = true) 
                { 
                    tutorialSlider.value = 0;
                }

            }
        }
    }
  
}
