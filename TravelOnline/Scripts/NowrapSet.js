var format = '{0}\r\n{1}';
document.body.onmouseover = function()
{
var srcElmt = event.srcElement;
if ( !srcElmt.tagName || !srcElmt.innerText || srcElmt.tagName == 'BODY' || srcElmt.tagName == 'DIV')
{
	return;
}
if ( srcElmt.offsetWidth < srcElmt.scrollWidth )
{
	if ( !srcElmt.__title )
	{
		if ( srcElmt.title == srcElmt.innerText )
		{
			return;
		}
		if ( srcElmt.title )
		{
			srcElmt.__title = srcElmt.title;
		}
	}
	if ( srcElmt.__title )
	{
		srcElmt.title = StringHelper.Format(format, srcElmt.__title, srcElmt.innerText);
	}
	else
	{
		srcElmt.title = srcElmt.innerText;
	}
}
else
{
	if ( srcElmt.__title )
	{
		srcElmt.title = srcElmt.__title;
		srcElmt.__title = null;
	}
	else
	{
		if ( srcElmt.title == srcElmt.innerText )
		{
			srcElmt.title = '';
		}
	}
}
}
<!-- Hide 
		function killErrors() { 
		return true; 
		} 
		window.onerror = killErrors; 
		// --> 