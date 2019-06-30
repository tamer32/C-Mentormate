using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentormateJSONAssignment
{
    class Program
    {
        
        public static List<NBAPlayers> nbaPlayers = new List<NBAPlayers>();
        public static List<NBAPlayers> specificNbaPlayers = new List<NBAPlayers>();
        private static void LoadJson(string pathToJsonFile)
        {
           
            using (StreamReader r = new StreamReader(pathToJsonFile))
            {
                string json = r.ReadToEnd();
                nbaPlayers = JsonConvert.DeserializeObject<List<NBAPlayers>>(json);
            }
          

        }
       
        public static int AgeDataValidation(out int value)
        {
            value = 0;
            try
            {
                value = int.Parse(Console.ReadLine());
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("You must give a number!");
                AgeDataValidation(out value);
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter integer");
                AgeDataValidation(out value);
            }
            finally
            {
                if(value > 30)
                {
                    //I have researched that and there were no players with more than 25-27 years of experience
                    Console.WriteLine("There cannot be player that has played more than 30 years");
                    AgeDataValidation(out value);
                }else if (value < 0)
                {
                    Console.WriteLine("There cannot be player with less than 0 years of playing");
                    AgeDataValidation(out value);
                }
            }
            return value;
        }
        public static int RatingDataValidation(out int value)
        {

            value = 0;
            try
            {
                value = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Pls enter integer! ");
                RatingDataValidation(out value);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("You must enter an integer!");
                RatingDataValidation(out value);
            }
            finally
            {
                if (value < 0)
                {
                    Console.WriteLine("There cannot be player with negative raiting... Or can it?");
                    RatingDataValidation(out value);
                }
                
            }
            return value;
        }
        public static void CSVFileValidation(out string pathToDirectory)
        {
            
            try
            {
                pathToDirectory = Console.ReadLine();
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("The directory is not found! Please try again");
                CSVFileValidation(out pathToDirectory);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The string is in not proper format");
                CSVFileValidation(out pathToDirectory);
            }
            
        }
        public static void LookingForPlayersFullfilingCriteria(int age, int raiting)
        {
            int today = DateTime.Now.Year;
            foreach (NBAPlayers nbaPlayer in nbaPlayers)
            {
                if (today - nbaPlayer.PlayingSince <= age && nbaPlayer.Rating >= raiting) 
                {
                    specificNbaPlayers.Add(nbaPlayer);
                }
            }
        }
        public static void WritePlayersToFile(string path)
        {
           
                try
                {
                    using(StreamWriter fileWriter = new StreamWriter(path + @"\report.txt")){
                        if (!specificNbaPlayers.Any())
                        {
                            Console.WriteLine("There are no players meeting the criteria thus there was no file created!");
                        }
                        else
                        {
                            specificNbaPlayers.Sort((x, y) => y.Rating.CompareTo(x.Rating));
                            fileWriter.WriteLine("Name,Rating");
                            foreach (NBAPlayers nbaPlayer in specificNbaPlayers)
                            {
                                fileWriter.WriteLine(nbaPlayer.Name + "," + nbaPlayer.Rating);
                            }
                            fileWriter.Flush();
                            Console.WriteLine("The report was created successfully");
                        }
                }
            }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Please make sure the path is given with the proper format and try again");
                    CSVFileValidation(out path);
                    WritePlayersToFile(path);
                }
                catch(DirectoryNotFoundException)
                {
                    Console.WriteLine("Probably messed up the path. Please try again");
                    CSVFileValidation(out path);
                    WritePlayersToFile(path);
                }
        }
        static void Main(string[] args)
        {
            string pathToJson = "";
            int years;
            int rating;
            string pathToCSVFile = "";
             Console.WriteLine("Please enter the path to the file: ");
             pathToJson = Console.ReadLine();
            if(File.Exists(pathToJson) == false)
             {
                 Console.WriteLine("The file doesn't exist or you gave wrong directory, pls try again");
                 Main(args);
             }else if (!pathToJson.EndsWith(".json"))
                {
                Console.WriteLine("You are probably not giving json file! Please make sure the file is json");
                Main(args);
                }
            LoadJson(pathToJson);
            Console.WriteLine("Please enter the maximum number of years the player has played in the league to qualify: ");
            AgeDataValidation(out years);
            Console.WriteLine("Please enter the minimum raiting the player must have to qualify: ");
            RatingDataValidation(out rating);
            LookingForPlayersFullfilingCriteria(years, rating);
            Console.WriteLine("Please enter the path where the file should be created: ");
            CSVFileValidation(out pathToCSVFile);
            WritePlayersToFile(pathToCSVFile);
            Console.WriteLine("Do you want to make a new report?[y/n]");
            string fastCheck = Console.ReadLine();
            if (fastCheck == "y")
            {
                Main(args);
            }
            Console.ReadKey();
        }
    }
}
