'use strict';

let subMenuItems = $('.sub-menu li');

subMenuItems.on('click', function () {
    if ($(this).data('type') === '*') {
        if ($(this).find('input').is(':checked'))
            $(this).nextAll().find('[type="checkbox"]').prop('checked', true)
        else
            $(this).nextAll().find('[type="checkbox"]').prop('checked', false)
    } else {
        if ($(this).siblings('[data-type="*"]').find('input').is(':checked'))
            $(this).siblings('[data-type="*"]').find('input').prop('checked', false);
    }
});
