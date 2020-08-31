// Namespace
var android = {};
android.selection = {};
	
android.selection.selectionStartRange = null;
android.selection.selectionEndRange = null;

/** The last point touched by the user. { 'x': xPoint, 'y': yPoint } */
android.selection.lastTouchPoint = null;

/** The last point of mouse up by the user. { 'x': xPoint, 'y': yPoint } */
android.selection.lastUpPoint = null;

/** Coordinate of the left border **/
android.selection.left = 0;


android.selection.pageWidth = 0;

android.selection.defaultScroll = '';

/** Initial Boundary values for filtering rectangles */
android.selection.contentsWidth = 0;
android.selection.contentsHeight = 0;
android.selection.multiColumnCount = 2;
android.selection.verticalWritingMode = false;
android.selection.leftToRight = true;

/** 
 * Starts the touch and saves the given x and y coordinates as last touch point
 */
android.selection.startTouch = function(x, y){
	
	android.selection.lastTouchPoint = {'x':  x, 'y': y};
    //alert(android.selection.lastTouchPoint.x + ' ' + y);
	//var temp = {'x': x,'y': y};
	var temp = x+','+y;
	window.FORM.getMouseStatus(temp);
	//return temp;
	
};

android.selection.checkLastPageVerticalWritingMode = function(leftPadding, id){
	var leftTop = android.selection.getElementTopLeft(id); 
	//alert("leftTop.left"+leftTop.left);
	//alert("leftTop.top"+leftTop.top);
	
	if(leftTop.left<0)
	{
		leftTop.left = Math.abs(leftTop.left);	
	}
	var left = leftTop.left % android.selection.pageWidth;
	
	//alert(left);
	if(left <= leftPadding)
		return true;
	else
		return false;

};

android.selection.checkLastPage = function(top, id) {	
	var leftTop = android.selection.getElementTopLeft(id); 
	//alert(leftTop.top);
	if(leftTop.top <= top)
		return true;
	else
		return false;
};


/**
 *	Checks to see if there is a selection.
 *
 *	@return boolean
 */
android.selection.hasSelection = function(){
	return window.getSelection().toString().length > 0;
};


/**
 *	Clears the current selection.
 */
android.selection.clearSelection = function(){
	
	try{
		// if current selection clear it.
	   	var sel = window.getSelection();
	   	sel.removeAllRanges();
	}catch(err){
		window.TextSelection.jsError(err);
	}	
};


/**
 *	Handles the long touch action by selecting the last touched element.
 */
