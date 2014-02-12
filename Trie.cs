using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRole1
{
 
    public class Trie
    {
        public TrieNode root;

        public Trie(){
            root = new TrieNode();
        }

        public void InsertPhrase(String phrase){
            TrieNode current = root;
            foreach(char currentLetter in phrase){
                char temp = currentLetter;
                if (temp == '_'){ //changes '_' to ' '
                    temp = ' ';
                }
                if (current.ContainsChild(temp)){ //contains the letter
                    current = current.GetChild(temp);
                }else{ //create a new child
                   current = current.AddChild(temp);
                }
               
            }
            //Adds char 0 to child to signify end of phrase
            current.isFullWord = true;
        //case for end?    
                
        }

  

        
        public List<string> SearchPrefix(string phrase)
        {
            int counter = 0;
            
            if (SearchPrefixHelper(phrase).PrefixMatches() == null)
            {
                return new List<string>();
            }
            else
            {
                List<string> tempOutput = SearchPrefixHelper(phrase).PrefixMatches();
                List<string> output = new List<string>();
                    foreach (string s in tempOutput)
                    {
                        if (counter <= 8)
                        {
                            output.Add(phrase + s);
                            counter++;
                        }
                    }               
                return output;
            }

        }

        /// <summary>
        /// Finds the node with passed phrase
        /// 
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public TrieNode SearchPrefixHelper(string phrase){
                TrieNode current = root;
                foreach(char letter in phrase){
                    if(current.ContainsChild(letter)){
                        current = current.GetChild(letter);
                    }else{
                        return null;
                    }
                }
                return current;
            }
        
    }
}
