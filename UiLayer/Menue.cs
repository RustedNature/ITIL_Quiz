using BuisinessLayer;
using System.Security.Cryptography;
namespace UiLayer
{
    public class Menue
    {
        static readonly string title =
            "IIIIIIIIIIIIII       TTTTTTTTTTTTTTTTT             IIIIIIIIIIIII         LLLLL\n" +
            "IIIIIIIIIIIIII       TTTTTTTTTTTTTTTTT             IIIIIIIIIIIII         LLLLL\n" +
            "     IIIII                 TTTT                        IIIII             LLLLL\n" +
            "     IIIII                 TTTT                        IIIII             LLLLL\n" +
            "     IIIII                 TTTT                        IIIII             LLLLL\n" +
            "     IIIII                 TTTT                        IIIII             LLLLL\n" +
            "     IIIII                 TTTT                        IIIII             LLLLL\n" +
            "IIIIIIIIIIIIII             TTTT                    IIIIIIIIIIIII         LLLLLLLLLLLLL\n" +
            "IIIIIIIIIIIIII             TTTT                    IIIIIIIIIIIII         LLLLLLLLLLLLL\n";
        private static bool valid_input = false;
        static int RightAnswerCount { get; set; } = 0;

        static List<Question> questions = new();

        public static void Start()
        {
            List<string> choices;
            choices = PrintAndGetChoices();
            GetQuestionsFromChoice(choices);
            int choosed_questions_count = GetQuestionCount();

            if (choosed_questions_count != questions.Count)
            {
                GetRandomQuestionsOfCount(choosed_questions_count);
            }

            RandomSortQuestions();
            StartQuestionRound();

        }

        private static void GetRandomQuestionsOfCount(int choosed_questions_count)
        {
            List<Question> randoms_choosed_count = new();

            for (int i = 0; i < choosed_questions_count; i++)
            {
                Question q;
                do
                {
                    q = questions[RandomNumberGenerator.GetInt32(questions.Count)];

                } while (randoms_choosed_count.Contains(q));
                randoms_choosed_count.Add(q);
            }

            questions = randoms_choosed_count;
        }

        private static int GetQuestionCount()
        {
            bool valid_question_count;
            int parsed_input = 0;
            do
            {
                Console.Clear();
                Console.WriteLine($"Wie viel Fragen möchten Sie beantworten? Wählen Sie eine Zahl zwischen 1 und {questions.Count} aus!");
                Console.WriteLine();
                Console.Write("Ihre Eingabe: ");
                string? input = Console.ReadLine();
                input ??= "0";


                try
                {
                    parsed_input = int.Parse(input);
                }
                catch (Exception)
                {

                    parsed_input = 0;
                }

                if (parsed_input >= 1 && parsed_input <= questions.Count)
                {
                    valid_question_count = true;
                }
                else
                {
                    valid_question_count = false;
                }
            } while (valid_question_count is false);
            return parsed_input;
        }

        private static List<string> PrintAndGetChoices()
        {
            List<string> choices;
            do
            {
                PrintTitle();
                Console.WriteLine("Herzlich Wilkommen in einem der besten ITIL Trainer auf dem ganzen Mars");
                Console.WriteLine();
                Console.WriteLine($"[1] Prüfungsfragen, Anzahl: {QuestionCollection.PQuestions.Count}");
                Console.WriteLine($"[2] AP Fragen, Anzahl: {QuestionCollection.APQuestions.Count}");
                Console.WriteLine($"[3] M Fragen, Anzahl: {QuestionCollection.MQuestions.Count}");
                Console.WriteLine($"[4] F Fragen, Anzahl: {QuestionCollection.FQuestions.Count}");
                Console.WriteLine($"[5] E Fragen, Anzahl: {QuestionCollection.ExamQuestions.Count}");
                Console.WriteLine();
                Console.WriteLine("Bitte wählen Sie eine oder mehrere Kategorien aus, seperiere mehrere Kategorien mit einem Komma(,)!");
                Console.WriteLine();
                Console.Write("Ihre Eingabe: ");


                valid_input = true;
                string? input = Console.ReadLine();
                choices = GetChoicesFrom(input);

                if (valid_input is false)
                {
                    Console.WriteLine("Ungültige eingabe. Beliebige Taste drücken um die Eingabe zu wiederholen!");
                    Console.ReadKey();
                }
            } while (valid_input is false);
            return choices;
        }

