using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager> {
    
    public float defaultInputDelay;
    public float inputDelay;
        
    private float delayTime = 0.0f;

    [HideInInspector]
    public bool isEnterPressed { get; private set; }
    [HideInInspector]
    public bool isCancelPressed { get; private set; }
    [HideInInspector]
    public bool isLeftPressed { get; private set; }
    [HideInInspector]
    public bool isRightPressed { get; private set; }
    [HideInInspector]
    public bool isUpPressed { get; private set; }
    [HideInInspector]
    public bool isDownPressed { get; private set; }
    [HideInInspector]
    public bool isMenuPressed { get; private set; }
    [HideInInspector]
    public float horizontal { get; private set; }
    [HideInInspector]
    public float vertical { get; private set; }
    [HideInInspector]
    public bool isPausePressed { get; private set; }
	// Use this for initialization
	public override void Awake () 
    {
        base.Awake();
        InitValues();
	}
	private void InitValues()
    {
        ResetInput();
    }
    private void ResetInput()
    {
        horizontal = 0.0f;
        vertical = 0.0f;
        isEnterPressed = false;
        isCancelPressed = false;
        isLeftPressed = false;
        isRightPressed = false;
        isUpPressed = false;
        isDownPressed = false;
        isMenuPressed = false;
        isPausePressed = false;
    }
	
	void Update () 
    {
        ResetInput();    
	    if(Time.time >= delayTime)
        {            
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            //isEnterPressed = Input.GetButtonUp("Accept");
            //isCancelPressed = Input.GetButtonUp("Cancel");
            //isMenuPressed = Input.GetButtonUp("Menu");
            //isPausePressed = Input.GetButtonUp("Pause");
            bool somethingWasPressed = false;
            if(horizontal < 0)
            {
                isLeftPressed = true;
            }
            if(horizontal > 0)
            {
                isRightPressed = true;
            }
            if(vertical < 0)
            {
                isDownPressed = true;
            }
            if(vertical > 0)
            {
                isUpPressed = true;
            }
            somethingWasPressed = (horizontal != 0 || vertical != 0 || isEnterPressed || isCancelPressed || isMenuPressed || isPausePressed);

            if (somethingWasPressed)
            {
                delayTime = Time.time + inputDelay;
            }
        }
	}    
}
