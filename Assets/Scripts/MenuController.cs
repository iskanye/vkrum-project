using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [SerializeField] private int editorScene;
    [SerializeField] private GameObject editorButton;
    [SerializeField] private RewindBall ball;

    void Awake()
    {
        if (PlayerPrefs.GetInt("editor", 0) == 1)
        {
            editorButton.SetActive(true);
        }   
    }

    IEnumerator Start()
    {
        ball.Initialize(JsonUtility.FromJson<LevelData>("{\"defaultBounciness\":0.8,\"gravityScale\":1.0,\"levelSize\":{\"x\":12.0,\"y\":10.0,\"z\":1.0},\"startVelocity\":{\"x\":5.0,\"y\":-3.0},\"ball\":{\"x\":5.0,\"y\":4.0},\"endPoint\":{\"x\":-5.0,\"y\":-4.0},\"panels\":[],\"panelRotations\":[],\"destroyablePanels\":[],\"destroyablePanelRotations\":[],\"spikes\":[],\"spikeRotations\":[],\"trajectoryPoints\":[],\"trajectoryPointRotations\":[],\"springs\":[],\"springRotations\":[],\"restrictions\":[0,0,0]}"));
        while (true)
        {
            yield return new WaitForSeconds(4f);
            ball.StartRewind();
            yield return new WaitForSeconds(4.05f);
            ball.StartRewind();
        }
    }

    public void Play()
    {
        LevelChanger.Current.NewGame();
    }

    public void Continue() 
    {
        LevelChanger.Current.Continue();
    }

    public void OpenEditor() 
    {
        SceneManager.LoadScene(editorScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
