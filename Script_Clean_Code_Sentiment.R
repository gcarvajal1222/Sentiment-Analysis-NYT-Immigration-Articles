#' ---
#' title: "Clean Code Sentiment Analysis with API extraction of first paragraph"
#' output: html_notebook
#' ---
#' 
#' 
#' #Loading Nessesary libraries for sentiment Analysis
## ------------------------------------------------------------------------
library(sentimentr)
library(qdap)
library(jsonlite)
library(dplyr)
library(ggplot2)
library(tm)
library(tidyverse)
library(SentimentAnalysis)
library(quanteda)

#' 
#' #Creating API varible for "Article Search API FORMAT"
## ------------------------------------------------------------------------
NYTIMES_KEY="a0HA3uBISDkGyvUGR3FeoAGybtDVPPM5"

#' 
#' #creating the query nessesary to extract wanted information
#' 
#' #Professor Johnson still has to give me the list 
#' 
#' #Doesn't seem like we can do "and" "or" for the specific query, something that academic search complete and pro quest are able to do
#' 
#' #Just do it for one month 2012/01/01 - 2012/02/01
## ------------------------------------------------------------------------
# Let's set some parameters
term <- "immigration+latino+hispanic" # Need to use + to string together separate words
begin_date <- "20120101"
end_date <- "20120201"

#' 
#' #Searching for the terms that we query above and essentially creating the query object in order to use it for the API
## ------------------------------------------------------------------------
baseurl <- paste0("http://api.nytimes.com/svc/search/v2/articlesearch.json?q=",term,
                  "&begin_date=",begin_date,"&end_date=",end_date,
                  "&facet_filter=true&api-key=",NYTIMES_KEY, sep="")

#' 
#' 
#' #Returning a Json object and calculating the max pages from the query, there is a max of 10 objects (newspaper articles) per page
## ------------------------------------------------------------------------
initialQuery <- fromJSON(baseurl)
maxPages <- round((initialQuery$response$meta$hits[1] / 10)-1) 

#' 
#' #from the maxpages we are pasting the baseurl and retriving the information and putting it in a dataframe
#' 
#' #Putting the system to sleep in order to 'trick' the computer for the amount of requests made per minute
#' 
#' #How to use sleep function to retrive the 77 pages in the sphere of immigration and Latinos, maybe add "In the US"?
## ------------------------------------------------------------------------
pages <- list()
for(i in 0:maxPages){
  nytSearch <- fromJSON(paste0(baseurl, "&page=", i), flatten = TRUE) %>% data.frame() 
  message("Retrieving page ", i)
  pages[[i+1]] <- nytSearch 
  Sys.sleep(1) 
}

#' 
#' 
#' #binding the pages together and creating the final dataframe. 
## ------------------------------------------------------------------------
allNYTSearch <- rbind_pages(pages)

#' 
#' #creating a dataframe that are only "news": Function part
## ------------------------------------------------------------------------
funct_remove_rows<- function(dataFrame,col_dataframe){
not_news_variable <- c("Op-Ed","Editorial")
allNYTSearch_OnlyNews<<-dataFrame[!grepl(paste(not_news_variable, collapse="|"), col_dataframe),]
return(allNYTSearch_OnlyNews)
}

#' 
#' #calling the function on the specific column of allNYTSearch and removing Co-ed and editorials
#' 
## ------------------------------------------------------------------------
funct_remove_rows(allNYTSearch,allNYTSearch$response.docs.type_of_material)

#' 
#' #Getting rid of white spaces
#' #verify that this is right
## ------------------------------------------------------------------------
allNYTSearch_OnlyNews$response.docs.lead_paragraph <-(str_squish(allNYTSearch_OnlyNews$response.docs.lead_paragraph))

#' 
#' #Add Id variable to the NYTSearch_OnlyNews 
## ------------------------------------------------------------------------
allNYTSearch_OnlyNews$id <- seq.int(nrow(allNYTSearch_OnlyNews))

#' 
#' #Trying to extract specific document in the term document matrix
#' # DOESNT WORK
## ------------------------------------------------------------------------
myReader <- DataframeSource(mapping=list(content=allNYTSearch_OnlyNews$response.docs.lead_paragraph ,id= allNYTSearch_OnlyNews$id))
tm_try <- VCorpus(DataframeSource(dd), readerControl=list(reader=myReader))

#' 
#' 
#' 
#' #Convert datarame into term document matrix from the allNYTSearch_onlyNews
## ------------------------------------------------------------------------
myCorpus <- Corpus(VectorSource(allNYTSearch_OnlyNews$response.docs.lead_paragraph))
dtm_NYTArticles <-  DocumentTermMatrix(myCorpus, 
                                   control = 
                                     list(removePunctuation = TRUE,
                                          stopwords = TRUE,
                                          tolower = TRUE,
              
                                          removeNumbers = TRUE)) 

#' 
#' 
#' 
#' 
#' 
#' 
#' #tidy format sentiment analysis
## ------------------------------------------------------------------------
library(dplyr)
library(tidytext)
ap_td <- tidy(dtm_NYTArticles)

#' 
#' #Sentiment from the tdm created above
## ------------------------------------------------------------------------
ap_sentiments <- ap_td %>%
  inner_join(get_sentiments("bing"), by = c(term = "word"))

#' #Sentiment from SentimentAnalysis package with LM dicctionary
#' 
## ------------------------------------------------------------------------
sentiment_scoreLM_cleancode<- analyzeSentiment(dtm_NYTArticles,
                              rules=list("SentimentLM"=list(ruleSentiment, loadDictionaryLM())))

#' #Sentiment from SentimentAnalysis package with GI dicctionary
#' 
#' 
## ------------------------------------------------------------------------
sentiment_scoreGI_cleancode<- analyzeSentiment(dtm_NYTArticles,
                              rules=list("SentimentGI"=list(ruleSentiment, loadDictionaryGI())))



#' 
#' #Sentiment from SentimentAnalysis package from qdap dicctionary
#' 
## ------------------------------------------------------------------------
sentiment_scoreQDAP_cleancode<- (analyzeSentiment(dtm_NYTArticles)$SentimentQDAP)
sentiment_scoreQDAP_DF<-as.data.frame(sentiment_scoreQDAP_cleancode)

#' 
#' 
#' 
#' 
