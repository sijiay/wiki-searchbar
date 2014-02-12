using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRole1
{
    public class TrieNode
    {
        /// <summary>
        /// 
        /// </summary>
        public char key{ get; set; }

        /// <summary>
        /// 
        ///
        /// </summary>
        public string value{get; set;}

        /// <summary>
        /// True if current node terminate the string
        /// </summary>
        public bool isFullWord{get; set ;}



        /// <summary>
        /// 
        /// </summary>
        public Dictionary<char, TrieNode> childList { get; set;}

        /// <summary>
        /// 
        /// </summary>
        public TrieNode()
        {
            this.isFullWord = false;
            this.childList = new Dictionary<char, TrieNode>();
        }

  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        public TrieNode(char key, TrieNode parent)
        {
            this.key = key;
            this.value = key.ToString();
            this.isFullWord = false;
            childList = new Dictionary<char, TrieNode>();

            
        }

        public TrieNode GetChild(char key)
        {
            if (ContainsChild(key))
            {
                return childList[key];
                
            }
            return null;
        }

        public bool ContainsChild(char key){
            if (this.childList == null)
            {
                return false;
            }
            return (childList.ContainsKey(key));

        }

        public bool IsOnlyPhrase()
        {
            return (ContainsChild('0') && childList.Count == 0);
        }
        public bool isLeaf(){
            return (childList.Count == 0);
        }


        public bool IsPhrase(){
            return (childList.ContainsKey('0'));
        }

        public TrieNode AddChild(char key){
            if (ContainsChild(key)){
                return childList[key];
            }else{
                TrieNode child = new TrieNode(key, this);
                childList.Add(key, child);
                return child;
            }
        }

        public List<String> PrefixMatches()
        {
            List<string> matches = new List<string>();
            return PrefixMatchesHelper("");
        }

        public List<string> PrefixMatchesHelper(string phraseBuilt){
            
            if (isLeaf())
            {
                if (isFullWord)
                {
                    return new List<string>(){phraseBuilt};
                }
                else
                {
                    return new List<string>();
                }
            }
            else
            {
                List<string> matches = new List<string>();
                string temp = phraseBuilt;
                foreach (TrieNode node in childList.Values)
                {
                    
                    matches.AddRange(node.PrefixMatchesHelper(temp+node.value));
                }
                if (isFullWord) //case if it is a phrase
                {
                    matches.Add(temp);
                }
                return matches;

            }
        }
    }
}