android.selection.longTouch = function(event) {

	try{
		//? Why to clear first?
    	//android.selection.clearSelection();		
	   	// if current selection clear it.
		//alert("android.selection.left="+android.selection.left);
	   	var sel = window.getSelection();
		alert("sel="+sel.toString());

	   	var temp = sel.getRangeAt(0);				
		var rectsOOO = temp.getClientRects();
		var realRects = new Array();
		var j=0;
		for (j = 0;j < rectsOOO.length; j++)
		{
			alert("rectsOOO[" + j + "]=" + rectsOOO[j].left + "," +rectsOOO[j].top + "," + rectsOOO[j].right + "," + rectsOOO[j].bottom);
		}		
//Filter out UNREASONABLE rectanges.		
		for (j = 0;j < rectsOOO.length; j++){			
			if(rectsOOO[j].left >= rectsOOO[j].right || rectsOOO[j].top >= rectsOOO[j].bottom)
			{
				//filter out this Rectangle
			}
			else if(rectsOOO[j].left < 0 || rectsOOO[j].right < 0 || rectsOOO[j].top < 0 || rectsOOO[j].bottom < 0)
			{
				//filter out this Rectangle
			}
			else
			{
				realRects.push(rectsOOO[j]);
			}
		}		
		for (j = 0;j < realRects.length; j++){
			alert("realRects[" + j + "]=" + realRects[j].left + "," +realRects[j].top + "," + realRects[j].right + "," + realRects[j].bottom);
		}
		
		var boundingRect = temp.getBoundingClientRect();
		//alert("boundingRect=" + boundingRect.left + " " + boundingRect.top + " " + boundingRect.right + " " + boundingRect.bottom);
		
		android.selection.clearSelection();
		
//New version		
		var rectsArray = new Array();
		for (j = 0; j < realRects.length; j++){			
			rectsArray[j] = true;
			for (var k = 0; k < j; k++){
				if(rectsArray[k]){
					if(realRects[k].width >= realRects[j].width)
					{
						if(android.selection.intersectRect(realRects[k], realRects[j])){
							rectsArray[k] = false;
							//continue;
						}
					}
					else {
						if(android.selection.intersectRect(realRects[k], realRects[j])){
							rectsArray[j] = false;
							continue;
						}				
					}
				}
			}
		
		}
		// for (j = 0; j < realRects.length; j++){
			// alert("rectsArray[" + j +" ]=" + rectsArray[j] + ":" + realRects[j].left + "," +realRects[j].top + "," + realRects[j].right + "," + realRects[j].bottom );		
		// }
		if(android.selection.leftToRight == false)
		{
//New version: Find out the most top-left rectangle.	
			var startX = 10000,	startY = 10000, endX=-1, endY=-1;
			var startX2 = 10000, startY2 = 10000, endX2=-1, endY2=-1;
			//alert("BEFORE startX=" + startX + "\n" + "startY=" + startY + "\n" + "endX=" + endX + "\n" + "endY=" + endY);
			for (j = 0; j < realRects.length; j++) {
				var realRect = realRects[j];
				if(!rectsArray[j])
					continue;
				if(android.selection.multiColumnCount==1)
				{
					if(realRect.left>=0 && realRect.right<=android.selection.pageWidth)
					{
						if(realRect.top < startY)
						{
							startY = realRect.top;
							startX = realRect.left;
						}
						else if(realRect.top == startY && realRect.left < startX)
						{
							startX = realRect.left;
						}
						
						if(realRect.bottom > endY)
						{
							endX = realRect.right;	
							endY = realRect.bottom;
						}
						else if(realRect.bottom == endY && realRect.right > endX)
						{
							endX = realRect.right;
						}				
					}
				}
				else if(android.selection.multiColumnCount==2)
				{
					if(realRect.left>=0 && realRect.right<=android.selection.pageWidth/2)
					{
						if(realRect.top < startY)
						{
							startY = realRect.top;
							startX = realRect.left;
						}
						else if(realRect.top == startY && realRect.left < startX)
						{
							startX = realRect.left;
						}
						
						if(realRect.bottom > endY)
						{
							endX = realRect.right;	
							endY = realRect.bottom;
						}
						else if(realRect.bottom == endY && realRect.right > endX)
						{
							endX = realRect.right;
						}				
					}
					else if(realRect.left>=android.selection.pageWidth/2 && realRect.right<=android.selection.pageWidth)
					{
						if(realRect.top < startY2)
						{
							startY2 = realRect.top;
							startX2 = realRect.left;
						}
						else if(realRect.top == startY2 && realRect.left < startX2)
						{
							startX2 = realRect.left;
						}
						
						if(realRect.bottom > endY2)
						{
							endX2 = realRect.right;	
							endY2 = realRect.bottom;
						}
						else if(realRect.bottom == endY2 && realRect.right > endX2)
						{
							endX2 = realRect.right;
						}					
					}	
				}
			}
			//alert("AFTER startX=" + startX + "\n" + "startY=" + startY + "\n" + "endX=" + endX + "\n" + "endY=" + endY + "\n" + "startX2=" +startX2 + "\n" + "startY2=" + startY2 + "\n" + "endX2=" + endX2 + "\n" + "endY2=" + endY2);	
			if(endX2!=-1 && endY2!=-1 && startX!=10000 && startY!=10000)
			{
				endX = endX2;
				endY = endY2;
			}
			else if(endX2!=-1 && endY2!=-1)
			{
				startX = startX2;
				startY = startY2;
				endX = endX2;
				endY = endY2;
			}

			//alert("AFTER startX=" + startX + "\n" + "startY=" + startY + "\n" + "endX=" + endX + "\n" + "endY=" + endY);
			
			//alert("before remove");
			sel.removeAllRanges();
			//alert("after remove");
			
			android.selection.clearSelection();
			//alert("before createSelectionFromPoint");
			android.selection.createSelectionFromPoint(startX+android.selection.left, startY, endX+android.selection.left, endY);
			//alert("after createSelectionFromPoint");	
			
			//alert("longTouch:before save");
			android.selection.saveSelectionStart();
			android.selection.saveSelectionEnd();
			//alert("longTouch:after save");
			
			// Show the context menu in app
			android.selection.selectionChanged(event.pageX, event.pageY);
			//android.selection.selectBetweenHandles();
		}
		else
		{
			var startX = 10000,	startY = 10000, endX=-1, endY=-1;
			var startX2 = 10000, startY2 = 10000, endX2=-1, endY2=-1;
			//alert("BEFORE startX=" + startX + "\n" + "startY=" + startY + "\n" + "endX=" + endX + "\n" + "endY=" + endY);
			for (j = 0; j < realRects.length; j++) {
				var realRect = realRects[j];
				if(!rectsArray[j])
					continue;
				if(android.selection.multiColumnCount==1)
				{
					if(realRect.left>=0 && realRect.right<=android.selection.pageWidth)
					{
						if(realRect.top < startY)
						{
							startY = realRect.top;
							startX = realRect.left;
						}
						else if(realRect.top == startY && realRect.left < startX)
						{
							startX = realRect.left;
						}
						
						if(realRect.bottom > endY)
						{
							endX = realRect.right;	
							endY = realRect.bottom;
						}
						else if(realRect.bottom == endY && realRect.right > endX)
						{
							endX = realRect.right;
						}				
					}
				}
				else if(android.selection.multiColumnCount==2)
				{
					if(realRect.left>=0 && realRect.right<=android.selection.pageWidth/2)
					{
						if(realRect.top < startY)
						{
							startY = realRect.top;
							startX = realRect.left;
						}
						else if(realRect.top == startY && realRect.left < startX)
						{
							startX = realRect.left;
						}
						
						if(realRect.bottom > endY)
						{
							endX = realRect.right;	
							endY = realRect.bottom;
						}
						else if(realRect.bottom == endY && realRect.right > endX)
						{
							endX = realRect.right;
						}				
					}
					else if(realRect.left>=android.selection.pageWidth/2 && realRect.right<=android.selection.pageWidth)
					{
						if(realRect.top < startY2)
						{
							startY2 = realRect.top;
							startX2 = realRect.left;
						}
						else if(realRect.top == startY2 && realRect.left < startX2)
						{
							startX2 = realRect.left;
						}
						
						if(realRect.bottom > endY2)
						{
							endX2 = realRect.right;	
							endY2 = realRect.bottom;
						}
						else if(realRect.bottom == endY2 && realRect.right > endX2)
						{
							endX2 = realRect.right;
						}					
					}	
				}
			}
			//alert("AFTER startX=" + startX + "\n" + "startY=" + startY + "\n" + "endX=" + endX + "\n" + "endY=" + endY + "\n" + "startX2=" +startX2 + "\n" + "startY2=" + startY2 + "\n" + "endX2=" + endX2 + "\n" + "endY2=" + endY2);	
			if(endX2!=-1 && endY2!=-1 && startX!=10000 && startY!=10000)
			{
				endX = endX2;
				endY = endY2;
			}
			else if(endX2!=-1 && endY2!=-1)
			{
				startX = startX2;
				startY = startY2;
				endX = endX2;
				endY = endY2;
			}

			//alert("AFTER startX=" + startX + "\n" + "startY=" + startY + "\n" + "endX=" + endX + "\n" + "endY=" + endY);
			
			//alert("before remove");
			sel.removeAllRanges();
			//alert("after remove");
			
			android.selection.clearSelection();
			//alert("before createSelectionFromPoint");
			android.selection.createSelectionFromPoint(startX+android.selection.left, startY, endX+android.selection.left, endY);
			//alert("after createSelectionFromPoint");	
			
			//alert("longTouch:before save");
			android.selection.saveSelectionStart();
			android.selection.saveSelectionEnd();
			//alert("longTouch:after save");
			
			// Show the context menu in app
			android.selection.selectionChanged(event.pageX, event.pageY);
			//android.selection.selectBetweenHandles();		
		
		}
	   	
	 }
	 catch(err){
	 	//window.TextSelection.jsError(err);
		alert("longTouch:"+err.toString());
	 }
   	
};

