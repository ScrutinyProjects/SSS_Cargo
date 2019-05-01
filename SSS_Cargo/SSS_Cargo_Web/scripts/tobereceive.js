var tocounterid = '';

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
            url: apiurl + "api/booking/getmastersfortobereceive",
            dataType: "json",
            success: function (data) {
                if (data.StatusId == 1) {
                    if (data.GCTypes != null) {
                        for (var i = 0; i < data.GCTypes.length; i++) {
                            var option = '<option value="' + data.GCTypes[i].GCTypeId + '">' + data.GCTypes[i].GCType + '</option>';
                            $('#selectgctype').append(option);
                        }
                        $('#selectgctype').val(0);
                    }
                    if (data.ToBeReceivingFrom != null) {
                        for (var i = 0; i < data.ToBeReceivingFrom.length; i++) {
                            var option = '<option value="' + data.ToBeReceivingFrom[i].InformationFromId + '">' + data.ToBeReceivingFrom[i].InformationFrom + '</option>';
                            $('#selectrecevingfrom').append(option);
                        }
                    }
                    if (data.Counters != null) {
                        if (data.Counters.length > 0) {
                            var counters = [];

                            for (var i = 0; i < data.Counters.length; i++) {
                                counters.push({ label: data.Counters[i].CounterName });//, value: data.Counters[i].CounterId
                            }

                            $('#textfromcounter').autocomplete({
                                minLength: 2,
                                source: counters,
                                select: function (event, ui) {
                                    tocounterid = ui.item.label;
                                    $('#textfromcounter').val(ui.item.label);
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

function tobereceiveconfirm() {
    var isvalid = true;

    var counterid = $('#hiddencounterid').val().trim();
    var loginid = $('#hiddenloginid').val().trim();

    var gcnuber = $('#textgcnuber').val().trim();
    var gctype = $('#selectgctype').val().trim();
    var fromcounter = $('#textfromcounter').val().trim();
    var numberofpieces = $('#textnumberofpieces').val().trim();
    var reachdatetime = $('#textreachdatetime').val().trim();
    var drivername = $('#textdrivername').val().trim();
    var drivernumber = $('#textdrivernumber').val().trim();
    var remarks = $('#textremarks').val().trim();
    var recevingfrom = $('#selectrecevingfrom').val().trim();
    
    if (validatetextbox(gcnuber, $('#spangcnuber'), 'Please enter GC Number') == false) {
        isvalid = false;
    }
    if (validatedropdown(gctype, $('#spangctype'), 'Please select GC Type') == false) {
        isvalid = false;
    }
    if (validatetextbox(fromcounter, $('#spanfromcounter'), 'Please select From Location') == false) {
        isvalid = false;
    }
    if (validatetextbox(numberofpieces, $('#spannumberofpieces'), 'Please enter number of pieces') == false) {
        isvalid = false;
    }
    if (validatetextbox(drivername, $('#spandrivername'), 'Please enter Driver Name') == false) {
        isvalid = false;
    }
    if (validatetextbox(drivernumber, $('#spandrivernumber'), 'Please enter Driver Mobile Number') == false) {
        isvalid = false;
    }
    if (validatedropdown(recevingfrom, $('#spanrecevingfrom'), 'Please select Information From') == false) {
        isvalid = false;
    }

    if (isvalid) {
        showloading();

        var input = [];
        input = {
            CounterId: counterid,
            UserLoginId: loginid,
            DriverMobileNumber: drivernumber,
            DriverName: drivername,
            EstimatedDateTime: reachdatetime,
            FromCounter: fromcounter,
            GCBookingNumber: gcnuber,
            GCType: gctype,
            Remarks: remarks,
            NumberOfPieces: numberofpieces,
            InformationFromId: recevingfrom
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/booking/savetobereceivedetails",
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
                    $('#closemodaltoberecieve').click();
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
    $('#selectgctype').val('1');
    $('#textfromcounter').val('');
    $('#textnumberofpieces').val('');
    $('#textreachdatetime').val('');
    $('#textdrivername').val('');
    $('#textdrivernumber').val('');
    $('#textremarks').val('');
    $('#selectrecevingfrom').val('0');
    
    tocounterid = '';
}

function savereceiving() {
    $('#spanconfirmgnnum').html($('#textgcnuber').val());
    $('#modaltoberecieve').modal();
}