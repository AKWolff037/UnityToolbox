using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager> {
    public GameStats GameStats;
	// Use this for initialization
	public override void Awake () 
    {
        base.Awake();
        InitGame();
	}

    private void OnLevelWasLoaded(int index)
    {
        InitGame();
    }

    void InitGame()
    {
            
    }
    
    public void StartNewGame()
    {
        Debug.Log("Starting new game...");
        SceneManager.LoadScene("NewGameSetup");
    }

    public void BeginGame()
    {
        Debug.Log("Starting main game...");
        var players = FindObjectsOfType<PlayerInformation>();
        GameStats.Players.Clear();
        foreach(var player in players)
        {
            player.Resources = new ResourceInformation();            
            if(player.Race == null) { player.Race = RaceFactory.GetRace(0); }
            GameStats.Players.Add(player);            
        }
        var player2 = gameObject.AddComponent<PlayerInformation>();
        player2.Resources = new ResourceInformation();
        player2.Race = RaceFactory.GetRace(0);
        player2.Name = "NOT ME YOU IDIOT";
        GameStats.Players.Add(player2);
        SceneManager.LoadScene("Main");
    }
	// Update is called once per frame
	void Update () 
    {

    }
}
