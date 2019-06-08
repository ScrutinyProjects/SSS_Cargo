//var lstBookingStatus = [];
//var lstBookings = [];
function getDeliveryReportsData() {

    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    var GcTypesCount = 0;
    if (loginid != "" && counterid) {
        showloading();


        $.ajax({
            type: "GET",
            url: apiurl + "api/reports/" + loginid + "/" + counterid + "/getdeliveryreportsdata?requesttype=" + $("#hiddenrequesttype").val(),
            dataType: "json",
            success: function (data) {
                if (data) {
                    //$("#txtSearch").keyup(function (e) {
                    //    //if (lstBookings && e.target.value)
                    //    //{
                    //    //    var bookings = [];
                    //    //    for (var i = 0; i < lstBookings.length; i++) {
                    //    //        for (var j = 0; j < Object.keys(lstBookings[i]).length; j++) {
                    //    //            if (lstBookings[i][Object.keys(lstBookings[i])[j]] && lstBookings[i][Object.keys(lstBookings[i])[j]].toString().toLowerCase().indexOf(e.target.value.toLowerCase()) != -1) {
                    //    //                bookings.push(lstBookings[i]);
                    //    //                break;
                    //    //            }
                    //    //        }
                    //    //    }
                    //    //    BindGrid(bookings);
                    //    //}
                    //    //else
                    //    //    BindGrid(lstBookings);
                    //});
                    if (data.Locations && data.Locations.length > 0) {
                        for (var i = 0; i < data.Locations.length; i++) {
                            var option = '<option value="' + data.Locations[i].CounterId + '">' + data.Locations[i].CounterName + '</option>';
                            $('#ddlLocation').append(option);
                        }
                    }
                    if (data.GCTypes && data.GCTypes.length > 0) {
                        data.GCTypes = data.GCTypes.filter(k=>k.GCTypeId != 0);
                        var ds = new kendo.data.DataSource({
                            data: data.GCTypes
                        });

                        var checkInputs = function (elements) {
                            elements.each(function () {
                                var element = $(this);
                                var input = element.children("input");
                                input.prop("checked", element.hasClass("k-state-selected"));
                                if (element.css('display') == 'none')
                                    $("#Header").attr("checked", false);
                            });
                        };

                        $("#ddlGCType").kendoMultiSelect({
                            dataValueField: "GCTypeId",
                            dataTextField: "GCType",
                            dataSource: ds,
                            dataBound: function () {
                                var items = this.ul.find("li");
                                setTimeout(function () {
                                    checkInputs(items);
                                });
                            },
                            itemTemplate: "<input type='checkbox' id= 'chkGCType_#:data.GCTypeId#'/> #:data.GCType#",
                            headerTemplate: "<div><input type='checkbox' id='Header'><label> Select All</label></div>",
                            autoClose: false,
                            change: function () {
                                var items = this.ul.find("li");
                                checkInputs(items);
                            }
                        });

                        $('#Header').click(function () {
                            if ($(this).is(':checked')) {
                                $('#ddlGCType_listbox').find("li").each(function () {
                                    if ($(this).css('display') != "none")
                                        $(this).trigger("click");
                                   // $(this).find("input").prop("checked",true);
                                });
                            } else {
                                $('#ddlGCType_listbox').find("li").each(function () {
                                    //$(this).trigger("click");
                                    $(this).find("input").prop("checked", false);
                                });
                            }
                        });

                        $('#containerDiv').keydown(function (e) {
                            $("#Header").attr("checked", false);
                        });
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
function GetDeliveryReport() {
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    var gcTypes = '';
    if (loginid != "" && counterid) {
        showloading();
        $('#ddlGCType_listbox').find("li").each(function () {
            if ($(this).css('display') == "none")
                gcTypes +=  $(this).find('input')[0].id.split('_')[1] + ",";
            // $(this).find("input").prop("checked",true);
        });
        var fromDate = $("#txtFromDate").val();
        var toDate = $("#txtToDate").val();
        var locationId = $("#ddlLocation").val();


        var isvalid = true;
        if (validatetextbox(fromDate, $('#spnFromDate'), 'Please select from date') == false) {
            isvalid = false;
        }
        if (validatetextbox(toDate, $('#spnToDate'), 'Please select to date') == false) {
            isvalid = false;
        }
        if (validatedropdown(locationId, $('#spnLocation'), 'Please select location') == false) {
            isvalid = false;
        }
        if (validatedropdown(gcTypes, $('#spnGCType'), 'Please select GC type') == false) {
            isvalid = false;
        }


        if (isvalid) {
          
            var input = {};
            input = {
                LoginId: loginid,
                CounterId: counterid,
                FromDate: fromDate,
                ToDate: toDate,
                LocationId: locationId,
                GCTypes: gcTypes,
            };
            $.ajax({
                type: "POST",
                data: (input),
                url: apiurl + "api/reports/getdeliveryreport",
                dataType: "json",
                success: function (data) {
                    //lstBookings = data;
                    BindGrid(data);
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
function BindGrid(data)
{
    var totalBillAmount = 0;
    var totalPieces = 0;
    var totalDeliveryCharges = 0;
    var totalDemoCharges = 0;
    if (data && data.Table && data.Table.length > 0) {
        $('#tbodydeliveryrecords').html('');
        for (var i = 0; i < data.Table.length; i++) {
            totalBillAmount += data.Table[i].BillAmount;
            totalPieces += data.Table[i].Pieces;
            totalDeliveryCharges += data.Table[i].DeliveryCharges;
            totalDemoCharges += data.Table[i].DemoCharges;

            var tr = $('<tr class="trdynamic" />');
            tr.append('' +
                '<td>' + '<span>' + (i + 1) + '</span></td>' +
                '<td><span>' + data.Table[i].GC_No + '</span></td>' +
                '<td><span>' + data.Table[i].FromLocation + '</span></td>' +
                '<td><span>' + data.Table[i].GC_Type + '</span></td>' +
                 '<td><span>' + data.Table[i].ProductType + '</span></td>' +
                '<td><span>' + data.Table[i].Pieces + '</span></td>' +
                  '<td><span>' + data.Table[i].BillAmount + '</span></td>' +
                '<td><span>' + data.Table[i].DeliveryTo + '</span></td>' +
                '<td><span>' + data.Table[i].PhoneNumber + '</span></td>' +
                '<td><span>' + data.Table[i].DeliveryCharges + '</span></td>' +
                 '<td><span>' + data.Table[i].DemoCharges + '</span></td>' +
                '<td><span>' + data.Table[i].Remarks + '</span></td>');
            $('#tbodydeliveryrecords').append(tr);
        }
        var tr = $('<tr class="trdynamic" />');
        tr.append('' +
                '<td colspan="5">Total</td>' +
                '<td><span id="spnTotalPieces_' + data.Table.length + '"  >' + totalPieces + '</span></td>' +
                '<td><span id="spnBillAmount_' + data.Table.length + '"  >' + totalBillAmount + '</span></td>' +
                '<td></td>' +
                '<td></td>' +
                 '<td><span>' + totalDeliveryCharges + '</span></td>' +
                 '<td><span>' + totalDemoCharges + '</span></td>' +
                '<td></td>');
        $('#tbodydeliveryrecords').append(tr);

    }
    else {
        $('#tbodydeliveryrecords').html('');
        var tr = $('<tr class="trdynamic" />');
        tr.append('<td colspan = "12"> No Records Found </td>');
        $('#tbodydeliveryrecords').append(tr);
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
