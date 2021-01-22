# SampleWordProcessor

1.Clone the repo.
2.CD into the directory (using visual studio IDE open the soln file).
3.Build the solution Ctrl+Shift+B (or Menu--> Build-->BuildSolution or equivalent shortcut).
4.Makesure IISExpress is visible next to the Run button.
5.Click on Run Button, now application is in run mode.
6.Open any browser of choice and enter.
  1) API reference to Get the List of Words and their occurences
    AdditonalInfo : The api reference is used for parsing the entire text file and then find out the number of occurences of each of the words       
    https://localhost:44311/word  (Note :localhost port could vary)
    
  2) API reference to Get the TOP 10 most used words with their occurences
    AdditionalInfo : The api reference is used for parsing the entire text file and then find out the Top 10 occurences of words    
    https://localhost:44311/Top10Words (Note :localhost port could vary)
    
  3) API reference to check to see if a random word exists in the the txt file and if exists prints word with occurences
    AdditionalInfo :The api reference is used for parsing the entire text file and then find out the word that is supplied as querystring param and displays occurences of word     
    https://localhost:44311/GetWordCount?word=University (Note: to check for any other words, replace the query stringparam value, localhost port could vary) 
    
  4) API refernce to get the references while connecting to OWLBOT Api's
   AdditonalInfo: The api reference is used contact OwlBot apis by passing a word as querystring parameter and gets back the definition of the word if found
       
    https://localhost:44311/GetWordDefinition?word=University (Note :localhost port could vary)
