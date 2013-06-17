/// <reference path="jquery-ui-1.8.24.js" />
/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery.ui.datepicker-pl.js" />


$(function () {
    $("[type=datetime]").datepicker($.datepicker.regional["pl"]);
});