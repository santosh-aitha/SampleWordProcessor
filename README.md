# SampleWordProcessor

1.Clone the repo.
2.CD into the directory (using visual studio IDE open the soln file).
3.Build the solution Ctrl+Shift+B (or Menu--> Build-->BuildSolution or equivalent shortcut).
4.Makesure IISExpress is visible next to the Run button.
5.Click on Run Button, now application is in run mode.
6.Open any browser of choice and enter.
  1) API reference to Get the List of Words and their occurences
    https://localhost:44311/word  
    
  2) API reference to Get the TOP 10 most used words with their occurences
    https://localhost:44311/word/Top10Words
  3) API reference to check to see if a random word exists in the the txt file and if exists prints word with occurences
    https://localhost:44311/word/GetWordCount?word=University (Note: to check for any other words, replace the query stringparam value 
  4) API refernce to get the references while connecting to OWLBOT Api's
    https://localhost:44311/word/GetWordDefinition?word=University
