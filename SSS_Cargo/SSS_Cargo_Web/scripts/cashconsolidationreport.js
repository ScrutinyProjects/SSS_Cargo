var dsUsers = null;
function getUserCashConsolidationReportsData() {

    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();


        $.ajax({
            type: "GET",
            url: apiurl + "api/reports/" + loginid + "/" + counterid + "/getcashconsolidationdata?requesttype=" + $("#hiddenrequesttype").val(),
            dataType: "json",
            success: function (data) {
                if (data) {
                    if (data.Locations && data.Locations.length > 0) {
                        for (var i = 0; i < data.Locations.length; i++) {
                            var option = '<option value="' + data.Locations[i].CounterId + '">' + data.Locations[i].CounterName + '</option>';
                            $('#ddlLocation').append(option);
                        }
                    }
                    dsUsers = data.Users;
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

function GetUserCashConsolidationReport() {
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        var transactionDate = $("#txtTransactionDate").val();
        var locationId = $("#ddlLocation").val();

        var isvalid = true;
        if (validatetextbox(transactionDate, $('#spnTransactionDate'), 'Please select transaction  date') == false) {
            isvalid = false;
        }
        if (validatedropdown(locationId, $('#spnLocation'), 'Please select location') == false) {
            isvalid = false;
        }
       

        if (isvalid) {
            var input = {};
            input = {
                LoginId: loginid,
                CounterId: counterid,
                TransactionDate: transactionDate,
                LocationId: locationId,
                UserId: userId,
            };
            LoadData(input);
        }
    }
    else {
        return false;
    }
}

function LoadData(input) {
    $.ajax({
        type: "POST",
        data: (input),
        url: apiurl + "api/reports/getcashconsolidationreport",
        dataType: "json",
        success: function (data) {
            debugger;
            if (data && data.Table.length > 0) {
                $('#tbodyusercashrecords').html('');
                var totalCashToBeHandover = 0;
                var totalPaidAmount = 0;
                for (var i = 0; i < data.Table.length; i++) {
                    totalCashToBeHandover += data.Table[i].CashToBeHandover;
                    totalPaidAmount += data.Table[i].PaidAmount;
                    var tr = $('<tr class="trdynamic" />');
                    tr.append('' +
                        '<td>' + '<span>' + (i + 1) + '</span></td>' +
                         '<td><span>' + data.Table[i].UserName + '</span></td>' +
                          '<td><span>' + data.Table[i].GCType + '</span></td>' +
                           '<td><span>' + data.Table[i].CashToBeHandover + '</span></td>' +
                        '<td><span>' + data.Table[i].PaidAmount + '</span></td>');
                    //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
                    $('#tbodyusercashrecords').append(tr);
                }
                var tr = $('<tr class="trdynamic" />');
                tr.append('' +
                    '<td colspan="2"> Total </td>' +
                    '<td><span>' + totalCashToBeHandover + '</span></td>' +
                    '<td><span>' + totalPaidAmount + '</span></td>');
                $('#tbodyusercashrecords').append(tr);
            }
            else {
                var tr = $('<tr class="trdynamic" />');
                tr.append('<td colspan = "4"> No Reocrds Found </td>');
                $('#tbodyusercashrecords').append(tr);
            }
            hideloading();
        },
        error: function (xhr) {
            hideloading();
            showerroralert(xhr.responseText);
        }
    });
}
function validatetextbox(value, span, message) {
    $(span).html('');
    if (value == "") {
        $(span).html(message);
        return false;
    }
}

function validatedropdown(value, span, message) {
    $(span).html('');
    if (value == "-1" || value == "") {
        $(span).html(message);
        return false;
    }
}
