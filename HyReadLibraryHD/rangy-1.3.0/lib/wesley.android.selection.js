// Namespace
var android = {};
android.selection = {};
	
android.selection.selectionStartRange = null;
android.selection.selectionEndRange = null;

/** The last point touched by the user. { 'x': xPoint, 'y': yPoint } */
android.selection.lastTouchPoint = null;
android.selection.lastTouchRange = null;
android.selection.latestTouchPoint = null;
android.selection.latestTouchRange = null;
android.selection.latestTouchRangeSerialID = null;

/** The last point of mouse up by the user. { 'x': xPoint, 'y': yPoint } */
android.selection.lastUpPoint = null;

/** Coordinate of the left border **/
android.selection.left = 0;

/** Boundary values for filtering rectangles **/
/** One Webview(what you can see) Width & Height **/
android.selection.pageWidth = 0;
android.selection.pageHeight = 0;

//android.selection.defaultScroll = '';

/** Initial Boundary values for filtering rectangles */
//android.selection.contentsWidth = 0;
//android.selection.contentsHeight = 0;

/** Set by .Net platform */
//android.selection.multiColumnCount = 1;
/** Set by .Net platform */
//android.selection.verticalWritingMode = ''; //false;
//android.selection.leftToRight = ''; //true;
//android.selection.fontSize = 0;
//android.selection.rightPadding = null;
//android.selection.leftPadding = null;

android.selection.webviewName = null;

android.selection.isMouseDown = false;	

android.selection.hotZone = false;
android.selection.deadZone = false;
android.selection.caretSet = false;
android.selection.beyondHorizontalBorder = false;

android.selection.needFilterAgain = true;

//Filter out UNREASONABLE rectanges.
android.selection.filter_left_border = 0;
android.selection.filter_right_border = 0;
android.selection.filter_top_border = 0;
android.selection.filter_bottom_border = 0;	

