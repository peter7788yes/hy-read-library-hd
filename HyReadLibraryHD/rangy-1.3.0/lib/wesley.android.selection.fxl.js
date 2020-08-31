// Namespace
var android = {};
android.selection = {};
	
/** Boundary values for filtering rectangles **/
/** One Webview(what you can see) Width & Height **/
android.selection.pageWidth = 0;
android.selection.pageHeight = 0;

android.selection.webviewName;

//android.selection.needFilterAgain = true;

//Filter out UNREASONABLE rectanges.
android.selection.filter_left_border = 0;
android.selection.filter_right_border = 0;
android.selection.filter_top_border = 0;
android.selection.filter_bottom_border = 0;	


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
 //alert('longTouch');
 try
 {
  var pageX = event.pageX;
  var pageY = event.pageY;
  var temp = pageX + ',' + pageY;
  window.FORM.onMouseUp(temp, android.selection.webviewName);
  
  if(android.selection.hasSelection())
  {
   var sel = window.getSelection(); 
   var range = sel.getRangeAt(0);

   var result = highlighter.serializeSelection();
   //var rangeObj = window.getSelection().getRangeAt(0);
   var handleBounds = android.selection.getHandleBounds2(range);

   window.FORM.returnSelectionStartAndEnd(android.selection.webviewName, result.start, result.end, event.clientX, event.clientY, handleBounds.toString(), range.toString());
  }
  
 }
 catch(err){
   //window.TextSelection.jsError(err);
  //alert("longTouch:"+err.toString());
 }
 
};

android.selection.filteredRects = [];

android.selection.intersectRect = function(r1, r2) {
    return !(r2.left >= r1.right || 
           r2.right <= r1.left || 
           r2.top >= r1.bottom ||
           r2.bottom <= r1.top);
};

android.selection.getHandleBounds2 = function(range){
	
	var boundingRect = Math.round(range.getBoundingClientRect().left) + "," + Math.round(range.getBoundingClientRect().top) + "," + Math.round(range.getBoundingClientRect().right) + "," + Math.round(range.getBoundingClientRect().bottom);
	//alert(boundingRect);
	return boundingRect;
/*
	var rectsOOO = range.getClientRects();	
	var realRects = new Array();
	var rectsArray = new Array();
	var j = 0;

	var hleft = android.selection.pageWidth;
	var htop = android.selection.pageHeight;	
	var hright = -1;
	var hbottom = -1;
	
	if(android.selection.verticalWritingMode && !android.selection.leftToRight)
	{
		alert('333');
		android.selection.filter_left_border = 0;
		android.selection.filter_right_border = android.selection.pageWidth;		
		android.selection.filter_top_border = 0;
		android.selection.filter_bottom_border = android.selection.pageHeight;			
	}		
	else//(!android.selection.verticalWritingMode && android.selection.leftToRight)
	{

		//alert('444');
		android.selection.filter_left_border = 0;
		android.selection.filter_right_border = android.selection.pageWidth;	
		android.selection.filter_top_border = 0;
		android.selection.filter_bottom_border = android.selection.pageHeight;			

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
	// for (j = 0;j < realRects.length; j++){
		// alert("realRects[" + j + "]=" + realRects[j].left + "," +realRects[j].top + "," + realRects[j].right + "," + realRects[j].bottom);
	// }
//New version			
	if(android.selection.leftToRight == true)
	{	
		for (j = 0; j < realRects.length; j++)
		{			
			rectsArray[j] = true;
			// for (var k = 0; k < j; k++)
			// {
				// if(rectsArray[k])
				// {
					// if(realRects[k].width >= realRects[j].width)
					// {
						// if(android.selection.intersectRect(realRects[k], realRects[j]))
						// {
							// rectsArray[k] = false;
							// //continue;
						// }
					// }
					// else {
						// if(android.selection.intersectRect(realRects[k], realRects[j]))
						// {
							// rectsArray[j] = false;
							// continue;
						// }				
					// }
				// }
			// }	
		}
	}
	else
	{
//android.selection.leftToRight == false	
		for (j = 0; j < realRects.length; j++)
		{			
			rectsArray[j] = true;
			// for (var k = 0; k < j; k++)
			// {
				// if(rectsArray[k])
				// {
					// if(realRects[k].height >= realRects[j].height)
					// {
						// if(android.selection.intersectRect(realRects[k], realRects[j]))
						// {
							// rectsArray[k] = false;
							// //continue;
							// //alert("realRects[" + k + "] is false!");
						// }
					// }
					// else {
						// if(android.selection.intersectRect(realRects[k], realRects[j]))
						// {
							// rectsArray[j] = false;
							// //alert("realRects[" + j + "] is false!");
							// continue;
						// }				
					// }
				// }
			// }	
		}		
	}
	
	var results = [];
	for (j = 0; j < rectsArray.length; j++)
	{	
		if(rectsArray[j]){			
			var goodRect = realRects[j];
			//alert('goodRect.left=' + goodRect.left + ',goodRect.top=' + goodRect.top +'\n goodRect.right=' + goodRect.right + ',goodRect.bottom=' + goodRect.bottom);
					
			if(htop == android.selection.pageHeight || goodRect.top <= htop) {
				htop = goodRect.top;
				//hleft = goodRect.left;
			}
			if(hbottom < 0 || goodRect.bottom >= hbottom) {
				hbottom = goodRect.bottom;
				//hright = goodRect.right;
			}
			if(hleft == android.selection.pageWidth || goodRect.left <= hleft) {
				//htop = goodRect.top;
				hleft = goodRect.left;
			}
			if(hright < 0 || goodRect.right >= hright) {
				//hbottom = goodRect.bottom;
				hright = goodRect.right;
			}		
		}
	}
	var handleBounds = "" + hleft + ", ";
	handleBounds += "" + htop + ", ";
	handleBounds += "" + hright + ", ";
	handleBounds += "" + hbottom + "";  
	alert("handleBounds2=" + handleBounds);
	//alert('line 1270 => results.length = ' + results.length);  
	//alert(results.join(';').toString());
	return handleBounds;//results.join(';').toString();
*/	
};

