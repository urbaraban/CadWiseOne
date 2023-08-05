namespace CadWiseTextReplacer.Services
{
    public static class FileWriting
    {
        public async static Task TextTransform(string filepath, string savepath, int word_length, bool remove_punctuation)
        {
            if (File.Exists(filepath) == true)
            {
                FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                using (StreamReader file = new StreamReader(fileStream))
                {
                    StreamWriter writer = new StreamWriter(savepath);
                    int counter = 0;
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        writer.Write(TextEditing.EditText(ln, word_length, remove_punctuation));
                        counter += 1;
                    }
                    file.Close();
                }
            }
        }
    }
}
