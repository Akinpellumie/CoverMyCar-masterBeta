﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Xamarin.Forms;
using Android.Views;
using Android.Widget;
using CoverMyCar.Droid;
using System.IO;
using Environment = System.Environment;

[assembly: Dependency(typeof(FileService))]
namespace CoverMyCar.Droid
{
        public class FileService : IFileService
        {
            public void SavePicture(string name, Stream data, string location = "temp")
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                documentsPath = Path.Combine(documentsPath, "Orders", location);
                Directory.CreateDirectory(documentsPath);

                string filePath = Path.Combine(documentsPath, name);

            byte[] vs = new byte[data.Length];
            byte[] bArray = vs;
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    using (data)
                    {
                        data.Read(bArray, 0, (int)data.Length);
                    }
                    int length = bArray.Length;
                    fs.Write(bArray, 0, length);
                }
            }
        }
}