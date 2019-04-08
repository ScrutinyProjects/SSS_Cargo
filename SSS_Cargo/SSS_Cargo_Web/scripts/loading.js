var fromcounter = '';
var tocounter = '';

function searchbooking() {
    var bookingnumber = $('#textgcnuber').val().trim();
    var counterid = $('#hiddencounterid').val();

    if (bookingnumber != "") {
        showloading();

        var input = [];
        input = {
            BookingNumber: bookingnumber,
            CounterId: counterid
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/getbookingdetailsbybookingnumber",
            dataType: "json",
            success: function (data) {
                if (data.StatusId != null) {
                    if (data.StatusId == 1) {
                        if ($('#tbodybookings').find('.trdynamic').length == 0) {
                            $('#tbodybookings tr').remove();

                            var route = '';

                            fromcounter = data.FromCounterName;
                            tocounter = data.ToCounterName;

                            route = fromcounter;

                            if (data.TranshipmentPoints != "") {
                                var transhipmentpoints = data.TranshipmentPoints.split(',');

                                for (var i = 0; i < transhipmentpoints.length; i++) {
                                    route = route + ' --> ' + transhipmentpoints[i];
                                }
                            }

                            route = route + ' --> ' + tocounter;

                            $('#spanroute').html(route);
                        }

                        if (fromcounter == data.FromCounterName && tocounter == data.ToCounterName) {
                            var meadurementin = data.MeasurementIn;
                            var innertable = '';

                            if (data.BookingParcels.length > 0) {

                                innertable = '<table class="table"><thead><tr>' +
                                            '<th style="width: 30%;">Pieces</th>' +
                                            '<th style="width: 30%;">Actual Weight</th>' +
                                            '<th style="width: 40%;">Total Weight</th>' +
                                            '</tr></thead>';

                                innertable = innertable + '<tbody>';

                                for (var i = 0; i < data.BookingParcels.length; i++) {
                                    var parcel = data.BookingParcels[i];

                                    innertable = innertable + '<tr>' +
                                    '<td>' + parcel.NumberOfPieces + ' ' + parcel.ParcelType + '</td>' +
                                    '<td>' + parcel.ActualWeight + ' ' + meadurementin + '</td>' +
                                    '<td>' + parcel.TotalWeight + ' ' + meadurementin + '</td>';
                                }

                                innertable = innertable + '</tbody>';
                                innertable = innertable + '</table>';
                            }

                            var tr = $('<tr class="trdynamic" />');
                            tr.append('<td class="sno"></td>' +
                                '<td><span id="spanbookingid" style="display:none">' + data.BookingId + '</span><span id="spanbookingnumber">' + data.BookSerialNumber + '</span></td>' +
                                '<td>' + data.GCType + '</td>' +
                                '<td>' + innertable + '</td>' +
                                '<td>' + data.TotalAmount + '</td>' +
                                '<td><a href="javascript:void(0)" onclick="deletebooking(this)"><i class="fa fa-remove"></i> Remove</a></td>');
                            $('#tbodybookings').append(tr);

                            updatetableserialnumbers($('#tbodybookings'));
                            $('#textgcnuber').val('');
                        }
                        else {
                            showwarningalert("You can add only same route bookings to load");
                        }
                    }
                    else {
                        showwarningalert(data.StatusMessage);
                    }
                }
                else {
                    showwarningalert("Oops!! unable to process your request");
                }

                hideloading();
            },
            error: function (xhr) {
                hideloading();
                showerroralert(xhr.responseText);
            }
        });
    }
    else {
        return false;
    }
}

function updatetableserialnumbers(tbody) {
    var trs = $(tbody).find('.trdynamic');

    for (var i = 0; i < trs.length; i++) {
        var td = $(trs[i]).find('.sno')[0];
        $(td).html((i + 1));
        $(trs[i]).attr("id", "trbooking" + (i + 1));
    }
}

var edittrid = '';

function deletebooking(obj) {
    var tr = $(obj).closest('tr')[0];
    edittrid = $(tr).attr('id');
    $('#modaldeletebooking').modal();
}

