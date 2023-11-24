using System.Text;

namespace DataLayer
{
    public static class Data
    {
        public static string? ReadTxtFile(string path, Encoding encoding)
        {
            try
            {
                return File.ReadAllText(path, encoding);
            }
            catch (Exception exceprion)
            {

                Console.WriteLine($"Fehler in ReadFile: {exceprion}");
                return null;
            }
        }

        public static void WriteTxtFile(string path, string content, Encoding encoding)
        {
            try
            {
                File.WriteAllText(path, content, encoding);
            }
            catch (Exception)
            {

                Console.WriteLine($"Fehler in WriteFile");
            }
        }
    }
}
