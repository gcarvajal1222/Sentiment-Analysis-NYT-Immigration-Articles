# Sentiment-Analysis-NYT-Articles
Sentiment analysis on NYT times immigration data with VADER and Textblob dictionaries                                                                                                                
# Poster Reference to project:                                                                                                                                                  https://scholar.valpo.edu/cus/917/                                                                                                                                                          

# Tools used

1. R Studio                                                                                                                                                                              
2. Jupyter Notebooks (python)                                                                                                                                                        

# First Step: Extracting Data from NYT API                                                                                                                                          
Folder: 1-NYT API Data Extraction                                                                                                                                                    
1. Created R notebook                                                                                                                                                                                       
2. Extract articles in a table like structure using https://www.storybench.org/working-with-the-new-york-times-api-in-r/                                                                 
3. Extracted immigration articles using query of "migrant OR immigration OR immigrant OR migration OR refugee OR alien OR undocumented OR asylum" to get a wide range of immigration as a whole.

4. Raw data from 1981-2020 is present in Exctracting NYT API (allNYTSearch1981to2020). Articles were extracted from the last date of requests until the API had a max number off calls. This was manually done and it usually failed every 2 years of data per the query specified in the notebook.


# Second Step: Preprocessing and Training                                                                                                                                                         
Folder: 2-Standarization and Training

1. Drop duplicate articles.                                                                                                                                                               
2. Data Standarization: Normalizing lead_paragraph with preprocess_regex function                                                                                                                                                
3. Textblob and VADER assign a first score between -1 to 1 to identify the polarity of the first paragraph using the default dicctionaries.



# Third Step: Identify Problematic words/ Recode words in the correct sphere from VADER dicctionary                                                                                                         
Folder 3- Recoding words by hand                                                                                                                                                       
1. Identify problematic words and n-grams counter parts such as "united" which most of them belonged to United States                                                                   
2. Recoded individual words in the correct sphere using inter-reliability measures to allocate words in the correct sphere                                                                

# Fourth Step: Retrain individual Words, Filter USA/ Latino News and Time Series plots:                                                                                                                                                                             

Folder 4- Retrain and Filter                                                                                                                                                                   

1. allocate words identified in step 3 into the correct sphere by VADER since it was identified as the better dicctionary                                                         
2. Filter latino news using query specified in folder "latino query"                                                                                                                          
3. Filter news that were written in the United States                                                                                                                                   
4. Normalize scores from -1-1 to 0-100 (in percent)                                                                                                                                    
5. Computed the Yearly, Quartely and Monthly mean of normalized scores                                                                                                               
5. Time series plots of normalized sentiment scores for All Articles, All Latino Articles, USA articles and USA Latino Articles                                                     
# Fith Step: Final Sentiment output                                                                                                                                                                                  
Folder 5- Final Sentiment output                                                                                                                                                 
1. Output retrained VADER scores for all the data (NYT_data_1980_to_2020_Retrained)                                                                                                       
2. Output articles that are just in the United States (US_News_Articles)                                                                                                                
3. Output articles that are Latino immigration news that appear in the United States (US_Latino_News_Articles)                                                                      
# Extra Analysis:                                                                                                                                                                          
Extra Analysis for further research implementation in order to aggregate monthly, yearly and quarterly data                                                                      
# Web App Predicctions (Still in development)                                                                                                                                                          
1. Build a Machine learning model on retrained VADER data                                                                                                                        
2. Web application in order to predict new articles from retrained data and create a service that ingests new articles via the NYT time API
