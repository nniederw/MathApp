using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(RectTransform))]
public class QuestionGenerator : MonoBehaviour
{
    [SerializeField] private TMP_Text QuestionNamePrefab = null;
    [SerializeField] private double IdealWithToHightRatio = 3.0;
    [SerializeField] private float QuestionHeightPercentage = 0.15f;
    private const float PaddingPercentage = 0.05f;
    [SerializeField] private Button DefaultButton = null;
    private RectTransform _RTransform;
    private RectTransform RTransform { get { if (_RTransform == null) { _RTransform = GetComponent<RectTransform>(); } return _RTransform; } }
    public void GenerateExampleQuestion()
    {
        GenerateQuestion("1 + 1", "one plus one", new List<string> { "1", "2", "3", "0" }, "2");
    }
    public void GenerateQuestion(string question, string name, List<string> answers, string rightAnswer)
    {
        GenerateQuestion(question, name, answers, rightAnswer, delegate () { Debug.Log("you lose"); }, delegate () { Debug.Log("you win"); });
    }
    public void GenerateQuestion(Question question, Action lose, Action win) => GenerateQuestion(question.Description, question.Name, question.Answers, question.RightAnswer, lose, win);
    public void GenerateQuestion(string question, string name, List<string> answers, string rightAnswer, Action lose, Action win)
    {
        GameObject parent = new GameObject();
        parent.AddComponent(typeof(RectTransform));
        parent.transform.SetParent(transform);
        parent.GetComponent<RectTransform>().localPosition = Vector3.zero;
        float height = QuestionHeightPercentage * RTransform.rect.height;
        var title = Instantiate(QuestionNamePrefab);
        title.transform.SetParent(parent.transform);
        title.transform.localScale = new Vector3(1, 1, 1);
        title.transform.localPosition = new Vector3(0, RTransform.rect.height * (0.5f - QuestionHeightPercentage / 2));
        title.rectTransform.sizeDelta = new Vector2(RTransform.sizeDelta.x, height);
        title.GetComponent<TMP_Text>().text = question;
        float padding = PaddingPercentage * RTransform.rect.height;
        var buttons = CreateButons(answers.Count, RTransform.rect.height - height - padding, RTransform.rect.width, new Vector2(0, -(padding + height) / 2), parent.transform);
        for (int i = 0; i < answers.Count; i++)
        {
            buttons[i].GetComponentInChildren<TMP_Text>().text = answers[i];
            buttons[i].onClick.AddListener(answers[i] == rightAnswer
                ? (UnityAction)(() => { Destroy(parent); win(); })
                : (UnityAction)(() => { Destroy(parent); lose(); })); //dont remove the casts. Unity has some problems otherwise
        }
    }
    private List<Button> CreateButons(int quantity, float BoundHeight, float BoundWidth, Vector2 offset, Transform parent)
    {
        var buttons = new List<Button>();
        for (int i = 0; i < quantity; i++)
        {
            buttons.Add(Instantiate(DefaultButton, parent));
        }
        if (quantity > 1)
        {
            int horizontal = 1;
            int vertical = 1;
            for (int i = 0; horizontal * vertical < quantity && quantity != horizontal * vertical && i < 1000; i++)
            {
                double ratio = ((double)BoundWidth / horizontal) / ((double)BoundHeight / vertical);
                if (ratio > IdealWithToHightRatio) { horizontal++; }
                else { vertical++; }
            }
            float width = BoundWidth / horizontal;
            float height = BoundHeight / vertical;
            for (int h = 0, i = 0; h < horizontal && i < buttons.Count; h++)
            {
                for (int v = 0; v < vertical && i < buttons.Count; i++, v++)
                {
                    var rect = buttons[i].GetComponent<RectTransform>();
                    rect.sizeDelta = new Vector2(width, height);
                    rect.localPosition = new Vector3(-BoundWidth / 2f + width / 2f + width * h, BoundHeight / 2f - height / 2f - height * v);
                    rect.localPosition += new Vector3(offset.x, offset.y);
                }
            }
        }
        return buttons;
    }
}
