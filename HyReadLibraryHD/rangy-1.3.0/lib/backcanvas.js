var CANVAS_WIDTH_LIMIT = 1000;  //Not 32767
var CANVAS_HEIGHT_LIMIT = 1000;  //Not 32767

function makeABackCanvasWithStartPosition(width, height, ltr, left, top)
{
//alert("makeABackCanvasWithStartPosition("+width+","+height+","+ltr+","+left+","+top+")");
    var rem_Width = width;
	var rem_Height = height;
	var loop;
	var id = "";//"backCanvas" + loop.toString();
	//alert(ltr);
	//true: horizontal one column only
	if(ltr==true)
	{
		//alert("true");
		loop = 0;
		while(rem_Width >= CANVAS_WIDTH_LIMIT)
		{
			//alert(rem_Width);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = CANVAS_WIDTH_LIMIT;
			canvas.height = height ;
			canvas.style.zIndex = -1;//-1;
			//canvas.style.background = "#333333";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			canvas.style.left = (left + CANVAS_WIDTH_LIMIT * loop)+"px";
			canvas.style.top = "0px";	
			//document.body.insertBefore(canvas, document.body.firstChild);
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			loop ++;
			rem_Width -= CANVAS_WIDTH_LIMIT;	

		}	
		if(rem_Width > 0){
			//alert(rem_Width);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = CANVAS_WIDTH_LIMIT;
			canvas.height = height ;
			canvas.style.zIndex = -1;//-1;
			//canvas.style.background = "#333333";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			canvas.style.left = (left + CANVAS_WIDTH_LIMIT * loop)+"px";
			
			canvas.style.top = "0px";	
			
			//document.body.insertBefore(canvas, document.body.firstChild);		
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			//loop ++;
			rem_Width = 0;	
		}
	}
	else
	{
//RIGHT_TO_LEFT	
//Chrome 37
		//alert("false");
		loop = 0;
		while(rem_Height >= CANVAS_WIDTH_LIMIT)
		{
			//alert(rem_Height);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = width; 
			canvas.height = CANVAS_HEIGHT_LIMIT;
			canvas.style.zIndex = -1;
			//canvas.style.background = "#ffffff";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			canvas.style.left = "0px";
			canvas.style.top = (top + CANVAS_HEIGHT_LIMIT * loop)+"px";
			//document.body.insertBefore(canvas, document.body.firstChild);
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			loop ++;
			rem_Height -= CANVAS_HEIGHT_LIMIT;	

		}	
		if(rem_Height > 0)
		{
			//alert(rem_Width);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = width;
			canvas.height = CANVAS_HEIGHT_LIMIT;
			canvas.style.zIndex = -1;
			//canvas.style.background = "#ffffff";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			canvas.style.left = "0px";
			
			canvas.style.top = (top + CANVAS_HEIGHT_LIMIT * loop)+"px";
			
			//document.body.insertBefore(canvas, document.body.firstChild);		
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			//loop ++;
			rem_Height = 0;	
		}
	}	
}

function makeABackCanvasWithColumnnumber(width, height, ltr, columnnumber)
{
	//alert("makeABackCanvasWithColumnnumber("+width+","+height+")");
    var rem_Width = width;
	var rem_Height = height;
	var loop;
	var id = "";//"backCanvas" + loop.toString();
	//alert(ltr);
	//true: horizontal one column only
	if(ltr==true)
	{
		//alert("true");
		loop = 0;
		while(rem_Width >= CANVAS_WIDTH_LIMIT)
		{
			//alert(rem_Width);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = CANVAS_WIDTH_LIMIT;
			canvas.height = height ;
			canvas.style.zIndex = -1;//-1;
			//canvas.style.background = "#333333";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			canvas.style.left = (CANVAS_WIDTH_LIMIT * loop)+"px";
			canvas.style.top = "0px";	
			//document.body.insertBefore(canvas, document.body.firstChild);
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			loop ++;
			rem_Width -= CANVAS_WIDTH_LIMIT;	

		}	
		if(rem_Width > 0){
			//alert(rem_Width);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = CANVAS_WIDTH_LIMIT;
			canvas.height = height ;
			canvas.style.zIndex = -1;//-1;
			//canvas.style.background = "#333333";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			canvas.style.left = (CANVAS_WIDTH_LIMIT * loop)+"px";
			
			canvas.style.top = "0px";	
			
			//document.body.insertBefore(canvas, document.body.firstChild);		
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			//loop ++;
			rem_Width = 0;	
		}
	}
	else
	{
//RIGHT_TO_LEFT	
//Chrome 37
		//alert("false");
		loop = 0;
		while(rem_Height >= CANVAS_WIDTH_LIMIT)
		{
			//alert(rem_Height);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = width; 
			canvas.height = CANVAS_HEIGHT_LIMIT;
			canvas.style.zIndex = -1;
			//canvas.style.background = "#ffffff";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			canvas.style.left = "0px";
			canvas.style.top = (CANVAS_HEIGHT_LIMIT * loop)+"px";
			//document.body.insertBefore(canvas, document.body.firstChild);
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			loop ++;
			rem_Height -= CANVAS_HEIGHT_LIMIT;	

		}	
		if(rem_Height > 0)
		{
			//alert(rem_Width);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = width;
			canvas.height = CANVAS_HEIGHT_LIMIT;
			canvas.style.zIndex = -1;
			//canvas.style.background = "#ffffff";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			canvas.style.left = "0px";
			
			canvas.style.top = (CANVAS_HEIGHT_LIMIT * loop)+"px";
			
			//document.body.insertBefore(canvas, document.body.firstChild);		
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			//loop ++;
			rem_Height = 0;	
		}
	}	
}


