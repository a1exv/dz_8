using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;


namespace ConsoleApplication1
{
    public class iniwr
    {
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        public static extern int GetPrivateString(string section, string key, string def, StringBuilder buffer, int size, string path);
        private const int SIZE = 1024;
        public static string path = String.Empty;
        public void setpath(string Path)
        {
            path = Path;
        }
        public static string GetPrivateString(string section, string key)
        {
            StringBuilder buffer = new StringBuilder(SIZE);
            GetPrivateString(section, key, null, buffer, SIZE, path);
            return buffer.ToString();
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MyAttribute : Attribute
    {
        public string inipath{get; set;}
    }

    public class human
    {
        private int age=0;
       // public human()
       // {
       //     age = Age;
       // }
        public string name = "default";
       
        public  void setpath()
        {
           
        }
        
        [MyAttribute(inipath = "E:/Programms/step/c3/1.ini")]
        public int Age
        {
            get
            {
                iniwr ini = new iniwr();
                var attributeValue = Attribute.GetCustomAttribute(this.GetType().GetProperty("Age"), typeof(MyAttribute)) as MyAttribute;
                // if (Attribute.IsDefined(this.Age.GetType(), typeof(MyAttribute)))
                // {

                ini.setpath(attributeValue.inipath);
               
                    age = Convert.ToInt32(iniwr.GetPrivateString("section 1", "var"));
                    
               // }
                return age;
            }
            set
            {
                age = value;
            }
        }
        // public var attributeValue = Attribute.GetCustomAttribute(this.Age.GetType(), typeof(MyAttribute)) as MyAttribute;
    }
    class Program
    {
        static void Main(string[] args)
        {
            human h = new human();
            h.setpath();
            Console.WriteLine(h.Age);
        }
    }
}
