$('.nav li:has(ul)').addClass('menu-item-has-children');
$('.nav li a[href="' + location.pathname + '"]').parent('li').addClass('current-menu-item');
$('.nav li:has(li.current-menu-item)').addClass('current-menu-parent');
//$('.nav li').children('ul').hide();
//$('.nav li.current-menu-item').children('ul').show();
//$('.nav li.current-menu-item').parent().show();
//$('.nav li.current-menu-parent').children().show();

$('.contact-header').off('click').on('click', function () {
    $(this).hide();
    $('.contact-body').show();
});
$('.body-header').off('click').on('click', function () {
    $('.contact-body').hide();
    $('.contact-header').show();
});

