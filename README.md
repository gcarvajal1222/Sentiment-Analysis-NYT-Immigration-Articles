# Sentiment-Analysis-NYT-Articles
Sentiment analysis on NYT times data with VADER and Textblob dictionaries

# First Step:
Summary: Extracting the data from NYT times Developer website using R notebook                                                                                                     
Folder: 1-NYT API Data Extraction                                                                                                                                                    
1. Created R notebook                                                                                                                                                                                       
2. I followed the steps to extract articles in a table like structure using https://www.storybench.org/working-with-the-new-york-times-api-in-r/                                                                                                                                           
3. Raw data from 1981-2020 is present in Exctracting NYT API including 21455. Only first paragraph is included


# Second Step: Preprocessing                                                                                                                                                         
Folder: 2-Standarization and Training

1. Get rid of Duplicate articles. Articles were extracted from the last date of requests until the API had a max number off calls. This was manually done and it usually failed every 2 years of data per the query specified in the notebook.                                                                                                                    
2. Data Standarization: Normalizing lead.paragraph using Pre_Process function                                                                                                                                                   
3. Textblob and Vader assign a first score between -1 to 1 to identify the polarity of the first paragraph using the default dicctionaries.

