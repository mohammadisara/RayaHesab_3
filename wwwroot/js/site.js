$("body").on("keyup", "input.TNPrice", function (event) {

    // skip for arrow keys
    if (event.which >= 37 && event.which <= 40) return;

    // format number
    $(this).val(function (index, value) {
        return value
            .replace(/\D/g, "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",")
            ;
    });
});

$("#showsearch").click(function () {
    $(".tablesorter-filter-row").toggle(500);
}); 

