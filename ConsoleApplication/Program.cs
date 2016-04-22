using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO.Compression;

namespace ConsoleApplication
{
    public class Person
    {
        public string name;
        public string surname;
        [XmlIgnore]
        public int age;

    }
    class Snow
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }
        char flake;
        public Snow(char c)
        {
            this.flake = c;
        }
        int n = 10;
        int sleepTime = 100;
        public void snowing()
        {
            for (int i = 0; i <= 80; i++)
            {
                int x = RandomNumber(0, Console.BufferWidth - 1);
                int y = RandomNumber(0, Console.BufferHeight - 1);
                Console.SetCursorPosition(x, y);
                Console.WriteLine(flake);
                if (i == n)
                {
                    Thread.Sleep(sleepTime);
                    i = 0;
                    sleepTime += 100;
                    n += 10;
                }
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            task1();
            //Atask2();
            //Btask2();

        }

        private static void Btask2()
        {
            string file = "file.xml";
            Person p = new Person();
            p.name = "Jane";
            p.surname = "Doe";
            p.age = 25;

            XmlSerializer xs = new XmlSerializer(typeof(Person));
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                xs.Serialize(fs, p);
            }
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                Person t = (Person)xs.Deserialize(fs);
                Console.WriteLine("{0} {1} {2}", t.name, t.surname, t.age);

            }


        }

        private static void Atask2()
        {

            string file = @"C:\Users\User\Desktop\kbtu2\.Net\lab12-13\folder";
            
            FileSystemWatcher w1 = new FileSystemWatcher(file);

            w1.Changed += new FileSystemEventHandler(OnChanged);
            w1.Created += new FileSystemEventHandler(OnChanged);
            w1.Deleted += new FileSystemEventHandler(OnChanged);
            w1.EnableRaisingEvents = true;

            string newFile = file + @"\newFile.txt";

            FileStream fs = File.Create(newFile);
            string text = "Hello world!";
            byte[] input = Encoding.ASCII.GetBytes(text);
            fs.Write(input, 0, text.Length);
            fs.Close();


            DirectoryInfo d = new DirectoryInfo(file);
            List<FileSystemInfo> files = new List<FileSystemInfo>();
            List<FileSystemInfo> folders = new List<FileSystemInfo>();

            files.AddRange(d.GetFiles());
            folders.AddRange(d.GetDirectories());

            foreach (var t in files)
            {
                Console.WriteLine(t.FullName + "\t" + t.Extension + "\t" + t.LastWriteTime);
            }

            Console.WriteLine("\nZip TextFile\n");
            string zipPath = file+ @"\archive.zip";
        

            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(newFile, "NewEntry.txt");
                
            }
            Console.WriteLine("\nDelete newFile\n");
            File.Delete(newFile);

            foreach (var t in folders)
            {
                Console.WriteLine(t.FullName + "\t" + t.Extension + "\t" + t.LastWriteTime);
            }
            Console.ReadKey();
            
            

        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File {0} {1}", e.FullPath, e.ChangeType);
        }

        private static void task1()
        {
            Console.SetBufferSize(80, 25);

            Snow s1 = new Snow('^');
            Snow s2 = new Snow('#');
            Snow s3 = new Snow('*');

            Thread t1 = new Thread(s1.snowing);
            Thread t2 = new Thread(s2.snowing);
            Thread t3 = new Thread(s3.snowing);

            t1.Start();
            t2.Start();
            t3.Start();


        }
    }
}
