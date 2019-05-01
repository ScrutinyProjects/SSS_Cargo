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
                        $('#selectgctype').val(1);
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