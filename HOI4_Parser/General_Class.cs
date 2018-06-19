using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOI4_Parser
{
    public abstract class General
    {
        protected string[] buffer;
        protected static string path;
        public static string HOI4_Path { get => path; set => path = value; }
        public abstract void Load(string FileName);
    }
}