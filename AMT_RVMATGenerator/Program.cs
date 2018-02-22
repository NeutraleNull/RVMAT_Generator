using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AMT_RVMATGenerator.stuff;

namespace AMT_RVMATGenerator
{
    class Program
    {


        static void Main(string[] args)
        {
            // ---- init -----
            var scan = new Scanner();
            var rvmatSettings = new RVMATSettings();
            Init(ref rvmatSettings);  //checking internal storage for default values


            if (args.Length == 0)
            {
                Console.WriteLine("No Arguments detected! Launching Console ");
                while (true)
                {
                    Console.Write("> ");
                    switch (Console.ReadLine().ToLower())
                    {
                        case "boi":
                            Console.WriteLine("Oh boi what are you doin");
                            break;

                        case "create":
                            Console.WriteLine("Please enter the folder to create blank rvmat");
                            Console.Write("> ");
                            var rvmat = new RVMAT();
                            rvmat.path = Console.ReadLine();

                            if (!Directory.Exists(rvmat.path))
                            {
                                Console.WriteLine("Path does not exist, aborting...");
                                return;
                            }

                            Console.WriteLine("Enter rvmat name");
                            Console.Write(">");
                            rvmat.name = Console.ReadLine();
                            rvmat.fileCo = string.Empty;
                            rvmat.fileNopx = string.Empty;
                            
                            Writer.GenerateRVMAT(rvmat, true);
                            Console.WriteLine("Done!");
                            break;

                        case "edit":
                            Console.WriteLine("Choose from the following values");
                            Console.WriteLine("\nambient\ndiffuse\nforced\nforcedDiffuse\nspecular\nspecularPower\nemmisive\naside\nup\ndir\npos\nYou can leave this menu by enter 'exit'\nReset the config by enter 'reset'");
                            Console.Write("> ");
                            while (true)
                            {
                                switch (Console.ReadLine())
                                {
                                    case "ambient":
                                        Console.WriteLine("Current Value: {0}", Properties.Data.Default.Settings.ambient);
                                        Console.WriteLine("Enter a value for ambient");
                                        Console.Write("> ");
                                        Properties.Data.Default.Settings.ambient = Console.ReadLine();
                                        Properties.Data.Default.Save();
                                        break;
                                    case "diffuse":
                                        Console.WriteLine("Current Value: {0}", Properties.Data.Default.Settings.diffuse);
                                        Console.WriteLine("Enter a value for diffuse");
                                        Console.Write("> ");
                                        Properties.Data.Default.Settings.diffuse = Console.ReadLine();
                                        Properties.Data.Default.Save();
                                        break;
                                    case "forcedDiffuse":
                                        Console.WriteLine("Current Value: {0}", Properties.Data.Default.Settings.forcedDiffuse);
                                        Console.WriteLine("Enter a value for forcedDiffuse");
                                        Console.Write("> ");
                                        Properties.Data.Default.Settings.forcedDiffuse = Console.ReadLine();
                                        Properties.Data.Default.Save();
                                        break;
                                    case "specular":
                                        Console.WriteLine("Current Value: {0}", Properties.Data.Default.Settings.specular);
                                        Console.WriteLine("Enter a value for specular");
                                        Console.Write("> ");
                                        Properties.Data.Default.Settings.specular = Console.ReadLine();
                                        Properties.Data.Default.Save();
                                        break;
                                    case "specularPower":
                                        Console.WriteLine("Current Value: {0}", Properties.Data.Default.Settings.specularPower);
                                        Console.WriteLine("Enter a value for specularPower");
                                        Console.Write("> ");
                                        Properties.Data.Default.Settings.specularPower = Console.ReadLine();
                                        Properties.Data.Default.Save();
                                        break;
                                    case "emmisive":
                                        Console.WriteLine("Current Value: {0}", Properties.Data.Default.Settings.emmisive);
                                        Console.WriteLine("Enter a value for emmisive");
                                        Console.Write("> ");
                                        Properties.Data.Default.Settings.emmisive = Console.ReadLine();
                                        Properties.Data.Default.Save();
                                        break;
                                    case "aside":
                                        Console.WriteLine("Current Value: {0}", Properties.Data.Default.Settings.aside);
                                        Console.WriteLine("Enter a value for aside");
                                        Console.Write("> ");
                                        Properties.Data.Default.Settings.aside = Console.ReadLine();
                                        Properties.Data.Default.Save();
                                        break;
                                    case "up":
                                        Console.WriteLine("Current Value: {0}", Properties.Data.Default.Settings.up);
                                        Console.WriteLine("Enter a value for up");
                                        Console.Write("> ");
                                        Properties.Data.Default.Settings.up = Console.ReadLine();
                                        Properties.Data.Default.Save();
                                        break;
                                    case "dir":
                                        Console.WriteLine("Current Value: {0}", Properties.Data.Default.Settings.dir);
                                        Console.WriteLine("Enter a value for dir");
                                        Console.Write("> ");
                                        Properties.Data.Default.Settings.dir = Console.ReadLine();
                                        Properties.Data.Default.Save();
                                        break;
                                    case "pos":
                                        Console.WriteLine("Current Value: {0}", Properties.Data.Default.Settings.pos);
                                        Console.WriteLine("Enter a value for pos");
                                        Console.Write("> ");
                                        Properties.Data.Default.Settings.pos = Console.ReadLine();
                                        Properties.Data.Default.Save();
                                        break;
                                    case "exit":
                                        return;
                                    case "reset":
                                        var temp = new RVMATSettings();
                                        Init(ref temp);
                                        Console.WriteLine("Settings reseted!");
                                        break;
                                    default:
                                        Console.WriteLine("Command not found, try again");
                                        break;
                                }

                            }

                        case "scan":
                            Console.WriteLine("Please enter the folder i should scan");
                            Console.Write("> ");
                            var path = Console.ReadLine();
                            scan.ScanFolder(path);
                            break;

                        case "generate":
                            Console.WriteLine("Overwrite existing rvmats? (y/n)");
                            Console.Write("> ");
                            var key = Console.ReadKey().KeyChar;

                            var overwrite = true;

                            if (key != 'n' && key != 'y')
                            {
                                Console.WriteLine("Try again... ");
                                return;
                            }

                            if (key == 'n')
                                overwrite = false;
                            
                            if (scan.RvmatList.Count > 0)
                            {
                                foreach (var rvmatInList in scan.RvmatList)
                                {
                                    Writer.GenerateRVMAT(rvmatInList, overwrite);
                                }
                            }
                            else
                                Console.WriteLine("No Files in List! Maybe you need to run a scan first?");
                            Console.WriteLine("Done!");
                            break;

                        case "stop":
                        case "exit":
                            return;
                        case "help":
                            Console.WriteLine("Commands:\nscan\ngenerate\ncreate\nexit\nTo create rvmats you first have to scan a directory! Then you can generate rvmats.\nIt's also possible to use this tool via arguments, just use > -generate \"YourPath\" <\nYou can also enable logging by using -logging");
                            break;
                        default:
                            Console.WriteLine("Command not found! Type 'help' for a commandlist!");
                            break;
                    }
                }
            }
            if (args.Length > 1)
            {
                var generate = false;
                string folder = "";

                var scanning = false;
                string scanfolder = "";

                var logging = false;

                var excluding = false;
                string excludingFolder = "";

                var overwrite = false;

                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-generate":
                            generate = true;
                            folder = args[i + 1];
                            break;
                        case "-logging":
                            logging = true;
                            break;
                        case "-scan":
                            scanning = true;
                            scanfolder = args[i + 1];
                            break;
                        case "-excluding":
                            excluding = true;
                            excludingFolder = args[i + 1];
                            break;
                        case "-overwrite":
                            overwrite = true;
                            break;
                        default:
                            break;
                    }    
                }

