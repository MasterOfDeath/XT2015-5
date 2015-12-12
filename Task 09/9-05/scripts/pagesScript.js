(function () {
    "use strict";
    
    var $timerLabel,
        $testPage,
		$finishPrompt,
        pageId,
        maxPages = 3,
        time = 5,
        curTime = time,
        pause = false,
        timer;
    
	$testPage = $(".testPage");
    pageId = $testPage.data("id");
    
    $timerLabel = $(".timer", $testPage);
    $(".backBtn", $testPage).click(backClick);
    $(".pauseBtn", $testPage).click(pauseClick);
	
	$finishPrompt = $(".finishPrompt", $testPage);
	if ($finishPrompt !== null) {
		$(".closeBtn", $finishPrompt).click(closeClick);
	}
	
	
    $timerLabel.text(curTime);
    timer = setInterval(timerFunc, 1000);   
	
	if (!isIE11) {
		switchAnimation($timerLabel, true);
	}

    function timerFunc() {
        if (curTime === 0) {
            gotoPage(1);
            return;
        }

        if (!pause) {
			curTime = curTime - 1;
            $timerLabel.text(curTime);
        }
    }

    function backClick() {
        gotoPage(-1);
    }
    
    function pauseClick(event) {
		var $btn = $(event.target);

        if ($btn.text() === "Suspend") {
            $btn.text("Refresh");
            pause = true;
			switchAnimation($timerLabel, false);
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
			switchAnimation($timerLabel, false);
			clearInterval(timer);
			showDialog();
			
			return;
		}
		
		window.location = getPageUrl(pageNumber);
    }
    
    function showDialog() {
        $(".finishPrompt").modal({backdrop: "static"}, {keyboard: true});
    }
	
	function closeWindow() {
		window.open(closePage, "_self");
	}
	
	function switchAnimation($element, enabled) {
		$element.toggleClass("animated", enabled);
	}
    
})();

