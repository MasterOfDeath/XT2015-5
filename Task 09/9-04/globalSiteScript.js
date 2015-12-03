var isIE11 = !!(navigator.userAgent.search(/Trident.*rv[ :]*11\./) + 1),
	indexPage = "index.html",
	firtPage = "page1.html",
	closePage = "closer.html";

function getPageUrl(pageNumber) {
	return "page" + pageNumber + ".html";
}