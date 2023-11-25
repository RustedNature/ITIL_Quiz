using DataLayer;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    internal static class ProcessQuestions
    {
        static readonly string question_file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "txts", "questions.txt");
        private static readonly Encoding file_encoding = Encoding.UTF8;


        public static List<string> GetQuestionsList()
        {
            string? questions_content = Data.ReadTxtFile(question_file_path, file_encoding);
            questions_content ??= "";
            List<string> questions_seperated = new();
            MatchCollection matchCollection = Regex.Matches(questions_content, @"Aufgabe (\d+)");
            for (int i = 0; i < matchCollection.Count; i++)
            {
                int first = matchCollection[i].Index;
                int second;
                if (i == matchCollection.Count - 1)
                {
                    second = questions_content.Length;
                }
                else
                {
                    second = matchCollection[i + 1].Index;
                }

                questions_seperated.Add(questions_content[first..(second - 1)].TrimEnd().TrimStart());
            }

            return questions_seperated;
        }

        public static string GetCleanQuestionsContent()
        {
            string? questions_content = Data.ReadTxtFile(question_file_path, file_encoding);
            questions_content ??= "";
            return questions_content.TrimEnd().TrimStart();
        }
    }
}
