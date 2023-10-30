using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sharp_Examination
{
    internal class Dict
    {
        string pathToDict = "0";
        string pathRes = "Result.txt";
        XmlDocument doc = new XmlDocument();
        XmlElement root = null;
        static private string Holder(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        private bool ContainWord(string word)
        {
            foreach (XmlElement svWord in root.GetElementsByTagName("Word"))
            {
                if (word == svWord.GetAttribute("Word"))
                {
                    return true;
                }
            }
            return false;
        }
        private bool containTranslate(string translate, string word)
        {
            foreach (XmlElement svWord in root.GetElementsByTagName("Word"))
            {
                if (word == svWord.GetAttribute("Word"))
                {
                    foreach (XmlElement svTrans in svWord.GetElementsByTagName("Translate"))
                    {
                        if (translate == svTrans.InnerText)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void menu()
        {
            while (true)
            {
                Console.WriteLine($"current dictionar: {(pathToDict == "0" ? "0" : pathToDict)};\n1 - Create dictionary;" +
                    $"\n2 - Load dictionary;\nfor next options you mast create|load dictionary;\n3 - add word and translation;" +
                    $"\n4 - redact word or translation;\n5 - delete word or translation;\n6 - search word;" +
                    $"\n7 - save to result.txt(find + save)\n0 - exit;");
                int choise = 0;
                choise = Convert.ToInt32(Console.ReadLine());
                if (choise == 0)
                {
                    return;
                }
                else if (choise == 1)
                {
                    CreateDict();
                }
                else if (choise == 2)
                {
                    LoadDict();
                }
                if (pathToDict != "0")
                {
                    if (choise == 3)
                    {
                        AddTranslate();
                    }
                    else if (choise == 4)
                    {
                        RedactWord();
                    }
                    else if (choise == 5)
                    {
                        DeleteWord();
                    }
                    else if (choise == 6)
                    {
                        FindWord();
                    }
                    else if (choise == 7)
                    {
                        FindNSave();
                    }
                }
                if (pathToDict != "0")
                {
                    doc.Save(pathToDict);
                }
            }
        }
        public void CreateDict()
        {
            while (true)
            {
                string path = Holder("Enter Dict Name(enter 0 for cancle): ");
                if (pathToDict != "0")
                {
                    doc.Save(pathToDict);
                }
                if (path == "0")
                {
                    return;
                }
                else if (File.Exists($"{path}.xml"))
                {
                    Console.WriteLine("Dictionary with current name already exist, try load first.");
                }
                else
                {
                    doc = new XmlDocument();
                    root = doc.CreateElement($"{path}");
                    doc.AppendChild(root);
                    pathToDict = $"{path}.xml";
                    doc.Save(pathToDict);
                    return;
                }
            }
        }
        public void LoadDict()
        {
            while (true)
            {
                string path = Holder("Enter Dict Name(enter 0 for cancle): ");
                if (pathToDict != "0")
                {
                    doc.Save(pathToDict);
                }
                if (path == "0")
                {
                    return;
                }
                else if (File.Exists($"{path}.xml"))
                {
                    Console.WriteLine("Loaded succes;");
                    doc = new XmlDocument();
                    pathToDict = $"{path}.xml";
                    doc.Load(pathToDict);
                    root = doc.DocumentElement;
                    return;
                }
                else
                {
                    Console.WriteLine("No such dictionary in base;");
                }
            }
        }
        public void AddTranslate()
        {
            string word = Holder("Enter word what you will add and translate(0 to cancle): ");
            bool firstTrans = true;
            string translate = null;
            if (word == "0")
            {
                return;
            }
            if (ContainWord(word))
            {
                Console.WriteLine($"Word: {word} - already exist in dictionary, try \"redact word or translation\"");
                return;
            }
            else
            {
                XmlElement Word = doc.CreateElement("Word");
                Word.SetAttribute("Word", word);
                root.AppendChild(Word);
                while (true)
                {
                    XmlElement Translate = doc.CreateElement($"Translate");
                    translate = Holder($"Enter translate for word{(firstTrans ? "" : "(0 to exit)")}: ");
                    if (!firstTrans && translate == "0")
                    {
                        return;
                    }
                    Translate.InnerText = translate;
                    Word.AppendChild(Translate);
                    firstTrans = false;
                }
            }
        }
        public void RedactWord()
        {
            while (true)
            {
                string choise = Holder("1 - redact word\n2 - redact translate\n0 - exit;\n");
                if (choise == "0")
                {
                    return;
                }
                else if (choise == "1")
                {
                    string word = Holder("Enter word you would like to redact(0 to cancle): ");
                    if (word != "0")
                    {
                        foreach (XmlElement svWord in root.GetElementsByTagName("Word"))
                        {
                            if (svWord.GetAttribute("Word") == word)
                            {
                                string newWord = Holder("Enter new version of word(0 to cancle): ");
                                if (newWord != "0")
                                {
                                    svWord.SetAttribute("Word", newWord);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                    }
                }
                else if (choise == "2")
                {
                    string word = Holder("Enter word what translate you would like to redact(0 to cancle): ");
                    if (word != "0")
                    {
                        foreach (XmlElement svWord in root.GetElementsByTagName("Word"))
                        {
                            if (word == svWord.GetAttribute("Word"))
                            {
                                string trans = Holder($"Enter translate you would like to redact in {svWord.GetAttribute("word")}(0 to cancle): ");
                                if (trans != "0")
                                {
                                    foreach (XmlElement svTrans in svWord.GetElementsByTagName("Translate"))
                                    {
                                        if (trans == svTrans.InnerText)
                                        {
                                            string newTrans = Holder("Enter new translate(0 to canle): ");
                                            if (newTrans != "0")
                                            {
                                                svTrans.InnerText = newTrans;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void DeleteWord()
        {
            while (true)
            {
                string choise = Holder("1 - delete word\n2 - delete translate\n0 - exit;\n");
                if (choise == "0")
                {
                    return;
                }
                else if (choise == "1")
                {
                    string word = Holder("Enter word you would like to delete(0 to cancle): ");
                    if (word != "0")
                    {
                        foreach (XmlElement svWord in root.GetElementsByTagName("Word"))
                        {
                            if (svWord.GetAttribute("Word") == word)
                            {
                                svWord.RemoveAll();
                                root.RemoveChild(svWord);
                                break;
                            }
                        }
                    }
                }
                else if (choise == "2")
                {
                    string word = Holder("Enter word what translate you would like to delete(0 to cancle): ");
                    if (word != "0")
                    {
                        foreach (XmlElement svWord in root.GetElementsByTagName("Word"))
                        {
                            if (word == svWord.GetAttribute("Word"))
                            {
                                string trans = Holder($"Enter translate you would like to delete in {svWord.GetAttribute("Word")}(0 to cancle): ");
                                if (trans != "0")
                                {
                                    if (svWord.GetElementsByTagName("Translate").Count > 1)
                                    {
                                        foreach (XmlElement svTrans in svWord.GetElementsByTagName("Translate"))
                                        {
                                            if (trans == svTrans.InnerText)
                                            {
                                                svWord.RemoveChild(svTrans);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You can not delete last translate for word, try \"delete word\"");
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }
        public void FindWord()
        {
            while (true)
            {
                string word = Holder("Enter word you would like tp find(0 to cancle): ");
                bool match = false;
                if (word == "0")
                {
                    return;
                }
                foreach (XmlElement svWord in root.GetElementsByTagName("Word"))
                {
                    if (svWord.GetAttribute("Word") == word)
                    {
                        match = true;
                        Console.Write($"{svWord.GetAttribute("Word")} - ");
                        foreach (XmlElement svTrans in svWord.GetElementsByTagName("Translate"))
                        {
                            Console.Write($"{svTrans.InnerText}, ");
                        }
                        Console.Write("\n");
                    }
                }
                if (!match)
                {
                    Console.WriteLine("Word not found;");
                }
            }
        }
        public void FindNSave()
        {
            while (true)
            {
                string word = Holder("Enter word you would like tp find(0 to cancle): ");
                bool match = false;
                
                if (word == "0")
                {
                    return;
                }
                foreach (XmlElement svWord in root.GetElementsByTagName("Word"))
                {
                    if (svWord.GetAttribute("Word") == word)
                    {
                        StreamWriter writer = new StreamWriter(pathRes, true);
                        DateTime current = DateTime.Now;
                        match = true;
                        Console.Write($"{svWord.GetAttribute("Word")} - ");
                        writer.Write($"\n{current}\n{svWord.GetAttribute("Word")} - ");
                        foreach (XmlElement svTrans in svWord.GetElementsByTagName("Translate"))
                        {
                            Console.Write($"{svTrans.InnerText}, ");
                            writer.Write($"{svTrans.InnerText}, ");
                        }
                        Console.Write("\n");
                        writer.Close();
                    }
                }
                if (!match)
                {
                    Console.WriteLine("Word not found;");
                }
            }
        }
    }
}
