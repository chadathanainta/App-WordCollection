using SQLite;

namespace WordCollect
{
    public class Collection
    {
        [PrimaryKey]
        public string KeyWord { get; set; }
        public string Detal { get; set; }

        public Collection() { }

        public Collection(string KeyWord, string detal)
        {
            this.KeyWord = KeyWord;
            this.Detal = detal;
        }
    }
} 