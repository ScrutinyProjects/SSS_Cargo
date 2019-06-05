var dsUsers = null;
function getUserCashReportsData() {

    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();


        $.ajax({
            type: "GET",
            url: apiurl + "api/reports/" + loginid + "/" + counterid + "/getusercashreportsdata?requesttype=" + $("#hiddenrequesttype").val(),
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

                    $("#ddlLocation").change(function () {
                        $('#ddlUser option:not(:first)').remove();
                        if (dsUsers && dsUsers.Table && dsUsers.Table.length > 0) {
                            for (var i = 0; i < dsUsers.Table.length; i++) {
                                if (dsUsers.Table[i].CounterId == parseInt($(this).val())) {
                                    var option = '<option value="' + dsUsers.Table[i].UserId + '">' + dsUsers.Table[i].UserName + '</option>';
                                    $('#ddlUser').append(option);
                                }
                            }
                        }
                    });

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

function GetUserCashReport() {
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        var transactionDate = $("#txtTransactionDate").val();
        var locationId = $("#ddlLocation").val();
        var userId = $("#ddlUser").val();

        var isvalid = true;
        if (validatetextbox(transactionDate, $('#spnTransactionDate'), 'Please select transaction  date') == false) {
            isvalid = false;
        }
        if (validatedropdown(locationId, $('#spnLocation'), 'Please select location') == false) {
            isvalid = false;
        }
        if (validatedropdown(userId, $('#spnUser'), 'Please select user') == false) {
            isvalid = false;
        }


        if (isvalid) {
            $("#btnPrintReport").css("display", "");
            $("#lblTransactionDate").text(transactionDate);
            $("#lblBy").text($("#ddlUser option:selected").html());
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
function PrintUserCashReport() {
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        var transactionDate = $("#hiddentransactiondate").val();
        var locationId = $("#hiddenlocationid").val();
        var userId = $("#hiddenuserid").val();
        $("#lblTransactionDate").text(transactionDate);
        $("#lblUserName").text();
        $("#lblLocation").text();
        var input = {};
        input = {
            LoginId: loginid,
            CounterId: counterid,
            TransactionDate: transactionDate,
            LocationId: locationId,
            UserId: userId,
        };
        LoadData(input, true);
    }
    else {
        return false;
    }
}
function LoadData(input, isPrint) {
    $.ajax({
        type: "POST",
        data: (input),
        url: apiurl + "api/reports/getusercashreport",
        dataType: "json",
        success: function (data) {
            debugger;
            if (data && data.Table.length > 0) {
                $('#tbodyusercashrecords').html('');
                var totalBillAmount = 0;
                var totalDriverCharges = 0;
                var totalHamaliCharges = 0;
                var totalPassCharges = 0;
                var totalSURCharges = 0;
                var totalLRCharges = 0;
                var totalCollectionCharges = 0;
                var totalBookingCharges = 0;
                var totalPickupCharges = 0;
                var totalDeliveryCharges = 0;
                var totalDemoCharges = 0;
                var sumTotalAmount = 0;

                for (var i = 0; i < data.Table.length; i++) {
                    totalBillAmount += data.Table[i].BillAmount;
                    totalDriverCharges += data.Table[i].DriverCharges;
                    totalHamaliCharges += data.Table[i].HamaliCharges;
                    totalPassCharges += data.Table[i].PassCharges;
                    totalSURCharges += data.Table[i].SURCharges;
                    totalLRCharges += data.Table[i].LRCharges;
                    totalCollectionCharges += data.Table[i].CollectionCharges;
                    totalBookingCharges += data.Table[i].BookingCharges;
                    totalPickupCharges += data.Table[i].PickupCharges;
                    totalDeliveryCharges += data.Table[i].DeliveryCharges;
                    totalDemoCharges += data.Table[i].DemoCharges;
                    sumTotalAmount += data.Table[i].TotalAmount;

                    var tr = $('<tr class="trdynamic" />');
                    tr.append('' +
                        '<td>' + '<span>' + (i + 1) + '</span></td>' +
                         '<td><span>' + data.Table[i].GCNumber + '</span></td>' +
                          '<td><span>' + data.Table[i].GCType + '</span></td>' +
                           '<td><span>' + data.Table[i].Articles + '</span></td>' +
                        '<td><span>' + data.Table[i].TransactionDate + '</span></td>' +
                        '<td><span>' + data.Table[i].Transaction + '</span></td>' +
                        '<td><span>' + data.Table[i].FromOrTo + '</span></td>' +
                        '<td><span>' + data.Table[i].BillAmount + '</span></td>' +
                        '<td><span>' + data.Table[i].DriverCharges + '</span></td>' +
                        '<td><span>' + data.Table[i].HamaliCharges + '</span></td>' +
                        '<td><span>' + data.Table[i].PassCharges + '</span></td>' +
                          '<td><span>' + data.Table[i].SURCharges + '</span></td>' +
                            '<td><span>' + data.Table[i].LRCharges + '</span></td>' +
                        '<td><span>' + data.Table[i].CollectionCharges + '</span></td>' +
                        '<td><span>' + data.Table[i].BookingCharges + '</span></td>' +
                         '<td><span>' + data.Table[i].PickupCharges + '</span></td>' +
                        '<td><span>' + data.Table[i].DeliveryCharges + '</span></td>' +
                        '<td><span>' + data.Table[i].DemoCharges + '</span></td>' +
                        '<td><span>' + data.Table[i].TotalAmount + '</span></td>' +
                        '<td><span>' + data.Table[i].Remarks + '</span></td>');
                    //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
                    $('#tbodyusercashrecords').append(tr);
                }
                var tr = $('<tr class="trdynamic" />');
                tr.append('' +
                    '<td colspan="7"> Total </td>' +
                    '<td><span>' + totalBillAmount + '</span></td>' +
                    '<td><span>' + totalDriverCharges + '</span></td>' +
                    '<td><span>' + totalHamaliCharges + '</span></td>' +
                    '<td><span>' + totalPassCharges + '</span></td>' +
                      '<td><span>' + totalSURCharges + '</span></td>' +
                        '<td><span>' + totalLRCharges + '</span></td>' +
                    '<td><span>' + totalCollectionCharges + '</span></td>' +
                    '<td><span>' + totalBookingCharges + '</span></td>' +
                     '<td><span>' + totalPickupCharges + '</span></td>' +
                    '<td><span>' + totalDeliveryCharges + '</span></td>' +
                    '<td><span>' + totalDemoCharges + '</span></td>' +
                    '<td><span>' + sumTotalAmount + '</span></td>' +
                    '<td></td>');
                //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
                $('#tbodyusercashrecords').append(tr);
            }
            else {
                $("#btnPrintReport").css("display", "none");
                var tr = $('<tr class="trdynamic" />');
                tr.append('<td colspan = "20"> No Reocrds Found </td>');
                $('#tbodyusercashrecords').append(tr);
            }
            if (data.Table1.length > 0) {
                $('#tbodyusercashsummary').html('');
                for (var i = 0; i < data.Table1.length; i++) {
                    var tr = $('<tr class="trdynamic" />');
                    tr.append('' +
                        '<td>' + '<span>' + (i + 1) + '</span></td>' +
                         '<td><span>' + data.Table1[i].Transaction + '</span></td>' +
                          '<td><span>' + data.Table1[i].LRS + '</span></td>' +
                           '<td><span>' + data.Table1[i].Articles + '</span></td>' +
                        '<td><span>' + data.Table1[i].BillAmount + '</span></td>');
                    //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
                    $('#tbodyusercashsummary').append(tr);
                }
            }
            else {
                var tr = $('<tr class="trdynamic" />');
                tr.append('<td colspan = "5"> No Reocrds Found </td>');
                $('#tbodyusercashsummary').append(tr);
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

$("#btnPrintReport").unbind().click(function () {
    debugger;
    var form = document.createElement("form");
    form.setAttribute("method", "post");
    form.setAttribute("action", "printusercashreport");

    //form.setAttribute("target", "view");

    //var hiddenField = document.createElement("input");
    //hiddenField.setAttribute("type", "hidden");
    //hiddenField.setAttribute("name", "message");
    //hiddenField.setAttribute("value", LoginId);
    form.appendChild(document.getElementById("txtTransactionDate"));
    form.appendChild(document.getElementById("ddlLocation"));
    form.appendChild(document.getElementById("ddlUser"));
    document.body.appendChild(form);
    //window.open('', 'view');
    form.submit();

});