/**
 * Tells the app to show the context menu. 
 */
android.selection.selectionChanged = function(x, y){

	try{
		var sel = window.getSelection();
		//alert("selectionChanged sel="+sel.toString());
		if(!sel){
			return;
		}
		
		var range = sel.getRangeAt(0);
		//alert("range="+range.startContainer+ " " +range.startOffset + " "+ range.endContainer + " " +range.endOffset);
		// Vertical-rl works?
		// var rectsOOO = range.getClientRects();
		// for (var i = 0; i < rectsOOO.length; i++) {//filter bad rects
			// var rectOOO = rectsOOO[i];
			// alert("rectOOO=" + rectOOO.left + " " + rectOOO.top + " " + rectOOO.right + " " + rectOOO.bottom);

		// }		
		
		// Selector bounds
		var handleBounds = android.selection.getHandleBounds(range);
		//alert("handleBounds:"+handleBounds.toString());
		
	   	// Menu bounds
	   	var rect = range.getBoundingClientRect();

	   	var menuBounds = "{'left': " + rect.left + ", ";
	   	menuBounds += "'top': " + rect.top + ", ";
	   	menuBounds += "'right': " + rect.right + ", ";
	   	menuBounds += "'bottom': " + rect.bottom + "}";
	   	
		//alert("menuBounds:"+menuBounds.toString());
	   	// Rangy
	   	var rangyRange = android.selection.getRange();
	   	//alert(rangyRange);
	   	// Text to send to the selection
	   	var text = window.getSelection().toString();
	   	
		//alert("rangyRange:"+rangyRange.toString());
		
		//window.FORM.getHandleBounds(handleBounds.toString());
		
		//window.FORM.getMenuBounds(menuBounds.toString());
	   	// Tell the interface that the selection changed
	   	//window.TextSelection.selectionChanged(rangyRange, text, //handleBounds, menuBounds);
		//alert("window.FORM.selectionChanged");
		// alert(x);
		window.FORM.selectionChanged(rangyRange.toString(), text.toString(), handleBounds.toString(), menuBounds.toString(), x, y);
		
	}
	catch(err){
		//window.TextSelection.jsError(err);
		//alert("selectionChanged:"+err.toString());
	}
};

