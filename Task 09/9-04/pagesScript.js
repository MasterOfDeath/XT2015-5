(function () {
    "use strict";
    
    var timerLabel,
        backBtn,
        pauseBtn,
        refreshBtn,
        closeBtn,
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
    
    refreshBtn = document.body.getElementsByClassName("refreshBtn").item(0);
    refreshBtn.onclick = refreshClick;
    
    closeBtn = document.body.getElementsByClassName("closeBtn").item(0);
    if (closeBtn !== null) {
        closeBtn.onclick = closeClick;
    }
    

    timerLabel.innerHTML = curTime;
    timer = setInterval(timerFunc, 1000);
    

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
            btnText.data = "Resume";
            pause = true;
        } else {
            btnText.data = "Suspend";
            pause = false;
        }
    }
    
    function refreshClick() {
        location.reload();
    }
    
    function closeClick() {
        window.close();
    }

    function gotoPage(shift) {
        var pageNumber = +pageId + shift,
            url;

        if (pageNumber <= maxPages) {
            url = "page" + pageNumber + ".html";
            if (pageNumber === 0) {
                //url = "index.html";
                window.close();
            }
            
            window.location = url;
        } else {
            clearInterval(timer);
            showDialog();
        }
    }
    
    function showDialog() {
        $("#myModal").modal();
    }
    
})();