android.selection.getHandleBounds = function(element){
//_dotNetWindowWidth/Height is not correct values for rect filtering	
	//var hleft = _dotNetWindowWidth;
	//var htop = _dotNetWindowHeight;
	var hleft = android.selection.pageWidth;
	var htop = android.selection.pageHeight;	
	var hright = -1;
	var hbottom = -1;

	// var hleft = -1;
	// var htop = 0;
	// var hright = -1;
	// var hbottom = 0;
//Unnecessary to clear selection area	
	//android.selection.clearSelection();
	//sel.addRange(element);
	
	var rects = element.getClientRects();
	var i;
	for (i = 0; i < rects.length; i++)
	{
		alert(rects[i].left + "," + rects[i].top + "," + rects[i].right + "," + rects[i].bottom);
	}
	//var rects = element.getBoundingClientRect();
	alert("rects.length=" + rects.length);
	var filterResults = [];
	for (i = 0; i < rects.length; i++) {//filter bad rects
		var rect = rects[i];
		var isGood = true;
		if (rect.left == rect.right || rect.top == rect.bottom)
		{
			isGood = false;
		}
		if(isGood)
		{
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
		}		
		if(isGood) {
			alert("Good rect:" + rect.left + "," + rect.top + "," + rect.right + "," + rect.bottom ); 
			filterResults.push(rect);
		}
	}	
	
    for (var i = 0; i < filterResults.length; i++) {//get the selector ranct
		//alert("i="+i);
		var goodRect = filterResults[i];	
		if(htop == android.selection.pageHeight || goodRect.top <= htop) {
			htop = goodRect.top;
			//hleft = goodRect.left;
		}
		if(hbottom < 0 || goodRect.bottom >= hbottom) {
			hbottom = goodRect.bottom;
			//hright = goodRect.right;
		}
		if(hleft == android.selection.pageWidth || goodRect.left <= hleft) {
			//htop = goodRect.top;
			hleft = goodRect.left;
		}
		if(hright < 0 || goodRect.right >= hright) {
			//hbottom = goodRect.bottom;
			hright = goodRect.right;
		}		
		
    }
   
    // var handleBounds = "{'left': " + hleft + ", ";
	   	// handleBounds += "'top': " + htop + ", ";
	   	// handleBounds += "'right': " + hright + ", ";
	   	// handleBounds += "'bottom': " + hbottom + "}";
    var handleBounds = "" + hleft + ", ";
	   	handleBounds += "" + htop + ", ";
	   	handleBounds += "" + hright + ", ";
	   	handleBounds += "" + hbottom + "";  
	alert("handleBounds="+handleBounds);
    return handleBounds;
};

