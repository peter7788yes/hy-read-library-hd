
var custom_SearchResultCount = 0;
var results = "";
var displayResult = "";

function custom_HighlightAllOccurencesOfStringForElement(element, keyword)
{
//alert("custom_HighlightAllOccurencesOfStringForElement");
    if (element) {
		
        if (element.nodeType == 3) {
            var elementParent = element.parentNode;
            var checkingStartIndex = 0;
            var value = element.nodeValue;
            while(true){
                var idx = value.toLowerCase().indexOf(keyword, checkingStartIndex);
				
                if (idx == -1){
                    break;
                }
                
                var rangeObj = document.createRange();
                rangeObj.setStart(element, idx);
                rangeObj.setEnd(element, idx + keyword.length);
                displayResult += getRectangles(rangeObj) + ';';
                results += getPos(rangeObj).x + "," + getPos(rangeObj).y + ";";
                checkingStartIndex = idx + keyword.length;
            }
        }
        else if (element.nodeType == 1) {
            if (element.style.display != "none" && element.nodeName.toLowerCase() != 'select') {
                for (var i = element.childNodes.length - 1; i >= 0; i--) {
                    custom_HighlightAllOccurencesOfStringForElement(element.childNodes[i],keyword);
                }
            }
        }
    }
}

function getPos(element)
{
    var rects = element.getClientRects();
    return {x : rects[0].left, y : rects[0].top};
}

function getRectangles(element)
{	var resultString = "";
    var results = new Array();
    var rects = element.getClientRects();
    for (var i = 0; i < rects.length; i++) {
        var rect = rects[i];
		var handleBounds = "{'left': " + rect.left + ", ";
	   	handleBounds += "'top': " + rect.top + ", ";
	   	handleBounds += "'right': " + rect.right + ", ";
	   	handleBounds += "'bottom': " + rect.bottom + "}";
        //results[i] = "{{" + rect.left + "," + rect.top + "}, {" + rect.width + "," + rect.height + "}}";
		resultString = resultString+handleBounds+";";
    }
    return resultString;
}

function custom_HighlightAllOccurencesOfString(keyword)
{
	//alert(keyword);
	custom_SearchResultCount = 0;
	results = "";
	displayResult = "";
	//alert("custom_HighlightAllOccurencesOfString");
    custom_HighlightAllOccurencesOfStringForElement(document.body, keyword.toLowerCase());
	
	//alert("window.FORM.returnSearchResult");
	window.FORM.returnSearchResult(results.toString(),displayResult.toString());
	//window.TextSelection.returnSearchResult(results,displayResult);
}


