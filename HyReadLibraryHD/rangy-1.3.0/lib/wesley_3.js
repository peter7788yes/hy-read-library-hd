var _dotNetWindowWidth,
    _dotNetWindowHeight,
	_dotNetWindowColumnGap;
var _dotNetWindowPaddingTop,
	_dotNetWindowPaddingBottom,
	_dotNetWindowPaddingLeft,
	_dotNetWindowPaddingRight;
	
var viewportwidth;
var viewportheight;

var documentwidth;
var documentheight;

var totalpages;
//var currentpage;

var justifieddocumentwidth;
var justifieddocumentheight;

var highlighter;
var highlight_id = ""
var dw_X, dw_Y, up_X, up_Y;
var mousedownInHighlight = false;
var mouseupInHighlight = false;

function slideUp() {
	//alert("slideUp()");
	var currentTopPos = $(window).scrollTop();
	//alert(currentTopPos);
	topPos=currentTopPos+(viewportheight);
	//alert(topPos);
	if(android.selection.currentPage+1 < totalpages)
	{
		$(window).scrollTop(topPos);
		android.selection.currentPage++;
	}
	else if(android.selection.currentPage+1 == totalpages)
	{
		$(window).scrollTop(topPos);
		android.selection.currentPage++;
	}	
	window.FORM.setCurTopAndCurPage(topPos,android.selection.currentPage);	
}

function slideDown() {
	//alert("slideDown()");
	var currentTopPos = $(window).scrollTop();
	topPos=currentTopPos-(viewportheight);
	if(android.selection.currentPage-1 > 1)
	{				
		$(window).scrollTop(topPos);
		android.selection.currentPage--;
	}
	else if(android.selection.currentPage-1 == 1)
	{
		$(window).scrollTop(topPos);
		android.selection.currentPage--;
	}	
	window.FORM.setCurTopAndCurPage(topPos,android.selection.currentPage);	
}

function slideLeft() {
	//alert('Left');
	//$(window).unbind('scroll');
	//alert("scrollLeft()=" + leftPos);
	var currentLeftPos = $(window).scrollLeft();
	//alert("currentLeftPos=" + currentLeftPos);
	leftPos=currentLeftPos-viewportwidth;//+100;
	//alert("leftPos=" + leftPos);
	//while(currentLeftPos != leftPos){
		//alert("leftPos=" + leftPos);
	if(android.selection.currentPage-1 > 1)
	{
		//alert("slide once");
		$(window).scrollLeft(leftPos);
		android.selection.currentPage--;
	}
	else if(android.selection.currentPage-1 == 1)
	{
		//alert("slide once");
		$(window).scrollLeft(leftPos);
		android.selection.currentPage--;
	}
		//currentLeftPos = $(window).scrollLeft();
	//}
	//$(window).bind('scroll');
	window.FORM.setCurLeftAndCurPage(leftPos,android.selection.currentPage);
	
}		

function slideRight() {
	//alert('Right');
	//$(window).unbind('scroll');
	//alert("scrollLeft()=" + leftPos);
	var currentLeftPos = $(window).scrollLeft();
	//alert("currentLeftPos=" + currentLeftPos);
	var leftPos=currentLeftPos+(viewportwidth);//-100;
	//alert("leftPos=" + leftPos);
	//alert("currentpage=" + currentpage);
	//alert("totalpages=" + totalpages);
	//while(currentLeftPos != leftPos){
		//alert("leftPos=" + leftPos);
	if(android.selection.currentPage+1 < totalpages)
	{
		//alert("slide once");
		$(window).scrollLeft(leftPos);
		android.selection.currentPage++;
	}
	else if(android.selection.currentPage+1 == totalpages)
	{
		//alert("slide once");
		$(window).scrollLeft(leftPos);
		android.selection.currentPage++;
	}
	//currentLeftPos = $(window).scrollLeft();
	//}
	//$(window).bind('scroll');
	window.FORM.setCurLeftAndCurPage(leftPos,android.selection.currentPage);
	
}
		
