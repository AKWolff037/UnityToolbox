using UnityEngine;
using System.Collections;

public class Loader : Singleton<Loader> {

	// Use this for initialization
	public override void Awake () 
    {
        base.Awake();
        Init();
	}

    void Init()
    {
        if (GameManager._instance == null)
        {
            GameManager._instance = GetComponent<GameManager>();
        }
        if (SoundManager._instance == null)
        {
            SoundManager._instance = GetComponent<SoundManager>();
        }
        if (InputManager._instance == null)
        {
            InputManager._instance = GetComponent<InputManager>();
        }
    }
}
