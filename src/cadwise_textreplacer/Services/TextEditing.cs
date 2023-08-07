namespace CadWiseTextFilter.Services
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
                    concat = WordCheckIncrement(concat, word, word_length);

                    word = string.Empty;

                    if (char.IsPunctuation(c) == false || remove_punctuation == false)
                    {
                        concat += c;
                    }
                }
            }
            concat = WordCheckIncrement(concat, word, word_length);
            concat += '\n';
            return concat;
        }

        private static string WordCheckIncrement(string recipient, string increment, int word_length)
        {
            if (word_length < 0 || increment.Length < word_length)
            {
                recipient += increment;
            }
            return recipient;
        }
    }
}
