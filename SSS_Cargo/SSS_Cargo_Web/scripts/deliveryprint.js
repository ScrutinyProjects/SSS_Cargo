function getdeliverydetails() {
    hideallalerts();
    var deliveryid = $('#hiddendeliveryid').val();

    var input = [];
    input = {
        DeliveryId: deliveryid
    };
    debugger;
    $.ajax({
        type: "POST",
        data: (input),
        url: apiurl + "api/booking/getdeliverydetailstoprintbydeliveryid",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $('.gcnumber').html(data.GCBookingNumber);
                $('.pieces').html(data.TotalPieces);
                $('.weight').html(data.TotalWeight);
                $('.paymenttype').html(data.PaymentType);
                $('.billamount').html(data.BillAmount);
                $('.deliverycharges').html(data.DeliveryCharges);
                $('.democharges').html(data.DemoCharges);
                $('.remarks').html(data.Remarks);
                $('.totalamount').html(data.TotalAmount);
                $('.countername').html(data.CounterName);
            }
            hideloading();
        },
        error: function (xhr) {
            hideloading();
            showerroralert(xhr.responseText);
        }
    });
}