using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostWebApp
{
    class Program
    {
        private IDisposable _signal;
        private FileStream _fileStream;

        static void Main(string[] args)
        {
            new Program().Start();
        }

        public void Start()
        {
            Console.WriteLine("Starting ...");
            while (Directory.GetFiles(Environment.CurrentDirectory).Contains(Environment.CurrentDirectory + "\\LOCKED"))
            {
                System.Threading.Thread.Sleep(1000);
            }

            //Array.ForEach(Directory.GetFiles(Environment.CurrentDirectory + @"\plugins"), File.Delete);

            foreach (string fullPath in Directory.GetFiles(Environment.CurrentDirectory + @"\plugins"))
            {
                File.Delete(fullPath);
            }

            foreach (string fullPath in Directory.GetFiles(Environment.CurrentDirectory + @"\uploads"))
            {
                string fileName = Path.GetFileName(fullPath);
                File.Copy(fullPath, Environment.CurrentDirectory + @"\plugins\" + fileName);
            }

            using (_signal = WebApp.Start<Startup>("http://localhost:9000"))
            {
                Console.WriteLine("Listening ...");
                SetFileWatcher();
                LockResources();
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public void LockResources()
        {
            _fileStream = File.Create(Environment.CurrentDirectory + "\\LOCKED");
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Closing ...");
            _signal.Dispose();
            _fileStream.Close(); //release file for deletion
            File.Delete(Environment.CurrentDirectory + "\\LOCKED");
        }

        private void SetFileWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Environment.CurrentDirectory + @"\uploads";
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                        | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Restarting a new process...");
            Process.Start(Environment.CurrentDirectory + @"\SelfHostWebApp.exe");
            Environment.Exit(0);
        }
    }
}
