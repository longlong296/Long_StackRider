using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject closePanel;
    
  
    // Update is called once per frame
    void LateUpdate()
    {
        if (PlayerController.winGame == true)
        {
            winPanel.SetActive(true);
        }
        if(PlayerController.lostGame == true)
        {
            losePanel.SetActive(true);
        }
    }
}
