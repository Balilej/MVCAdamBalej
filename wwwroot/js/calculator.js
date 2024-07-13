$(document).ready(function () {
    var clearOnNextInput = false;

    function calculate() {
        var expression = $('#calcDisplay').val();

        $.ajax({
            type: "POST",
            url: "/CalcHistr/Calculate",
            data: {
                expression: expression
            },
            success: function (data) {
                clearDisplay();
                if (data.result === "Not calculable") {
                    $('#calcDisplay').val("Not calculable");
                } else {
                    $('#calcDisplay').val(data.result);
                    var newRow = '<tr><td>' + data.history[0].id + '</td><td>' + data.history[0].calcEntry + '</td></tr>';
                    $('#historyTable').find('tbody').prepend(newRow);

                    // Remove the 11th row if there are more than 10 rows
                    if ($('#historyTable').find('tbody tr').length > 10) {
                        $('#historyTable').find('tbody tr:last').remove();
                    }
                }
                clearOnNextInput = true;
            },
            error: function (error) {
                console.error("Error calculating:", error.responseJSON.error);
                alert("Error calculating: " + error.responseJSON.error);
            }
        });
    }

    function clearDisplay() {
        $('#calcDisplay').val('');
    }

    $('#calcDisplay').on('input', function () {
        if (clearOnNextInput) {
            clearDisplay();
            clearOnNextInput = false;
        }
    });

    $('#calculateButton').click(function () {
        calculate();
    });

    $('.calc-button').click(function () {
        var buttonValue = $(this).val();
        $('#calcDisplay').val($('#calcDisplay').val() + buttonValue);
    });

    $('#clearButton').click(function () {
        clearDisplay();
    });
});