android.selection.getHandleBounds = function(element){

	var hleft = 0;
	var htop = -1;
	var hright = 0;
	var hbottom = -1;

	// var hleft = -1;
	// var htop = 0;
	// var hright = -1;
	// var hbottom = 0;
	
	var rects = element.getClientRects();
	var filterResults = [];
	for (var i = 0; i < rects.length; i++) {//filter bad rects
		var rect = rects[i];
		var isGood = true;
		for (var t = 0; t < rects.length; t++) {//compare other rect to filter
			var tmp = rects[t];
			if(tmp.top >= rect.top) {//if top is equal, this is the same
				if(tmp.bottom < rect.bottom) {//this is the problem rect, remove it
					// //alert("filter one out");
					isGood = false;
					break;
				}
			}
		}
		for (var t = 0; t < rects.length; t++) {//compare other rect to filter
			var tmp = rects[t];
			if(tmp.left >= rect.left) {//if top is equal, this is the same
				if(tmp.right < rect.right) {//this is the problem rect, remove it
					// //alert("filter one out");
					isGood = false;
					break;
				}
			}
		}		
		if(isGood) {
		//alert("filterResults.push(rect):(" + rect.left + "," + rect.top + ")" + "(" + rect.right + "," + rect.bottom + ")" ); 
			filterResults.push(rect);
		}
	}	
	
    for (var i = 0; i < filterResults.length; i++) {//get the selector ranct
		//alert("i="+i);
		var goodRect = filterResults[i];	
		if(htop < 0 || goodRect.top <= htop) {
			htop = goodRect.top;
			hleft = goodRect.left;
		}
		if(hbottom < 0 || goodRect.bottom >= hbottom) {
			hbottom = goodRect.bottom;
			hright = goodRect.right;
		}
		// if(hleft < 0 || goodRect.left <= hleft) {
			// htop = goodRect.top;
			// hleft = goodRect.left;
		// }
		// if(hright < 0 || goodRect.right >= hright) {
			// hbottom = goodRect.bottom;
			// hright = goodRect.right;
		// }		
		
    }
   
   var handleBounds = "{'left': " + hleft + ", ";
	   	handleBounds += "'top': " + htop + ", ";
	   	handleBounds += "'right': " + hright + ", ";
	   	handleBounds += "'bottom': " + hbottom + "}";
   
   return handleBounds;
}

