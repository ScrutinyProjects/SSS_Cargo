var dsUsers = null;
function getCashHandoverReportsData() {

    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();


        $.ajax({
            type: "GET",
            url: apiurl + "api/reports/" + loginid + "/" + counterid + "/getnotloadedreportsdata?requesttype=" + $("#hiddenrequesttype").val(),
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
var transactionDate = "";
var locationId  = "";
function GetCashHandoverReport() {
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        transactionDate  = $("#txtTransactionDate").val();
        locationId = $("#ddlLocation").val();

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
            };
            LoadData(input);
        }
    }
    else {
        return false;
    }
}

$("#btnUpdatePayment").unbind().click(function () {
    var isValid = true;
    $("[id^=txtPaidAmount_]").each(function () {
        if (this.value.trim() != "") {
           // input.push({ id: $(this).attr("data-userId"), value: parseInt(this.value) });
           // i++;
        }
        else {
            isValid = false;
        }
    });
    if (isValid) {
        $("#lblHandOverAmount").text($("#spnTotalHandOverAmount").html());
        $("#lblPaidAmount").text($("#spnTotalPaidAmount").html());
        $("#lblRemarks").text($("#txtRemarks").val());
        $("#confirmModal").modal('show');
        $("#btnCloseconfirmModal, #btnCancelPayment").unbind().click(function () {
            $("#confirmModal").modal('hide');
        });
    }
});

function UpdatePayment()
{
    var input = [];
    var isValid = true;
    $("[id^=txtPaidAmount_]").each(function () {
        debugger;
        if (this.value.trim() != "") {
            input.push({ UserId: $(this).attr("data-userId"), PaidAmount: parseInt(this.value) });
        }
        else {
            isValid = false;
        }
    });
    if (isValid) {
        $.ajax({
            type: "POST",
            data: ({ CashData: input, TransactionDate: transactionDate, LocationId: locationId }),
            url: apiurl + "api/reports/updatecashhandover",
            dataType: "json",
            success: function (result) {
                debugger;
                $("#confirmModal").modal('hide');
                $("#spnMessage").css("display", "");

                if (result) {
                    $("#spnMessage").html("Cash Hand Over details saved successfully");
                }
                else {
                    $("#spnMessage").html("Failed to save Cash Hand Over details ");
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

function LoadData(input) {
    $.ajax({
        type: "POST",
        data: (input),
        url: apiurl + "api/reports/getcashhandoverreport",
        dataType: "json",
        success: function (data) {
            debugger;
            if (data && data.Table.length > 0) {
                $('#tbodycashhandoverrecords').html('');
                var totalCashToBeHandover = 0;
                var totalPaidAmount = 0;
                for (var i = 0; i < data.Table.length; i++) {
                    totalCashToBeHandover += data.Table[i].CashToBeHandover;
                    totalPaidAmount += data.Table[i].PaidAmount;
                    var tr = $('<tr class="trdynamic" />');
                    tr.append('' +
                        '<td>' + '<span>' + (i + 1) + '</span></td>' +
                         '<td><span>' + data.Table[i].UserName + '</span></td>' +
                           '<td><span>' + data.Table[i].CashToBeHandover + '</span></td>' +
                        '<td><input type="number" value="0" data-userId="' + data.Table[i].UserId + '" id="txtPaidAmount_' + i + '" /></td>');
                    //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
                    $('#tbodycashhandoverrecords').append(tr);
                }
                var tr = $('<tr class="trdynamic" />');
                tr.append('' +
                    '<td colspan="2"> Total </td>' +
                    '<td><span id="spnTotalHandOverAmount">' + totalCashToBeHandover + '</span></td>' +
                    '<td><span id="spnTotalPaidAmount">' + 0 + '</span></td>');
                $('#tbodycashhandoverrecords').append(tr);
                $("[id^=txtPaidAmount_]").change(function (e) {
                    var total = 0;
                    $("[id^=txtPaidAmount_]").each(function () {
                        if (this.value.trim() != "") {
                            total += parseInt(this.value);
                            $(this).css("border", "1px solid #d5d5d5");
                        }
                        else
                            $(this).css("border-color", "red");
                    });
                    $("#spnTotalPaidAmount").html(total);
                });
            }
            else {
                var tr = $('<tr class="trdynamic" />');
                tr.append('<td colspan = "4"> No Reocrds Found </td>');
                $('#tbodycashconsolidationrecords').append(tr);
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
