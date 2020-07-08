/**
 * Created by colorz - mohammad talebkhah on 1/25/18.
 */

$(".box .box-header.has-close-btn").on('click', function (e) {
    e.preventDefault();
    var parent = $(this).closest('.box');
    
    if($(parent).hasClass("close")) {
        $(parent).removeClass('close');        
    } else {
        $(parent).addClass('close');       
    }
});

$("#user-profile-avatar").on('click', function (e) {
    e.preventDefault();
    $("#head-menu-user-profile").show();
});

$("#people_in_network_search_show").on('click', function (e) {
    e.preventDefault();
    $("#search-people-in-network").show();
});

$("#search-people-in-network-cancel").on('click', function (e) {
    e.preventDefault();
    $("#search-people-in-network").hide();
});

$(document).on('click', function (e) {
    if (!$(e.target).closest('#head-menu-profile-user').length) {
        $("#head-menu-user-profile").hide();
    }
});

$(".pageLikeTab").on('click', function (e) {
    $(".pageLikeTab").removeClass('active');
    $(this).addClass('active');
    var pageTabId = $(this).attr('data-tab-id');
    $('.page-like-tab').addClass('display-none');
    $('.page-like-tab[data-tab-id=' + pageTabId + ']').removeClass('display-none');
    
    $(".select2").select2({});
});

$(".close-page").on('click', function (e) {
    var pageTabId = $(this).parent().attr('data-tab-id');
    $('.page-like-tab[data-tab-id=' + pageTabId + ']').remove();
    $(this).parent().remove();

    if ($('.pageLikeTab.active').length !== 1) {
        pageTabId = $($('.pageLikeTab')[0]).attr('data-tab-id');
        console.log('pageTabId', pageTabId);
        $('.pageLikeTab[data-tab-id=' + pageTabId + ']').addClass('active');
        $('.page-like-tab').addClass('display-none');
        $('.page-like-tab[data-tab-id=' + pageTabId + ']').removeClass('display-none');
    }
});

$(".show-modal").on('click', function (e) {
    e.preventDefault();
    var modalId = $(this).attr('data-id');
    $("#" + modalId).fadeIn(150);
});

$(".hide-modal").on('click', function (e) {
    e.preventDefault();
    var modalId = $(this).attr('data-id');
    $("#" + modalId).fadeOut(150);
});

$(".setting-page").on('click', function (e) {
    e.preventDefault();
});

$("#close-pin").on('click', function (e) {
    $("aside").animate({
        right: '-270px'
    }, 300, "swing", function () {
        $(".tab-container").addClass('auto-margin');
        $("main").addClass('open');
        $(".aside-open").animate({
            right: '-10px'
        }, 300);
    });
});

$(".aside-open").on('click', function (e) {
    $(".aside-open").animate({
        right: '-50px'
    }, 300, "swing", function () {
        $(".tab-container").removeClass('auto-margin');
        $("main").removeClass('open');
        $("aside").animate({
            right: '0'
        }, 300);
    });
});

$(".datepicker-trigger").on('click', function (e) {    
    e.preventDefault();
    var input = $(this).closest('.datepicker-holder').find('.has-datepicker');
    $(input).focus();
});

(function ($) {
    $('select.nice-select').niceSelect();
    //$( "#factorDate, #insertDate, #endDate").datepicker();
    $( "#factorDate").datepicker();
    $( "#insertDate").datepicker();
    $( "#endDate").datepicker();
    $( "#infoDate").datepicker();
    $(".select2").select2({});
    $("#payTable").tablesorter();
    $(window).on("load", function () {
        $("#latest_news_content").mCustomScrollbar();
        $("#people_in_network_content").mCustomScrollbar();
    });
})(jQuery);