android.selection.OnMouseMove = function(e)
{

	if(android.selection.webviewName == 'RIGHT_WEBVIEW' && android.selection.verticalWritingMode && !android.selection.leftToRight)
	{
		android.selection.latestTouchPoint = {'x':  e.clientX, 'y': e.clientY};
		if(e.clientY >= android.selection.pageHeight || e.clientY <= 0)
		{
			android.selection.beyondHorizontalBorder = true;			
		}
		else
		{
			if(e.clientX > -android.selection.rightPadding && e.clientX <= android.selection.leftPadding && android.selection.isMouseDown)
			{
				//var sel = window.getSelection();			
				//var range = sel.getRangeAt(0); 			
				//android.selection.latestTouchRange = range;
				//android.selection.latestTouchRangeSerialID = rangy.serializeRange(android.selection.latestTouchRange, true);
				if(android.selection.caretSet)
					android.selection.caretSet = false;
				android.selection.hotZone = true;
				android.selection.deadZone = false;
				android.selection.caretSet = false;
				//window.FORM.onMouseMove(JSON.stringify(android.selection.latestTouchPoint), "<leftPadding: " + android.selection.latestTouchRangeSerialID, android.selection.webviewName);			
			}
			else if(e.clientX > android.selection.leftPadding && android.selection.isMouseDown)
			{
				android.selection.hotZone = false;
				android.selection.deadZone = false;
				
				if(window.getSelection().rangeCount > 0)
				{
					var sel = window.getSelection();			
					var range = sel.getRangeAt(0); 			
					android.selection.latestTouchRange = range;
					
					android.selection.latestTouchRangeSerialID = rangy.serializeRange(android.selection.latestTouchRange, true);
								
					window.FORM.onMouseMove(JSON.stringify(android.selection.latestTouchPoint), "Normal: " + android.selection.latestTouchRangeSerialID, android.selection.webviewName);
					
				}
			}
			else if(e.clientX <= -android.selection.rightPadding)
			{
				android.selection.hotZone = true;
				android.selection.deadZone = true;		
				android.selection.caretSet = true;
				
				var result = window.FORM.getCaretRangeFromPointInTheOtherWebview((android.selection.latestTouchPoint.x + android.selection.pageWidth)+ ',' + android.selection.latestTouchPoint.y, android.selection.webviewName);
							
				//alert(result);
				var result = result.split(/\|/g);
				
				window.getSelection().extend(document.evaluate(result[0], document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue, Number(result[1]));
				
	/*			
				var range = document.createRange();
				
				range.setStart(android.selection.latestTouchRange.startContainer, android.selection.latestTouchRange.startOffset);
				
				range.setEnd(document.evaluate(result[0], document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue, Number(result[1]));
	*/
				var range = window.getSelection().getRangeAt(0);
				
				android.selection.latestTouchRange = range;
				android.selection.latestTouchRangeSerialID = rangy.serializeRange(android.selection.latestTouchRange, true);
				
				window.FORM.onMouseMove(JSON.stringify(android.selection.latestTouchPoint), "<= -android.selection.rightPadding: " + android.selection.latestTouchRangeSerialID, android.selection.webviewName);
			}
		}
	}	
	else if(android.selection.webviewName == 'LEFT_WEBVIEW' && android.selection.verticalWritingMode && !android.selection.leftToRight)
	{
		android.selection.latestTouchPoint = {'x':  e.clientX, 'y': e.clientY};
		
		if(e.clientY >= android.selection.pageHeight || e.clientY <= 0)
		{
			android.selection.beyondHorizontalBorder = true;			
		}
		else
		{
		
		
			if((e.clientX > (android.selection.pageWidth - android.selection.rightPadding)) && (e.clientX <= android.selection.pageWidth + android.selection.leftPadding) && android.selection.isMouseDown)
			{
				//var sel = window.getSelection();			
				//var range = sel.getRangeAt(0); 			
				//android.selection.latestTouchRange = range;
				//android.selection.latestTouchRangeSerialID = rangy.serializeRange(android.selection.latestTouchRange, true);
				if(android.selection.deadZone)
				{
					android.selection.latestTouchRange = android.selection.lastTouchRange;
				}
				android.selection.hotZone = true;
				android.selection.deadZone = false;
				android.selection.caretSet = false;		
			}
			else if((e.clientX < (android.selection.pageWidth - android.selection.rightPadding)) && android.selection.isMouseDown)
			{
				android.selection.hotZone = false;
				android.selection.deadZone = false;
				
				if(window.getSelection().rangeCount > 0)
				{
					var sel = window.getSelection();				
					var range = sel.getRangeAt(0); 			
					android.selection.latestTouchRange = range;
					
					android.selection.latestTouchRangeSerialID = rangy.serializeRange(android.selection.latestTouchRange, true);
									
					window.FORM.onMouseMove(JSON.stringify(android.selection.latestTouchPoint), "Normal: " + android.selection.latestTouchRangeSerialID, android.selection.webviewName);
				}
			}
			else if(e.clientX > (android.selection.pageWidth + android.selection.leftPadding))
			{
				android.selection.hotZone = true;
				android.selection.deadZone = true;		
				android.selection.caretSet = true;
				
				var result = window.FORM.getCaretRangeFromPointInTheOtherWebview((android.selection.latestTouchPoint.x - android.selection.pageWidth)+ ',' + android.selection.latestTouchPoint.y, android.selection.webviewName);
							
				var result = result.split(/\|/g);
				var range = document.createRange();
				
				range.setEnd(android.selection.lastTouchRange.startContainer, android.selection.lastTouchRange.startOffset);
				
				range.setStart(document.evaluate(result[0], document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue, Number(result[1]));
				
				android.selection.latestTouchRange = range;
				android.selection.latestTouchRangeSerialID = rangy.serializeRange(android.selection.latestTouchRange, true);
				
				window.FORM.onMouseMove(JSON.stringify(android.selection.latestTouchPoint), "> +android.selection.leftPadding: " + android.selection.latestTouchRangeSerialID, android.selection.webviewName);
			}	
	
		}
	}
	
};

android.selection.OnSelectionChange = function(event) 
{
	//alert('5');
	if(!android.selection.verticalWritingMode && android.selection.leftToRight)
	{
		if(window.getSelection().rangeCount>0)
		{
			//alert(android.selection.webviewName);
			//var rs = rangy.getSelection();
			var range = window.getSelection().getRangeAt(0);
			var selectionDetails = rangy.serializeRange(range);
			var h = selectionDetails.indexOf("{");
			var t = selectionDetails.indexOf("}");
			selectionDetails = selectionDetails.substring(0,h);
			
			//alert(rangy_range_id);
			
			/*
			var selectionDetails = android.selection.makeXPath(range.startContainer) + '|' 
			+ range.startOffset + '|' 
			+ android.selection.makeXPath(range.endContainer) + '|' 
			+ range.endOffset;
			*/
			//alert(selectionDetails);
			window.FORM.onSelectionChange(selectionDetails, android.selection.webviewName, false);
		}		
	}	
	else if(android.selection.verticalWritingMode && !android.selection.leftToRight)
	{
		if(android.selection.beyondHorizontalBorder)
		{
//Case 1: empty the selection		
			//alert('Emptify()');
			//window.getSelection().empty();
			//android.selection.beyondHorizontalBorder = false;
			//android.selection.latestTouchRangeSerialID = null;
			//android.selection.latestTouchRange = null;
			//return;			
//Case 2: restore to android.selection.latestTouchRange		
			//if(android.selection.latestTouchRange)
			//{
				//var sel = window.getSelection();
				//sel.empty();
				//sel.addRange(android.selection.latestTouchRange);
				//window.FORM.onSelectionChange(android.selection.latestTouchRangeSerialID, android.selection.webviewName, false);
/*
				if(android.selection.deadZone)
				{		
					window.FORM.onSelectionChange(android.selection.latestTouchRangeSerialID, android.selection.webviewName, false);
				}
				else
				{
					window.FORM.onSelectionChange(android.selection.latestTouchRangeSerialID, android.selection.webviewName, true);					
				}			
*/			
			//}
	
		}
		if(android.selection.webviewName == 'RIGHT_WEBVIEW' && window.getSelection().rangeCount>0)
		{			
			if(android.selection.hotZone)
			{
				//window.FORM.onSelectionChange('android.selection.hotZone = ' + android.selection.latestTouchRangeSerialID, android.selection.webviewName, false);
				
				var sel = window.getSelection();
				sel.empty();
				sel.addRange(android.selection.latestTouchRange);

				if(android.selection.deadZone)
				{		
					window.FORM.onSelectionChange(android.selection.latestTouchRangeSerialID, android.selection.webviewName, false);
				}
				else
				{
					window.FORM.onSelectionChange(android.selection.latestTouchRangeSerialID, android.selection.webviewName, true);					
				}
				android.selection.hotZone = false;
			}	
		}
		else if(android.selection.webviewName == 'LEFT_WEBVIEW' && window.getSelection().rangeCount>0)
		{					
			if(android.selection.hotZone)
			{
				window.FORM.onSelectionChange('android.selection.hotZone = ' + android.selection.latestTouchRangeSerialID, android.selection.webviewName, false);
				var sel = window.getSelection();
				sel.empty();
				sel.addRange(android.selection.latestTouchRange);
				
				if(android.selection.deadZone)
				{
					window.FORM.onSelectionChange(android.selection.latestTouchRangeSerialID, android.selection.webviewName, false);
				}
				else//(!android.selection.deadZone)
				{
					window.FORM.onSelectionChange(android.selection.latestTouchRangeSerialID, android.selection.webviewName, true);					
				}
				android.selection.hotZone = false;
			}	
		}
	}
};

//Lousy function
android.selection.showSelectionFromTheOtherWebview = function(selectionDetails)
{
	//alert(selectionDetails);
	//var sel = window.getSelection();
	//var range = document.createRange();
	//if(range!=null)
	//alert(range.toString());
	//window.getSelection().empty();
	if (selectionDetails != null) 
	{
		rangy.deserializeSelection(selectionDetails);
/*	
		selectionDetails = selectionDetails.split(/\|/g);
		range.setStart(document.evaluate(selectionDetails[0], document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue, Number(selectionDetails[1]));
		range.setEnd(document.evaluate(selectionDetails[2], document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue, Number(selectionDetails[3]));
		//android.selection.clearSelection();
		sel.empty();
		sel.addRange(range);
		//sel.focus();

		//alert("after");
*/		
	}	
};

/** 
 * Starts the touch and saves the given x and y coordinates as last touch point
 */
android.selection.startTouch = function(x, y)
{
	android.selection.isMouseDown = true;
	android.selection.lastTouchPoint = {'x':  x, 'y': y};
    if(android.selection.verticalWritingMode && !android.selection.leftToRight)
	{
		//alert('android.selection.verticalWritingMode && !android.selection.leftToRight');
		android.selection.lastTouchRange = document.caretRangeFromPoint(x, y);
	}
	
    //targetNode = range.startContainer;
    //offset = range.startOffset
    //range.setStart(targetNode, offset)	
	//alert(android.selection.lastTouchPoint.x + ' ' + y);
	//var temp = {'x': x,'y': y};
	var temp = x + ',' + y;
	//window.FORM.getMouseStatus(temp);
	window.FORM.onMouseDown(JSON.stringify(android.selection.lastTouchPoint), android.selection.webviewName);
	
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

android.selection.checkLastPage = function(top, id)
{	
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
	//return window.getSelection().rangeCount > 0;
};


/**
 *	Clears the current selection.
 */
android.selection.clearSelection = function()
{	
	try
	{
		// if current selection clear it.
	   	var sel = window.getSelection();
	   	sel.removeAllRanges();
	}catch(err){
		window.TextSelection.jsError(err);
	}	
};

/* 
http://home.arcor.de/martin.honnen/javascript/storingSelection1.html
this should suffice in HTML documents for selectable nodes, XML with namespaces needs more code 
*/
android.selection.makeXPath = function (node, currentPath) {
	currentPath = currentPath || '';
	switch (node.nodeType) {
		case 3:
		case 4:
			return android.selection.makeXPath(node.parentNode, 'text()[' + (document.evaluate('preceding-sibling::text()', node, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null).snapshotLength + 1) + ']');
		case 1:
			return android.selection.makeXPath(node.parentNode, node.nodeName + '[' + (document.evaluate('preceding-sibling::' + node.nodeName, node, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null).snapshotLength + 1) + ']' + (currentPath ? '/' + currentPath : ''));
		case 9:
			return '/' + currentPath;
		default:
			return '';
	}
};	

/**
 *	Handles the long touch action by selecting the last touched element.
 */
android.selection.longTouch = function(event) 
{	
	try
	{
	//alert("android.selection.left="+android.selection.left);
		var pageX = event.pageX;
		var pageY = event.pageY;
		var temp = pageX + ',' + pageY;
		window.FORM.onMouseUp(temp, android.selection.webviewName);
		//alert(android.selection.makeXPath(temp.startContainer) + '|' 
		//+ temp.startOffset + '|' 
		//+ android.selection.makeXPath(temp.endContainer) + '|' 
		//+ temp.endOffset);
		
		if(android.selection.verticalWritingMode && !android.selection.leftToRight)
		{
			if(android.selection.caretSet) //extended to the other webview
			{			
				if(android.selection.webviewName == 'LEFT_WEBVIEW')
				{
					//alert('111');
					android.selection.filter_left_border = 0;
					android.selection.filter_right_border = android.selection.pageWidth;		
					android.selection.filter_top_border = -android.selection.pageHeight;
					android.selection.filter_bottom_border = android.selection.pageHeight;					
				}
				else//(android.selection.webviewName=='RIGHT_WEBVIEW')
				{					
					//alert('222');
					android.selection.filter_left_border = 0;
					android.selection.filter_right_border = android.selection.pageWidth;		
					android.selection.filter_top_border = 0;
					android.selection.filter_bottom_border = android.selection.pageHeight * 2;

				}
				android.selection.caretSet = false;
			}
			else
			{
				//alert('333');
				android.selection.filter_left_border = 0;
				android.selection.filter_right_border = android.selection.pageWidth;		
				android.selection.filter_top_border = 0;
				android.selection.filter_bottom_border = android.selection.pageHeight;			
			}
		
		}		
		else//(!android.selection.verticalWritingMode && android.selection.leftToRight)
		{
			if(android.selection.webviewName == 'LEFT_WEBVIEW')
			{
				//alert('444');
				android.selection.filter_left_border = 0;
				android.selection.filter_right_border = android.selection.pageWidth * 2;	
				android.selection.filter_top_border = 0;
				android.selection.filter_bottom_border = android.selection.pageHeight;			
			
			}
			else//(webviewName == 'RIGHT_WEBVIEW')
			{
				//alert('555');
				android.selection.filter_left_border = -android.selection.pageWidth;
				android.selection.filter_right_border = android.selection.pageWidth;	
				android.selection.filter_top_border = 0;
				android.selection.filter_bottom_border = android.selection.pageHeight;
			}
		}
		
		var sel = window.getSelection();	
	   	var range = sel.getRangeAt(0);

		var flag_exceedborder = false;	
		// var boundingRect = range.getBoundingClientRect();
		// alert("boundingRect=" + boundingRect.left + "," +boundingRect.top + "," + boundingRect.right + "," + boundingRect.bottom);
		
		var rectsOOO = range.getClientRects();
		var realRects = new Array();
			
		var j = 0;
		// alert('rectsOOO.length='+rectsOOO.length);
		// for (j = 0;j < rectsOOO.length; j++)
		// {
			// alert("rectsOOO[" + j + "]=" + rectsOOO[j].left + "," +rectsOOO[j].top + "," + rectsOOO[j].right + "," + rectsOOO[j].bottom);
		// }		
			

		for (j = 0;j < rectsOOO.length; j++)
		{	
			if(rectsOOO[j].left >= rectsOOO[j].right || rectsOOO[j].top >= rectsOOO[j].bottom)// || rectsOOO[j].right < 0 || rectsOOO[j].bottom < 0)
			{
				continue;//filter out this Rectangle
			}
			
			if(rectsOOO[j].bottom > android.selection.filter_bottom_border || rectsOOO[j].top < android.selection.filter_top_border || rectsOOO[j].right > android.selection.filter_right_border || rectsOOO[j].left < android.selection.filter_left_border )				
			{
				//flag_exceedborder = true;
				//alert('flag_exceedborder in ' + android.selection.webviewName);
				continue;//filter out this Rectangle
			}
			realRects.push(rectsOOO[j]);
		}		
		// for (j = 0;j < realRects.length; j++){
			// alert("realRects[" + j + "]=" + realRects[j].left + "," +realRects[j].top + "," + realRects[j].right + "," + realRects[j].bottom);
		// }
//New version		
		var rectsArray = new Array();
		if(android.selection.leftToRight == true)
		{	
			//alert("length of realRects =" + realRects.length);
			for (j = 0; j < realRects.length; j++)
			{			
				rectsArray[j] = true;
				for (var k = 0; k < j; k++)
				{
					if(rectsArray[k])
					{
						if(realRects[k].width >= realRects[j].width)
						{
							if(android.selection.intersectRect(realRects[k], realRects[j]))
							{
								rectsArray[k] = false;
								//continue;
							}
						}
						else {
							if(android.selection.intersectRect(realRects[k], realRects[j]))
							{
								rectsArray[j] = false;
								continue;
							}				
						}
					}
				}			
			}
		}
		else
		{
//android.selection.leftToRight == false	
			//alert("length of realRects =" + realRects.length);
			for (j = 0; j < realRects.length; j++)
			{			
				rectsArray[j] = true;
				for (var k = 0; k < j; k++)
				{
					if(rectsArray[k])
					{
						if(realRects[k].height >= realRects[j].height)
						{
							
							if(android.selection.intersectRect(realRects[k], realRects[j]))
							{
								//if(realRects[j].height < 4)
									//continue;
								rectsArray[k] = false;
								//continue;
								//alert("realRects[" + k + "] is false!");
							}
						}
						else {
							if(android.selection.intersectRect(realRects[k], realRects[j]))
							{
								//if(realRects[k].height < 4)
									//continue;
								rectsArray[j] = false;
								//alert("realRects[" + j + "] is false!");
								continue;
							}				
						}
					}
				}			
			}		
		}
		
		if(!flag_exceedborder)
		{
			//alert("!flag_exceedborder");
			//alert("longTouch:before save");
			//android.selection.saveSelectionStart();
		
			var saveRange = document.createRange();			
			saveRange.setStart(range.startContainer, range.startOffset);
			android.selection.selectionStartRange = saveRange;

			//android.selection.saveSelectionEnd();	
			//var saveRange = document.createRange();
			saveRange.setStart(range.endContainer, range.endOffset);		
			android.selection.selectionEndRange = saveRange;	

			// Show the context menu in app
			//alert('android.selection.selectionChanged(event.pageX, event.pageY)');
			//android.selection.filteredRects = realRects;
			for (j = 0; j < rectsArray.length; j++)
			{
				if(rectsArray[j])
					android.selection.filteredRects.push(realRects[j]);
			}
			//alert('android.selection.filteredRects.length = ' + android.selection.filteredRects.length);
			
			android.selection.selectionChanged(event.pageX, event.pageY, realRects.toString());	
			
			android.selection.needFilterAgain = false;
			sel.removeAllRanges();
	
//Attach SelectionChange handler here?
			window.FORM.notifyToAttachOnSelectionChangeHandler(android.selection.webviewName, false);	
			//window.FORM.notifyToAttachOnSelectionChangeHandler(android.selection.webviewName, true);				
		}
		else
		{
			android.selection.needFilterAgain = true;
			alert('flag_exceedborder in ' + android.selection.webviewName);
			//var boundingRect = range.getBoundingClientRect();
			//alert("boundingRect=" + boundingRect.left + " " + boundingRect.top + " " + boundingRect.right + " " + boundingRect.bottom);
			
			sel.removeAllRanges();
			
	//New version		
			var rectsArray = new Array();
			if(android.selection.leftToRight == true)
			{		
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

			}
			else
			{
	//android.selection.leftToRight == false	
				//alert("length of realRects =" + realRects.length);
				for (j = 0; j < realRects.length; j++){			
					rectsArray[j] = true;
					for (var k = 0; k < j; k++){
						if(rectsArray[k]){
							if(realRects[k].height >= realRects[j].height)
							{
								if(android.selection.intersectRect(realRects[k], realRects[j])){
									rectsArray[k] = false;
									//continue;
									//alert("realRects[" + k + "] is false!");
								}
							}
							else {
								if(android.selection.intersectRect(realRects[k], realRects[j])){
									rectsArray[j] = false;
									//alert("realRects[" + j + "] is false!");
									continue;
								}				
							}
						}
					}
				
				}		
			}
			
			if(android.selection.leftToRight == false)
			{
	//New version: Find out the most left-top rectangle.	
				// var startX = 10000,	startY = 10000, endX=-1, endY=-1;
				// var startX2 = 10000, startY2 = 10000, endX2=-1, endY2=-1;
				var startX = -10000,	startY = -10000, endX=10000, endY=10000;
				var startX2 = -10000, startY2 = -10000, endX2=10000, endY2=10000;

				//alert("BEFORE startX=" + startX + "\n" + "startY=" + startY + "\n" + "endX=" + endX + "\n" + "endY=" + endY);
				for (j = 0; j < realRects.length; j++) {
					var realRect = realRects[j];
					if(!rectsArray[j])
						continue;
					//if(android.selection.multiColumnCount==1)
					//{
					
					if(realRect.left >= android.selection.filter_left_border && realRect.right <= android.selection.filter_right_border)
					{
						if(realRect.left > startX)
						{
							startY = realRect.top;
							startX = realRect.left;
						}
						else if(realRect.left == startX && realRect.top > startY)
						{
							startY = realRect.top;
						}
						
						if(realRect.right < endX)
						{
							endX = realRect.right;	
							endY = realRect.bottom;
						}
						else if(realRect.right == endX && realRect.bottom < endY)
						{
							endY = realRect.bottom;
						}				
					}				
					//}
					
				}
				//alert("AFTER startX=" + startX + "\n" + "startY=" + startY + "\n" + "endX=" + endX + "\n" + "endY=" + endY + "\n" + "startX2=" +startX2 + "\n" + "startY2=" + startY2 + "\n" + "endX2=" + endX2 + "\n" + "endY2=" + endY2);	
				if(endX2 != 10000 && endY2 != 10000 && startX != -10000 && startY != -10000)
				{
					endX = endX2;
					endY = endY2;
				}
				else if(endX2 != 10000 && endY2 != 10000)
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
				
				android.selection.createSelectionFromPoint(startX, startY, endX, endY);
				//alert("after createSelectionFromPoint");	
				
				//alert("longTouch:before save");
				android.selection.saveSelectionStart();
				android.selection.saveSelectionEnd();
				//alert("longTouch:after save");
				
				// Show the context menu in app
				android.selection.selectionChanged(event.pageX, event.pageY, '');
				//android.selection.selectBetweenHandles();
			}
			else
			{
	//android.selection.leftToRight == true
				var startX = 10000,	startY = 10000, endX = -10000, endY = -10000;
				var startX2 = 10000, startY2 = 10000, endX2 = -10000, endY2 = -10000;
				//alert("BEFORE startX=" + startX + "\n" + "startY=" + startY + "\n" + "endX=" + endX + "\n" + "endY=" + endY);
				for (j = 0; j < realRects.length; j++) {
					var realRect = realRects[j];
					if(!rectsArray[j])
						continue;
					//if(android.selection.multiColumnCount==1)
					//{
					//if(realRect.left>=0 && realRect.right<=android.selection.pageWidth)
					if(realRect.left >= android.selection.filter_left_border && realRect.right <= android.selection.filter_right_border)
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
					//}

				}
				//alert("AFTER startX=" + startX + "\n" + "startY=" + startY + "\n" + "endX=" + endX + "\n" + "endY=" + endY + "\n" + "startX2=" +startX2 + "\n" + "startY2=" + startY2 + "\n" + "endX2=" + endX2 + "\n" + "endY2=" + endY2);	
				if(endX2!=-10000 && endY2!=-10000 && startX!=10000 && startY!=10000)
				{
					endX = endX2;
					endY = endY2;
				}
				else if(endX2!=-10000 && endY2!=-10000)
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
				//alert("android.selection.left=" + android.selection.left);
				android.selection.createSelectionFromPoint(startX, startY, endX, endY);
				//alert("after createSelectionFromPoint");	
				
				//alert("longTouch:before save");
				android.selection.saveSelectionStart();
				android.selection.saveSelectionEnd();
				//alert("longTouch:after save");
				
				// Show the context menu in app
				android.selection.selectionChanged(event.pageX, event.pageY, '');
				//android.selection.selectBetweenHandles();		

			}
//Attach SelectionChange handler here?
			//window.FORM.notifyToAttachOnSelectionChangeHandler(android.selection.webviewName, true);
	   	}
	 }
	 catch(err){
	 	//window.TextSelection.jsError(err);
		alert("longTouch:"+err.toString());
	 }
//Attach SelectionChange handler here?
	//window.FORM.notifyToAttachOnSelectionChangeHandler(android.selection.webviewName);   	
};

android.selection.filteredRects = [];
/**
 * Tells the app to show the context menu. 
 */
android.selection.selectionChanged = function(x, y, rects){
	
	try{
		
		
		
		var sel = window.getSelection();
		//alert('selectionChanged sel = ' + sel.toString());
		if(!sel){
			//alert("return");
			return;
		}
		
		var range = sel.getRangeAt(0);
		//alert("selectionChanged range="+range.startContainer+ " " +range.startOffset + " "+ range.endContainer + " " +range.endOffset);
	
		
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
		//alert("text:"+text.toString());
		// alert("handleBounds:"+handleBounds.toString());
		// alert("menuBounds:"+menuBounds.toString());
		// alert("x:"+x.toString());
		// alert("y:"+y.toString());
		//window.FORM.getHandleBounds(handleBounds.toString());
		
		//window.FORM.getMenuBounds(menuBounds.toString());
	   	// Tell the interface that the selection changed
	   	//window.TextSelection.selectionChanged(rangyRange, text, //handleBounds, menuBounds);
		//alert("window.FORM.selectionChanged");
		
		window.FORM.selectionChanged(rangyRange.toString(), text.toString(), handleBounds.toString(), menuBounds.toString(), x.toString(), y.toString());
		
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
		//alert("getTotalPages:"+err.toString());
	}
}

android.selection.elementRectsByIdentifier = function(serializerId){
    //alert("elementRectsByIdentifier");
	try
	{
		//alert("serializerId = " + serializerId.toString());
		var rects = [];
		//var results = '';//new Array();
		if(!android.selection.needFilterAgain)
		{
			//alert("!android.selection.needFilterAgain = " + !android.selection.needFilterAgain);
			//alert('length of android.selection.filteredRects in android.selection.elementRectsByIdentifier = ' + android.selection.filteredRects.length);
			//for (j = 0;j < android.selection.filteredRects.length; j++){
				//alert("android.selection.filteredRects[" + j + "].left=" + android.selection.filteredRects[j].left);
			//}
			//rects = android.selection.filteredRects;
				//alert("filterResults.length = " + filterResults.length);
			rects = new Array();
			for (var tt = 0; tt < android.selection.filteredRects.length; tt++) 
			{//get the selector ranct
				var goodRect = android.selection.filteredRects[tt];
				//alert("goodRect.left="+goodRect.left+",goodRect.top="+goodRect.top+"\n goodRect.right="+goodRect.right+",goodRect.bottom="+goodRect.bottom);
				var handleBounds = "{'left': " + goodRect.left + ", ";
				handleBounds += "'top': " + goodRect.top + ", ";
				handleBounds += "'right': " + goodRect.right + ", ";
				handleBounds += "'bottom': " + goodRect.bottom + "}";
				rects.push(handleBounds);
			}
			rects = rects.join(';');
			android.selection.needFilterAgain = true;
			android.selection.filteredRects.length = 0;
		}
		else
		{
			//alert("android.selection.needFilterAgain = " + android.selection.needFilterAgain);
			rects = android.selection.getRectsByIdentifier(serializerId);
			//alert("rects.toString = " + rects.toString());
		}
		//var rects = android.selection.getRectsByIdentifier(serializerId);
		//alert("line 1089 in " + android.selection.webviewName );
		//alert("line 1090 rects = " + rects.toString());
		//alert("line 1091 in " + android.selection.webviewName );
		//window.TextSelection.returnElementRectsByIdentifier(android.seltion.getRe//ctsByIdentifier(serializerId));
		//alert("rects="+rects.toString());
		//window.TextSelection.returnElementRectsByIdentifier(rects);
		
		window.FORM.returnElementRectsByIdentifier(rects.toString());
    }
	catch(err)
	{
		//window.TextSelection.jsError(err);
		//alert("elementRectsByIdentifier:"+err.toString());
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
	//alert("android.selection.getRectsByIdentifier");
	//alert("serializerId = " + serializerId.toString());
	var sel = rangy.deserializeSelection(serializerId);
	var selection  = sel.nativeSelection;
	var range = selection.getRangeAt(0);
	//var results = [];
	//var rects = range.getClientRects();
	var rectsOOO = range.getClientRects();
	
	var realRects = new Array();
	var rectsArray = new Array();
	var j = 0;
	
	if(android.selection.verticalWritingMode && !android.selection.leftToRight)
	{
		if(android.selection.caretSet) //extended to the other webview
		{			
			if(android.selection.webviewName == 'LEFT_WEBVIEW')
			{
				//alert('111');
				android.selection.filter_left_border = 0;
				android.selection.filter_right_border = android.selection.pageWidth;		
				android.selection.filter_top_border = -android.selection.pageHeight;
				android.selection.filter_bottom_border = android.selection.pageHeight;					
			}
			else//(android.selection.webviewName=='RIGHT_WEBVIEW')
			{					
				//alert('222');
				android.selection.filter_left_border = 0;
				android.selection.filter_right_border = android.selection.pageWidth;		
				android.selection.filter_top_border = 0;
				android.selection.filter_bottom_border = android.selection.pageHeight * 2;

			}
			//android.selection.caretSet = false;
		}
		else
		{
			//alert('333');
			android.selection.filter_left_border = 0;
			android.selection.filter_right_border = android.selection.pageWidth;		
			android.selection.filter_top_border = 0;
			android.selection.filter_bottom_border = android.selection.pageHeight;			
		}
	
	}		
	else//(!android.selection.verticalWritingMode && android.selection.leftToRight)
	{
		if(android.selection.webviewName == 'LEFT_WEBVIEW')
		{
			//alert('444');
			android.selection.filter_left_border = 0;
			android.selection.filter_right_border = android.selection.pageWidth * 2;	
			android.selection.filter_top_border = 0;
			android.selection.filter_bottom_border = android.selection.pageHeight;			
		
		}
		else//(webviewName == 'RIGHT_WEBVIEW')
		{
			//alert('555');
			android.selection.filter_left_border = -android.selection.pageWidth;
			android.selection.filter_right_border = android.selection.pageWidth;	
			android.selection.filter_top_border = 0;
			android.selection.filter_bottom_border = android.selection.pageHeight;
		}
	}

	for (j = 0;j < rectsOOO.length; j++)
	{	
		if(rectsOOO[j].left >= rectsOOO[j].right || rectsOOO[j].top >= rectsOOO[j].bottom)
		{
			continue;//filter out this Rectangle
		}
		
		if(android.selection.verticalWritingMode == false)
		{
			if(rectsOOO[j].bottom > android.selection.filter_bottom_border || rectsOOO[j].top < android.selection.filter_top_border)				
			{				
				continue;//filter out this Rectangle
			}
		}
		else//(android.selection.verticalWritingMode == true)
		{
			if(rectsOOO[j].left < android.selection.filter_left_border || rectsOOO[j].right > android.selection.filter_right_border)				
			{				
				continue;//filter out this Rectangle
			}				
		}
		realRects.push(rectsOOO[j]);
	}		
	//for (j = 0;j < realRects.length; j++){
		//alert("line 1189 => realRects[" + j + "]=" + realRects[j].left + "," +realRects[j].top + "," + realRects[j].right + "," + realRects[j].bottom);
	//}
//New version			
	if(android.selection.leftToRight == true)
	{	
		for (j = 0; j < realRects.length; j++)
		{			
			rectsArray[j] = true;
			for (var k = 0; k < j; k++)
			{
				if(rectsArray[k])
				{
					if(realRects[k].width >= realRects[j].width)
					{
						if(android.selection.intersectRect(realRects[k], realRects[j]))
						{
							rectsArray[k] = false;
							//continue;
						}
					}
					else {
						if(android.selection.intersectRect(realRects[k], realRects[j]))
						{
							rectsArray[j] = false;
							continue;
						}				
					}
				}
			}	
		}
	}
	else
	{
//android.selection.leftToRight == false	
		for (j = 0; j < realRects.length; j++)
		{			
			rectsArray[j] = true;
			for (var k = 0; k < j; k++)
			{
				if(rectsArray[k])
				{
					if(realRects[k].height >= realRects[j].height)
					{
						if(android.selection.intersectRect(realRects[k], realRects[j]))
						{
							rectsArray[k] = false;
							//continue;
							//alert("realRects[" + k + "] is false!");
						}
					}
					else {
						if(android.selection.intersectRect(realRects[k], realRects[j]))
						{
							rectsArray[j] = false;
							//alert("realRects[" + j + "] is false!");
							continue;
						}				
					}
				}
			}	
		}		
	}
	
	var results = [];
	for (j = 0; j < rectsArray.length; j++)
	{	
		if(rectsArray[j]){			
			var goodRect = realRects[j];
			//alert('line 1262 => goodRect.left=' + goodRect.left + ',goodRect.top=' + goodRect.top +'\n goodRect.right=' + goodRect.right + ',goodRect.bottom=' + goodRect.bottom);
			var handleBounds = "{'left': " + goodRect.left + ", ";
			handleBounds += "'top': " + goodRect.top + ", ";
			handleBounds += "'right': " + goodRect.right + ", ";
			handleBounds += "'bottom': " + goodRect.bottom + "}";
			results.push(handleBounds);
		}
	}
	//alert('line 1270 => results.length = ' + results.length);  
	//alert(results.join(';').toString());
	return results.join(';').toString();
};

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
};

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
		//alert("saveSelectionStart:"+err.toString());
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
		//alert("saveSelectionEnd:"+err.toString());
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
    // alert("startX=" + startX +"," +
	   // "startY=" + startY +"," +
	   // "endX=" + endX +","+
	   // "endY=" + endY);
	
	var doc = document;
    var start, end, range = null;
    if (typeof doc.caretPositionFromPoint != "undefined") 
	{
		//alert("case 1");
        start = doc.caretPositionFromPoint(startX, startY);
        end = doc.caretPositionFromPoint(endX, endY);
        range = doc.createRange();
        range.setStart(start.offsetNode, start.offset);
        range.setEnd(end.offsetNode, end.offset);
		//return range;
    } 
	else if (typeof doc.caretRangeFromPoint != "undefined") 
	{
		
		if(android.selection.leftToRight == false)
		{
			//alert("case 2");
			start = doc.caretRangeFromPoint(startX+Math.floor(android.selection.fontSize/5), startY+Math.floor(android.selection.fontSize/5));
			end = doc.caretRangeFromPoint(endX-Math.floor(android.selection.fontSize/5), endY-Math.floor(android.selection.fontSize/5));
			//start = doc.caretRangeFromPoint(startX, startY);
			//end = doc.caretRangeFromPoint(endX, endY);
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
		else
		{
			//alert("case 7");
			start = doc.caretRangeFromPoint(startX, startY + Math.floor(android.selection.fontSize/5));
			//start = doc.caretRangeFromPoint(startX+Math.floor(android.selection.fontSize/5), startY+Math.floor(android.selection.fontSize/5));
			//start = doc.caretRangeFromPoint(startX, startY);
			//alert("android.selection.fontSize=" + android.selection.fontSize);
			//alert(Math.ceil(android.selection.fontSize/5));
			
			end = doc.caretRangeFromPoint(endX, endY - Math.ceil(android.selection.fontSize/5));
			//end = doc.caretRangeFromPoint(endX, endY);
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
    }
    if (range !== null && typeof window.getSelection != "undefined") {	
        //alert("5");
		var sel = window.getSelection();
        sel.removeAllRanges();
		//alert("addRange");
        sel.addRange(range);		
    } else if (typeof doc.body.createTextRange != "undefined")
	{		
		//alert("6");
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
};

android.selection.intersectRect = function(r1, r2) {
    return !(r2.left >= r1.right || 
           r2.right <= r1.left || 
           r2.top >= r1.bottom ||
           r2.bottom <= r1.top);
};

android.selection.encloseRect = function(r1, r2) {	   
	return ((r1.left >= r2.left) && 
	       (r1.right <= r2.right) && 
           (r1.top >= r2.top) &&
           (r1.bottom <= r2.bottom));	    
};

android.selection.returnTrue = function() {
	return true;
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