function initializeCSSRules()
{
	// the more standards compliant browsers (mozilla/netscape/opera/IE7) use window.innerWidth and window.innerHeight
	if(android.selection.verticalWritingMode != true){
				
		//alert('horizontal-tb');
		addHtmlRule('horizontal-tb',_dotNetWindowWidth,_dotNetWindowColumnGap,_dotNetWindowHeight,_dotNetWindowPaddingTop,_dotNetWindowPaddingLeft,_dotNetWindowPaddingBottom,_dotNetWindowPaddingRight);
		//alert('font-size:' + android.selection.fontSize+'px;' );
		addCSSRule('body', 'font-size: ' + android.selection.fontSize + 'px;');
		//alert(parseInt(android.selection.fontSize * android.selection.lineHeight, 10));
		addCSSRule('body', 'line-height: ' + parseInt(android.selection.fontSize * android.selection.lineHeight, 10) + 'px;');	
				
		addCSSRule('.highlight_dummy','background-color: #FFFFFF !important;');
		addCSSRule('.highlight_dummy','text-decoration: none !important;');				

		addCSSRule('.highlight_yellow','background-color: #FFF200 !important;');
		addCSSRule('.highlight_yellow','text-decoration: none !important;');		
		
		addCSSRule('.highlight_pink','background-color: #F1BFCE !important;');
		addCSSRule('.highlight_pink','text-decoration: none !important;');
			
		addCSSRule('.highlight_green','background-color: #B5DDB7 !important;');
		addCSSRule('.highlight_green','text-decoration: none !important;');		
			
		addCSSRule('.highlight_blue','background-color: #C2DFF1 !important;');
		addCSSRule('.highlight_blue','text-decoration: none !important;');		
		
		if (typeof window.innerWidth != 'undefined')
		{
			viewportwidth = window.innerWidth;
			viewportheight = window.innerHeight;

		}			
		//alert("viewportwidth=" + viewportwidth);
		//alert("viewportheight=" + viewportheight);
		
		if (typeof $(document).width != 'undefined')
		{
			documentwidth = $(document).width();
			documentheight = $(document).height();
		}					
		//alert("documentwidth=" + documentwidth);
		//alert("documentheight=" + documentheight);	
		
		android.selection.pageWidth = viewportwidth; 
		android.selection.pageHeight = viewportheight;
		
		totalpages = Math.ceil(documentwidth / viewportwidth);
		if(android.selection.multiColumnCount==2)
		{
			if(totalpages % 2==1)
				totalpages++;
			//alert("totalpages="+totalpages);
		}
		
		//alert(totalpages);
		window.FORM.setTotalPages(totalpages);
		
		totalpages+=2;
		justifieddocumentwidth = viewportwidth * totalpages;
		//alert("justifieddocumentwidth=" + justifieddocumentwidth);
		android.selection.contentsWidth = justifieddocumentwidth;
		window.FORM.setContentsWidthAndHeight(justifieddocumentwidth,viewportheight);
		android.selection.contentsHeight = viewportheight;
		
		//makeABackCanvasWithColumnnumber(justifieddocumentwidth, viewportheight, true, 1)
	
		makeABackCanvasWithStartPosition(justifieddocumentwidth-documentwidth, viewportheight, true, documentwidth, 0);
		
		
		if(viewportwidth!=_dotNetWindowWidth+_dotNetWindowPaddingLeft+_dotNetWindowPaddingRight)
		{
			//alert("modifyPanelWidth");
			//alert(_dotNetWindowWidth+_dotNetWindowPaddingLeft+_dotNetWindowPaddingRight);
			//alert(android.selection.webviewName);
			//window.FORM.modifyPanelWidth(android.selection.webviewName, viewportwidth);
			viewportwidth = _dotNetWindowWidth+_dotNetWindowPaddingLeft+_dotNetWindowPaddingRight;
		
		}
		
		if(android.selection.pageProgressionDirection == true) //ltr
		{
			//alert(android.selection.pageProgressionDirection);
			$(window).scrollLeft(0);
			//$(window).scrollTop(0);
			//currentpage = 1;
			android.selection.currentPage = 1;
			//if(android.selection.webviewName == 'RIGHT_WEBVIEW')
			//{
				//alert('slide');
				//slideRight();
				
				//totalpages-=2;
			//}
			//else
			//{
			window.FORM.setCurLeftAndCurPage(0,1);
			//}
			
			totalpages-=2;
			if(android.selection.jumpToLastPageAfterLoaded)
			{
				if(android.selection.multiColumnCount==1)
				{
					for(var i=1; i<totalpages; i++)
						slideRight();
				}	
				else if(android.selection.multiColumnCount==2)
				{
					if(android.selection.webviewName == 'LEFT_WEBVIEW')
					{
						var j = totalpages;
						
						for(var i=1; i<j-1; i++){
							slideRight();
						}
					}
					else if(android.selection.webviewName == 'RIGHT_WEBVIEW')
					{
						var j = totalpages;
						for(var i=1; i<j; i++){
							slideRight();
						}
					}
				}
			}
			else//(!android.selection.jumpToLastPageAfterLoaded)
			{	
				if(android.selection.multiColumnCount==2)
				{
					if(android.selection.webviewName == 'RIGHT_WEBVIEW')
					{
						slideRight();
					}
				}		
			}		
		}
		else //rtl
		{
			//alert(android.selection.pageProgressionDirection);
			$(window).scrollLeft(0);
			window.FORM.setCurLeftAndCurPage(0,1);
			
			//slideRight();			
			android.selection.currentPage = 1;
			
			totalpages-=2;
			if(android.selection.jumpToLastPageAfterLoaded)
			{
				if(android.selection.multiColumnCount==1)
				{
					for(var i=1; i<totalpages; i++)
						slideRight();
				}	
				else if(android.selection.multiColumnCount==2)
				{
					if(android.selection.webviewName == 'LEFT_WEBVIEW')
					{
						var j = totalpages;
						
						for(var i=1; i<j; i++){
							slideRight();
						}
					}
					else if(android.selection.webviewName == 'RIGHT_WEBVIEW')
					{
						var j = totalpages;
						for(var i=1; i<j-1; i++){
							slideRight();
						}
					}
				}
			}
			else//(!android.selection.jumpToLastPageAfterLoaded)
			{	
				if(android.selection.multiColumnCount==2)
				{
					if(android.selection.webviewName == 'LEFT_WEBVIEW')
					{
						//alert('line 302');
						slideRight();
					}
				}		
			}			
		}		
		
		
/*		
		$(window).scrollLeft(0);
		//$(window).scrollTop(0);
		//currentpage = 1;
		android.selection.currentPage = 1;
		//if(android.selection.webviewName == 'RIGHT_WEBVIEW')
		//{
			//alert('slide');
			//slideRight();
			
			//totalpages-=2;
		//}
		//else
		//{
		window.FORM.setCurLeftAndCurPage(0,1);
		//}
		
		totalpages-=2;
		if(android.selection.jumpToLastPageAfterLoaded)
		{
			if(android.selection.multiColumnCount==1)
			{
				for(var i=1; i<totalpages; i++)
					slideRight();
			}	
			else if(android.selection.multiColumnCount==2)
			{
				if(android.selection.webviewName == 'LEFT_WEBVIEW')
				{
					var j = totalpages;
					
					for(var i=1; i<j-1; i++){
						slideRight();
					}
				}
				else if(android.selection.webviewName == 'RIGHT_WEBVIEW')
				{
					var j = totalpages;
					for(var i=1; i<j; i++){
						slideRight();
					}
				}
			}
		}
		else//(!android.selection.jumpToLastPageAfterLoaded)
		{	
			if(android.selection.multiColumnCount==2)
			{
				if(android.selection.webviewName == 'RIGHT_WEBVIEW')
				{
					slideRight();
				}
			}		
		}
*/		
	}
	else{
		//alert('vertical-rl');
		addHtmlRule('vertical-rl',_dotNetWindowWidth,_dotNetWindowColumnGap,_dotNetWindowHeight,_dotNetWindowPaddingTop,_dotNetWindowPaddingLeft,_dotNetWindowPaddingBottom,_dotNetWindowPaddingRight);
	
		//alert('font-size:' + android.selection.fontSize+'px;' );
		addCSSRule('body', 'font-size: ' + android.selection.fontSize + 'px;');
		//alert(parseInt(android.selection.fontSize * android.selection.lineHeight, 10));
		addCSSRule('body', 'line-height: ' + parseInt(android.selection.fontSize * android.selection.lineHeight, 10) + 'px;');
		
		addCSSRule('.highlight_dummy','background-color: #FFFFFF !important;');
		addCSSRule('.highlight_dummy','text-decoration: none !important;');				

		addCSSRule('.highlight_yellow','background-color: #FFF200 !important;');
		addCSSRule('.highlight_yellow','text-decoration: none !important;');		
		
		addCSSRule('.highlight_pink','background-color: #F1BFCE !important;');
		addCSSRule('.highlight_pink','text-decoration: none !important;');
			
		addCSSRule('.highlight_green','background-color: #B5DDB7 !important;');
		addCSSRule('.highlight_green','text-decoration: none !important;');		
			
		addCSSRule('.highlight_blue','background-color: #C2DFF1 !important;');
		addCSSRule('.highlight_blue','text-decoration: none !important;');			
				
		if (typeof window.innerWidth != 'undefined')
		{
			viewportwidth = window.innerWidth;
			viewportheight = window.innerHeight;
		}			
		//alert("viewportwidth=" + viewportwidth);
		//alert("viewportheight=" + viewportheight);
		
		if (typeof $(document).width != 'undefined')
		{
			documentwidth = $(document).width();
			documentheight = $(document).height();
		}					
		//alert("documentwidth=" + documentwidth);
		//alert("documentheight=" + documentheight);	
		
		android.selection.pageWidth = viewportwidth; 
		android.selection.pageHeight = viewportheight;
	
		totalpages = Math.ceil(documentheight / viewportheight);
		
		if(android.selection.multiColumnCount==2)
		{
			if(totalpages % 2==1)
				totalpages++;
			//alert("totalpages="+totalpages);
		}
		//alert(totalpages);
		window.FORM.setTotalPages(totalpages);
		
		totalpages+=2;
		justifieddocumentheight = viewportheight * totalpages;
		//alert("justifieddocumentheight=" + justifieddocumentheight);
		android.selection.contentsWidth = viewportwidth;
		window.FORM.setContentsWidthAndHeight(viewportwidth,justifieddocumentheight);
		android.selection.contentsHeight = justifieddocumentheight;	
		
		//makeABackCanvasWithColumnnumber(viewportwidth, justifieddocumentheight, false, 1);

		//makeABackCanvasWithStartPosition(justifieddocumentheight-documentheight, viewportwidth, false, 0, documentheight);
		makeABackCanvasWithStartPosition(viewportwidth, justifieddocumentheight-documentheight, false, 0, documentheight); 
		
		if(viewportheight != _dotNetWindowHeight + _dotNetWindowPaddingTop +_dotNetWindowPaddingBottom)
		{
			//alert("modifyPanelHeight");
			//alert(_dotNetWindowWidth+_dotNetWindowPaddingLeft+_dotNetWindowPaddingRight);
			//alert(android.selection.webviewName);
			//window.FORM.modifyPanelWidth(android.selection.webviewName, viewportwidth);
			viewportheight = _dotNetWindowHeight + _dotNetWindowPaddingTop +_dotNetWindowPaddingBottom;		
		}
	
		$(window).scrollTop(0);	
		//$(window).scrollLeft(0);
		//currentpage = 1;
		android.selection.currentPage = 1;
		//if(android.selection.webviewName == 'LEFT_WEBVIEW')
		//{
			//alert('slide');
			//slideUp();
			//window.FORM.setCurLeftAndCurPage(0,1);			
		//}
		//else
		//{
		window.FORM.setCurTopAndCurPage(0,1);
		//}
		
		totalpages-=2;
		if(android.selection.jumpToLastPageAfterLoaded)
		{
			if(android.selection.multiColumnCount==1)
			{
				for(var i=1; i<totalpages; i++)
					slideUp();
			}	
			else if(android.selection.multiColumnCount==2)
			{
				if(android.selection.webviewName == 'LEFT_WEBVIEW')
				{
					var j = totalpages;			
					for(var i=1; i<j; i++){
						slideUp();
					}
				}
				else if(android.selection.webviewName == 'RIGHT_WEBVIEW')
				{
					var j = totalpages;
					for(var i=1; i<j-1; i++){
						slideUp();
					}
				}
			}
		}
		else
		{
			if(android.selection.multiColumnCount==2 && android.selection.webviewName == 'LEFT_WEBVIEW')
			{
				slideUp();
			}		
		}
		
	}
	//currentpage = 1;
}

