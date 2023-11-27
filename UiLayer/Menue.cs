using BusinessLayer;
namespace UiLayer
{
    public class Menue
    {
        static readonly string title =
            "IIIIIIIIIIIIII         TTTTTTTTTTTTTTTTT         IIIIIIIIIIIII         LLLLL\n" +
            "IIIIIIIIIIIIII         TTTTTTTTTTTTTTTTT         IIIIIIIIIIIII         LLLLL\n" +
            "     IIIII                   TTTT                    IIIII             LLLLL\n" +
            "     IIIII                   TTTT                    IIIII             LLLLL\n" +
            "     IIIII                   TTTT                    IIIII             LLLLL\n" +
            "     IIIII                   TTTT                    IIIII             LLLLL\n" +
            "     IIIII                   TTTT                    IIIII             LLLLL\n" +
            "IIIIIIIIIIIIII               TTTT                IIIIIIIIIIIII         LLLLLLLLLLLLL\n" +
            "IIIIIIIIIIIIII               TTTT                IIIIIIIIIIIII         LLLLLLLLLLLLL\n";




        public static void FlowLogic()
        {
            do
            {
                Console.Clear();
                PrintTitle();
                PrintAvailableQuestionTopics();
                GetUserTopicsChoice();
                GetQuestionsFromTopicChoices();

            } while (MenueController.ValidTopicChoices is false);
            do
            {
                PrintQuestionCountChoice();
                GetUserQuestionCountChoice();

            } while (MenueController.ValidQuestionCountChoice is false);

            GetQuestionsForQuiz();
            GetRandomSortedQuestions();
            StartQuiz();

        }

        private static void StartQuiz()
        {
            do
            {
                do
                {
                    PrintQuestionGetAnswer();

                } while (MenueController.ValidQuestionAnswer is false);

                if (MenueController.RightAnswer())
                {
                    PrintRightAnswerText();
                }
                else
                {
                    PrintWrongAnswerText();
                }
                Console.WriteLine();
                Console.WriteLine("Beliebige taste drücken um fortzufahren!");
                Console.ReadKey();

                MenueController.CheckForMoreQuestions();
                MenueController.IncrementIndexOfActualQuestionToAsk();
            } while (MenueController.MoreQuestions is true);


            PrintStatistics();

        }

        private static void PrintWrongAnswerText()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Leider nicht richtig, die Antwort wäre {MenueController.Questions[MenueController.IndexOfActualQuestionToAsk].RightAnswer.ToUpper()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void PrintRightAnswerText()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Jep, die Antwort ist {MenueController.Questions[MenueController.IndexOfActualQuestionToAsk].RightAnswer.ToUpper()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void PrintQuestionGetAnswer()
        {
            Console.Clear();
            Console.WriteLine($"Frage {MenueController.IndexOfActualQuestionToAsk + 1} von {MenueController.Questions.Count}");
            Console.WriteLine(MenueController.GetQuestionText());
            Console.WriteLine();
            Console.Write($"Ihre Antwort: ");
            MenueController.ValidateUserAnswer(Console.ReadLine());
        }

        private static void GetRandomSortedQuestions()
        {
            MenueController.RandomSortQuestions();
        }

        private static void GetQuestionsForQuiz()
        {
            MenueController.GetQuestionsForQuiz();
        }

        private static void GetUserQuestionCountChoice()
        {

            Console.Write("Ihre Eingabe: ");
            MenueController.ValidateUserQuestionCountChoice(Console.ReadLine());
        }

        private static void PrintQuestionCountChoice()
        {
            Console.Clear();
            Console.WriteLine($"Wie viel Fragen möchten Sie beantworten? Wählen Sie eine Zahl zwischen 1 und {MenueController.Questions.Count} aus!");
            Console.WriteLine();

        }

        private static void GetQuestionsFromTopicChoices()
        {
            MenueController.GetQuestionsFromTopics();
        }

        private static void GetUserTopicsChoice()
        {
            Console.Write("Ihre Eingabe: ");

            MenueController.ValidateUserTopicChoice(Console.ReadLine());

            if (MenueController.ValidTopicChoices is false)
            {
                Console.WriteLine("Ungültige eingabe. Beliebige Taste drücken um die Eingabe zu wiederholen!");
                Console.ReadKey();
            }
        }

        private static void PrintAvailableQuestionTopics()
        {
            Console.WriteLine("Herzlich Wilkommen in einem der besten ITIL Trainer auf dem Mars");
            Console.WriteLine();
            Console.WriteLine($"[1] Prüfungsfragen, Anzahl: {QuestionCollection.PQuestions.Count}");
            Console.WriteLine($"[2] AP Fragen, Anzahl: {QuestionCollection.APQuestions.Count}");
            Console.WriteLine($"[3] M Fragen, Anzahl: {QuestionCollection.MQuestions.Count}");
            Console.WriteLine($"[4] F Fragen, Anzahl: {QuestionCollection.FQuestions.Count}");
            Console.WriteLine($"[5] E Fragen, Anzahl: {QuestionCollection.ExamQuestions.Count}");
            Console.WriteLine();
            Console.WriteLine("Bitte wählen Sie eine oder mehrere Kategorien aus, seperiere mehrere Kategorien mit einem Komma(,)!");
            Console.WriteLine();
        }

        public static void StartProgramm()
        {
            Console.Title = "ITIL QUIZ";
            Question.CreateQuestionList();
            FlowLogic();
        }

        private static void PrintTitle()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void PrintStatistics()
        {
            PrintTitle();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"WOW! Du hat {MenueController.RightAnswers} von {MenueController.Questions.Count} richtig");
            Console.WriteLine($"Das sind {MenueController.GetPercentRightAnswer()}%!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}