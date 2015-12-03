(function () {
    "use strict";
    
    var expValid = /^ ?\d+(?:\.\d+)?(?: ?[\+\-\*\/] ?\d+(?:\.\d+)?)* ?= ?$/,
        exp = /(\d+(?:\.\d+)?)|([\+\-\*\/])/g,
        outputSpan,
        inputExp,
        calcBtn;

    outputSpan = document.body.getElementsByClassName("outputSpan").item(0);
    inputExp = document.body.getElementsByClassName("inputExp").item(0);
    inputExp.value = "3.5 +4*10-5.3 /5 =";
    calcBtn = document.body.getElementsByClassName("calcBtn").item(0);
    calcBtn.onclick = onClick;

    function isValid(str) {
        return str.search(expValid);
    }

    function msgOut(str) {
        outputSpan.innerHTML = str;
        animateSpan();
    }

    function parse(str) {
        return str.match(exp);
    }

    function operation(a, b, operator) {
        switch (operator) {
        case "+":
            return (+a) + (+b);

        case "-":
            return a - b;

        case "*":
            return a * b;

        case "/":
            return a / b;

        default:
            return;
        }
    }

    function calc(arr) {
        var result = +arr[0],
            i = 0,
            len = arr.length;
        
        if (len === 1) {
            return result;
        }

        for (i = 1; i <= len - 1; i += 2) {
            result = operation(result, arr[i + 1], arr[i]);
        }

        return result;
    }

    function onClick() {
        var str = inputExp.value,
            arr,
            result;

        if (isValid(str) === -1) {
            msgOut("Invaid input");
            return;
        }

        arr = parse(str);
        result = calc(arr).toFixed(2);
        msgOut("Result: " + result);
    }

    function animateSpan() {
        var newone = outputSpan.cloneNode(true);
        outputSpan.parentNode.replaceChild(newone, outputSpan);
        outputSpan = newone;
    }
})();