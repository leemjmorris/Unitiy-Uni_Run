using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//################################

//HomeWork
//Fix the game.
//Situation:
//  When the game is over, reference that gamemanager was having gets destroyed and starts to refer to null.
//  When the game restarts because the gamemanger is refering to null, the game doesnt work.
//  scrolling background and the platform stops, score count stops as well.
//  **** The homework is to fix this situation.
//  Hint: GameManger doesnt get destroyed like other components so you will have to find a way for your gamemanager to get newly created components refered.
//  Conclusion by teacher: please dont use Singleton on GameManager cuz it sucks. This homework was just for us to feel how bad singleton would be when matched with stuffs like gamemanager.

//################################

public class GameManager : MonoBehaviour //Change MonoBehavior to Singleton<GameManager> for the Homework.
{
    public static GameManager Instance; //Added this to fix the problems that occured after changing parent from Singleton to MonoBehaviour. Other scripts compiled errors occured. (counldnt find Gamemanger.Instance)
    public bool IsGameOver { get; private set; } //made this to let game manager decide if the game is over or not

    private int score; //need to save scores

    public TextMeshProUGUI scoreText; //need this to put score as a texted UI on the screen. we use TextMeshProUGUI just like we did before
    public GameObject gameOverUI; //this one as a Game Object to control the UI of the game over scene.

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (IsGameOver && Input.GetMouseButtonDown(0)) //Load the new game scene (restart) on mouse left click. only active when the game is over.
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //This is the typical way of restarting the game. by loading the active scene in this game. since we only have one scene in this game
        }
    }

    public void AddScore(int add) //adding score methods.
    {
        if (!IsGameOver) //only active when the game is active. not over.
        {
            score += add;
            scoreText.text = $"Score: {score}"; //adding text to the scoretext we announced with TextMeshProUGUI. to put the text on the screen.
        }
    }

    public void OnPlayerDead()
    {
        IsGameOver = true;
        gameOverUI.SetActive(true);
    }
}

//if NullException for the score UI happens, check with the inspector if you have placed the Text object into the gamemanager inspector.