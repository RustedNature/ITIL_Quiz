using DataLayer;
using System.Text;
using System.Text.RegularExpressions;
namespace BuisinessLayer
{
    public static class ProcessSolutions
    {
        private static readonly string solution_file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "txts", "solutions.txt");
        private static readonly Encoding file_encoding = Encoding.UTF8;


        private static MatchCollection GetSolutionRegexMatches()
        {
            string? solutions_content = Data.ReadTxtFile(solution_file_path, file_encoding);
            solutions_content ??= " ";
            string modified_solutions_content = Regex.Replace(solutions_content, @"\(.*\)", "");
            modified_solutions_content = Regex.Replace(modified_solutions_content, @"[\n\r\t\s+]", "");
            MatchCollection seperated_soulutions = Regex.Matches(modified_solutions_content, @"\d{1,3}[a-zA-Z]");

            return seperated_soulutions;
        }

        public static Dictionary<int, string> GetDictOfSolutionPairs()
        {
            MatchCollection seperated_soulutions = GetSolutionRegexMatches();
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