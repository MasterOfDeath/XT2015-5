(function() {
    var startBtn = document.body.getElementsByClassName("startBtn").item(0);
    startBtn.onclick = startClick;
    
    function startClick() {
        window.open("page1.html");
    }
})();

//var myWindow;
//
//function myFunction() {
////    "use strict";
//      myWindow = window.open("index.html");
////      alert(myWindow);
////    myWindow.document.write("<p>I replaced the current window.</p><button onclick=\"myFuncClose()\">Close</button>");
////    myWindow.close();
////    document.write("cd");
////    var someVarName = document.getElementById("test");
////    var someVarName = "test";
////    someVarName.disabled = true;
////    localStorage.setItem("someVarName", myWindow.toJson());
////    localStorage.setItem('myObject', JSON.stringify(someVarName));
////    document.write(JSON.stringify(someVarName));
//    
//    myWindow.windowToClose = myWindow;
////    window.windowToClose = myWindow;
//}
//
//function myFuncClose() {
////    "use strict";
////    myWindow.close();
////    var wind = localStorage.getItem("someVarName").fromJson();
////    var wind = JSON.parse(localStorage.getItem('myObject'));
////    document.write(window.ghh);
////    wind.disabled = true;
//    //window.windowToClose.close();
//    window.close();
//}