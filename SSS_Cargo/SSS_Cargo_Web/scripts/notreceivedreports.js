function GetNotReceivedReport() {
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
                url: apiurl + "api/reports/getnotreceivedreport",
                dataType: "json",
                success: function (data) {
                    if (data && data.Table && data.Table.length > 0) {
                        $('#tbodynotreceivedrecords').html('');
                        for (var i = 0; i < data.Table.length; i++) {
                            var tr = $('<tr class="trdynamic" />');
                            tr.append('' +
                                '<td>' + '<span id="spnSNo_' + i + '">' + (i + 1) + '</span></td>' +
                                '<td><span id="spnGCNo_' + i + '">' + data.Table[i].GC_No + '</span></td>' +
                                '<td><span id="spnGC_Type_' + i + '">' + data.Table[i].GC_Type + '</span></td>' +
                                '<td><span id="spnFromLocation_' + i + '" >' + data.Table[i].FromLocation + '</span></td>' +
                               '<td><span id="spnPieces_' + i + '" >' + data.Table[i].Pieces + '</span></td>' +
                                '<td><span id="spnToLocation_' + i + '"  >' + data.Table[i].EstimatedReachTime + '</span></td>' +
                                '<td><span id="spnRouteInfo_' + i + '"  >' + data.Table[i].DriverName + '</span></td>' +
                                '<td><span id="spnProductType_' + i + '"  >' + data.Table[i].DriverNumber + '</span></td>' +
                                '<td><span id="spnWeightInfo_' + i + '"  >' + data.Table[i].Remarks + '</span></td>' +
                                '<td><span id="spnCurrentStatus_' + i + '"  >' + data.Table[i].RequestFrom + '</span></td>');
                            $('#tbodynotreceivedrecords').append(tr);
                        }

                    }
                    else {
                        var tr = $('<tr class="trdynamic" />');
                        tr.append('<td colspan = "10" No Reocrds Found </td>');
                        $('#tbodynotreceivedrecords').append(tr);
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
