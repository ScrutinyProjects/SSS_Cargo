var fromcounterid = '';
var gcbookingnumber = '';
var bookingid = 0;
var receiveid = 0;

function getmasters() {
    hideallalerts();

    var counterid = $("#hiddencounterid").val();
    var loginid = $("#hiddenloginid").val();

    if (counterid != "" && loginid != "") {
        showloading();

        var input = [];
        input = {
            CounterId: counterid,
            LoginId: loginid
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/getmastersfordelivery",
            dataType: "json",
            success: function (data) {
                if (data.StatusId == 1) {
                    if (data.GCTypes != null) {
                        for (var i = 0; i < data.GCTypes.length; i++) {
                            var option = '<option value="' + data.GCTypes[i].GCTypeId + '">' + data.GCTypes[i].GCType + '</option>';
                            $('#selectgctype').append(option);
                        }
                    }
                }
                else {
                    showwarningalert(data.StatusMessage);
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

function validatetextbox(value, span, message) {
    if (value == "") {
        $(span).html(message);
        return false;
    }
}

function validatedropdown(value, span, message) {
    if (value == "0" || value == "") {
        $(span).html(message);
        return false;
    }
}

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
            url: apiurl + "api/booking/getreceivingdetailsbybookingnumber",
            dataType: "json",
            success: function (data) {
                if (data.StatusId != null) {
                    if (data.StatusId == 1) {
                        gcbookingnumber = data.GCBookingNumber;
                        bookingid = data.BookingId;
                        receiveid = data.ReceivingId;
                        var weight = (data.TotalWeight > 0) ? data.TotalWeight + " " + data.MeasurementIn : "--";
                        $("#divReceivingDetails").css('display', '');
                        $('#spangcnumber').html(bookingnumber);
                        $('#spandeliveryfrom').html((data.FromCounterName == "") ? "--" : data.FromCounterName);
                        $('#selectgctype').val(data.GCTypeId);
                        $('#spandeliverypieces').html((data.NumberOfPieces == "") ? "--" : data.NumberOfPieces);
                        $('#spandeliveryweight').html(weight);

                        $('#spandeliveryto').html('');
                        $('#textdeliveryto').val(data.DeliveryToName);
                        $('#spandeliveryphonenumber').html('');
                        $('#textdeliveryphonenumber').val(data.DeliveryToNumber);
                        $('#spandeliverytoberemarks').html((data.Remarks == "") ? "--" : data.Remarks);
                          
                        $('#textdeliverycharges').val(data.DeliveryCharges);
                        $('#textdemocharges').val(data.DemoCharges);
                         $('#textdeliveryremarks').val('');
                    }   
                    else {
                        $("#divReceivingDetails").css('display', 'none');
                        gcbookingnumber = '';
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

function updatedelivery() {
    var isvalid = true;

    var counterid = $('#hiddencounterid').val().trim();
    var loginid = $('#hiddenloginid').val().trim();

    var gctype = $('#selectgctype').val();
    var deliverycharges = $('#textdeliverycharges').val().trim();
    var democharges = $('#textdemocharges').val().trim();
    var deliveryremarks = $('#textdeliveryremarks').val().trim();
    var deliveryphonenumber = $("#textdeliveryphonenumber").val().trim();
    var deliveryto = $("#textdeliveryto").val().trim();
    var billamount = $('#textbillamount').val().trim();

    if (validatedropdown(gctype, $('#spandeliverygctype'), 'Please select GC Type') == false) {
        isvalid = false;
    }
    if (validatetextbox(billamount, $('#spanbillamount'), 'Please enter bill amount') == false) {
        isvalid = false;
    }
    if (isvalid) {
        showloading();

        var input = [];
        input = {
            CounterId: counterid,
            UserLoginId: loginid,
            BookingId: bookingid,
            GCBookingNumber: gcbookingnumber,
            GCType: gctype,
            Remarks: deliveryremarks,
            DeliveryCharges: (deliverycharges == "" ? 0 : deliverycharges),
            DemoCharges: (democharges == "" ? 0 : democharges),
            ReceivingId: receiveid,
            DeliveryPhoneNumber: deliveryphonenumber,
            DeliveryTo: deliveryto,
            BillAmount: billamount
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/insertdeliverydetails",
            dataType: "json",
            success: function (data) {
                if (data.StatusId != null) {
                    if (data.StatusId == 1) {
                        showsuccessalert(data.StatusMessage);
                        window.location = "/cargo/deliverysuccess/" + data.DeliveryId;
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

function clearallfields() {
    $('#textgcnuber').val('');

    $('#textdeliverycharges').val('');
    $('#textdemocharges').val('');
    $('#textdeliveryremarks').val('');

    $('#spandeliveryfrom').html("--");
    $('#selectgctype').val(0);
    $('#spandeliverypieces').html("--");
    $('#spandeliveryweight').html("--");
    $('#spandeliveryto').html("--");
    $('#spandeliveryphonenumber').html("--");
    $('#spandeliverytoberemarks').html("--");
    
    fromcounterid = '';
    gcbookingnumber = '';
    bookingid = 0;
    receiveid = 0;
}