function makeABackCanvas(width, height, ltr)
{
	//alert("makeABackCanvas");
    var rem_Width = width;
	var rem_Height = height;
	var loop;
	var id = "";//"backCanvas" + loop.toString();
	//alert(ltr);
	if(ltr==true)
	{
		loop = 0;
		while(rem_Width >= CANVAS_WIDTH_LIMIT)
		{
			//alert(rem_Width);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = CANVAS_WIDTH_LIMIT;
			canvas.height = height ;
			canvas.style.zIndex = -1;
			canvas.style.background = "#ff0000";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			canvas.style.left = (CANVAS_WIDTH_LIMIT * loop)+"px";
			canvas.style.top = "0px";	
			//document.body.insertBefore(canvas, document.body.firstChild);
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			loop ++;
			rem_Width -= CANVAS_WIDTH_LIMIT;	

		}	
		if(rem_Width > 0){
			//alert(rem_Width);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = CANVAS_WIDTH_LIMIT;
			canvas.height = height ;
			canvas.style.zIndex = -1;
			canvas.style.background = "#ff0000";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			canvas.style.left = (CANVAS_WIDTH_LIMIT * loop)+"px";
			
			canvas.style.top = "0px";	
			
			//document.body.insertBefore(canvas, document.body.firstChild);		
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			//loop ++;
			rem_Width = 0;	
		}
	}
	else
	{
//RIGHT_TO_LEFT	
		//alert(rem_Width);
		
		loop = 0;
		var currentLeftPosition = viewportwidth/2 - CANVAS_WIDTH_LIMIT;
		//while(rem_Width >= android.selection.pageWidth)
		while(rem_Width >= CANVAS_WIDTH_LIMIT)
		{
			//alert("rem_Width=" + rem_Width);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			canvas.width = CANVAS_WIDTH_LIMIT;
			
			canvas.height = height ;
			canvas.style.zIndex = -1;
			canvas.style.background = "#7fffff";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			//canvas.style.left = (android.selection.pageWidth-CANVAS_WIDTH_LIMIT * loop)+"px";
			//canvas.style.left = (0-CANVAS_WIDTH_LIMIT * (loop))+"px";
			canvas.style.left = currentLeftPosition;
			//alert("canvas.style.left=" + canvas.style.left);
			
			canvas.style.top = "0px";	
			//alert("canvas.style.top=" + canvas.style.top);
			//document.body.insertBefore(canvas, document.body.firstChild);
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			//loop ++;
			loop --;
			rem_Width -= CANVAS_WIDTH_LIMIT;
			currentLeftPosition -= CANVAS_WIDTH_LIMIT;
		
		}	
		//alert("rem_Width=" + rem_Width);
		if(rem_Width > 0){
			//alert("rem_Width=" + rem_Width);
			id = "backCanvas" + loop.toString();
			//alert(id);
			var canvas = document.createElement("canvas");
			canvas.setAttribute("id",id);
			//canvas.width = CANVAS_WIDTH_LIMIT;
			canvas.width = rem_Width;
			canvas.height = height ;
			canvas.style.zIndex = -1;
			canvas.style.background = "#7fffff";
			canvas.style.position = "absolute";
			canvas.style.margin = "0px";
			canvas.style.padding = "0px";
			canvas.style.display = "block";
			currentLeftPosition += CANVAS_WIDTH_LIMIT;
			currentLeftPosition -= rem_Width;
			canvas.style.left = currentLeftPosition;
			//canvas.style.left = (0-CANVAS_WIDTH_LIMIT * (loop))+"px";
			//canvas.style.left = (android.selection.pageWidth-CANVAS_WIDTH_LIMIT * loop)+"px";
			//alert("canvas.style.left=" + canvas.style.left);
			canvas.style.top = "0px";	
			//alert("canvas.style.top=" + canvas.style.top);
			//document.body.insertBefore(canvas, document.body.firstChild);		
			document.body.appendChild(canvas);
			//alert(canvas.style.left + "_" + canvas.style.top);
			loop ++;
			rem_Width = 0;	
		}	
	}
}