                if (scanning)
                {
                    var scanner = new Scanner();
                    scanner.ScanFolder(scanfolder);
                    var list = scanner.RvmatList;
                    using (var writer = new StreamWriter(
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.txt"), false, Encoding.UTF8))
                    {
                        foreach (var rvmat in list)
                        {
                            writer.WriteLine(rvmat.fileCo);
                            writer.WriteLine(rvmat.fileNopx);
                        }
                    }
                }

                if (generate)
                {
                    var scanner = new Scanner();
                    scanner.ScanFolder(folder);
                    foreach (var rvmat in scanner.RvmatList)
                    {
                        Writer.GenerateRVMAT(rvmat, overwrite);
                    }
                }

                if (excluding)
                {
                    Console.WriteLine("Not implemented!");
                    #if DEBUG
                    Console.ReadKey();
                    #endif
                }

                if (logging)
                {
                    Console.WriteLine("Not implemented!");
                    #if DEBUG
                    Console.ReadKey();
                    #endif
                }
                #if DEBUG
                Console.ReadKey();
                #endif
            }
        }

        private static void Init(ref RVMATSettings rvmatSettings)
        {
            if (Properties.Data.Default.Settings != null)
            {
                rvmatSettings = Properties.Data.Default.Settings;
            }
            else
            {
                rvmatSettings.ambient = "{1.0,1.0,1.0,1}";
                rvmatSettings.diffuse = "{1.0,1.0,1.0,1}";
                rvmatSettings.forcedDiffuse = "{0,0,0,0}";
                rvmatSettings.specular = "{0,0,0,0}";
                rvmatSettings.specularPower = "1";
                rvmatSettings.emmisive = "{0,0,0,0}";
                rvmatSettings.aside = "{5,0,0}";
                rvmatSettings.up = "{0,5,0}";
                rvmatSettings.dir = "{0,0,5}";
                rvmatSettings.pos = "{0,0,0}";
                Save(rvmatSettings);
            }
        }

        private static void Save(RVMATSettings rvmatSettings)
        {
            Properties.Data.Default.Settings = rvmatSettings;
            Properties.Data.Default.Save();
        }
    }
}