function onMouseMove (event) {
	console.log("OnMouseMove");
	if(window.getSelection().rangeCount>0)
	{				
		if(event.clientX < _dotNetWindowPaddingLeft || event.clientX > android.selection.pageWidth - _dotNetWindowPaddingRight || event.clientY > android.selection.pageHeight - _dotNetWindowPaddingBottom || event.clientY < _dotNetWindowPaddingTop){
			var selection = window.getSelection();
			selection.removeAllRanges();
			console.log("OnMouseMove Cleared");
			return;
		}
		
//Continuous Checkup to Clear Unreasonable User Selection		
/*
		var handleBounds = android.selection.getHandleBounds2(window.getSelection().getRangeAt(0));	

		if((handleBounds.left!=0 || handleBounds.top!=0 || handleBounds.right!=0 || handleBounds.bottom!=0) && (handleBounds.left<_dotNetWindowPaddingLeft || handleBounds.right>android.selection.pageWidth -_dotNetWindowPaddingRight || handleBounds.top < _dotNetWindowPaddingTop || handleBounds.bottom > android.selection.pageHeight - _dotNetWindowPaddingBottom))
		{

			alert('HANDLEBOUNDS ERROR:' + handleBounds.left + ',' + handleBounds.top + ',' + handleBounds.right + ',' + handleBounds.bottom);
			var selection = window.getSelection();
			selection.removeAllRanges();
			console.log("OnMouseMove Re-Cleared");
			return;			

		}		
*/		
	} 
}