function clearBackCanvasByRegion(left, top, width, height)
{
    var rem_Width = width;
	var rem_Height = height;
	var id = "";
	var startIndex = Math.floor(left / CANVAS_WIDTH_LIMIT);
	var endIndex = Math.floor((left+width) / CANVAS_WIDTH_LIMIT);
	var startLeft = startIndex * CANVAS_WIDTH_LIMIT;
	var endLeft = endIndex * CANVAS_WIDTH_LIMIT;
	var startTop = top;
	for (var i=startIndex; i<=endIndex; i=i+1)
	{
		id = "backCanvas" + i.toString();
		var canvas = document.getElementById(id);
		var ctx = canvas.getContext('2d');
		if(i==startIndex){
			var cWidth;
			if(startIndex==endIndex){
				cWidth= width;
			}
			else{			
				cWidth= CANVAS_WIDTH_LIMIT - (left-startLeft);	
			}
			//ctx.fillStyle = 'white';
			//ctx.fillRect(left-startLeft, top, cWidth, rem_Height);
			ctx.clearRect(left-startLeft, top, cWidth, rem_Height);
		}
		else if(i==endIndex){
			var cWidth = left+width-endLeft;		
			//ctx.fillStyle = 'white';
			//ctx.fillRect(0, top, cWidth, rem_Height);	
			ctx.clearRect(0, top, cWidth, rem_Height);	
		}
		else{
			//ctx.fillStyle = 'white';
			//ctx.fillRect(0, top, CANVAS_WIDTH_LIMIT, rem_Height);
			ctx.clearRect(0, top, CANVAS_WIDTH_LIMIT, rem_Height);
		}			
	}
}

function draw(r, g, b, a, left, top, right, bottom)
{	
	//alert("draw");
	if(android.selection.verticalWritingMode == false)
	{
		//alert("android.selection.verticalWritingMode == false");
		var width = right - left;
		var height = bottom - top;
		//alert('draw='+left+","+top+","+width+","+height);
		var rem_Width = width;
		var rem_Height = height;
		var id = "";
		var startIndex = Math.floor(left / CANVAS_WIDTH_LIMIT);
		var endIndex = Math.floor((left+width) / CANVAS_WIDTH_LIMIT);
		var startLeft = startIndex * CANVAS_WIDTH_LIMIT;
		var endLeft = endIndex * CANVAS_WIDTH_LIMIT;
		var startTop = top;
		for (var i=startIndex; i<=endIndex; i=i+1)
		{			
			id = "backCanvas" + i.toString();
			//alert("id=" + id);
			var canvas = document.getElementById(id);
			var ctx = canvas.getContext('2d');
			if(i==startIndex){
				var cWidth;
				if(startIndex==endIndex){
					cWidth= width;
				}
				else{			
					cWidth= CANVAS_WIDTH_LIMIT - (left-startLeft);	
				}
				ctx.beginPath();			
				ctx.fillStyle = 'rgba(' + r +',' + g + ',' + b + ',' + a + ')';
				ctx.fillRect(left-startLeft, top, cWidth, rem_Height);	
				ctx.closePath();
				ctx.fill();				
			}
			else if(i==endIndex){
				var cWidth = left+width-endLeft;	
				ctx.beginPath();			
				ctx.fillStyle = 'rgba(' + r +',' + g + ',' + b + ',' + a + ')';
				ctx.fillRect(0, top, cWidth, rem_Height);	
				ctx.closePath();
				ctx.fill();			
			}
			else{
				ctx.beginPath();
				ctx.fillStyle = 'rgba(' + r +',' + g + ',' + b + ',' + a + ')';
				ctx.fillRect(0, top, CANVAS_WIDTH_LIMIT, rem_Height);
				ctx.closePath();
				ctx.fill();			
			}			
		}
	}
	else
	{
//Chrome 37	
		//alert("android.selection.verticalWritingMode ="+android.selection.verticalWritingMode);
		//alert("android.selection.leftToRight == "+android.selection.leftToRight);

		var width = right - left;
		var height = bottom - top;
		//alert('draw='+left+","+top+","+width+","+height);
		var rem_Width = width;
		var rem_Height = height;
		var id = "";
		var startIndex = Math.floor(top / CANVAS_WIDTH_LIMIT);
		var endIndex = Math.floor((top+height) / CANVAS_WIDTH_LIMIT);
		var startTop = startIndex * CANVAS_WIDTH_LIMIT;
		var endTop = endIndex * CANVAS_WIDTH_LIMIT;
		var startLeft = left;
		for (var i=startIndex; i<=endIndex; i=i+1)
		{			
			id = "backCanvas" + i.toString();
			//alert("id=" + id);
			var canvas = document.getElementById(id);
			var ctx = canvas.getContext('2d');
			if(i==startIndex)
			{
				var cHeight;
				if(startIndex==endIndex)
				{
					//alert("startIndex==endIndex=" + startIndex);
					cHeight= height;
				}
				else{			
					cHeight= CANVAS_WIDTH_LIMIT - (top-startTop);	
				}
				ctx.beginPath();			
				ctx.fillStyle = 'rgba(' + r +',' + g + ',' + b + ',' + a + ')';
				//alert("left="+left);
				//alert("top-startTop="+(top-startTop));
				//alert("rem_Width="+rem_Width);
				//alert("cHeight="+cHeight);
				ctx.fillRect(left, top-startTop, rem_Width, cHeight);	
				ctx.closePath();
				ctx.fill();				
			}
			else if(i==endIndex){
				var cHeight = top+height-endTop;	
				ctx.beginPath();			
				ctx.fillStyle = 'rgba(' + r +',' + g + ',' + b + ',' + a + ')';
				ctx.fillRect(left, 0, rem_Width, cHeight);	
				ctx.closePath();
				ctx.fill();			
			}
			else{
				ctx.beginPath();
				ctx.fillStyle = 'rgba(' + r +',' + g + ',' + b + ',' + a + ')';
				ctx.fillRect(left, 0, rem_Width, CANVAS_WIDTH_LIMIT);
				ctx.closePath();
				ctx.fill();			
			}			
		}
	}
}

