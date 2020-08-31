var highlighter;
var highlight_id = ""
var dw_X, dw_Y, up_X, up_Y;
var mousedownInHighlight = false;
var mouseupInHighlight = false;

window.onload=function()
{		
	//alert('window.onload');	
	rangy.init();	
	highlighter = rangy.createHighlighter();//document,"TextRange");
	
	highlighter.addClassApplier(rangy.createClassApplier("highlight_yellow", {
		ignoreWhiteSpace: true,
		elementTagName: "a",
		//elementTagName: "span",
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
					event.preventDefault();
					return false;
				}
				else if(mousedownInHighlight && highlight_id == highlight.id){
					//alert('highlight clicked');
					window.FORM.clickHighlight(android.selection.webviewName, event.clientX, event.clientY,highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
					if (event.stopPropagation) 
						event.stopPropagation();
					event.preventDefault();
					return false;
				}
				else{
					//alert('error: mouseup');
					if (event.stopPropagation) 
						event.stopPropagation();
					event.preventDefault();
					return false;
				}
			},
			onclick: function() {
				//var highlight = highlighter.getHighlightForElement(this);
				//window.FORM.clickedHighlight(android.selection.webviewName, event.clientX, event.clientY, highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
				event.preventDefault();
				return false;
			}			
		}
	}));
	
	highlighter.addClassApplier(rangy.createClassApplier("highlight_pink", {
		ignoreWhiteSpace: true,
		elementTagName: "a",
		//elementTagName: "span",
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
					event.preventDefault();
					return false;
				}
				else if(mousedownInHighlight && highlight_id == highlight.id){
					//alert('highlight clicked');
					window.FORM.clickHighlight(android.selection.webviewName, event.clientX, event.clientY,highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
					if (event.stopPropagation) 
						event.stopPropagation();
					event.preventDefault();
					return false;
				}
				else{
					//alert('error: mouseup');
					if (event.stopPropagation) 
						event.stopPropagation();
					event.preventDefault();
					return false;
				}
			},
			onclick: function() {
				//var highlight = highlighter.getHighlightForElement(this);
				//window.FORM.clickedHighlight(android.selection.webviewName, event.clientX, event.clientY, highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
				event.preventDefault();
				return false;
			}
		}		
		
	}));			
	
	highlighter.addClassApplier(rangy.createClassApplier("highlight_green", {
		ignoreWhiteSpace: true,
		elementTagName: "a",
		//elementTagName: "span",
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
					event.preventDefault();
					return false;
				}
				else if(mousedownInHighlight && highlight_id == highlight.id){
					//alert('highlight clicked');
					window.FORM.clickHighlight(android.selection.webviewName, event.clientX, event.clientY,highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
					if (event.stopPropagation) 
						event.stopPropagation();
					event.preventDefault();
					return false;
				}
				else{
					//alert('error: mouseup');
					if (event.stopPropagation) 
						event.stopPropagation();
					event.preventDefault();
					return false;
				}
			},
			onclick: function() {
				//var highlight = highlighter.getHighlightForElement(this);
				//window.FORM.clickedHighlight(android.selection.webviewName, event.clientX, event.clientY, highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
				event.preventDefault();					
				return false;
			}			
		}
	}));

	highlighter.addClassApplier(rangy.createClassApplier("highlight_blue", {
		ignoreWhiteSpace: true,
		elementTagName: "a",
		//elementTagName: "span",
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
					event.preventDefault();
					return false;
				}
				else if(mousedownInHighlight && highlight_id == highlight.id){
					//alert('highlight clicked');
					window.FORM.clickHighlight(android.selection.webviewName, event.clientX, event.clientY,highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
					if (event.stopPropagation) 
						event.stopPropagation();
					event.preventDefault();
					return false;
				}
				else{
					//alert('error: mouseup');
					if (event.stopPropagation) 
						event.stopPropagation();
					event.preventDefault();
					return false;
				}
			},
			onclick: function() {
				//var highlight = highlighter.getHighlightForElement(this);
				//window.FORM.clickedHighlight(android.selection.webviewName, event.clientX, event.clientY, highlight.characterRange.start, highlight.characterRange.end, highlight.classApplier.className);
				event.preventDefault();
				return false;
			}
		}		
	}));
	
	highlighter.addClassApplier(rangy.createClassApplier("highlight_dummy", {
		ignoreWhiteSpace: true,
		elementTagName: "a",	
		//elementTagName: "span",		
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
	
    $(document).keyup(function(e){ 
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
	}); 
	
	if (typeof window.innerWidth != 'undefined')
	{
		viewportwidth = window.innerWidth;
		viewportheight = window.innerHeight;
	}			
	//alert("viewportwidth=" + viewportwidth);
	//alert("viewportheight=" + viewportheight);

	android.selection.pageWidth = viewportwidth; 
	android.selection.pageHeight = viewportheight;
};





