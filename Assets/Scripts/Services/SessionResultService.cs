using System.Collections.Generic;

namespace Services
{
    public class SessionResultService 
    {
        private List<string> _claimedWords = new();

        public void Init()
        {
            _claimedWords.Clear();
        }

        public void AddClaimedWord(string word)
        {
            _claimedWords.Add(word);
        }

        public string[] ClaimedWords()
        {
            
            return _claimedWords.ToArray();
        }
    }
}