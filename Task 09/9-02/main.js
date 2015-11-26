var expValid = /^ ?\d+(?:\.\d+)*(?: ?[\+\-\*\/] ?\d+(?:\.\d+)*)+ ?= ?$/;
var exp = /(\d+(?:\.\d+)*)|([\+\-\*\/])/g;

function onClick() {
    var str = document.getElementById("exp").value;
    
    if (isValid(str) === -1) {
        msgOut("Invaid input");
        return;
    }
    
    var arr = parse(str);
    var result = calc(arr).toFixed(2);
    msgOut("Result: " + result);
}

function calc(arr) {
    var result = arr[0];
    
    for (var i = 1, len = arr.length; i <= len - 1; i += 2) {
        result = operation(result, arr[i+1], arr[i]);
    }
    
    return result;
}

function operation(a, b, operator) {
    
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

function isValid(str) {
    return str.search(expValid);
}

function parse(str) {
    return str.match(exp);
}

function msgOut(str) {
    document.getElementById("result").innerHTML = str;
}