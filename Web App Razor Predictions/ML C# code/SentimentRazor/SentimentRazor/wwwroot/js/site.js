// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
/* Style for sentiment display */

function getSentiment(userInput) {
    return fetch(`Index?handler=AnalyzeSentiment&text=${userInput}`)
        .then((response) => {
            return response.text(); // this is a promise of type text
        })
}


function getArticle() {
    var apikeyInput = $('#apikeyid').val();
    fetch(`Index?handler=Articles&apiKey=${apikeyInput}`)
        .then((response) => {
            return response.text();
        }).then((ReturnResponse) => {
            console.log(JSON.stringify(ReturnResponse));
            document.getElementById('Message').value = ReturnResponse;
        })
}


function updateMarker(markerPosition, sentiment) {
    $("#markerPosition").attr("style", `left:${markerPosition}%`);
    $("#markerValue").text(sentiment);
}

function updateSentiment() {

    var userInput = $("#Message").val();

    getSentiment(userInput)
        .then((sentiment) => { // Convert the promise type text to use it in the if-else statements

            if (sentiment.startsWith("Sentiment: positive")) {
                updateMarker(100.0, sentiment);
            }
            else if (sentiment.startsWith("Sentiment: negative")) {
                updateMarker(100.0, sentiment);
            }
            else if (sentiment.startsWith("Sentiment: neutral")) {
                updateMarker(100.0, sentiment);
            }
            else {
                updateMarker(100.0, "Sentiment");
            }
            
        });
}

function getConfidenceScore() {
    var prediction = $("#markerValue").text();// why is this different than val()
    if (prediction.startsWith("Sentiment: positive")) {
        var confidenceScorePositive = prediction.substring(38, 42); //substring of confidenceScore;
        return confidenceScorePositive
    }
    if (prediction.startsWith("Sentiment: negative")) {
        var confidenceScoreNegative = prediction.substring(38, 42); //substring of confidenceScore;
        return confidenceScoreNegative
    }
    if (prediction.startsWith("Sentiment: neutral")) {
        var confidenceScoreNeutral = prediction.substring(37, 41); //substring of confidenceScore;
        return confidenceScoreNeutral
    }
    

}

function getPredictionSentiment() {
    var prediction = $("#markerValue").text();// why is this different than val()

    if (prediction.startsWith("Sentiment: positive")) {
        var positivePred = prediction.substring(11, 19); //getting just the sentiment prediction
        return positivePred
    }
    if (prediction.startsWith("Sentiment: negative")) {
        var NegativePred = prediction.substring(11, 19); //getting just the sentiment prediction
        return NegativePred;
    }
    if (prediction.startsWith("Sentiment: neutral")) {
        var neutralPred = prediction.substring(11, 18); //getting just the sentiment prediction
        return neutralPred
    }
    


}

$("#Message").on('click', updateSentiment)
//$('#apikeyid').on('change input paste', )