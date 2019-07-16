
$(document).ready(
    function () {
        var reportTypeSelector = $(".js-report-selector")[0];
        if (reportTypeSelector === null || reportTypeSelector === undefined) {
            return;
        }
        $(reportTypeSelector).on('change', function() {
            getReportForm(reportTypeSelector.value)
        });
       
        function getReportForm(reportType) {
            $.ajax(
                {
                    url: "/tickets/GetReportForm",
                    type: 'GET',
                    dataType: 'html',
                    contentType: 'application/json;charset=utf-8',
                    data: {
                        type: reportType
                    }
                }).done(function (result) {
                    
                    $(".js-report-form-container").html(result);
                    $('#DateFrom').datetimepicker({ format: 'DD.MM.YYYY hh:mm:ss' });
                    $('#DateTo').datetimepicker({ format: 'DD.MM.YYYY hh:mm:ss' });

                }).fail(function () {
                    alert("Report selection request processing failed. Please, contact system administrator.");
                });
        }
    });