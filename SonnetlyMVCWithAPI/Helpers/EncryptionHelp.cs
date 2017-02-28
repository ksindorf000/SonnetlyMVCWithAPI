using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SonnetlyMVCWithAPI.Helpers
{
    public class EncryptionHelp
    {
        private static int lineNum = 0;
        public static string results;
        
        private static string path = HttpContext.Current.Server.MapPath(@"\App_Data\TheSonnets.txt");
        
        public static string AndGo()
        {
            //Get lines
            List<string> lineList = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                lineNum = GetLineNum();
                lineList.Add(GetLine(lineNum));
            }

            //Get random words from linesList
            List<string> rWords = new List<string>();
            foreach (var line in lineList)
            {
                rWords.Add(GetRandWord(line));
            }

            //Put words together and return as string
            results = Results(rWords);

            return results;
        }

        /*****************************************
         * GetLineNum()
         * Gets line num based on digits in hash
         *****************************************/
        private static int GetLineNum()
        {            
            Random rng = new Random();
            int lineNum = rng.Next(1, 1231);

            return lineNum;
        }


        /*****************************************
         * GetLine()
         * Gets line based on hash 
         * Orders words in line, Desc
         *****************************************/
        private static string GetLine(int lineNum)
        {
            //Get Line from File
            string line = File.ReadLines(path).Skip(lineNum).Take(1).First();

            return line;
        }

        /*****************************************
         * GetRandWord()
         * Gets a random word from the given line
         *****************************************/
        private static string GetRandWord(string line)
        {
            string word;
            Random rng = new Random();

            List<string> sList = new List<string>(
                line.Split(new string[] { " " }, StringSplitOptions.None));

            int index = new Random().Next(sList.Count());
            word = sList[index];

            return word;
        }


        /*****************************************
         * Results()
         *****************************************/
        private static string Results(List<string> rWords)
        {
            string results = string.Join(" ", rWords.ToArray());
            
            results = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(results.ToLower());

            results = results.Replace(" ", "");

            return results;
        }
    }
}
