using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AMT_RVMATGenerator.stuff
{
    class RVMAT
    {
        public string path;
        public string name;
        public string fileNopx;
        public string fileCo;
    }

    class RVMATSettings
    {
        public string ambient;
        public string diffuse;
        public string forcedDiffuse;
        public string specular;
        public string specularPower;
        public string emmisive;
        public string aside;
        public string up;
        public string dir;
        public string pos;
    }
}
