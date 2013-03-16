﻿$(document).ready(function () {

    // Confirm deletion on red buttons
    $('.btn-danger').click(function () {
        return confirm('Are you sure?');
    });

    // Fade out notification
    setTimeout(function () {
        $('.alert').fadeOut();
    }, 5000);

    // Select text on focus
    $('input:text').focus(function () { this.select() });
    
});