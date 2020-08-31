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
	topPos=currentTopPos+(viewportheight);
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
	//alert("leftPos=" + leftPos);
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

$(document).ready(function()
{		
	//alert('ready');
	
	rangy.init();
	
	// the more standards compliant browsers (mozilla/netscape/opera/IE7) use window.innerWidth and window.innerHeight
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
	
	//viewportwidth = 1.25; //DPI-X
	//viewportheight *= 1.25; //DPI-Y
	if(android.selection.leftToRight == true){
		totalpages = Math.ceil(documentwidth / viewportwidth);
		if(android.selection.multiColumnCount==2)
		{
			if(totalpages % 2==1)
				totalpages++;
			//alert("totalpages="+totalpages);
		}
		window.FORM.setTotalPages(totalpages);
		justifieddocumentwidth = viewportwidth * totalpages;
		//alert("justifieddocumentwidth=" + justifieddocumentwidth);
		android.selection.contentsWidth = justifieddocumentwidth;
		window.FORM.setContentsWidthAndHeight(justifieddocumentwidth,viewportheight);
		android.selection.contentsHeight = viewportheight;
		
		makeABackCanvasWithColumnnumber(justifieddocumentwidth, viewportheight, true, 1)
	
		$(window).scrollLeft(0);

		window.FORM.setCurLeftAndCurPage(0,1);

	}
	else{
		totalpages = Math.ceil(documentheight / viewportheight);
		if(android.selection.multiColumnCount==2)
		{
			if(totalpages % 2==1)
				totalpages++;
			//alert("totalpages="+totalpages);
		}
		window.FORM.setTotalPages(totalpages);
		justifieddocumentheight = viewportheight * totalpages;
		//alert("justifieddocumentheight=" + justifieddocumentheight);
		android.selection.contentsWidth = viewportwidth;
		window.FORM.setContentsWidthAndHeight(viewportwidth,justifieddocumentheight);
		android.selection.contentsHeight = justifieddocumentheight;	
		
		makeABackCanvasWithColumnnumber(viewportwidth, justifieddocumentheight, false, 1)		
		
		$(window).scrollTop(0);	
		window.FORM.setCurTopAndCurPage(0,1);
	}
	currentpage = 1;
	

});




