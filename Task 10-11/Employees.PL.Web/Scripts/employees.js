(function () {
    var $content,
        $table;

    $content = $(".content");
    $table = $("table", $content);
    $("tbody > tr", $table).click(trClick);

    function trClick(event) {
        var userId = $(event.target).parents("tr").data("user-id");
        window.location = "NewEmployee?userid=" + userId;
    }
})();