android.selection.getRectangles = function(element){
   var results = new Array();
   var rects = element.getClientRects();
   for (var i = 0; i < rects.length; i++) {
       var rect = rects[i];
       results[i] = "{{" + rect.left + "," + rect.top + "}, {" + rect.width + "," + rect.height + "}}";
   }
   return results.join(';');
}

android.selection.getTotalPages = function(){
	try{
		//window.TextSelection.setTotalPages(document.documentElement.scrollWidth, document.body.clientWidth);
		window.FORM.setTotalPages(document.body.scrollWidth, document.body.clientWidth);
		//alert("Pages=" + document.body.scrollWidth/document.body.clientWidth);
	}
	catch(err){
		//window.TextSelection.jsError(err);
		alert("getTotalPages:"+err.toString());
	}
}

android.selection.elementRectsByIdentifier = function(serializerId){
    
try{
	alert("elementRectsById");
   var rects = android.selection.getRectsByIdentifier(serializerId);
   //alert(rects.toString());
   // //window.TextSelection.returnElementRectsByIdentifier(android.seltion.getRe//ctsByIdentifier(serializerId));
   //alert("rects="+rects.toString());
   //window.TextSelection.returnElementRectsByIdentifier(rects);
   window.FORM.returnElementRectsByIdentifier(rects);
   }
catch(err){
		//window.TextSelection.jsError(err);
		alert("elementRectsByIdentifier:"+err.toString());
	}
   
}

android.selection.elementRectsByIdentifierForAll = function(serializerIdString){
	var elementRectsResult = "";
	var serializerIdArray = serializerIdString.split("=");
	for(var i = 0 ; i < serializerIdArray.length ;i++){
		var serializerId = serializerIdArray[i];
		if(serializerId.length > 0) {
			elementRectsResult += android.selection.getRectsByIdentifier(serializerId);
			if(i < serializerIdArray.length-1)
				elementRectsResult += "annoseprator";
		}
	}
	window.TextSelection.returnElementRectsByIdentifierForAll(elementRectsResult);
}

android.selection.searchAnnotationByRangyId = function(serializerIdString){
	//alert("searchAnnotationByRangyId");
	var elementRectsResult = android.selection.getRectsByIdentifier(serializerIdString);
	//window.TextSelection.returnSearchAnnotationByRangyId(elementRectsResult);
	window.FORM.returnElementRectsByIdentifier(elementRectsResult);
}

android.selection.getRectsByIdentifier = function(serializerId){
	//alert("getRectsByIdentifier");
	var sel = rangy.deserializeSelection(serializerId);
	var selection  = sel.nativeSelection;
	var range = selection.getRangeAt(0);
	var results = [];
	var rects = range.getClientRects();
	//var rects = range.getBoundingClientRect();
   
	var filterResults = [];
	for (var i = 0; i < rects.length; i++) {
       var rect = rects[i];
	   
	   var isGood = true;
		for (var t = 0; t < rects.length; t++) {//compare other rect to filter
			var tmp = rects[t];
			if(tmp.top >= rect.top) {//if top is equal, this is the same
				if(tmp.bottom < rect.bottom) {//this is the problem rect, remove it
					isGood = false;
					break;
				}
			}
			
		}
		if(isGood) {
			filterResults.push(rect);
		}
		
		for (var tt = 0; tt < filterResults.length; tt++) {//get the selector ranct
			var goodRect = filterResults[tt];
			//alert("goodRect.left="+goodRect.left+",goodRect.top="+goodRect.top+"\n goodRect.right="+goodRect.right+",goodRect.bottom="+goodRect.bottom);
			var handleBounds = "{'left': " + goodRect.left + ", ";
			handleBounds += "'top': " + goodRect.top + ", ";
			handleBounds += "'right': " + goodRect.right + ", ";
			handleBounds += "'bottom': " + goodRect.bottom + "}";
			results.push(handleBounds);
		}
   }
   return results.join(';');
}

