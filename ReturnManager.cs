using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnManager : MonoBehaviour
{   
    public void OnClickReStartButton() 
    { 
        SceneManager.LoadScene("StartScene"); 
    }
}
