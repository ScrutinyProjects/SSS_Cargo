function GetNotDeliveredReport() {
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        var fromDate = $("#txtFromDate").val();
        var toDate = $("#txtToDate").val();

        var isvalid = true;
        if (validatetextbox(fromDate, $('#spnFromDate'), 'Please select from date') == false) {
            isvalid = false;
        }
        if (validatetextbox(toDate, $('#spnToDate'), 'Please select to date') == false) {
            isvalid = false;
        }
     

        if (isvalid) {

            var input = {};
            input = {
                LoginId: loginid,
                CounterId: counterid,
                FromDate: fromDate,
                ToDate: toDate,
            };
            $.ajax({
                type: "POST",
                data: (input),
                url: apiurl + "api/reports/getnotdeliveredreport",
                dataType: "json",
                success: function (data) {
                    if (data && data.Table && data.Table.length > 0) {
                        $('#tbodynotdelivredrecords').html('');
                        for (var i = 0; i < data.Table.length; i++) {
                            var tr = $('<tr class="trdynamic" />');
                            tr.append('' +
                                '<td>' + '<span id="spnSNo_' + i + '">' + (i + 1) + '</span></td>' +
                                '<td><span id="spnGCNo_' + i + '">' + data.Table[i].GC_No + '</span></td>' +
                                '<td><span id="spnGC_Type_' + i + '">' + data.Table[i].GC_Type + '</span></td>' +
                                '<td><span id="spnFromLocation_' + i + '" >' + data.Table[i].FromLocation + '</span></td>' +
                                '<td><span id="spnPieces_' + i + '"  >' + data.Table[i].Pieces + '</span></td>' +
                                '<td><span id="spnWeightInfo_' + i + '"  >' + data.Table[i].WeightInfo + '</span></td>' +
                                '<td><span id="spnDeliveryTo_' + i + '"  >' + data.Table[i].DeliveryTo + '</span></td>' +
                                '<td><span id="spnPhoneNumber_' + i + '" >' + data.Table[i].PhoneNumber + '</span></td>' +
                                '<td><span id="spnRemarks_' + i + '"  >' + data.Table[i].Remarks + '</span></td>' +
                                '<td><span id="spnLastUpdatedDate_' + i + '"  >' + $.datepicker.formatDate('mm/dd/yy', new Date(data.Table[i].LastUpdatedDate)) + '</span></td>' +
                                '<td><span id="spnLatestRemarks_' + i + '"  >' + data.Table[i].LatestRemarks + '</span></td>');

                            
                                //'<td><a href="javascript:void(0)" onclick="viewbillbreakup(' + data[i].BookingId + ')"><i class="fa fa-remove"></i> View Bill Breakup</a> <a href="javascript:void(0)" onclick="updatestatus(' + data[i].BookingId + ', ' + i + ')"><i class="fa fa-remove"></i> Update Status</a></td>');
                            //<a href="javascript:void(0)" onclick="editparcelitem(this)"><i class="fa fa-pencil"></i> Edit</a>&nbsp;&nbsp;
                            $('#tbodynotdelivredrecords').append(tr);
                        }

                    }
                    else {
                        var tr = $('<tr class="trdynamic" />');
                        tr.append('<td colspan = "11" No Reocrds Found </td>');
                        $('#tbodynotdelivredrecords').append(tr);
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
