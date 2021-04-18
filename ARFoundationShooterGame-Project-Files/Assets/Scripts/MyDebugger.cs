using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDebugger : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.XR.ARFoundation.ARSession m_Session;

    [SerializeField]
    private UnityEngine.UI.Text textField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(UnityEngine.XR.ARFoundation.ARSession.state);

            if (textField)
            {
                textField.text = UnityEngine.XR.ARFoundation.ARSession.state.ToString();
            }
        }
    }
}
