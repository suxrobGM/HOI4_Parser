using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HOI4_Parser
{
    public class TechTree : General
    {
        private List<string> tech_id_list;
        private List<string> root_id_list;
        private string techTreeFileName;
        private string techTreeGuiFileName;
        
        /// <summary>
        /// List of Tech Identifications
        /// </summary>
        public List<string> TechIDs { get => tech_id_list; }
        public List<string> RootIDs { get => root_id_list; }

        /// <summary>
        /// Initializes a new empty instance of the Tech tree class
        /// </summary>
        public TechTree()
        {
            this.techTreeGuiFileName = HOI4_Path + @"\interface\countrytechtreeview.gui";
        }

        /// <summary>
        /// Initializes a new instance of the Tech tree class with specified name
        /// </summary>
        /// <param name="TechTreeFileName"> The file name of tech tree </param>
        public TechTree(string TechTreeFileName)
        {
            this.techTreeFileName = TechTreeFileName;
            this.techTreeGuiFileName = HOI4_Path + @"\interface\countrytechtreeview.gui";
            this.Load(TechTreeFileName);           
        }
        
        public override void Load(string TechTreeFileName)
        {
            this.buffer = File.ReadAllLines(TechTreeFileName);
            this.techTreeFileName = TechTreeFileName;

            string root_tech_id = String.Empty;
            bool isTechFile = false;

            foreach (var item in buffer)
            {
                if (item.Contains("technologies"))
                {
                    isTechFile = true;
                    break;
                }
            }

            if (isTechFile)
            {
                var tech_id_collection = from a in buffer
                                         where a.Contains("leads_to_tech")
                                         select a.Replace(" ", "").Remove(0, a.IndexOf("="));

                tech_id_list = new List<string>(tech_id_collection);                

                //Find root id list the file of countrytechtreeview.gui              
                string[] gui_file_content = File.ReadAllLines(techTreeGuiFileName);
                root_id_list = new List<string>();

                for (var i=0; i< gui_file_content.Length; i++)
                {
                    string temp = gui_file_content[i];
                    temp = temp.Trim();

                    if (!temp.StartsWith("#") && temp.Contains("\"" + this.GetFolderName() + "\""))
                    {
                        while(!gui_file_content[i].Contains("gridboxtype"))
                        {
                            i++;
                        }                       

                        while (!gui_file_content[i].Contains("containerWindowType"))
                        {
                            if(gui_file_content[i].Contains("gridboxtype"))
                            {
                                for(var j = i+1; j< gui_file_content.Length; j++)
                                {
                                    string temp_root_id = gui_file_content[j];
                                    temp_root_id = temp_root_id.Trim();

                                    if (!temp_root_id.StartsWith("#") && temp_root_id.Contains("name") && temp_root_id.Contains("_tree"))
                                    {
                                        string root_id = temp_root_id;                                     
                                        root_id = root_id.Replace(" ", "");
                                        root_id = root_id.Replace("\"", "");
                                        root_id = root_id.Remove(0, root_id.IndexOf("=") + 1);
                                        root_id_list.Add(root_id);
                                        i = j;
                                        break;
                                    }
                                }
                            }
                            i++;
                        }
                        break;
                    }
                }

                tech_id_list.InsertRange(0, root_id_list);
            }
            else
            {
                throw new Exception("ERROR: The file " + TechTreeFileName + " is not tech tree file");
            }
        }

        /// <summary>
        /// Get folder name of specified tech tree
        /// </summary>
        /// <param name="TechTreeFileName"></param>
        /// <returns>string of folder name</returns>
        public static string GetFolderNameOf(string TechTreeFileName)
        {
            string folder_name = String.Empty;
            string[] buffer = File.ReadAllLines(TechTreeFileName);

            for(var i=0; i<buffer.Length; i++)
            {
                string temp = buffer[i];
                temp = temp.Trim();

                if (!temp.StartsWith("#") && temp.Contains("folder"))
                {                 
                    while(!buffer[i].Contains("}"))
                    {
                        i++;
                        if (temp.Contains("name"))
                        {
                            folder_name = buffer[i];
                            folder_name = folder_name.Trim();
                            folder_name = folder_name.Replace(" ", "");
                            folder_name = folder_name.Remove(0, folder_name.IndexOf("=") + 1);
                            break;
                        }
                    }
                    break;
                }
            }

            return folder_name;
        }
        
        /// <summary>
        /// Get folder name of tech tree
        /// </summary>
        /// <param name="TechTreeFileName"></param>
        /// <returns>string of folder name</returns>
        public string GetFolderName()
        {
            string folder_name = String.Empty;

            for (var i = 0; i < buffer.Length; i++)
            {
                string temp = buffer[i];
                temp = temp.Trim();

                if (!temp.StartsWith("#") && temp.Contains("folder"))
                {
                    while (!buffer[i].Contains("}"))
                    {
                        i++;
                        if (buffer[i].Contains("name"))
                        {
                            folder_name = buffer[i];
                            folder_name = folder_name.Trim();
                            folder_name = folder_name.Replace(" ", "");
                            folder_name = folder_name.Remove(0, folder_name.IndexOf("=") + 1);
                            break;
                        }
                    }
                    break;
                }
            }

            return folder_name;
        }
    }
}
