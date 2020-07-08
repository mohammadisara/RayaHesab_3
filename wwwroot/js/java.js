$(document).ready(function ()
{
    $('#page1').collapse('show');
    $('#sidebarCollapse').on('click', function ()
    {
        $('.side').toggleClass('active');
    });

    $('li').on('click', function ()
    {
        $('li.link-active').removeClass('link-active');
        $(this).addClass('link-active');
    });

    $('.side ul li a').click(function()
    {
        var rel=$(this).attr('rel');
        if (rel!=="tag")
        {
            var h=$(this).attr('href');
            $('.showw').removeClass('showw').addClass('hidee');
            $(h).removeClass('hidee');
            $(h).addClass('showw');
        }
    });
});