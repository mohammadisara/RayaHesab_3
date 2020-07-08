
$(document).ready(function () {
    menuoff();
    //$('[data-toggle="tooltip"]').tooltip();
    //$(".Ftxtbox").focus();
    //$("#MySelect").select2({
    //    placeholder: "Select Country",
    //    allowClear: true
    //});


    $("#myInputSearch").on("keyup", function (key) {
        var value = $(this).val().toLowerCase();
        $("#myTable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#myInputSearch1").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable1 tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#myInputSearch2").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable2 tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#myInputSearch3").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable3 tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#myInputSearch4").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable4 tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });


    $("#closealert").click(function () {
        $(".showact").hide();
    });

    $('.btnshow').click(function () {
        if (($('.AMenu').css('right')) == "-220px") {
            $('.AMenu').css({ 'right': '0' });
            $('.btnshow img').css({ '-moz-transform': 'rotate(180deg)', '-ms-transform': 'rotate(180deg)', '-o-transform': 'rotate(180deg)', '-webkit-transform': 'rotate(180deg)', 'transform': 'rotate(180deg)' });
            $('.AContent').css({ 'padding-right': '16.5em' });
            $('.Aheader').css({ 'right': '16em' });
        }
        else {
            $('.AMenu').css({ 'right': '-220px' });
            $('.btnshow img').css({ '-moz-transform': 'rotate(0deg)', '-ms-transform': 'rotate(0deg)', '-o-transform': 'rotate(0deg)', '-webkit-transform': 'rotate(0deg)', 'transform': 'rotate(0deg)' });
            $('.AContent').css({ 'padding-right': '1em' });
            $('.Aheader').css({ 'right': '0em' });
        }
        if ($(window).width() <= '500') {
            $('.AContent').css({ 'padding-right': '1em' });
            $('.Aheader').css({ 'right': '0em' });
        }
    });


    AjaxLoad();
    $('#MyModal').on('shown.bs.modal', function () {
        $("input:text, textarea").eq(0).focus()
        $(".TNPrice").val(function (index, value) {
            return value
                .replace(/\D00/g, "")
                .replace(/\B(?=(\d{3})+(?!\d))/g, ",")
                ;
        });
    })
});
function menuon() {
    //if (($('.AMenu').css('right')) == "-220px") {
        $('.AMenu').css({ 'right': '0' });
        $('.btnshow img').css({ '-moz-transform': 'rotate(180deg)', '-ms-transform': 'rotate(180deg)', '-o-transform': 'rotate(180deg)', '-webkit-transform': 'rotate(180deg)', 'transform': 'rotate(180deg)' });
        $('.AContent').css({ 'padding-right': '16.5em' });
        $('.Aheader').css({ 'right': '16em' });
    //}
    //else {
        //$('.AMenu').css({ 'right': '-220px' });
        //$('.btnshow img').css({ '-moz-transform': 'rotate(0deg)', '-ms-transform': 'rotate(0deg)', '-o-transform': 'rotate(0deg)', '-webkit-transform': 'rotate(0deg)', 'transform': 'rotate(0deg)' });
        //$('.AContent').css({ 'padding-right': '1em' });
        //$('.Aheader').css({ 'right': '0em' });
    //}
    if ($(window).width() <= '500') {
        $('.AContent').css({ 'padding-right': '1em' });
        $('.Aheader').css({ 'right': '0em' });
    }

}
function menuoff() {
    $('.AMenu').css({ 'right': '-220px' });
    $('.btnshow img').css({ '-moz-transform': 'rotate(0deg)', '-ms-transform': 'rotate(0deg)', '-o-transform': 'rotate(0deg)', '-webkit-transform': 'rotate(0deg)', 'transform': 'rotate(0deg)' });
    $('.AContent').css({ 'padding-right': '1em' });
    $('.Aheader').css({ 'right': '0em' });

}

function handle(e, name) {
    if (e.keyCode === 13) {
        e.preventDefault();
        document.getElementById(name).focus();
    }
}

function AjaxLoad() {
    $(".btload").click(function (e) {
        $(".loading").show();
        doLoad();
    });

    $(".lc").click(function () {
        $(".loading").hide();
    });
}

function doLoad() {

    var myParam = 'somevalue';

    $.ajax({
        url: "",
        type: "POST",
        dataType: "json",
        data: "{param1:" + myParam + "}",

        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            $(".loading").hide();
        }, error: function (msg) {
            $(".loading").hide();
        }
    });
}

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

$(document).ready(function () {

    $(".TNPrice").val(function (index, value) {
        return value
            .replace(/\D00/g, "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",")
            ;
    });
})