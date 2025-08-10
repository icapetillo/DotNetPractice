using System.Text.RegularExpressions;

namespace FieldExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //This will give us the full name path of the executable file:
            //i.e. C:\Program Files\MyApplication\MyApplication.exe
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //This will strip just the working path name:
            //C:\Program Files\MyApplication
            string strWorkPath = Path.GetDirectoryName(strExeFilePath);

            string fileLocation = strWorkPath + @"\source.txt"; //@"C:\Users\icaps\OneDrive\Escritorio\test.txt";
            string pathToCsv = strWorkPath + @"\fieldMap.csv"; //@"C:\Users\icaps\OneDrive\Escritorio\fieldMap.csv"; 
            Dictionary<string, string> fieldMap = new Dictionary<string, string>();

            //Get code lines that work with fields
            fieldMap.Add("PDF Field", "DB Field");
            IEnumerable<string> txtLines = File.ReadAllLines(fileLocation);
            foreach (string line in txtLines)
            {
                if (line.Trim().StartsWith("Reader.Process") || line.Trim().StartsWith("Reader.Default"))
                {
                    //Extract file names and create map
                    IEnumerable<Match> matches = Regex.Matches(line, "\"([^\"]*)\"");
                    string pdfField = matches.First().Value.Trim('"');
                    string dbField = matches.Last().Value.Trim('"');

                    if (!fieldMap.ContainsKey(pdfField))
                    {
                        fieldMap.Add(pdfField, dbField);
                    }
                }
            }

            //Create CSV file to dump field map
            String csv = String.Join(
                Environment.NewLine,
                fieldMap.Select(d => $"{d.Key},{d.Value}")
                );
            System.IO.File.WriteAllText(pathToCsv, csv);

            //Print field map to screen
            foreach (KeyValuePair<string, string> pair in fieldMap)
            {
                Console.WriteLine("PDF Field = {0}, DB Field = {1}", pair.Key, pair.Value);
            }
        }
    }
}
