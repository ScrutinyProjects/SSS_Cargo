var fromcounterid = '';
var gcbookingnumber = '';
var bookingid = 0;
var tobereceiveid = 0;

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
            url: apiurl + "api/booking/getmastersforreceiving",
            dataType: "json",
            success: function (data) {
                if (data.StatusId == 1) {
                    if (data.GCTypes != null) {
                        for (var i = 0; i < data.GCTypes.length; i++) {
                            var option = '<option value="' + data.GCTypes[i].GCTypeId + '">' + data.GCTypes[i].GCType + '</option>';
                            $('#selectactualgctype').append(option);
                        }
                    }
                    if (data.ReceivingTypes != null) {
                        for (var i = 0; i < data.ReceivingTypes.length; i++) {
                            var option = '<option value="' + data.ReceivingTypes[i].ReceivingTypeId + '">' + data.ReceivingTypes[i].ReceivingTypeName + '</option>';
                            $('#selectactualreceivingtype').append(option);
                        }
                    }
                    if (data.Counters != null) {
                        if (data.Counters.length > 0) {
                            var counters = [];

                            for (var i = 0; i < data.Counters.length; i++) {
                                counters.push({ label: data.Counters[i].CounterName });//, value: data.Counters[i].CounterId
                            }

                            $('#textactualfrom').autocomplete({
                                minLength: 2,
                                source: counters,
                                select: function (event, ui) {
                                    tocounterid = ui.item.label;
                                    $('#textactualfrom').val(ui.item.label);
                                }
                            });
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
    if (value) {
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
        $("#spantobegcnumber").html(bookingnumber);
        showloading();

        var input = [];
        input = {
            BookingNumber: bookingnumber,
            CounterId: counterid
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/gettobereceiveddetailsbybookingnumber",
            dataType: "json",
            success: function (data) {
                if (data.StatusId != null) {
                    if (data.StatusId == 1) {
                        gcbookingnumber = data.GCBookingNumber;
                        bookingid = data.BookingId;
                        tobereceiveid = data.ToBeReceiveId;

                        $('#spantobefrom').html((data.FromCounterName == "") ? "--" : data.FromCounterName);
                        $('#spantobegctype').html((data.GCType == "") ? "--" : data.GCType);
                        $('#spantobepieces').html((data.NumberOfPieces == "") ? "--" : data.NumberOfPieces);
                        $('#spantobeeta').html((data.EstimatedDateTime == "") ? "--" : data.EstimatedDateTime);
                        $('#spantobedrivername').html((data.DriverName == "") ? "--" : data.DriverName);
                        $('#spantobedrivernumber').html((data.DriverNumber == "") ? "--" : data.DriverNumber);
                        $('#spantoberemarks').html((data.Remarks == "") ? "--" : data.Remarks);

                        $('#textactualfrom').val(data.FromCounterName);
                        $('#selectactualgctype').val(data.GCTypeId);
                        $('#selectactualreceivingtype').val('0');
                        $('#textactualnumberofpieces').val(data.NumberOfPieces);
                        $('#textactualvehiclenumber').val('');
                        $('#textactualdrivername').val(data.DriverName);
                        $('#textactualdrivernumber').val(data.DriverNumber);
                        $('#textactualhamaliamount').val('0');
                        $('#textactualtranshipmentamount').val('0');
                        $('#textactualtotalweight').val(data.TotalWeight);
                        $('#textactualdeliveryto').val('');
                        $('#textactualphonenumber').val('');
                        $('#textactualremarks').val('');
                    }
                    else if (data.StatusId == 2) {
                        gcbookingnumber = bookingnumber;
                        bookingid = data.BookingId;
                        tobereceiveid = 0;

                        $('#spantobefrom').html("--");
                        $('#spantobegctype').html("--");
                        $('#spantobepieces').html("--");
                        $('#spantobeeta').html("--");
                        $('#spantobedrivername').html("--");
                        $('#spantobedrivernumber').html("--");
                        $('#spantoberemarks').html("--");

                        $('#textactualfrom').val('');
                        $('#selectactualgctype').val('0');
                        $('#selectactualreceivingtype').val('0');
                        $('#textactualnumberofpieces').val('');
                        $('#textactualvehiclenumber').val('');
                        $('#textactualdrivername').val('');
                        $('#textactualdrivernumber').val('');
                        $('#textactualhamaliamount').val('0');
                        $('#textactualtranshipmentamount').val('0');
                        $('#textactualtotalweight').val('');
                        $('#textactualdeliveryto').val('');
                        $('#textactualphonenumber').val('');
                        $('#textactualremarks').val('');
                    }
                    else {
                        gcbookingnumber = bookingnumber;
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

function savereceiveing() {
    var isvalid = true;

    var counterid = $('#hiddencounterid').val().trim();
    var loginid = $('#hiddenloginid').val().trim();

    var actualfrom = $('#textactualfrom').val().trim();
    var actualgctype = $('#selectactualgctype').val();
    var actualreceivingtype = $('#selectactualreceivingtype').val();
    var actualnumberofpieces = $('#textactualnumberofpieces').val().trim();
    var actualvehiclenumber = $('#textactualvehiclenumber').val().trim();
    var actualdrivername = $('#textactualdrivername').val().trim();
    var actualdrivernumber = $('#textactualdrivernumber').val().trim();
    var actualhamaliamount = $('#textactualhamaliamount').val().trim();
    var actualtranshipmentamount = $('#textactualtranshipmentamount').val().trim();
    var actualtotalweight = $('#textactualtotalweight').val().trim();
    var actualdeliveryto = $('#textactualdeliveryto').val().trim();
    var actualphonenumber = $('#textactualphonenumber').val().trim();
    var actualremarks = $('#textactualremarks').val().trim();
    var billamount = $('#textbillamount').val().trim();
    
    if (validatetextbox(actualfrom, $('#spanactualfrom'), 'Please select From Counter') == false) {
        isvalid = false;
    }
    if (validatedropdown(actualgctype, $('#spanactualgctype'), 'Please select GC Type') == false) {
        isvalid = false;
    }
    if (validatedropdown(actualreceivingtype, $('#spanactualreceivingtype'), 'Please select Receiving Type') == false) {
        isvalid = false;
    }
    if (validatetextbox(actualnumberofpieces, $('#spanactualnumberofpieces'), 'Please enter Number of Pieces') == false) {
        isvalid = false;
    }
    if (validatetextbox(actualvehiclenumber, $('#spanactualvehiclenumber'), 'Please enter Vehicle/Service Number') == false) {
        isvalid = false;
    }
    if (validatetextbox(actualdrivername, $('#spanactualdrivername'), 'Please enter Driver Name') == false) {
        isvalid = false;
    }
    if (validatetextbox(actualdrivernumber, $('#spanactualdrivernumber'), 'Please enter Driver Name') == false) {
        isvalid = false;
    }
    if (validatetextbox(actualhamaliamount, $('#spanactualhamaliamount'), 'Please enter Hamali Charges') == false) {
        isvalid = false;
    }
    if (validatetextbox(actualtranshipmentamount, $('#spanactualtranshipmentamount'), 'Please enter Transhipment Charges') == false) {
        isvalid = false;
    }
    if (validatetextbox(actualtotalweight, $('#spanactualtotalweight'), 'Please enter Total Weight') == false) {
        isvalid = false;
    }
    if (validatetextbox(actualdeliveryto, $('#spanactualdeliveryto'), 'Please enter Delivery To Person Name') == false) {
        isvalid = false;
    }
    if (validatetextbox(actualphonenumber, $('#spanactualphonenumber'), 'Please enter Delivery To Person Mobile Number') == false) {
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
            DriverMobileNumber: actualdrivernumber,
            DriverName: actualdrivername,
            BookingId: bookingid,
            FromCounter: actualfrom,
            GCBookingNumber: gcbookingnumber,
            GCType: actualgctype,
            Remarks: actualremarks,
            NumberOfPieces: actualnumberofpieces,
            DeliveryToName: actualdeliveryto,
            DeliveryToNumber: actualphonenumber,
            HamaliCharges: actualhamaliamount,
            ReceivingType: actualreceivingtype,
            TranshipmentCharges: actualtranshipmentamount,
            TotalWeight: actualtotalweight,
            VehicleNumber: actualvehiclenumber,
            ToBeReceiveId: tobereceiveid,
            BillAmount: billamount
        };
        
        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/savereceivingdetails",
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

function clearallfields() {
    $('#textgcnuber').val('');

    $('#textactualfrom').val('');
    $('#selectactualgctype').val('0');
    $('#selectactualreceivingtype').val('0');
    $('#textactualnumberofpieces').val('');
    $('#textactualvehiclenumber').val('');
    $('#textactualdrivername').val('');
    $('#textactualdrivernumber').val('');
    $('#textactualhamaliamount').val('0');
    $('#textactualtranshipmentamount').val('0');
    $('#textactualtotalweight').val('');
    $('#textactualdeliveryto').val('');
    $('#textactualphonenumber').val('');
    $('#textactualremarks').val('');

    $("#spantobegcnumber").html("--");
    $('#spantobefrom').html("--");
    $('#spantobegctype').html("--");
    $('#spantobepieces').html("--");
    $('#spantobeeta').html("--");
    $('#spantobedrivername').html("--");
    $('#spantobedrivernumber').html("--");
    $('#spantoberemarks').html("--");

    fromcounterid = '';
    gcbookingnumber = '';
    bookingid = 0;
    tobereceiveid = 0;
}