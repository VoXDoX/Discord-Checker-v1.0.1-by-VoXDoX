using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Text.Json;

namespace Discord_Checker_v1._0._1_by_VoXDoX
{
    class Program
    {
        public static int valid;
        public static int unverified;
        public static int invalid;

        private static void Main(string[] args)
        {
            Console.Title = "Discord Checker 1.0.1 by VoXDoX | Developer Channel: https://t.me/End_Soft";
            string path = "tokens.txt";
            
            bool flag = !File.Exists("accs-valid.txt");
            if (flag)
            {
                File.Create("accs-valid.txt");
            }

            bool flag1 = !File.Exists("accs-invalid.txt");
            if (flag1)
            {
                File.Create("accs-invalid.txt");
            }

            bool flag2 = !File.Exists("accs-unverified.txt");
            if (flag2)
            {
                File.Create("accs-unverified.txt");
            }

            File.WriteAllText("accs-valid.txt", "");
            File.WriteAllText("accs-invalid.txt", "");
            File.WriteAllText("accs-unverified.txt", "");

            Logger.Printf("\nDiscord Checker 1.0.1 by VoX DoX\n" +
                "Developer Channel: https://t.me/End_Soft\n" +
                "Developer link: https://t.me/The_VoX", Logger.Type.INFO);

            using (FileStream fileStream = File.OpenRead(path))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 128))
                {
                    string token;
                    while ((token = streamReader.ReadLine()) != null)
                    {
                        WebClient client = new WebClient();

                        client.Headers["Authorization"] = token;

                        try
                        {
                            string array = client.DownloadString("https://discord.com/api/v9/users/@me/library");
                            Logger.Printf(" [SUCCESS] " + token, Logger.Type.SUCCESS);
                            Program.valid++;
                            File.AppendAllText("accs-valid.txt", token + Environment.NewLine);
                        }
                        catch (WebException exception)
                        {
                            bool flag4 = !File.Exists("accs-valid.txt");
                            if (flag4)
                            {
                                File.Create("accs-valid.txt");
                            }
                            bool flag5 = !File.Exists("accs-unverified.txt");
                            if (flag5)
                            {
                                File.Create("accs-unverified.txt");
                            }
                            bool flag6 = !File.Exists("accs-invalid.txt");
                            if (flag6)
                            {
                                File.Create("accs-invalid.txt");
                            }
                            HttpWebResponse response = (HttpWebResponse)exception.Response;

                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                Logger.Printf(" [BAD] " + token, Logger.Type.ERROR);
                                Program.invalid++;
                                File.AppendAllText("accs-invalid.txt", token + Environment.NewLine);
                            }
                            else if (response.StatusCode == HttpStatusCode.Forbidden)
                            {
                                Logger.Printf(" [LIMITED] " + token, Logger.Type.WARNING);
                                Program.unverified++;
                                File.AppendAllText("accs-unverified.txt", token + Environment.NewLine);
                            }
                            if (response.StatusCode == HttpStatusCode.NotFound)
                            {
                                Logger.Printf("ммм");
                            }
                        }

                    }
                    int total = Program.valid + Program.invalid + Program.unverified;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("");
                    Logger.Printf("---Discord Token Checker by VoXDoX---", Logger.Type.INFO);
                    Logger.Printf(" Валиды: " + Program.valid, Logger.Type.INFO);
                    Logger.Printf(" Неверифы: " + Program.unverified, Logger.Type.INFO);
                    Logger.Printf(" Невалиды: " + Program.invalid, Logger.Type.INFO);
                    Logger.Printf(" Всего проверено: " + total, Logger.Type.INFO);
                    Logger.Printf("---Discord Token Checker by VoXDoX---", Logger.Type.INFO);
                    Console.WriteLine("");
                    Logger.Printf("Подпишись: https://t.me/End_Soft", Logger.Type.SUCCESS);
                    Console.ReadLine();
                }
            }
        }
    }
}
