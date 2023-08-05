namespace CadWiseTextReplacer.Services
{
    internal class TextEditing
    {
        public static string EditText(string text, int word_length, bool remove_punctuation)
        {
            string concat = string.Empty;
            string word = string.Empty;
            foreach (char c in text)
            {
                if (char.IsLetterOrDigit(c) == true)
                {
                    word += c;
                }
                else
                {
                    if (word_length < 0 || word.Length < word_length)
                    {
                        concat += word;
                    }

                    word = string.Empty;

                    if (char.IsPunctuation(c) == false || remove_punctuation == false)
                    {
                        concat += c;
                    }
                }
            }
            concat += word;
            concat += '\n';
            return concat;
        }
    }
}
