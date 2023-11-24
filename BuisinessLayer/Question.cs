using System.Text.RegularExpressions;

namespace BuisinessLayer
{


    public class Question
    {
        public int QuestionNumber { get; }
        public string QuestionText { get; } = string.Empty;
        public QuestionOrigin QuestionOrigin { get; }
        public string RightAnswer { get; }

        private Question(int questionNumber, string questionText, QuestionOrigin questionOrigin, string rightAnswer)
        {
            QuestionNumber = questionNumber;
            QuestionText = questionText;

            RightAnswer = rightAnswer;
            QuestionOrigin = questionOrigin;
        }

        public void PrintQuestion()
        {
            Console.WriteLine(QuestionText);
        }

        public static void CreateQuestionList()
        {
            List<string> questions = ProcessQuestions.GetQuestionsList();
            Dictionary<int, string> solutions = ProcessSolutions.GetDictOfSolutionPairs();
            foreach (string question in questions)
            {
                int question_number;
                string question_text;
                QuestionOrigin question_origin;

                question_number = FindQuestionNumber(question);
                question_text = question;
                question_origin = FindQuestionQrigin(question);

                if (solutions.TryGetValue(question_number, out string? question_right_answer))
                {
                    //Console.WriteLine(question);
                    CreateQuestion(question_number, question_text, question_origin, question_right_answer);
                }
            }
        }

        private static void CreateQuestion(int question_number, string question_text, QuestionOrigin question_origin, string question_right_answer)
        {
            Question q = new(question_number, question_text, question_origin, question_right_answer);

            switch (question_origin)
            {
                case QuestionOrigin.AP:
                    QuestionCollection.AddApQuestion(q);
                    break;

                case QuestionOrigin.P:
                    QuestionCollection.AddPQuestion(q);
                    break;

                case QuestionOrigin.M:
                    QuestionCollection.AddMQuestion(q);
                    break;

                case QuestionOrigin.F:
                    QuestionCollection.AddFQuestion(q);
                    break;

                default:
                    QuestionCollection.AddExamQuestion(q);
                    break;
            }
        }

        private static QuestionOrigin FindQuestionQrigin(string question)
        {
            string origin = Regex.Match(question, @"\(.*\)").ToString().Trim();
            origin = origin[1..(origin.Length - 1)].ToLower();
            //Console.WriteLine($"{origin}");
            QuestionOrigin question_origin;
            if (origin.Contains("ap"))
            {
                question_origin = QuestionOrigin.AP;
            }
            else if (origin.Contains('p'))
            {
                question_origin = QuestionOrigin.P;
            }
            else if (origin.Contains('f'))
            {
                question_origin = QuestionOrigin.F;
            }
            else if (origin.Contains('m'))
            {
                question_origin = QuestionOrigin.M;
            }
            else
            {
                question_origin = QuestionOrigin.Examtopics;
            }

            return question_origin;
        }

        private static int FindQuestionNumber(string question)
        {
            string number_str = Regex.Match(question, @" \d{1,3} ").ToString().Trim();
            //Console.WriteLine($"{number_str}");
            return int.Parse(number_str);
        }
    }

    public enum QuestionOrigin
    {
        P,
        AP,
        F,
        M,
        Examtopics,
    }
}