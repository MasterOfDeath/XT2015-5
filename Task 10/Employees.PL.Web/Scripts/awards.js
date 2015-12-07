(function () {
    var $content,
        $table;

    $content = $(".content");
    $table = $("table", $content);
    $("tbody > tr", $table).click(trClick);

    function trClick(event) {
        var awardId = $(event.target).parents("tr").data("award-id");
        window.location = "NewAward?awardid=" + awardId;
    }
})();