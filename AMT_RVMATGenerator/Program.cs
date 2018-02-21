using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AMT_RVMATGenerator.src;

namespace AMT_RVMATGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Scanner scan = new Scanner();
            if (args.Length == 0)
            {
                Console.WriteLine("No Arguments detected! Launching Console ");
                while (true)
                {
                    Console.Write("> ");
                    switch (Console.ReadLine().ToLower())
                    {
                        case "create":
                            break;
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
                                foreach (var rvmat in scan.RvmatList)
                                {
                                    Writer.GenerateRVMAT(rvmat, overwrite);
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
                            Console.WriteLine("Commands: \nscan\n\ncreate\nexit\nTo create rvmats you first have to scan a directory! Then you can generate rvmats.\nIt's also possible to use this tool via arguments, just use > -generate \"YourPath\" <\nYou can also enable logging by using -logging");
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
    }
}
