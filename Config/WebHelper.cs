using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;


namespace FindersKeepers
{
    public class WebHelper
    {
        private string Version = "http://finderskeepersd3.com/FK/Binaries/test.json";
        private string DownloadServer = "http://finderskeepersd3.com/.Download/";
        public string Directory = System.AppDomain.CurrentDomain.BaseDirectory;
        public Dictionary<string, FileHelper> FileList;
        public FileHelper FKDownloader;
        public WebClient webClient;

        public class FileHelper
        {
            public string Name;
            public string NewVersion;
            public string CurrentVersion;
            public bool Exists;
            public int FileSize;

            public double BytesDownloaded;
            public bool Success;
        }

        public WebHelper()
        {
            Task.Factory.StartNew(() =>
            {
                if (!Config.Get<FKConfig>().General.FKSettings.AllowUpdates)
                    return;

                UpdateCheck();
            }, TaskCreationOptions.LongRunning);
        }

        public void UpdateCheck()
        {
            if (isValid() && ReadJSON(Download(Version)))
            {
                if (GetFileinfo())
                {
                    if (!FKDownloader.Exists)
                        DownloadFile();
                    
                    if (FileList.Any(x => !x.Value.Exists))
                    {
                        Extensions.Execute.UIThread(() =>
                        {
                            if (MessageBox.Show("Would you like to update?", "New update is available", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                if(!File.Exists(Directory + "FK.Updater.exe"))
                                     MessageBox.Show("Oups looks like you don't have the installer!");
                                else
                                {
                                    ProcessStartInfo startInfo = new ProcessStartInfo(System.IO.Path.Combine(Directory, "FK.Updater.exe"));
                                    startInfo.UseShellExecute = true;
                                    Process.Start(System.IO.Path.Combine(Directory, "FK.Updater.exe"));
                                    Application.Current.Shutdown();
                                }
                            }
                        });
                    }
                }
            }
        }

        public void DownloadFile()
        {
            Thread t = new Thread(() =>
            {
                webClient = new WebClient();
                webClient.Proxy = null;
                webClient.UseDefaultCredentials = true;

                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadFileAsync(new Uri(DownloadServer + "FK.Updater.exe"), Directory + "FK.Updater.exe");
            });

            t.Start();
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            webClient.CancelAsync();

            if (webClient != null)
                webClient.Dispose();
        }

        public bool ReadJSON(string JSONQuery)
        {
            JSONQuery = Regex.Replace(JSONQuery, @"\s+", string.Empty);
            string[] Files = Regex.Split(JSONQuery, "\"([^\"]*)\"");
            HashSet<string> Ignore = new HashSet<string>() { ",", ":", "}", "{" };

            if (JSONQuery[0] != '{' && JSONQuery[JSONQuery.Count() - 1] != '}' && Files.Count() == 0) // Valid JSON?
                return false;

            Dictionary<string, FileHelper> ValuePair = new Dictionary<string, FileHelper>();
            List<string> File = Files.Where(x => !Ignore.Contains(x)).ToList();

            for (int i = 0; i < File.Count; i += 2)
            {
                int FileSize = 0;
                string FileTmp = File[i + 1];
                string NewVersion = "";

                try
                {
                    int.TryParse(Regex.Match(File[i + 1], @"(?<=\().+?(?=\))").Value, out FileSize);
                    NewVersion = FileTmp.Substring(0, FileTmp.IndexOf("("));
                }

                catch { }
                
                if (File[i] == "FK.Updater.exe")
                    FKDownloader = new FileHelper { Name = File[i], NewVersion = NewVersion, Exists = false, FileSize = FileSize };
                else
                    ValuePair.Add(File[i], new FileHelper { Name = File[i], NewVersion = NewVersion, Exists = false, FileSize = FileSize });
            }

            FileList = ValuePair;
            return true;
        }

        public bool isValid()
        {
            WebClient wc = null;

            try
            {
                wc = new WebClient
                {
                    Proxy = null,
                    UseDefaultCredentials = true
                };

                wc.DownloadString(Version);
                return true;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                    if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound) // HTTP 404
                        return false;
            }

            finally
            {
                if (wc != null)
                    wc.Dispose();
            }

            return false;
        }

        public string Download(string URL)
        {
            WebClient web = new WebClient();
            web.UseDefaultCredentials = true;
            return web.DownloadString(URL);
        }


        public bool GetFileinfo()
        {
            foreach(KeyValuePair<string, FileHelper> Fil in FileList)
            {
                if (File.Exists(Directory + Fil.Key))
                {
                    Fil.Value.CurrentVersion = FileVersionInfo.GetVersionInfo(Directory + Fil.Key).ProductVersion;

                    if ((new FileInfo(Directory + Fil.Key).Length == Fil.Value.FileSize) && Fil.Value.CurrentVersion == Fil.Value.NewVersion) // Corrupt file
                        Fil.Value.Exists = true;
                }
            }

            return true;
        }

    }
}
