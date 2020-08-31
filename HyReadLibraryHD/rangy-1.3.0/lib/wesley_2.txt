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
var currentpage;

var justifieddocumentwidth;
var justifieddocumentheight;

function slideUp() {
	//alert("slideUp()");
	var currentTopPos = $(window).scrollTop();
	//alert(currentTopPos);
	topPos=currentTopPos+(viewportheight);
	//alert(topPos);
	if(currentpage+1 < totalpages)
	{
		$(window).scrollTop(topPos);
		currentpage++;
	}
	else if(currentpage+1 == totalpages)
	{
		$(window).scrollTop(topPos);
		currentpage++;
	}	
	window.FORM.setCurTopAndCurPage(topPos,currentpage);	
}

function slideDown() {
	//alert("slideDown()");
	var currentTopPos = $(window).scrollTop();
	topPos=currentTopPos-(viewportheight);
	if(currentpage-1 > 1)
	{				
		$(window).scrollTop(topPos);
		currentpage--;
	}
	else if(currentpage-1 == 1)
	{
		$(window).scrollTop(topPos);
		currentpage--;
	}	
	window.FORM.setCurTopAndCurPage(topPos,currentpage);	
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
	if(currentpage-1 > 1)
	{
		//alert("slide once");
		$(window).scrollLeft(leftPos);
		currentpage--;
	}
	else if(currentpage-1 == 1)
	{
		//alert("slide once");
		$(window).scrollLeft(leftPos);
		currentpage--;
	}
		//currentLeftPos = $(window).scrollLeft();
	//}
	//$(window).bind('scroll');
	window.FORM.setCurLeftAndCurPage(leftPos,currentpage);
	
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
	if(currentpage+1 < totalpages)
	{
		//alert("slide once");
		$(window).scrollLeft(leftPos);
		currentpage++;
	}
	else if(currentpage+1 == totalpages)
	{
		//alert("slide once");
		$(window).scrollLeft(leftPos);
		currentpage++;
	}
	//currentLeftPos = $(window).scrollLeft();
	//}
	//$(window).bind('scroll');
	window.FORM.setCurLeftAndCurPage(leftPos,currentpage);
	
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
		
		makeABackCanvasWithColumnnumber(justifieddocumentwidth, viewportheight, true, 1)
	
		
		
		if(viewportwidth!=_dotNetWindowWidth+_dotNetWindowPaddingLeft+_dotNetWindowPaddingRight)
		{
			//alert("modifyPanelWidth");
			//alert(_dotNetWindowWidth+_dotNetWindowPaddingLeft+_dotNetWindowPaddingRight);
			//alert(android.selection.webviewName);
			//window.FORM.modifyPanelWidth(android.selection.webviewName, viewportwidth);
			viewportwidth = _dotNetWindowWidth+_dotNetWindowPaddingLeft+_dotNetWindowPaddingRight;
		
		}
		$(window).scrollLeft(0);
		//$(window).scrollTop(0);
		currentpage = 1;
		if(android.selection.webviewName == 'RIGHT_WEBVIEW')
		{
			//alert('slide');
			slideRight();
		}
		else
		{
			window.FORM.setCurLeftAndCurPage(0,1);
		}
	
	}
	else{
		//alert('vertical-rl');
		addHtmlRule('vertical-rl',_dotNetWindowWidth,_dotNetWindowColumnGap,_dotNetWindowHeight,_dotNetWindowPaddingTop,_dotNetWindowPaddingLeft,_dotNetWindowPaddingBottom,_dotNetWindowPaddingRight);
	
		//alert('font-size:' + android.selection.fontSize+'px;' );
		addCSSRule('body', 'font-size: ' + android.selection.fontSize + 'px;');
		//alert(parseInt(android.selection.fontSize * android.selection.lineHeight, 10));
		addCSSRule('body', 'line-height: ' + parseInt(android.selection.fontSize * android.selection.lineHeight, 10) + 'px;');
		
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
		
		makeABackCanvasWithColumnnumber(viewportwidth, justifieddocumentheight, false, 1)		
		

		
		if(viewportheight!=_dotNetWindowHeight+_dotNetWindowPaddingTop+_dotNetWindowPaddingBottom)
		{
			//alert("modifyPanelHeight");
			//alert(_dotNetWindowWidth+_dotNetWindowPaddingLeft+_dotNetWindowPaddingRight);
			//alert(android.selection.webviewName);
			//window.FORM.modifyPanelWidth(android.selection.webviewName, viewportwidth);
			viewportheight =_dotNetWindowHeight+_dotNetWindowPaddingTop+_dotNetWindowPaddingBottom;
		
		}
		
		$(window).scrollTop(0);	
		//$(window).scrollLeft(0);
		currentpage = 1;
		if(android.selection.webviewName == 'LEFT_WEBVIEW')
		{
			//alert('slide');
			slideUp();
			//window.FORM.setCurLeftAndCurPage(0,1);
			
		}
		else
		{
			window.FORM.setCurTopAndCurPage(0,1);
		}
		
		
	}
	//currentpage = 1;
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
	
};