android.selection.getRectangles = function(element){
   var results = new Array();
   var rects = element.getClientRects();
   for (var i = 0; i < rects.length; i++) {
       var rect = rects[i];
       results[i] = "{{" + rect.left + "," + rect.top + "}, {" + rect.width + "," + rect.height + "}}";
   }
   return results.join(';');
};

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
};

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
};

android.selection.searchAnnotationByRangyId = function(serializerIdString){
	//alert("searchAnnotationByRangyId");
	var elementRectsResult = android.selection.getRectsByIdentifier(serializerIdString);
	//window.TextSelection.returnSearchAnnotationByRangyId(elementRectsResult);
	window.FORM.returnElementRectsByIdentifier(elementRectsResult);
};

//To fix
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
	//alert(results.join(';').toString());
	return results.join(';').toString();
};

android.selection.getElementLeftTop = function(id) {
    var ele = document.getElementById(id);
    var top = 0;
    var left = 0; 
    while(ele.tagName != "BODY") {
        top += ele.offsetTop;
        left += ele.offsetLeft;
        ele = ele.offsetParent;
    }   
    return { left: left, top: top }; 
};

android.selection.removeNoteMarkByID = function(id){
	var oDiv = document.getElementById("" + id);
	document.body.removeChild(oDiv);
};

android.selection.removeNoteMarkByLeftAndTop = function(left, top){
	var oDiv = document.getElementById(left + "-" + top);
	document.body.removeChild(oDiv);
};


var NOTE_MARK_WIDTH = 30;
var NOTE_MARK_HEIGHT  = 30;
android.selection.addNoteMark = function(start, end, left, top){
	var div = document.createElement("div");
	if(android.selection.leftToRight == true)
	{
		if(android.selection.webviewName == 'LEFT_WEBVIEW')
		{	
			//alert('L_W');			
			//div.setAttribute("id", start + "-" + end );
			div.setAttribute("id", "" + top );
			div.style.width = NOTE_MARK_WIDTH + "px";
			div.style.height = NOTE_MARK_HEIGHT + "px";		
			//div.style.zIndex = 1;//-1;
			//div.style.background = "red";
			div.style.background = "url(ios_epub_pen.png)";
			div.style.position = "absolute";
			//div.style.margin = "0px";
			//div.style.padding = "0px";
			div.style.display = "block";
			div.style.left = left + "px";
			div.style.top = top + "px";
			//div.setAttribute("onclick","alert('" + div.style.width + "');" );
			div.setAttribute("onclick","window.FORM.showNote('" + android.selection.webviewName + "','" + div.getAttribute("id") + "')" );
			document.body.appendChild(div);
		}
		else if(android.selection.webviewName == 'RIGHT_WEBVIEW')
		{
			//alert('R_W');			
			//div.setAttribute("id","" + start + "-" + end );
			div.setAttribute("id","" + top );
			div.style.width = NOTE_MARK_WIDTH + "px";
			div.style.height = NOTE_MARK_HEIGHT + "px";
			//div.style.zIndex = 1;//-1;
			//div.style.background = "red";
			div.style.background = "url(ios_epub_pen.png)";
			div.style.position = "absolute";
			//div.style.margin = "0px";
			//div.style.padding = "0px";
			div.style.display = "block";
			div.style.left = left + "px";
			div.style.top = top + "px";
			//div.setAttribute("onclick","alert('" + div.getAttribute("id") + "');" );
			div.setAttribute("onclick","window.FORM.showNote('" + android.selection.webviewName + "','" + div.getAttribute("id") + "')" );
			document.body.appendChild(div);	
		}
	}
};

HashMap = function()
{
	/** Map 大小 **/
	var size = 0;
	/** 对象 **/
	var entry = new Object();

	/** 存 **/
	this.put = function (key , value)
	{
	 if(!this.containsKey(key))
	 {
		 size ++ ;
	 }
	 entry[key] = value;
	}

	/** 取 **/
	this.get = function (key)
	{
	 if( this.containsKey(key) )
	 {
		 return entry[key];
	 }
	 else
	 {
		 return null;
	 }
	}

	/** 删除 **/
	this.remove = function ( key )
	{
	 if( delete entry[key] )
	 {
		 size --;
	 }
	}

	/** 是否包含 Key **/
	this.containsKey = function ( key )
	{
	 return (key in entry);
	}

	/** 是否包含 Value **/
	this.containsValue = function ( value )
	{
	 for(var prop in entry)
	 {
		 if(entry[prop] == value)
		 {
			 return true;
		 }
	 }
	 return false;
	}

	/** 所有 Value **/
	this.values = function ()
	{
	 var values = new Array(size);
	 for(var prop in entry)
	 {
		 values.push(entry[prop]);
	 }
	 return values;
	}

	/** 所有 Key **/
	this.keys = function ()
	{
	 var keys = new Array(size);
	 for(var prop in entry)
	 {
		 keys.push(prop);
	 }
	 return keys;
	}

	/** Map Size **/
	this.size = function ()
	{
	 return size;
	}
};

