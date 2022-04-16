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
    void Update()
    {
        coinText.text=PlayerController.score.ToString();
        if (tutorialSlider != null)
        {
            if (PlayerController.going == true) { 
                tutorialSlider.enabled=false;
                isSliding = false;
            }
            else
            {
                //tutorialSlider.value=0;
                if (tutorialSlider.value != 10)
                {
                    isSliding = true;
                    tutorialSlider.value=tutorialSlider.value*Time.deltaTime;
                }
                else
                {
                    isSliding = false;
                    
                }
            }
        }
    }
  
}
