//Namespace
var CustomSearch = CustomSearch || {};
CustomSearch.custom_SearchResultCount = 0;
CustomSearch.results = "";
CustomSearch.displayResult = "";
CustomSearch.startAndEnd = "";

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
				
				window.getSelection().removeAllRanges();
				window.getSelection().addRange(rangeObj);
				var result = highlighter.serializeSelection();

				CustomSearch.startAndEnd.push(result.start + "," + result.end);				
				//alert(CustomSearch.startAndEnd);
				window.getSelection().removeAllRanges();
				
				var boundingRect = highlighter.getBoundingRectByStartAndEnd(result.start, result.end);
				CustomSearch.displayResult.push(result.start + ", " + result.end + ", " + boundingRect);
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

/*
function getPos(element)
{
    var rects = element.getClientRects();
	//console.log(rects);
    return {x : rects[0].left, y : rects[0].top};
}
*/
/*
function getRectangles(element)
{	var resultString = "";
    var results = new Array();
    var rects = element.getClientRects();
    for (var i = 0; i < rects.length; i++) {
        var rect = rects[i];		
		//var handleBounds = "{'left': " + rect.left + ", ";
		var handleBounds = "'left': " + rect.left + ", ";
	   	handleBounds += "'top': " + rect.top + ", ";
	   	handleBounds += "'right': " + rect.right + ", ";
	   	handleBounds += "'bottom': " + rect.bottom + "}";
        //results[i] = "{{" + rect.left + "," + rect.top + "}, {" + rect.width + "," + rect.height + "}}";
		resultString = resultString+handleBounds+";";
    }
    return resultString;
}
*/

function custom_HighlightAllOccurencesOfString(keyword)
{
	//alert(keyword);
	CustomSearch.custom_SearchResultCount = 0;
	CustomSearch.displayResult = [];
	CustomSearch.startAndEnd = [];
    custom_HighlightAllOccurencesOfStringForElement(document.body, keyword.toLowerCase());
		
	CustomSearch.startAndEnd.sort(function(item1, item2){
		if(parseInt(item1) < parseInt(item2))
			return -1;
		if(parseInt(item1) > parseInt(item2))
			return 1;
		return 0;					
	});
	CustomSearch.displayResult.sort(function(item1, item2){
		if(parseInt(item1) < parseInt(item2))
			return -1;
		if(parseInt(item1) > parseInt(item2))
			return 1;
		return 0;					
	});	
	window.FORM.fxl_returnSearchResult(android.selection.webviewName, CustomSearch.startAndEnd.join(';').toString(), CustomSearch.displayResult.join(';').toString());
	
}