var audioCount = 0;
var totalAudioDurationCount = 0, audioDurationCountFlag = false;

//var audioPlayMap = null;

android.selection.setHtml5AudioDurationCount = function(durCount){
	//alert("setCount" + durCount);
	totalAudioDurationCount = durCount;
};

android.selection.addHtml5Audio = function(mediaFilename, cssClassName, mimetype){
	
	var audio = document.createElement("audio");


		//alert('addHtml5Audio' + cssClassName);		
		audio.setAttribute("id", "audio" + (++audioCount) );
		audio.setAttribute("src", mediaFilename);
		audio.setAttribute("type", mimetype);
		
		audio.setAttribute("oncanplay", "onAudioCanPlay(this)");
		audio.setAttribute("onended", "onAudioEnded(this)");
		audio.setAttribute("ontimeupdate", "onAudioTimeUpdate(this)");
		//alert('before 1');
		audio.map = new HashMap();
		//alert('before 2');		
		if(cssClassName != ""){
			audio.cssClassName = cssClassName;
			//alert(audio.cssClassName);
		}
		else
			audio.cssClassName = "";
		//alert('before 3');	
		document.body.appendChild(audio);
		//alert('after 3');
		//var test = document.getElementById('audio1');
		//alert(test.ontimeupdate);
		
};


android.selection.playAudio = function(){
	var audio = document.getElementById("audio" + audioCount);	
	if(audio!=undefined){
		//alert("play()");
		audio.play();
	}
	else{
		//alert("play() error");
	}
};

android.selection.playAudioByTime = function(startTime){
	var audio = document.getElementById("audio" + audioCount);	
	if(audio!=undefined){
		audio.currentTime = startTime;
		//alert("playAudioByTime()");
		audio.play();
	}
	else{
		//alert("play() error");
	}
};

android.selection.pauseAudio = function(){
	var audio = document.getElementById("audio" + audioCount);	
	if(audio!=undefined)
		audio.pause();
};

android.selection.stopAudio = function(){
	var audio = document.getElementById("audio" + audioCount);	
	if(audio!=undefined)
	{
		//audio.load(audio.src);
		audio.pause();
		if(currentMapRowNo != -1){
			element = document.getElementById(audio.map.get(currentMapRowNo)["id"]);
			element.className = oldClassName;			
			currentMapRowNo = -1;
		}
	}
};


var currentMapRowNo = -1;
var oldClassName = '';
onAudioCanPlay = function (audio){
	window.FORM.onAudioCanPlay(android.selection.webviewName, true);
};

onAudioEnded = function(audio){
	window.FORM.onAudioEnded(android.selection.webviewName, true);
};

gotoPage = function(page)
{
	window.FORM.jumpToSpecificPage(android.selection.webviewName, page);
};

