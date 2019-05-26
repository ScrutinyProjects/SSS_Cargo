function getloadingdetails() {
    hideallalerts();
    var loadingid = $('#hiddenloadingid').val();

    var input = [];
    input = {
        LoadingId: loadingid
    };

    $.ajax({
        type: "POST",
        data: (input),
        url: apiurl + "api/booking/getloadingdetailstoprintbyloadingid",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.StatusId == 1) {
                    $('.loadingdate').html(data.LoadingDate);
                    $('.route').html(data.RouteInfo);
                    $('.vehiclenumber').html(data.VehicleNumber);
                    $('.drivernumber').html(data.DriverNumber);
                    $('.transactionby').html(data.TransactionBy);
                    $('.drivername').html(data.DriverName);
                    $('.loadingremarks').html(data.Remarks);

                    if (data.LoadingBookings.length > 0) {
                        for (var i = 0; i < data.LoadingBookings.length; i++) {
                            var booking = data.LoadingBookings[i];

                            var tr = $('<tr />');
                            tr.append('<td style="border-right: 1px solid;border-bottom: 1px solid;" align="right">' + (i + 1) + '</td>' +
                                '<td style="border-right: 1px solid;border-bottom: 1px solid;">' + booking.BookSerialNumber + '</td>' +
                                '<td style="border-right: 1px solid;border-bottom: 1px solid;">' + booking.GCType + '</td>' +
                                '<td style="border-right: 1px solid;border-bottom: 1px solid;">' + booking.Description + '</td>' +
                                '<td style="border-right: 1px solid;border-bottom: 1px solid;" align="right">' + booking.TopayAmount + '</td>' +
                                '<td style="border-right: 1px solid;border-bottom: 1px solid;" align="right">' + booking.TotalPieces + '</td>' +
                                '<td style="border-right: 1px solid;border-bottom: 1px solid;" align="right">' + booking.LoadedArticles + '</td>' +
                                '<td style="border-bottom: 1px solid;" align="right">' + booking.DriverInc + '</td>');
                            $('#tbodyloadingitems').append(tr);
                        }
                    }
                }
            }
            hideloading();
        },
        error: function (xhr) {
            hideloading();
            showerroralert(xhr.responseText);
        }
    });
}