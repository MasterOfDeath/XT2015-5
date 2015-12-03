(function () {
    "use strict";
    
    var timerLabel,
        backBtn,
        pauseBtn,
        refreshBtn,
        closeBtn,
		finishPrompt,
        pageId,
        maxPages = 3,
        time = 5,
        curTime = time,
        pause = false,
        timer;
    
    pageId = document.body.getElementsByClassName("testPage").item(0).dataset.id;
    
    timerLabel = document.body.getElementsByClassName("timer").item(0);
    
    backBtn = document.body.getElementsByClassName("backBtn").item(0);
    backBtn.onclick = backClick;
    
    pauseBtn = document.body.getElementsByClassName("pauseBtn").item(0);
    pauseBtn.onclick = pauseClick;
	
	finishPrompt = document.body.getElementsByClassName("finishPrompt").item(0);
	if (finishPrompt !== null) {
		closeBtn = finishPrompt.getElementsByClassName("closeBtn").item(0);
		closeBtn.onclick = closeClick;
	}
	
    timerLabel.innerHTML = curTime;
    timer = setInterval(timerFunc, 1000);   
	
	if (!isIE11) {
		switchAnimation(timerLabel, true);
	}

    function timerFunc() {
        if (curTime === 0) {
            gotoPage(1);
            return;
        }

        if (!pause) {
            timerLabel.innerHTML = curTime = curTime - 1;
        }
    }

    function backClick() {
        gotoPage(-1);
    }
    
    function pauseClick() {
        var btnText = pauseBtn.firstChild;

        if (btnText.data === "Suspend") {
            btnText.data = "Refresh";
            pause = true;
			switchAnimation(timerLabel, false);
        } else {
			location.reload();
        }
    }
    
    function closeClick() {
        closeWindow();
    }

    function gotoPage(shift) {
        var pageNumber = +pageId + shift,
            url;
		
		if (pageNumber === 0) {
			if (isIE11) {
				window.location = indexPage;
			} else {
				closeWindow();
			}
			
			return;
		}
		
		if (pageNumber > maxPages) {
			switchAnimation(timerLabel, false);
			clearInterval(timer);
			showDialog();
			
			return;
		}
		
		window.location = getPageUrl(pageNumber);
    }
    
    function showDialog() {
        $(".finishPrompt").modal({backdrop: "static"});
    }
	
	function closeWindow() {
		window.open(closePage, "_self");
	}
	
	function switchAnimation(element, enabled) {
		var className = element.className;
		
		if (enabled) {
			if (className.search(/\banimated\b/) === -1) {
				element.className = className + " animated";
			}
		} else {
			element.className = className.replace(/ animated( |$)/, "");
		}
	}
    
})();

