(function () {
    var separators = [" ", "\n", ".", ",", "!", ":", ";", "?"],
        string = "У попа была собака",
        originalOutput,
        changedOutput,
        arr,
        forDel;

    Array.prototype.unique = function() {
        var a = this.concat(),
			i = 0,
			j = 0,
			len = 0;
		
        for(i = 0, len = a.length; i < len; i += 1) {
            for(j = i + 1; j < len; j += 1) {
                if(a[i] === a[j]) {
                    a.splice(j--, 1);
					len = a.length;
				}
            }
        }

        return a;
    };
    
    originalOutput = document.body.getElementsByClassName("originalOutput").item(0);
    changedOutput = document.body.getElementsByClassName("changedOutput").item(0);

    originalOutput.innerHTML = string;

    arr = mySplit(string);
    forDel = getRepeatsAll(arr);
    string = removeChars(forDel, string);
    
    changedOutput.innerHTML = string;

    function mySplit(str) {
        var result = [],
            resultStr = "",
            indexStart = 0,
            indexEnd = 0,
			i,
			len;

        for (i = 0, len = str.length; i <= len; i++) {
            if (separators.indexOf(str[i]) != -1 || i == len) {
                indexEnd = i;
                resultStr = str.substring(indexStart, indexEnd);

                if (resultStr != "") {
                    result.push(resultStr);
                }

                indexStart = indexEnd + 1;
            }
        }

        return result;
    }

    function getRepeatsOne(str) {
        var result = [];

        for (var i = 0, len = str.length; i < len; i++) {
            if(getCountRepeats(str, str[i]) > 1) {
                if (result.indexOf(str[i]) == -1) {
                    result.push(str[i]);
                }
            }
        }

        return result;
    }

    function getRepeatsAll(arr) {
        var result = [];

        for (var i = 0, len = arr.length; i < len; i++) {
            var tmp = getRepeatsOne(arr[i]);
            if (tmp != null) {
                result = result.concat(tmp);
            }
        }

        return result.unique();
    }

    function getCountRepeats(arr, val) {
        var indexes = [], i = -1, result = 0;
        while ((i = arr.indexOf(val, i+1)) != -1){
            result++;
        }
        return result;
    }

    function removeChars(arr, str) {
        var result = [].slice.call(str), 
            j = -1;

        for (var i = 0, len = arr.length; i < len; i++) {
            while ((j = result.indexOf(arr[i], j+1)) != -1){
                result.splice(j, 1);
            }
        }

        return result.join("");
    }
})();