        private static void PrintTitle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.White;

        }

        private static void RandomSortQuestions()
        {
            questions = questions.OrderBy(item => Guid.NewGuid()).ToList();
        }

        private static void StartQuestionRound()
        {
            bool valid_answer;
            string? answer;
            uint question_cnt = 0;
            foreach (Question question in questions)
            {
                question_cnt++;
                do
                {

                    PrintOuestionGetAnswer(out valid_answer, out answer, question_cnt, question);

                } while (valid_answer is false);

                CheckAnswer(answer, question);

            }

            ShowStatistics();

        }

        private static void ShowStatistics()
        {
            PrintTitle();
            Console.WriteLine();
            float percent_right;
            if (RightAnswerCount == 0)
            {
                percent_right = 0;
            }
            else
            {
                percent_right = (float)RightAnswerCount / (float)questions.Count * 100;

            }
            Console.WriteLine($"Du hast {RightAnswerCount} von {questions.Count} Fragen richtig! Das sind {percent_right:F2}%");
            Thread.Sleep(3000);
            Console.ReadKey();
        }

        private static void CheckAnswer(string? answer, Question question)
        {
            answer = ConvertAnswer(answer);
            if (answer == question.RightAnswer)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                RightAnswerCount++;
                Console.WriteLine("RICHTIG");
                Console.WriteLine("Beliebige Taste drücken um fortzufahren!");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Leider falsch, die richtige Antwort wäre {question.RightAnswer} gewesen");
                Console.WriteLine("Beliebige Taste drücken um fortzufahren!");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;

            }
        }

        private static string? ConvertAnswer(string? answer)
        {
            return answer switch
            {
                "1" => "a",
                "2" => "b",
                "3" => "c",
                "4" => "d",
                _ => answer,
            };
        }

        private static void PrintOuestionGetAnswer(out bool valid_answer, out string? answer, uint question_cnt, Question question)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine($"Frage {question_cnt} von {questions.Count}\n");
            Console.WriteLine(question.QuestionText);
            Console.WriteLine();
            Console.Write("Ihre Eingabe: ");
            answer = Console.ReadLine();
            answer ??= "Nothing";
            valid_answer = ValidateAnswer(answer);
            if (valid_answer is false)
            {
                Console.WriteLine("");
            }
        }

        private static bool ValidateAnswer(string? answer)
        {
            return answer switch
            {
                "a" or "b" or "c" or "d" or "1" or "2" or "3" or "4" => true,
                _ => false,
            };
        }

        private static void GetQuestionsFromChoice(List<string> choices)
        {
            List<string> verwursted = new();
            foreach (string choice in choices)
            {
                if (verwursted.Contains(choice))
                {
                    continue;
                }
                verwursted.Add(choice);

                switch (choice)
                {
                    case "1":
                        CopyQuestionsFrom(QuestionCollection.PQuestions);
                        break;
                    case "2":
                        CopyQuestionsFrom(QuestionCollection.APQuestions);
                        break;
                    case "3":
                        CopyQuestionsFrom(QuestionCollection.MQuestions);
                        break;
                    case "4":
                        CopyQuestionsFrom(QuestionCollection.FQuestions);
                        break;
                    case "5":
                        CopyQuestionsFrom(QuestionCollection.ExamQuestions);
                        break;

                    default:
                        break;
                }
            }
        }

        private static void CopyQuestionsFrom(List<Question> questions)
        {
            foreach (Question question in questions)
            {
                Menue.questions.Add(question);
            }
        }

        private static List<string> GetChoicesFrom(string? input)
        {
            List<string> choices = new();
            if (input != null)
            {
                input = input.Trim();
                string[]? splitted = input.Split(',');
                foreach (string c in splitted)
                {
                    switch (c)
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                            choices.Add(c);
                            break;
                        case "q":
                        case "Q":
                            Environment.Exit(0);
                            break;
                        default:
                            valid_input = false;
                            break;
                    }
                }
            }
            return choices;
        }
    }
}