using System.Collections.Generic;
public class Question
{
    public string Description;
    public string Name;
    public List<string> Answers;
    public string RightAnswer;
    public Question(string question, string name, List<string> answers, string rightAnswer)
    {
        Description = question;
        Name = name;
        Answers = answers;
        RightAnswer = rightAnswer;
    }
}