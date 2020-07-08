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


$(document).ready(function() {
    $("body").on('click', function(e) {
        $(".container-modal").fadeOut(150);
    });

    $(".modal-body").on('click', function(e) {
        e.stopPropagation();
    });

    $(".show-modal").on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var modalId = $(this).attr('data-id');
        $("#" + modalId).fadeIn(150);
    
        initGallerySlider();
    
        
    });

    $(document).on("click", ".otp-trigger-link", function() {
        var targetElem = $(this).data("target");
        
        $(this).parent().eq(0).removeClass("active");        
        $(targetElem).addClass("active");
    });

    $(document).on("click", ".otp-close", function() {
        var triggerElem = $(this).data("trigger");

        $(this).closest(".otp-holder").removeClass("active");        
        $(triggerElem).addClass("active");
    });

    $('.news-slider').flexslider({
        animation: "slide",
        controlNav: false,
        customDirectionNav: $(".custom-navigation a")
    });

   

    var initGallerySlider = function () {
        $('#galleryCarousel').flexslider({
            animation: "slide",
            controlNav: false,
            animationLoop: false,
            slideshow: false,
            itemWidth: 102,
            itemMargin: 10,
            asNavFor: '#gallerySlider',
            // rtl: true
          
          });
         
          $('#gallerySlider').flexslider({
            animation: "slide",
            controlNav: false,
            animationLoop: false,
            slideshow: false,
            sync: "#galleryCarousel",
            //rtl: true
          });
    }
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
        left: '-270px'
    }, 300, "swing", function () {
        $(".tab-container").addClass('auto-margin');
        $("main").addClass('open');
        $(".aside-open").animate({
            left: '-10px'
        }, 300);
    });
});

$(".aside-open").on('click', function (e) {
    $(".aside-open").animate({
        left: '-50px'
    }, 300, "swing", function () {
        $(".tab-container").removeClass('auto-margin');
        $("main").removeClass('open');
        $("aside").animate({
            left: '0'
        }, 300);
    });
});

$(".datepicker-trigger").on('click', function (e) {    
    e.preventDefault();
    var input = $(this).closest('.datepicker-holder').find('.has-datepicker');
    $(input).focus();
});

(function ($) {
    $(".list-link").on("click", function() {
        var dataClick = $(this).data("click");

        if(dataClick === "no_click") {
            $(this).data("click", "clicked");            
            $(this).closest(".list").find(".list-box").addClass("active");
        } else {
            $(this).data("click", "no_click");
            $(this).closest(".list").find(".list-box").removeClass("active");
        }
    });

    $(".list-box .remove").on("click", function() {
        var dataClick = $(this).closest(".list").find(".list-link").data("click", "no_click");
        $(this).closest(".list").find(".list-box").removeClass("active");        
    });

    
    $(".file-uploader").on("change", function(event) {
        var holder = $(this).closest(".file-uploader-holder");
        var ulElem = $(this).closest(".file-uploader-holder").find(".file-uploader-value-list");

        $(ulElem).empty();

        if(this.files && this.files.length > 0) {
            var filesLength = this.files.length;
            for(var i = 0; i < filesLength; i++) {
                $(ulElem).append("<li>" + this.files[i].name + "</li>");
            }
        }
        
    });

    $('select.nice-select').niceSelect();
    //$( "#factorDate, #insertDate, #endDate").datepicker();
    $( "#factorDate").datepicker();
    $( "#insertDate").datepicker();
    $( "#endDate").datepicker();
    $( "#infoDate").datepicker();
    $("#repairInsertDate").datepicker();
    $("#deliveryDate").datepicker();
    $("#repairProductMoneyDate").datepicker();
    $("#productManagementFactorDate").datepicker();
    $(".select2").select2({});
    $("#payTable").tablesorter();    
    $("#repairTable").tablesorter();
    $("#repairPayTable").tablesorter();
    $(window).on("load", function () {
        $("#latest_news_content").mCustomScrollbar();
        $("#people_in_network_content").mCustomScrollbar();                
    });
    
    // autocomplate
    var states = [
        'Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California',
        'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 
        'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 
        'Kansas', 'Kentucky', 'Louisiana', 'Maine', 'Maryland', 
        'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi', 
        'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire',
        'New Jersey', 'New Mexico', 'New York', 'North Carolina', 
        'North Dakota', 'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 
        'Rhode Island', 'South Carolina', 'South Dakota', 'Tennessee', 
        'Texas', 'Utah', 'Vermont', 'Virginia', 'Washington', 
        'West Virginia', 'Wisconsin', 'Wyoming'
    ];

    $("#barCode").autocomplete({
        source:[states]
    });
})(jQuery);