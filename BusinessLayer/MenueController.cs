


using System.Security.Cryptography;

namespace BusinessLayer
{
    public static class MenueController
    {
        public static bool ValidTopicChoices { get; set; } = false;
        public static bool ValidQuestionCountChoice { get; set; } = false;
        public static bool ValidQuestionAnswer { get; set; } = false;
        public static bool MoreQuestions { get; set; } = true;

        public static List<string> TopicChoices { get; set; } = [];
        public static List<Question> Questions { get; set; } = [];
        public static int QuestionCountChoice { get; set; } = 0;
        public static int IndexOfActualQuestionToAsk { get; set; } = 0;
        public static string QuestionAnswer { get; set; } = string.Empty;
        public static int RightAnswers { get; set; } = 0;

        private static void GetRandomQuestionsForCount()
        {
            List<Question> randoms_choosed_count = [];

            for (int i = 0; i < QuestionCountChoice; i++)
            {
                Question q;
                do
                {
                    q = Questions[RandomNumberGenerator.GetInt32(Questions.Count)];

                } while (randoms_choosed_count.Contains(q));
                randoms_choosed_count.Add(q);
            }

            Questions = randoms_choosed_count;
        }

        public static void GetQuestionsFromTopics()
        {
            if (ValidTopicChoices is false)
            {
                return;
            }

            List<Question> questions_to_add = [];
            foreach (string topic in TopicChoices)
            {
                List<string> already_added = [];

                if (already_added.Contains(topic))
                {
                    continue;
                }

                switch (topic)
                {
                    case "1":
                        questions_to_add = QuestionCollection.PQuestions;
                        break;
                    case "2":
                        questions_to_add = QuestionCollection.APQuestions;
                        break;
                    case "3":
                        questions_to_add = QuestionCollection.MQuestions;
                        break;
                    case "4":
                        questions_to_add = QuestionCollection.FQuestions;
                        break;
                    case "5":
                        questions_to_add = QuestionCollection.ExamQuestions;
                        break;
                    default:
                        break;
                }

                foreach (Question q in questions_to_add)
                {
                    Questions.Add(q);
                }
            }

            if (questions_to_add.Count == 0)
            {
                ValidTopicChoices = false;
            }



        }

        public static void ValidateUserQuestionCountChoice(string? user_question_count_choice)
        {
            if (user_question_count_choice == null)
            {
                return;
            }

            try
            {
                QuestionCountChoice = int.Parse(user_question_count_choice);
            }
            catch (Exception)
            {

                return;
            }

            if (QuestionCountChoice < 1 || QuestionCountChoice > Questions.Count)
            {
                return;
            }

            ValidQuestionCountChoice = true;
        }

        public static void ValidateUserTopicChoice(string? user_topic_choice)
        {
            if (user_topic_choice == null)
            {
                return;
            }

            List<string> choices = user_topic_choice.Split(",").ToList();
            bool all_elements_valid = true;
            foreach (string choice in choices)
            {
                switch (choice)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                        break;
                    default:
                        all_elements_valid = false;
                        break;
                }
            }

            if (all_elements_valid is false)
            {
                return;
            }
            else if (all_elements_valid is true)
            {
                ValidTopicChoices = true;
                TopicChoices = choices;
            }

        }

        public static void ValidateUserAnswer(string? user_answer)
        {
            ValidQuestionAnswer = false;
            if (user_answer == null)
            {
                return;
            }

            switch (user_answer)
            {
                case "1" or "a":
                case "2" or "b":
                case "3" or "c":
                case "4" or "d":
                    ValidQuestionAnswer = true;
                    break;
                default:
                    break;

            }

            if (ValidQuestionAnswer)
            {
                QuestionAnswer = user_answer;
            }
        }

        public static void GetQuestionsForQuiz()
        {
            if (QuestionCountChoice == Questions.Count)
            {
                return;
            }

            GetRandomQuestionsForCount();

        }

        public static void RandomSortQuestions()
        {
            Questions = Questions.OrderBy(item => Guid.NewGuid()).ToList();
        }

        public static string GetQuestionText()
        {
            return Questions[IndexOfActualQuestionToAsk].QuestionText;
        }

        public static bool RightAnswer()
        {
            ConvertAnswer();

            if (Questions[IndexOfActualQuestionToAsk].RightAnswer == QuestionAnswer)
            {
                RightAnswers++;
                return true;
            }
            return false;

        }

        private static void ConvertAnswer()
        {
            switch (QuestionAnswer)
            {
                case "1": QuestionAnswer = "a"; break;
                case "2": QuestionAnswer = "b"; break;
                case "3": QuestionAnswer = "c"; break;
                case "4": QuestionAnswer = "d"; break;
                default:
                    break;
            }
        }

        public static void CheckForMoreQuestions()
        {
            if (IndexOfActualQuestionToAsk == Questions.Count - 1)
            {
                MoreQuestions = false;
            }

        }

        public static void IncrementIndexOfActualQuestionToAsk()
        {
            ++IndexOfActualQuestionToAsk;
        }

        public static float GetPercentRightAnswer()
        {
            if (RightAnswers == 0)
            {
                return 0;
            }
            else
            {
                return (float)RightAnswers / (float)Questions.Count * 100;
            }
        }
    }
}
