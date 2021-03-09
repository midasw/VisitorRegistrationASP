// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {

    let $selCounter = $('#js-selection-counter');

    $(".js-table-check").each(function (_i, table) {
        let $tbody = $('tbody', table);
        let $checkAll = $("thead input[type='checkbox']", table).first();

        function updateCounter(count) {
            $selCounter.text(count > 0 ? count : "");
        }

        $tbody.on('change', "input[type='checkbox']", function () {
            $(this).closest('tr').toggleClass("table-active", this.checked);

            let $checks = $("input[type='checkbox']", $tbody);
            let nChecked = $checks.filter(':checked').length;

            if (this.checked && nChecked == $checks.length) {
                $checkAll.prop({
                    indeterminate: false,
                    checked: true
                });
            }
            else if (nChecked == 0) {
                $checkAll.prop({
                    indeterminate: false,
                    checked: false
                });
            }
            else {
                $checkAll.prop({
                    indeterminate: true,
                    checked: false
                });
            }

            updateCounter(nChecked);
        });

        $checkAll.on('change', function () {
            let $checks = $("input[type='checkbox']", $tbody);

            $checks.prop('checked', this.checked);
            $('tr', $tbody).toggleClass("table-active", this.checked);

            let nChecked = $checks.filter(':checked').length;
            updateCounter(nChecked);
        });
    });
});
