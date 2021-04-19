using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startGamePanel;
    public GameObject narrativePanel;

    // Start is called before the first frame update
    void Start()
    {
        narrativePanel.SetActive(false);
        startGamePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        startGamePanel.SetActive(false);
        narrativePanel.SetActive(true);
    }
}