onAudioTimeUpdate = function (audio){

	var i,j;
	var stopFlag = false;
	
	if(currentMapRowNo != -1 && audio.currentTime > audio.map.get(currentMapRowNo)["beginTime"] && audio.currentTime <= audio.map.get(currentMapRowNo)["endTime"])
	{
		//alert('remains');
		for(j=audio.map.get(currentMapRowNo)["pagelist"].length-1;j>0;j--){
			if (audio.currentTime <= audio.map.get(currentMapRowNo)["pagelist"][j][0] && audio.currentTime > audio.map.get(currentMapRowNo)["pagelist"][j-1][0] ){
				console.log("page transfers to " + audio.map.get(currentMapRowNo)["pagelist"][j][1]);
				if(android.selection.currentPage != audio.map.get(currentMapRowNo)["pagelist"][j][1])
				{
					gotoPage(audio.map.get(currentMapRowNo)["pagelist"][j][1]);
					
				}
				stopFlag = true;
				break;
			}
		}
		
		if(!stopFlag){
			console.log("page transfers to " + audio.map.get(currentMapRowNo)["pagelist"][0][1]);
			if(android.selection.currentPage != audio.map.get(currentMapRowNo)["pagelist"][0][1])
			{
				gotoPage(audio.map.get(currentMapRowNo)["pagelist"][0][1]);
			}
		}
		
		return;
	}
	
	stopFlag = false;
	for(i=0;i<audio.map.size();i++)
	{
		if (audio.currentTime > audio.map.get(i)["beginTime"] && audio.currentTime <= audio.map.get(i)["endTime"] ){
			var element;
			if(currentMapRowNo != -1)
			{
				element = document.getElementById(audio.map.get(currentMapRowNo)["id"]);
				element.className = oldClassName;
				
				window.FORM.modifyCssInOtherWebviews(android.selection.webviewName, audio.map.get(currentMapRowNo)["id"], oldClassName, true);
			}
			currentMapRowNo = i;
			element = document.getElementById(audio.map.get(i)["id"]);
			oldClassName = element.className;
			element.className = audio.cssClassName;	
			
			//alart("className : " + element.className );
			
			window.FORM.modifyCssInOtherWebviews(android.selection.webviewName, audio.map.get(i)["id"], audio.cssClassName, true);			
			console.log("time bwt " + audio.map.get(i)["beginTime"] + " and " + audio.map.get(i)["endTime"]);
			stopFlag = true;
			var pageFlag = false;
			for(j=audio.map.get(i)["pagelist"].length-1;j>0;j--){
				if (audio.currentTime < audio.map.get(i)["pagelist"][j][0] && audio.currentTime >= audio.map.get(i)["pagelist"][j-1][0] ){
					console.log("page transfers to " + audio.map.get(i)["pagelist"][j][1]);
					if(android.selection.currentPage != audio.map.get(currentMapRowNo)["pagelist"][j][1])
					{
						gotoPage(audio.map.get(currentMapRowNo)["pagelist"][j][1]);
					}					
					pageFlag = true;
					break;
				}
			}
			if(!pageFlag){
				console.log("page transfers to " + audio.map.get(i)["pagelist"][0][1]);
				if(android.selection.currentPage != audio.map.get(currentMapRowNo)["pagelist"][0][1])
				{
					gotoPage(audio.map.get(currentMapRowNo)["pagelist"][0][1]);
				}				
				break;
			}
		}
		
		if(stopFlag)
			break;
	}

	
	if(stopFlag == false && currentMapRowNo != -1){
		element = document.getElementById(audio.map.get(currentMapRowNo)["id"]);
		element.className = oldClassName;	
		
		//window.FORM.modifyCssInOtherWebviews(android.selection.webviewName, audio.map.get(currentMapRowNo)["id"], oldClassName, true);

		window.FORM.notifyDurationOutOfRange(android.selection.webviewName, true);
		currentMapRowNo = -1;
	}

};


