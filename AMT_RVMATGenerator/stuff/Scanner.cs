using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace AMT_RVMATGenerator.src
{
    class Scanner
    {
        public List<RVMAT> RvmatList { get; }

        public Scanner()
        {
            RvmatList = new List<RVMAT>();    
        }


        public void ScanFolder()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            ScanFolder(path);
        }
        public void ScanFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Error path does not exist!");
                return;
            }
            try
            {
                var temp = Directory.GetFiles(path);

                var co = temp.Where(x => x.Contains("_co")).ToArray();
                var nopx = temp.Where(x => x.Contains("_nopx")).ToArray();

                RvmatList.Clear();

                foreach (var element in co)
                {
                    RVMAT rvmat = new RVMAT();
                    var tempNopx = nopx.FirstOrDefault(x => x.Replace("_nopx", "_co") == element);
                    if (tempNopx == null)
                    {
                        Console.WriteLine("Not working for " + element);
                        continue;
                    }
                    rvmat.fileCo = element;
                    rvmat.fileNopx = tempNopx;
                    rvmat.path = element.Substring(0, element.LastIndexOf(@"\", StringComparison.Ordinal));
                    rvmat.name = element.Remove(element.LastIndexOf("."), element.Length - element.LastIndexOf(".")).Substring(element.LastIndexOf(@"\")).Replace("\\", "").Replace("_co", "").Replace("_nopx", "");
                    Console.WriteLine(rvmat.name);

                    RvmatList.Add(rvmat);
                }
                Console.WriteLine("Done!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
