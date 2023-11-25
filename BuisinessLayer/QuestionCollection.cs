namespace BusinessLayer
{
    public class QuestionCollection
    {

        public static List<Question> PQuestions { get; } = [];
        public static List<Question> APQuestions { get; } = [];
        public static List<Question> FQuestions { get; } = [];
        public static List<Question> MQuestions { get; } = [];
        public static List<Question> ExamQuestions { get; } = [];

        public static void AddApQuestion(Question q)
        {
            APQuestions.Add(q);
            // Console.WriteLine($"\nP:{PQuestions.Count}\nAP:{APQuestions.Count}\nF:{FQuestions.Count}\nM:{MQuestions.Count}\nE:{ExamQuestions.Count}\n");
        }

        public static void AddPQuestion(Question q)
        {
            PQuestions.Add(q);
            // Console.WriteLine($"\nP:{PQuestions.Count}\nAP:{APQuestions.Count}\nF:{FQuestions.Count}\nM:{MQuestions.Count}\nE:{ExamQuestions.Count}\n");
        }

        public static void AddMQuestion(Question q)
        {
            MQuestions.Add(q);
            // Console.WriteLine($"\nP:{PQuestions.Count}\nAP:{APQuestions.Count}\nF:{FQuestions.Count}\nM:{MQuestions.Count}\nE:{ExamQuestions.Count}\n");
        }

        public static void AddFQuestion(Question q)
        {
            FQuestions.Add(q);
            //Console.WriteLine($"\nP:{PQuestions.Count}\nAP:{APQuestions.Count}\nF:{FQuestions.Count}\nM:{MQuestions.Count}\nE:{ExamQuestions.Count}\n");
        }

        public static void AddExamQuestion(Question q)
        {
            ExamQuestions.Add(q);
            // Console.WriteLine($"\nP:{PQuestions.Count}\nAP:{APQuestions.Count}\nF:{FQuestions.Count}\nM:{MQuestions.Count}\nE:{ExamQuestions.Count}\n");
        }


    }

}