android.selection.addDurationAndIdToAudioMap = function(beginTime, endTime, id){
	//alert('addDurationAndIdToAudioMap');
	var pageWidth = android.selection.pageWidth;
	var pageHeight = android.selection.pageHeight;
	var audio = document.getElementById("audio" + audioCount);
	var obj = new Object();
	obj.beginTime = beginTime;
	obj.endTime = endTime;
	obj.id = id;
	audio.map.put(audio.map.size(), obj);
	var rects = document.getElementById(id).getClientRects();
	var i, pageAry = [], areaAry = [];
	//alert("rects.length = " + rects.length);
	
	for(i=0;i<rects.length;i++)
	{
		var rect = rects[i];		
		var left = Math.round(rect.left),
			top = Math.round(rect.top),
			right = Math.round(rect.right),
			bottom = Math.round(rect.bottom);
		//alert("left=" + left + ",top=" + top + ",right=" + right + ",bottom=" + bottom);
		if(android.selection.leftToRight == false && android.selection.verticalWritingMode == true){
			//alert('here');
			if(pageAry.indexOf(Math.floor(top/pageHeight)) == -1)
			{
				//alert('not found');
				pageAry.push( Math.floor(top/pageHeight) );
				areaAry.push( (right-left)*(bottom-top) );
				//alert("push : " + (Math.floor(left/pageWidth)) + " page : area = " + (right-left)*(bottom-top));
			}
			else
			{
				//alert('found');
				areaAry[pageAry.indexOf( Math.floor( top/pageHeight ) )] += (right-left)*(bottom-top);
				//alert("add : " + (right-left)*(bottom-top));
			}		
		}
		else if(android.selection.leftToRight == true && android.selection.verticalWritingMode == false){
			//alert('here');
			if(pageAry.indexOf(Math.floor(left/pageWidth)) == -1)
			{
				//alert('not found');
				pageAry.push( Math.floor(left/pageWidth) );
				areaAry.push( (right-left)*(bottom-top) );
				//alert("push : " + (Math.floor(left/pageWidth)) + " page : area = " + (right-left)*(bottom-top));
			}
			else
			{
				//alert('found');
				areaAry[pageAry.indexOf(Math.floor( left / pageWidth ) )] += (right-left)*(bottom-top);
				//alert("add : " + (right-left)*(bottom-top));
			}
		}		
	}
	
	var sum = 0;
	for(i in pageAry)
	{
		//alert(pageAry[i] + " page has area :" + areaAry[i]);
		sum += areaAry[i];
	}
	
	var ratioAry = [], accumulatedArea = 0;		
	for(i=0; i<pageAry.length; i++)
	{		
		for(var j=0; j<=i; j++){
			accumulatedArea += areaAry[j];
		}
		ratioAry.push(accumulatedArea / sum);
		//alert(accumulatedArea / sum);
		areaAry[i] = ratioAry[i]*(audio.map.get(audio.map.size()-1)["endTime"]-audio.map.get(audio.map.size()-1)["beginTime"]) + audio.map.get(audio.map.size()-1)["beginTime"];
		//alert(areaAry[i]);
	}
	//alert("audio.map.size()="+audio.map.size());
	audio.map.get(audio.map.size()-1)["pagelist"] = [];
	for(i=0; i<pageAry.length; i++)
	{
		//alert('areaAry[i]= ' + areaAry[i]);
		//alert('currentPage=' + currentpage );
		audio.map.get(audio.map.size()-1)["pagelist"].push([areaAry[i] , (pageAry[i]+android.selection.currentPage)]);
	}	
	//alert('["pagelist"].length = ' + audio.map.get(audio.map.size()-1)["pagelist"].length);
	//for(i in audio.map.get(audio.map.size()-1)["pagelist"])
	//{
		//alert('i in pagelist');
		//alert(audio.map.get(audio.map.size()-1)["pagelist"][i][0] + " in " + audio.map.get(audio.map.size()-1)["pagelist"][i][1]);		
	//}
	if(audio.map.size()==totalAudioDurationCount)
	{
		audioDurationCountFlag = true;
		//alert("audioCountFlag = true");
		window.FORM.notifyToAutoPlay(android.selection.webviewName,true);
	}
	
};

android.selection.aClicked = function(href)
{
 //alert(href);
 window.FORM.aClicked(android.selection.webviewName, href);
};


android.selection.modifyAtags = function()
{
	//alert("android.selection.modifyAtags");
	var aAry = document.getElementsByTagName("a");
	var i =0;
	for(i=0;i<aAry.length;i++)
	{
		var _href = aAry[i].getAttribute('xlink:href') == null ? 'href' : 'xlink:href';	
		//alert(aAry[i].getAttribute(_href));
		
		if(aAry[i].getAttribute('type')=="footnote" || aAry[i].getAttribute('epub:type')=="footnote" || aAry[i].getAttribute('rel')=="footnote")
		{
			//alert("atag");
			return;
		}
		
		if(aAry[i].getAttribute(_href).slice(-1)=="#")
		{
			//alert("continue");
			continue;			
		}
		
		aAry[i].setAttribute(_href, "javascript:android.selection.aClicked('" + aAry[i].getAttribute(_href) + "')");
	}
};
/*
android.selection.modifyAtags = function()
{
 //alert("android.selection.modifyAtags");
 var aAry = document.getElementsByTagName("a");
 var i =0;
 for(i=0;i<aAry.length;i++)
 {
  //alert(android.selection.aClicked(aAry[i].getAttribute("href")));
  if(aAry[i].getAttribute("href").slice(-1)=="#")
   continue;
  aAry[i].setAttribute("href", "javascript:android.selection.aClicked('" + aAry[i].getAttribute('href') + "')");
 }
};
*/













