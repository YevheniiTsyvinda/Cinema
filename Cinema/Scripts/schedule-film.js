// эта функция обрабатывает нажатия на места, фиксирует выбранные места и проверяет если нажатое место в среди выбраных ранее
$(document).ready(
    function () {
        var seatTemplateContainer = document.querySelector(".js-selected-seat-template");
        if (seatTemplateContainer === null || seatTemplateContainer === undefined) {
            return;
        }
        var source = seatTemplateContainer.innerHTML;
        var template = Handlebars.compile(source);
        var currentCost = $('.js-seat-container')[0].dataset.currentCost;
        var currentTimeslotId = $('.js-seat-container')[0].dataset.currentTimeslotId;
        var selectedSeats = {
            addedSeats: [],
            sum: 0
        };
        $(".js-seat-container").on('click',
            '.js-seat-selector',
            function (e) {
                var targetElem = e.currentTarget;
                var dataSet = targetElem.dataset;
                //var resultString = 'row:' + dataSet.seatRow + ' col:' + dataSet.seatCol;
                var newSeat = {
                    row: dataSet.seatRow,
                    seat: dataSet.seatCol,
                    elem: targetElem
                };
                //проверяем если нажатое место в списке уже выбранных
                var existingSeatIndex = -1;
                for (var i = 0; i < selectedSeats.addedSeats.length; i++) {
                    var currentSeat = selectedSeats.addedSeats[i];
                    if (currentSeat.row === newSeat.row && currentSeat.seat === newSeat.seat) {
                        existingSeatIndex = i;
                        break;
                    }
                }
                    if (existingSeatIndex !== -1) {
                        selectedSeats.addedSeats.splice(existingSeatIndex, 1);
                    } else {
                        selectedSeats.addedSeats.push(newSeat);
                    }
                    selectedSeats.sum = currentCost * selectedSeats.addedSeats.length;
                    var resultHtml = template(selectedSeats);
                $(".js-seat-result-container").html(resultHtml);
            });
        $(".js-seat-container").on('click',
            '.js-reserve-seats', function (e) {
                sendSeatsToServer('reserve');
            });
        $(".js-seat-container").on('click',
            '.js-buy-seats', function (e) {
                sendSeatsToServer('buy');
            });
        function sendSeatsToServer(status) {
            var resultModel = {
                seatsRequest: selectedSeats,
                selectedStatus: status,
                timeslotId: currentTimeslotId
            };
            $.ajax(
                {
                    url: "/tickets/processRequest",
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json;charset=utf-8',
                    data: JSON.stringify(resultModel)
                }).done(function () {
                    for (var i = 0; i < selectedSeats.addedSeats.length; i++) {
                        var currentSeat = selectedSeats.addedSeats[i].elem;
                        if (status === 'reserve') {
                            currentSeat.parentNode.classList.add('is-reserved');
                        }
                        else {
                            currentSeat.parentNode.classList.add('is-sold');
                            currentSeat.checked = false;
                            currentSeat.disabled = true;
                        }
                        selectedSeats.addedSeats = [];
                        var resultHtml = template(selectedSeats);
                        $(".js-seat-result-container").html(resultHtml);
                    }
                }).fail(function () {
                    alert("Order processing failed. Please, contact system administrator.");
                });
        }
    });