android.selection.getRange = function() {
    var serializedRangeSelected = rangy.serializeSelection();
    var serializerModule = rangy.modules.Serializer;
    if (serializedRangeSelected != '') {
        if (rangy.supported && serializerModule && serializerModule.supported) {
            var beginingCurly = serializedRangeSelected.indexOf("{");
            serializedRangeSelected = serializedRangeSelected.substring(0, beginingCurly);
			//alert("serializedRangeSelected=" +serializedRangeSelected);
            return serializedRangeSelected;
        }
    }
}

/** 
 * Returns the last touch point as a readable string.
 */
android.selection.lastTouchPointString = function(){
	if(android.selection.lastTouchPoint == null)
		return "undefined";
		
	return "{" + android.selection.lastTouchPoint.x + "," + android.selection.lastTouchPoint.y + "}";
};

android.selection.saveSelectionStart = function(){
	try{

		// Save the starting point of the selection
	   	var sel = window.getSelection();
		var range = sel.getRangeAt(0);
		
		var saveRange = document.createRange();
		
		saveRange.setStart(range.startContainer, range.startOffset);
		
		android.selection.selectionStartRange = saveRange;
	}catch(err){
		//window.TextSelection.jsError(err);
		alert("saveSelectionStart:"+err.toString());
	}
};

android.selection.saveSelectionEnd = function(){

	try{

		// Save the end point of the selection
	   	var sel = window.getSelection();
		var range = sel.getRangeAt(0);
		
		var saveRange = document.createRange();
		saveRange.setStart(range.endContainer, range.endOffset);
		
		android.selection.selectionEndRange = saveRange;
	}catch(err){
		//window.TextSelection.jsError(err);
		alert("saveSelectionEnd:"+err.toString());
	}
	
};

/**
 * Sets the last caret position for the start handle.
 */
android.selection.setStartPos = function(x, y){
	
	try{
		android.selection.selectionStartRange = document.caretRangeFromPoint(x, y);
		
		android.selection.selectBetweenHandles();
	}catch(err){
		window.TextSelection.jsError(err);
	}

};

/**
 * Sets the last caret position for the end handle.
 */
android.selection.setEndPos = function(x, y){
	
	try{	
		android.selection.selectionEndRange = document.caretRangeFromPoint(x, y);
		
		android.selection.selectBetweenHandles();
	
	}catch(err){
		window.TextSelection.jsError(err);
	}

};

/**
 *	Selects all content between the two handles
 */
android.selection.selectBetweenHandles = function(){
	
	try{
		var startCaret = android.selection.selectionStartRange;
		var endCaret = android.selection.selectionEndRange;
		
		// If we have two carets, update the selection
		if (startCaret && endCaret) {
		
			// If end caret comes before start caret, need to flip
			if(startCaret.compareBoundaryPoints (Range.START_TO_END, endCaret) > 0){
				var temp = startCaret;
				startCaret = endCaret;
				endCaret = temp;
				
				android.selection.selectionStartRange = startCaret;
				android.selection.selectionEndRange = endCaret;
			}
			
			var range = document.createRange();
			range.setStart(startCaret.startContainer, startCaret.startOffset);
			range.setEnd(endCaret.startContainer, endCaret.startOffset);
			
			
			android.selection.clearSelection();
				
			var selection = window.getSelection();
			selection.addRange(range);
	
			
			
		}
		//alert("selectBetweenHandles");
		android.selection.selectionChanged();
   	}
   	catch(err){
   		window.TextSelection.jsError(err);
   	}
};

android.selection.scrollLeft = function(leftPos)
{
	//alert("scrollLeft()=" + leftPos);
	var currentLeftPos = $(window).scrollLeft();
	//alert("currentLeftPos=" + currentLeftPos);
	while(currentLeftPos != leftPos){
		//alert("leftPos=" + leftPos);
		$(window).scrollLeft(leftPos);
		currentLeftPos = $(window).scrollLeft();
	}
};

android.selection.scrollTop = function(topPos)
{
	//alert("scrollTop()=" + topPos);
	var currentTopPos = $(window).scrollTop();
	//alert("currentTopPos=" + currentTopPos);
	while(currentTopPos != topPos){
		//alert("topPos=" + topPos);
		$(window).scrollTop(topPos);
		currentTopPos = $(window).scrollTop();
	}
};

