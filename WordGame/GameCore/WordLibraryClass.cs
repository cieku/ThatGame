using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;

namespace WordGame.GameCore
{
    class WordLibraryClass
    {
        const string fileName = @"Words.txt";
        List<string> words = new List<string>();
        private Context context; 

        public WordLibraryClass(Context context)
        {
            this.context = context;
        }

        public void LoadWordsFromFile()
        {
            AssetManager assets = context.Assets;
            System.IO.StreamReader file = new System.IO.StreamReader(assets.Open(fileName));
            string line;
            while ((line = file.ReadLine()) != null)
            {
                words.Add(line);
            }
            file.Close();
        }

        public string GetRandomWord()
        {
            Random r = new Random();
            return words[r.Next(words.Count)-1];
        }
    }
}