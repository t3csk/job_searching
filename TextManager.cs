using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{   
    public void OnClickStartButton() 
    { 
        SceneManager.LoadScene("SampleScene"); 
    }
}