// http://stackoverflow.com/questions/11191136/set-a-selection-range-from-a-to-b-in-absolute-position
android.selection.createSelectionFromPoint = function(startX, startY, endX, endY) {
    var doc = document;
    var start, end, range = null;
    if (typeof doc.caretPositionFromPoint != "undefined") {
        start = doc.caretPositionFromPoint(startX, startY);
        end = doc.caretPositionFromPoint(endX, endY);
        range = doc.createRange();
        range.setStart(start.offsetNode, start.offset);
        range.setEnd(end.offsetNode, end.offset);
		//return range;
    } else if (typeof doc.caretRangeFromPoint != "undefined") {
		//alert("2");
		start = doc.caretRangeFromPoint(startX+4-android.selection.left, startY+4);
        end = doc.caretRangeFromPoint(endX-4-android.selection.left, endY-4);
		try{
			range = doc.createRange();
			range.setStart(start.startContainer, start.startOffset);
			range.setEnd(end.startContainer, end.startOffset);
			if(range.toString().trim() == "")
			{				
				//alert("null string!");
			}
			else{
				//alert("4");
				//alert(range.toString());
			}
			//alert("after doc.caretRangeFromPoint");
		}
		catch(err){
			//alert("range error:"+err.toString());
		}
    }
    if (range !== null && typeof window.getSelection != "undefined") {	
        //alert("5");
		var sel = window.getSelection();
        sel.removeAllRanges();
		//alert("addRange");
        sel.addRange(range);		
    } else if (typeof doc.body.createTextRange != "undefined")
	{		
        range = doc.body.createTextRange();
        range.moveToPoint(startX, startY);
        var endRange = range.duplicate();
        endRange.moveToPoint(endX, endY);
        range.setEndPoint("EndToEnd", endRange);
        range.select();
    }
};

android.selection.getElementTopLeft = function(id) {
    var ele = document.getElementById(id);
    var top = 0;
    var left = 0; 
    while(ele.tagName != "BODY") {
        top += ele.offsetTop;
        left += ele.offsetLeft;
        ele = ele.offsetParent;
    }   
    return { top: top, left: left }; 
}

android.selection.intersectRect = function(r1, r2) {
  return !(r2.left >= r1.right || 
           r2.right <= r1.left || 
           r2.top >= r1.bottom ||
           r2.bottom <= r1.top);
};



android.selection.returnTrue = function() {
	return true;
};

android.selection.scrollToHorizontalPosition = function(paddingLeft, pos) {

	//alert(paddingLeft);
	//alert(pos);
	
	var x = android.selection.pageWidth - paddingLeft;
	var r = pos % x;
	var p = pos - r;
	//alert(p);
	android.selection.scrollLeft(p);	
	//$(window).scrollLeft(p);
};

android.selection.scrollToVerticalPosition = function(paddingTop, pos) {

	//alert(paddingLeft);
	//alert(pos);
	
	var x = android.selection.pageHeight - paddingTop;
	var r = pos % x;
	var p = pos - r;
	//alert(p);
	android.selection.scrollTop(p);	
	//$(window).scrollLeft(p);
};


android.selection.createPageMark = function(left, top, right, bottom) {
	android.selection.clearSelection();
	alert("createPageMark="+left+ " "+top+ " "+right+ " "+bottom);
	android.selection.createSelectionFromPoint(left, top, right, bottom);
	//var smallRange = window.getSelection().getRangeAt(0);
	android.selection.saveSelectionStart();
	android.selection.saveSelectionEnd();
	//alert("after saveSelectionEnd()");
	var rangyRange = android.selection.getRange();
	var rangyRange2 = rangy.getSelection();
	//var text = window.getSelection().toString();
	//alert("{"+rangyRange.toString()+"}");
	//alert(text.toString());
	//var str = "{"+rangyRange.toString()+"}";
	//alert (str);
	//window.FORM.showMsg(rangyRange2.toString());
	//return str;
	//window.FORM.selectionChanged(rangyRange.toString(), text.toString(), handleBounds.toString(), menuBounds.toString());
	
};
