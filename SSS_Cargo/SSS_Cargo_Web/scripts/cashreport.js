function getCashReportsData() {

    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();


        $.ajax({
            type: "GET",
            url: apiurl + "api/reports/" + loginid + "/" + counterid + "/getcashreportsdata?requesttype=" + $("#hiddenrequesttype").val(),
            dataType: "json",
            success: function (data) {
                if (data) {
                    if (data.Table && data.Table.length > 0) {
                        for (var i = 0; i < data.Table.length; i++) {
                            var option = '<option value="' + data.Table[i].UserId + '">' + data.Table[i].UserName + '</option>';
                            $('#ddlUser').append(option);
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
    else {
        return false;
    }
}
function GetCashReport() {
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        var transactionDate = $("#txtTransactionDate").val();
        var userId = $("#ddlUser").val();

        var isvalid = true;
        if (validatetextbox(transactionDate, $('#spnTransactionDate'), 'Please select transaction  date') == false) {
            isvalid = false;
        }
        if (validatedropdown(userId, $('#spnUser'), 'Please select user') == false) {
            isvalid = false;
        }


        if (isvalid) {

            var input = {};
            input = {
                LoginId: loginid,
                CounterId: counterid,
                TransactionDate: transactionDate,
                UserId: userId,
            };
            $.ajax({
                type: "POST",
                data: (input),
                url: apiurl + "api/reports/getcashreport",
                dataType: "json",
                success: function (data) {
                    if (data && data.Table.length > 0) {
                        $('#tbodycashrecords').html('');
                        for (var i = 0; i < data.Table.length; i++) {
                            var tr = $('<tr class="trdynamic" />');
                            tr.append('' +
                                '<td>' + '<span id="spnSNo_' + i + '">' + (i + 1) + '</span></td>' +
                                '<td><span id="spnTransactionDate_' + i + '">' + data.Table[i].TransactionDate + '</span></td>' +
                                '<td><span id="spnTransaction_' + i + '">' + data.Table[i].Transaction + '</span></td>' +
                                '<td><span id="spnFromOrTo_' + i + '" >' + data.Table[i].FromOrTo + '</span></td>' +
                                '<td><span id="spnBillAmount_' + i + '"  >' + data.Table[i].BillAmount + '</span></td>' +
                                '<td><span id="spnBasicCharges_' + i + '"  >' + data.Table[i].BasicCharges + '</span></td>' +
                                '<td><span id="spnPassCharges_' + i + '"  >' + data.Table[i].PassCharges + '</span></td>' +
                                '<td><span id="spnCollectionCharges_' + i + '" >' + data.Table[i].CollectionCharges + '</span></td>' +
                                '<td><span id="spnLoadingCharges_' + i + '"  >' + data.Table[i].LoadingCharges + '</span></td>' +
                                 '<td><span id="spnUnloadingCharges_' + i + '"  >' + data.Table[i].UnloadingCharges + '</span></td>' +
                                '<td><span id="spnSenderMobileNumber_' + i + '"  >' + data.Table[i].TranshipCharges + '</span></td>' +
                                '<td><span id="spnTranshipCharges_' + i + '"  >' + data.Table[i].PickupCharges + '</span></td>' +
                                '<td><span id="spnDeliveryCharges_' + i + '"  >' + data.Table[i].DeliveryCharges + '</span></td>' +
                                '<td><span id="spnDemoCharges_' + i + '"  >' + data.Table[i].DemoCharges + '</span></td>' +
                                '<td><span id="spnTotalAmount_' + i + '"  >' + data.Table[i].TotalAmount + '</span></td>');
                            //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
                            $('#tbodycashrecords').append(tr);
                        }

                    }
                    else {
                        var tr = $('<tr class="trdynamic" />');
                        tr.append('<td colspan = "15" No Reocrds Found </td>');
                        $('#tbodycashrecords').append(tr);
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
    else {
        return false;
    }
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