window.onload=function()
{		
	//alert('window.onload');	
	rangy.init();
	//alert(document.styleSheets.length);
	if(document.styleSheets.length!=0)
	{
		initializeCSSRules();
	}
	
	//window.FORM.notifyColumnized(true);
	highlighter = rangy.createHighlighter();//document,"TextRange");
	
	highlighter.addClassApplier(rangy.createClassApplier("highlight_yellow", {
		ignoreWhiteSpace: true,
		//elementTagName: "a",
		elementTagName: "span",
		elementProperties: {
			href: "#",		
			onmousedown: function(event) {
				var highlight = highlighter.getHighlightForElement(this);
				highlight_id = highlight.id;
				mousedownInHighlight = true;
				dw_X = event.clientX;
				dw_Y = event.clientY;	
				if (event.stopPropagation) 
					event.stopPropagation();			
			},
			onmouseup: function(event){
				var highlight = highlighter.getHighlightForElement(this);
				mouseupInHighlight = true;
				up_X = event.clientX;
				up_Y = event.clientY;
				if(android.selection.hasSelection()){
					//alert('hasSelection');
					android.selection.longTouch(event); 
					if (event.stopPropagation) 
						event.stopPropagation();
				}
				else if(mousedownInHighlight && highlight_id == highlight.id){
					//alert('highlight clicked');
					window.FORM.clickHighlight(android.selection.webviewName, event.clientX, event.clientY,highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
					if (event.stopPropagation) 
						event.stopPropagation();
				}
				else{
					alert('error: mouseup');
					if (event.stopPropagation) 
						event.stopPropagation();
				}
			},
			onclick: function() {
				var highlight = highlighter.getHighlightForElement(this);
				window.FORM.clickedHighlight(android.selection.webviewName, event.clientX, event.clientY, highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
				return false;
			}			
		}
	}));
	
	highlighter.addClassApplier(rangy.createClassApplier("highlight_pink", {
		ignoreWhiteSpace: true,
		//elementTagName: "a",
		elementTagName: "span",
		elementProperties: {
			href: "#",
			onmousedown: function(event) {
				var highlight = highlighter.getHighlightForElement(this);
				highlight_id = highlight.id;
				mousedownInHighlight = true;
				dw_X = event.clientX;
				dw_Y = event.clientY;	
				if (event.stopPropagation) 
					event.stopPropagation();			
			},
			onmouseup: function(event){
				var highlight = highlighter.getHighlightForElement(this);
				mouseupInHighlight = true;
				up_X = event.clientX;
				up_Y = event.clientY;
				if(android.selection.hasSelection()){
					//alert('hasSelection');
					android.selection.longTouch(event); 
					if (event.stopPropagation) 
						event.stopPropagation();
				}
				else if(mousedownInHighlight && highlight_id == highlight.id){
					//alert('highlight clicked');
					window.FORM.clickHighlight(android.selection.webviewName, event.clientX, event.clientY,highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
					if (event.stopPropagation) 
						event.stopPropagation();
				}
				else{
					alert('error: mouseup');
					if (event.stopPropagation) 
						event.stopPropagation();
				}
			},
			onclick: function() {
				var highlight = highlighter.getHighlightForElement(this);
				window.FORM.clickedHighlight(android.selection.webviewName, event.clientX, event.clientY, highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
				return false;
			}
		}		
		
	}));			
	
	highlighter.addClassApplier(rangy.createClassApplier("highlight_green", {
		ignoreWhiteSpace: true,
		elementTagName: "span",
		//elementTagName: "a", "span"
		//tagNames: ["*"],
		//tagNames: ["span", "a"],
		elementProperties: {
			href: "#",		
			onmousedown: function(event) {
				var highlight = highlighter.getHighlightForElement(this);
				highlight_id = highlight.id;
				mousedownInHighlight = true;
				dw_X = event.clientX;
				dw_Y = event.clientY;	
				if (event.stopPropagation) 
					event.stopPropagation();			
			},
			onmouseup: function(event){
				var highlight = highlighter.getHighlightForElement(this);
				mouseupInHighlight = true;
				up_X = event.clientX;
				up_Y = event.clientY;
				if(android.selection.hasSelection()){
					//alert('hasSelection');
					android.selection.longTouch(event); 
					if (event.stopPropagation) 
						event.stopPropagation();
				}
				else if(mousedownInHighlight && highlight_id == highlight.id){
					//alert('highlight clicked');
					window.FORM.clickHighlight(android.selection.webviewName, event.clientX, event.clientY,highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
					if (event.stopPropagation) 
						event.stopPropagation();
				}
				else{
					alert('error: mouseup');
					if (event.stopPropagation) 
						event.stopPropagation();
				}
			},
			onclick: function() {
				var highlight = highlighter.getHighlightForElement(this);
				window.FORM.clickedHighlight(android.selection.webviewName, event.clientX, event.clientY, highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
				return false;
			}			
		}
	}));

	highlighter.addClassApplier(rangy.createClassApplier("highlight_blue", {
		ignoreWhiteSpace: true,
		//elementTagName: "a",
		elementTagName: "span",
		//tagNames: ["*"],
		//tagNames: ["span", "a"],
		elementProperties: {
			href: "#",
			onmousedown: function(event) {
				var highlight = highlighter.getHighlightForElement(this);
				highlight_id = highlight.id;
				mousedownInHighlight = true;
				dw_X = event.clientX;
				dw_Y = event.clientY;	
				if (event.stopPropagation) 
					event.stopPropagation();			
			},
			onmouseup: function(event){
				var highlight = highlighter.getHighlightForElement(this);
				mouseupInHighlight = true;
				up_X = event.clientX;
				up_Y = event.clientY;
				if(android.selection.hasSelection()){
					//alert('hasSelection');
					android.selection.longTouch(event); 
					if (event.stopPropagation) 
						event.stopPropagation();
				}
				else if(mousedownInHighlight && highlight_id == highlight.id){
					//alert('highlight clicked');
					window.FORM.clickHighlight(android.selection.webviewName, event.clientX, event.clientY,highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
					if (event.stopPropagation) 
						event.stopPropagation();
				}
				else{
					alert('error: mouseup');
					if (event.stopPropagation) 
						event.stopPropagation();
				}
			},
			onclick: function() {
				var highlight = highlighter.getHighlightForElement(this);
				window.FORM.clickedHighlight(android.selection.webviewName, event.clientX, event.clientY, highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
				return false;
			}
		}		
	}));
	
	highlighter.addClassApplier(rangy.createClassApplier("highlight_dummy", {
		ignoreWhiteSpace: true,
		//elementTagName: "a",	
		elementTagName: "span",		
		elementProperties: {
			href: "#",
			// onmousedown: function(event) {
				// var highlight = highlighter.getHighlightForElement(this);
				// highlight_id = highlight.id;
				// mousedownInHighlight = true;
				// dw_X = event.clientX;
				// dw_Y = event.clientY;	
				// if (event.stopPropagation) 
					// event.stopPropagation();			
			// },
			// onmouseup: function(event){
				// var highlight = highlighter.getHighlightForElement(this);
				// mouseupInHighlight = true;
				// up_X = event.clientX;
				// up_Y = event.clientY;
				// if(android.selection.hasSelection()){
					// //alert('hasSelection');
					// android.selection.longTouch(event); 
					// if (event.stopPropagation) 
						// event.stopPropagation();
				// }
				// else if(mousedownInHighlight && highlight_id == highlight.id){
					// //alert('highlight clicked');
					// window.FORM.clickHighlight(android.selection.webviewName, event.clientX, event.clientY,highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
					// if (event.stopPropagation) 
						// event.stopPropagation();
				// }
				// else{
					// alert('error: mouseup');
					// // alert(mousedownInHighlight);
					// // alert(highlight_id);
					// // alert(highlight.id);
					// if (event.stopPropagation) 
						// event.stopPropagation();
				// }
			// },		
			// onclick: function(event){
				// var highlight = highlighter.getHighlightForElement(this);
				// window.FORM.clickedHighlight(android.selection.webviewName, event.clientX, event.clientY, highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
				// return false;
			// }
		}
		
	}));	
	

		//$(document).mouseup(function(e){ if(e.which==1) { android.selection.longTouch(e); } });
	
/*     $(document).keyup(function(e){ 
		window.FORM.keyup(e.keyCode); 
	});             
	
	$(document).mousedown(function(e){ 
		//window.FORM.keyup(e.keyCode); 
		//alert('in $ mousedown');
		mousedownInHighlight = false;
		
	}); 

	$(document).mouseup(function(event){ 
		//window.FORM.keyup(e.keyCode); 
		mouseupInHighlight = false;
		if(event.which==1) 
		{ 
			android.selection.longTouch(event); 
		} 	
	});  */
	
	document.onmousemove = onMouseMove;
	document.addEventListener("dragstart", function(event) {
	  event.preventDefault();
	 });
	 
	 /*
	if(android.selection.verticalWritingMode == true  && android.selection.webviewName == 'LEFT_WEBVIEW')
	{
	  getImgPos();
	}
	*/
};

String.prototype.endsWith = function(suffix)
{
    return (this.substr(this.length - suffix.length) === suffix);
}

function getImgPos() { 
 var imgs = $("img");
 var posStr = "";
 for(i=0;i<imgs.size();i++) 
 { 
  var img=imgs[i]; 
  var slices = img.id.split("/");
  if (slices[slices.length-1].endsWith(".mp4")) 
  { 
   posStr += (img.id + " " + img.offsetLeft + " " + img.offsetTop + " " + img.offsetWidth + " " + img.offsetHeight + " "); 
  } 
  posStr += "$";
 }
 //return posStr;
 window.FORM.getImgPos(posStr);
};

function mediaClicked(srcUrl)
{
 //alert(srcUrl);
 window.FORM.mediaClicked(srcUrl);
};



