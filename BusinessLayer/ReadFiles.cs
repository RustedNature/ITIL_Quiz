using System.Text;
using System.Text.RegularExpressions;
namespace BusinessLayer
{
    public class ReadFiles
    {
        static readonly string question_file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdfs", "questions.txt");
        static readonly string solution_file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdfs", "solutions.txt");
        public List<string> GetQuestionsList(string questions_content)
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




        public MatchCollection GetSolutionRegexMatches()
        {
            string solutions_content = File.ReadAllText(solution_file_path, Encoding.UTF8);

            string modified_solutions_content = Regex.Replace(solutions_content, @"\(.*\)", "");
            modified_solutions_content = Regex.Replace(modified_solutions_content, @"[\n\r\t\s+]", "");
            MatchCollection seperated_soulutions = Regex.Matches(modified_solutions_content, @"\d{1,3}[a-zA-Z]");
            return seperated_soulutions;
        }

        public Dictionary<int, string> GetDictOfSolutionPairs(MatchCollection seperated_soulutions)
        {
            Dictionary<int, string> solutions = new();

            foreach (var match in seperated_soulutions)
            {
                string soulution = match.ToString().ToLower()[match.ToString().Length - 1].ToString();
                int solution_number = int.Parse(match.ToString()[..^1]);
                solutions.Add(solution_number, soulution);
            }

            return solutions;
        }
    }
}