using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text;
using System.Text.RegularExpressions;

namespace itil_questions
{
    internal class ReadFiles
    {
        static readonly string question_file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdfs", "questions.txt");
        static readonly string solution_file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdfs", "solutions.txt");
        public static List<string> GetQuestionsList(string questions_content)
        {
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
            string questions_content = File.ReadAllText(question_file_path, Encoding.UTF8);
            //questions_content = Regex.Replace(questions_content, @"Aufgabensammlung.*R-D Härter", "");
            //questions_content = Regex.Replace(questions_content, @"Aufgabensammlung.*Seite 82", "");
            //questions_content = Regex.Replace(questions_content, @"\(Eine richtige Antwort ankreuzen\)", "");
            //questions_content = Regex.Replace(questions_content, @".*?ITIL 4", "");
            //questions_content = Regex.Replace(questions_content, @".*?Foundation", "");
            //questions_content = Regex.Replace(questions_content, @".*?15\.12\.2022", "");
            //questions_content = Regex.Replace(questions_content, @".*?Peoplecert", "");
            //questions_content = Regex.Replace(questions_content, @".*?mITSM", "");
            //questions_content = Regex.Replace(questions_content, @".*?Fragenbuch", "");
            //questions_content = Regex.Replace(questions_content, @".*?Präsentation", "");
            //questions_content = Regex.Replace(questions_content, @".*?Examtopics", "");
            //Console.WriteLine(questions_content);
            return questions_content.TrimEnd().TrimStart();
        }

        public static string ExtractPdfContent(string path)
        {
            PdfDocument questions = new(new PdfReader(path));

            StringBuilder stringBuilder = new();

            for (int i = 1; i <= questions.GetNumberOfPages(); i++)
            {
                PdfPage page = questions.GetPage(i);

                stringBuilder.Append(PdfTextExtractor.GetTextFromPage(page));
            }

            return stringBuilder.ToString();
        }

        public static MatchCollection GetSolutionRegexMatches()
        {
            string solutions_content = File.ReadAllText(solution_file_path, Encoding.UTF8);

            string modified_solutions_content = Regex.Replace(solutions_content, @"\(.*\)", "");
            modified_solutions_content = Regex.Replace(modified_solutions_content, @"[\n\r\t\s+]", "");
            MatchCollection seperated_soulutions = Regex.Matches(modified_solutions_content, @"\d{1,3}[a-zA-Z]");
            return seperated_soulutions;
        }

        public static Dictionary<int, string> GetDictOfSolutionPairs(MatchCollection seperated_soulutions)
        {
            Dictionary<int, string> solutions = new();

            foreach (var match in seperated_soulutions)
            {
                string soulution = match.ToString().ToLower()[match.ToString().Length - 1].ToString();
                int solution_number = int.Parse(match.ToString()[..^1]);
                //Console.WriteLine($"{solution_number}: {soulution}");
                solutions.Add(solution_number, soulution);
            }

            return solutions;
        }
    }
}