function deletebookingconfirm() {
    if (edittrid != '') {
        var tr = $('#tbodybookings').find('#' + edittrid);
        $(tr).remove();
        edittrid = '';

        if ($('#tbodybookings').find('.trdynamic').length == 0) {
            var tr = $('<tr />');
            tr.append('<td colspan="6" style="text-align:center">No search results found</td>');
            $('#tbodybookings').append(tr);

            fromcounter = '';
            tocounter = '';
            $('#spanroute').html('');
        }

        updatetableserialnumbers($('#tbodybookings'));
    }
    $('#closemodaldeletebooking').click();
}

function saveloading() {
    var isvalid = true;

    var counterid = $('#hiddencounterid').val().trim();
    var loginid = $('#hiddenloginid').val().trim();

    var drivername = $('#textdrivername').val().trim();
    var drivernumber = $('#textdrivernumber').val().trim();
    var vehiclenumber = $('#textvehiclenumber').val().trim();
    var driveramount = $('#textdriveramount').val().trim();
    var hamaliamount = $('#texthamaliamount').val().trim();
    var reachdatetime = $('#textreachdatetime').val().trim();
    var loadtime = $('#textloadtime').val().trim();
    var remarks = $('#textremarks').val().trim();

    if (validatetextbox(drivername, $('#spandrivername'), 'Please enter Driver Name') == false) {
        isvalid = false;
    }
    if (validatetextbox(drivernumber, $('#spandrivernumber'), 'Please enter Driver Mobile Number') == false) {
        isvalid = false;
    }
    if (validatetextbox(vehiclenumber, $('#spanvehiclenumber'), 'Please enter Vehicle/Service Number') == false) {
        isvalid = false;
    }
    if (validatetextbox(driveramount, $('#spandriveramount'), 'Please enter Driver Amount') == false) {
        isvalid = false;
    }
    if (validatetextbox(hamaliamount, $('#spanhamaliamount'), 'Please enter Hamali Amount') == false) {
        isvalid = false;
    }

    if ($('#tbodybookings').find('.trdynamic').length == 0) {
        isvalid = false;
        alert("Please select atleast one Booking to load");
    }

    if (isvalid) {
        showloading();

        var bookingids = '';

        var trs = $('#tbodybookings').find('.trdynamic');

        for (var i = 0; i < trs.length; i++) {
            var bookingid = $(trs[i]).find('#spanbookingid').html();
            bookingids = (bookingids == "") ? bookingid : bookingids + "," + bookingid;
        }

        var input = [];
        input = {
            BookingIds: bookingids,
            CounterId: counterid,
            DriverAmount: driveramount,
            DriverMobileNumber: drivernumber,
            DriverName: drivername,
            LoadingDateTime: loadtime,
            EstimatedDateTime: reachdatetime,
            HamaliAmount: hamaliamount,
            Remarks: remarks,
            UserLoginId: loginid,
            VehicleNumber: vehiclenumber
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/saveloadingdetails",
            dataType: "json",
            success: function (data) {
                if (data.StatusId != null) {
                    if (data.StatusId == 1) {
                        showsuccessalert(data.StatusMessage);
                        clearallfields();
                    }
                    else {
                        showwarningalert(data.StatusMessage);
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
}

function validatetextbox(value, span, message) {
    if (value == "") {
        $(span).html(message);
        return false;
    }
}

function clearallfields() {
    $('#textdrivername').val('');
    $('#textdrivernumber').val('');
    $('#textvehiclenumber').val('');
    $('#textdriveramount').val('0');
    $('#texthamaliamount').val('0');
    $('#textreachdatetime').val('');
    $('#textloadtime').val('');
    $('#textremarks').val('');
    $('#spanroute').html('');
    
    $('#tbodybookings tr').remove();
    var tr = $('<tr />');
    tr.append('<td colspan="6" style="text-align:center">No search results found</td>');
    $('#tbodybookings').append(tr);

    fromcounter = '';
    tocounter = '';
}