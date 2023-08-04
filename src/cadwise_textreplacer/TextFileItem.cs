using CadWiseTextReplacer.Services;

namespace CadWiseTextReplacer
{
    public class TextFileItem
    {
        private FileInfo FileInfo_ { get; }
        public string SaveAsPath { get; set; }
        public string TextPreview { get; }


        public TextFileItem(string filepath)
        {
            this.FileInfo_ = new FileInfo(filepath);
            this.SaveAsPath = filepath + "_edit";
            if (this.FileInfo_.Exists == true) 
            {
                this.TextPreview = FileReading.GetPrewiewFromFile(this.FileInfo_.FullName);
            }
        }

        public async Task TextTransform(int word_count, bool remove_punctuation)
        {
            FileStream fileStream = new FileStream(FileInfo_.FullName, FileMode.Open, FileAccess.Read);
            using (StreamReader file = new StreamReader(fileStream))
            {
                StreamWriter writer = new StreamWriter(SaveAsPath);
                int counter = 0;
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    int wordlength = 0;
                    string concat = string.Empty;
                    string word = string.Empty;
                    foreach (char c in ln)
                    {
                        wordlength += 1;
                        if (char.IsWhiteSpace(c) == true || char.IsPunctuation(c) == true)
                        {
                            if (char.IsPunctuation(c) == false || remove_punctuation == false)
                            {
                                concat += c;
                            }

                            if (word_count < 0 || wordlength < word_count)
                            {
                                concat += word;
                                word = string.Empty;
                                wordlength = 0;
                            }
                        } 
                        else
                        {
                            word += c;
                        }

                    }
                    concat += word;
                    concat += '\n';
                    writer.Write(concat);
                    counter += 1;
                }
                file.Close();
            }
        }
    }
}