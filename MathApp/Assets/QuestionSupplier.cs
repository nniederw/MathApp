using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class QuestionSupplier : MonoBehaviour
{
    [SerializeField] private QuestionGenerator generator;
    private List<Func<Question>> Questions = new List<Func<Question>>();
    private void Start()
    {
        Questions.AddRange(new List<Func<Question>>
        {
            RIntPlus,
            RPIntPlus,
            RPIntMinus,
            RPIntMultiply
        });
        Win();
    }
    private void Lose()
    {
        SceneLoader.Menu();
    }
    private void Win()
    {
        generator.GenerateQuestion(GetRandomQuestion(), Lose, Win);
    }
    private Question GetRandomQuestion() => Questions[MTRandom.Next(0, Questions.Count)]();
    /// <summary>
    /// Random Positve Int question with Plus 
    /// </summary>
    private Question RPIntPlus()
    {
        var first = MTRandom.Next(0, 11);
        var second = MTRandom.Next(0, 11);
        return SimpleIntQuestion(new List<int> { first, second }, new List<string> { "+" }, first + second);
    }
    /// <summary>
    /// Random Positve Int question with Minus 
    /// </summary>
    private Question RPIntMinus()
    {
        var first = MTRandom.Next(0, 11);
        var second = MTRandom.Next(0, 11);
        return SimpleIntQuestion(new List<int> { first, second }, new List<string> { "-" }, first - second);
    }
    /// <summary>
    /// Random Int question with Plus
    /// </summary>
    private Question RIntPlus()
    {
        var first = MTRandom.Next(-10, 11);
        var second = MTRandom.Next(-10, 11);
        return SimpleIntQuestion(new List<int> { first, second }, new List<string> { "+" }, first + second);
    }
    /// <summary>
    /// Random positive Int question with Multiply
    /// </summary>
    private Question RPIntMultiply()
    {
        var first = MTRandom.Next(0, 11);
        var second = MTRandom.Next(0, 11);
        return SimpleIntQuestion(new List<int> { first, second }, new List<string> { UnicodeChars.Dot }, first * second);
    }
    private Question SimpleIntQuestion(List<int> terms, List<string> operations, int answer, int totalAnswers = 4)
    {
        List<string> answers = new List<string>();
        int righta = MTRandom.Next(0, totalAnswers);
        for (int i = 0; i < totalAnswers; i++)
        {
            answers.Add((answer + MTRandom.Next(-Math.Abs(answer), Math.Abs(answer) * 2)+MTRandom.Next(-5,6)).ToString());
        }
        answers[righta] = answer.ToString();
        string result = "";
        for (int i = 0; i < terms.Count - 1; i++)
        {
            result += $"{terms[i]} {operations[i]} ";
        }
        result += $"{terms.Last()}";
        return new Question(result, "", answers, answer.ToString());
    }
}
