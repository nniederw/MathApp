using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] public string QuestionsScene;
    public void LoadQuestions()
    {
        SceneManager.LoadScene(QuestionsScene);
    }
}
