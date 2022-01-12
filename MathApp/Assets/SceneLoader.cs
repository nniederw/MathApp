using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string QuestionScene = "Questions";
    private const string MenuScene = "Menu";
    public static void Simple()
    {
        QuestionSupplier.Mode = QuestionSupplier.QuestionMode.Simple;
        SceneManager.LoadScene(QuestionScene);
    }

    public static void Squares()
    {
        QuestionSupplier.Mode = QuestionSupplier.QuestionMode.Squares;
        SceneManager.LoadScene(QuestionScene);
    }
    public static void Menu() => SceneManager.LoadScene(MenuScene);
}
