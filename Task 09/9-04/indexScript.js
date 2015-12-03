(function () {
    "use strict";
	
	var startBtn = document.body.getElementsByClassName("startBtn").item(0);
    startBtn.onclick = startClick;
	
    function startClick() {	
		if (isIE11) {
			window.open(firtPage, "_self");
		} else {
			window.open(firtPage);
		}
    }
})();