function clearBackCanvas()
{
	//alert('clearBackCanvas()');
	var canvases = document.getElementsByTagName('canvas');
	//alert('# of Canvas = ' + canvases.length);	
	for(var i=0; i<canvases.length; i++)
	{
		var ctx = canvases[i].getContext('2d');
		//alert('canvas.width=' + canvases[i].width + ' canvas.height=' + canvases[i].height);
		ctx.clearRect(0, 0, canvases[i].width, canvases[i].height);
	}	
}

function drawLeftAnnotationMark(left, top, fullImageFilePath, defaultIconWidth, defaultIconHeight)
{
	//alert(fullImageFilePath);
	var leftImage = new Image();
	leftImage.onload = function()
	{
		//alert("drawLeftAnnotationMark");
		if(android.selection.verticalWritingMode == false)
		{
			//alert("android.selection.verticalWritingMode == false");
			//alert('drawLeftAnnotationMark='+left+","+top);

			var startIndex = Math.floor(left / CANVAS_WIDTH_LIMIT);
			var startLeft = startIndex * CANVAS_WIDTH_LIMIT;		
			var startTop = top;		
			var id = "backCanvas" + startIndex.toString();
			//alert("id=" + id);
			var canvas = document.getElementById(id);
			var ctx = canvas.getContext('2d');		
		
			//var imageWidth = leftImage.width;
			//var imageHeight = leftImage.Height;
			ctx.drawImage(leftImage, left - startLeft - defaultIconWidth/2, top - defaultIconHeight/2, defaultIconWidth, defaultIconHeight);
				
		}
		else
		{
	//Chrome 37up: Vertical Writing Mode	
			//alert("android.selection.verticalWritingMode ="+android.selection.verticalWritingMode);
			//alert("android.selection.leftToRight == "+android.selection.leftToRight);
//left here is "RIGHT" coordinate
			
			var startIndex = Math.floor(top / CANVAS_HEIGHT_LIMIT);
			var startTop = startIndex * CANVAS_HEIGHT_LIMIT;
			var id = "backCanvas" + startIndex.toString();

			//alert("id=" + id);
			var canvas = document.getElementById(id);
			var ctx = canvas.getContext('2d');
			
			//ctx.fillRect(left, top-startTop, rem_Width, cHeight);
			
			ctx.drawImage(leftImage, left, top - startTop - defaultIconHeight/2, defaultIconWidth, defaultIconHeight);

		}
	};		
	var pattern = /\\/gi;
	//alert(fullImageFilePath);
	var modifiedImageFilePath = fullImageFilePath.replace(pattern, "/");
	//alert(modifiedImageFilePath);
	
	leftImage.src = "file:///" + modifiedImageFilePath;	
}