using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class QuestionSupplier : MonoBehaviour
{
    [SerializeField] private QuestionGenerator generator;
    private List<Func<Question>> SimpleQuestions = new List<Func<Question>>();
    private List<Func<Question>> SquareQuestions = new List<Func<Question>>();
    private Dictionary<QuestionMode, Func<Question>> GetQuestion = new Dictionary<QuestionMode, Func<Question>>();
    public static QuestionMode Mode = QuestionMode.Simple;
    public enum QuestionMode { Simple, Squares};
    private void Start()
    {
        SimpleQuestions.AddRange(new List<Func<Question>>
        {
            RIntPlus,
            RPIntPlus,
            RPIntMinus,
            RPIntMultiply
        });
        SquareQuestions.AddRange(new List<Func<Question>>
        {
            RIntSquare
        });
        GetQuestion.Add(QuestionMode.Simple, GetSimpleQuestion);
        GetQuestion.Add(QuestionMode.Squares, GetSquareQuestion);
        Win();
    }
    private void Lose()
    {
        SceneLoader.Menu();
    }
    private void Win()
    {
        generator.GenerateQuestion(GetQuestion[Mode](), Lose, Win);
    }
    private Question GetSquareQuestion() => SquareQuestions[MTRandom.Next(0, SquareQuestions.Count)]();
    private Question GetSimpleQuestion() => SimpleQuestions[MTRandom.Next(0, SimpleQuestions.Count)]();
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
    private Question RIntSquare()
    {
        var sqr = MTRandom.Next(2, 25);
        return SquareIntQuestion(new List<string>{ sqr.ToString()},new List<string> { UnicodeChars.Upper2 },sqr*sqr);
    }
    private Question SquareIntQuestion(List<string> terms, List<string> operations, int answer, int totalAnswers = 4)
    {
        List<string> answers = new List<string>();
        int righta = MTRandom.Next(0, totalAnswers);
        for (int i = 0; i < totalAnswers; i++)
        {
            answers.Add((answer + MTRandom.Next(-Math.Abs(answer), Math.Abs(answer) * 2) + MTRandom.Next(-5, 6)).ToString());
        }
        answers[righta] = answer.ToString();
        string result = "";
        for (int i = 0; i < terms.Count - 1; i++)
        {
            result += $"{terms[i]}{operations[i]} ";
        }
        result += operations.Count==terms.Count? $"{terms.Last()}{operations.Last()}" : $"{terms.Last()}";
        return new Question(result, "", answers, answer.ToString());
    }
}