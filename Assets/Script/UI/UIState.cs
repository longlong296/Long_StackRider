using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIState : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject closePanel;
    public Text scoreText;
  
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

    IEnumerator popUpText(string score, Color color, Text text, Vector3 direction,float fadeTime )
    {
        scoreText.text = score; 
        scoreText.color = color;    

        scoreText.transform.Translate(direction);
       
        yield return new WaitForSeconds(fadeTime);   
    }

}
