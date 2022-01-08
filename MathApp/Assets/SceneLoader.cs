using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string QuestionsScene = "Questions";
    private const string MenuScene = "Menu";
    public static void Questions() => SceneManager.LoadScene(QuestionsScene);
    public static void Menu() => SceneManager.LoadScene(MenuScene);
}
