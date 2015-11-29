var expValid = /^ ?\d+(?:\.\d+)*(?: ?[\+\-\*\/] ?\d+(?:\.\d+)*)+ ?= ?$/;
var exp = /(\d+(?:\.\d+)*)|([\+\-\*\/])/g;

function isValid(str) {
    "use strict";
    
    return str.search(expValid);
}

function msgOut(str) {
    "use strict";
    
    document.getElementById("result").innerHTML = str;
}

function parse(str) {
    "use strict";
    
    return str.match(exp);
}

function operation(a, b, operator) {
    "use strict";
    
    switch (operator) {
            case "+":
                return (a - 0) + (b - 0);
                
            case "-":
                return a - b;
                
            case "*":
                return a * b;
                
            case "/":
                return a / b;
                
            default :
                return;
    }
}

function calc(arr) {
    "use strict";
    
    var result = arr[0],
        i = 0,
        len = 0;
    
    for (i = 1, len = arr.length; i <= len - 1; i += 2) {
        result = operation(result, arr[i + 1], arr[i]);
    }
    
    return result;
}

function onClick() {
    "use strict";
    
    var str = document.getElementById("exp").value,
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



document.getElementById("calcBtn").onclick = onClick;