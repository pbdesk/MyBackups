
/* OffCanvas Bootstrap example - Start*/
$(document).ready(function () {
    $('[data-toggle=offcanvas]').click(function () {
        $('.row-offcanvas').toggleClass('active');
    });
});
/* OffCanvas Bootstrap example - End*/
$('a[rel=tooltip]').tooltip({
    'placement': 'bottom'
});