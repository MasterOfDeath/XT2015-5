(function () {
    "use strict";
	
	$(".startBtn").click(startClick);
	
    function startClick() {
		if (isIE11) {
			window.open(firtPage, "_self");
		} else {
			window.open(firtPage);
		}
    }
})();