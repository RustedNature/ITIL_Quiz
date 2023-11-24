// See https://aka.ms/new-console-template for more information
using itil_questions;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Menue menue = new();
        string questions_content = ReadFiles.GetCleanQuestionsContent();
        List<string> questions_list = ReadFiles.GetQuestionsList(questions_content);

        MatchCollection seperated_soulutions = ReadFiles.GetSolutionRegexMatches();

        Dictionary<int, string> solution_pairs = ReadFiles.GetDictOfSolutionPairs(seperated_soulutions);

        Question.MatchQuestionAndSolutionsAndCreate(questions_list, solution_pairs);


        //string json = JsonConvert.SerializeObject(QuestionCollection.PQuestions, Formatting.Indented).ToString();

        Menue.Start();

        //Console.WriteLine(questions_content);
    }
}