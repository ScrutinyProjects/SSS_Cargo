var logindata = [];
var loginid = '';

function UserLogin() {
    hideallalerts();
    var textusername = $("#textusername");
    var textpassword = $("#textpassword");
    var spanusernamehelper = $("#spanusernamehelper");
    var spanpasswordhelper = $("#spanpasswordhelper");
    $(spanusernamehelper).html('');
    $(spanpasswordhelper).html('');

    var isvalid = true;

    if (validatephonenumber(textusername) == false) {
        isvalid = false;
        $(spanusernamehelper).html('Invalid username');
    }
    if (validatetextbox(textpassword) == false) {
        isvalid = false;
        $(spanpasswordhelper).html('Invalid password');
    }

    if (isvalid) {
        showloading();

        var logindetails = [];
        logindetails = {
            Username: textusername.val().trim(),
            Password: textpassword.val().trim()
        };

        $.ajax({
            type: "POST",
            data: (logindetails),
            url: apiurl + "api/account/login",
            dataType: "json",
            success: function (data) {
                if (data.StatusId == 1) {
                    logindata = data;
                    loginid = data.LoginId;

                    if (data.IsOTPRequired) {
                        hideotpallalerts();
                        $("#textotp").val('');
                        $("#spanotphelper").html('');
                        $('#modalotp').modal();
                    }
                    else {
                        SaveLoginDetails();
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

function SaveLoginDetails() {
    $.ajax({
        type: "POST",
        data: (logindata),
        url: "/account/savelogindetails",
        dataType: "json",
        success: function (datac) {
            if (datac.StatusId == 1) {
                window.location = "/cargo/index";
            }
            else {
                showwarningalert(datac.StatusMessage);
            }
            hideloading();
        },
        error: function (xhr) {
            hideloading();
            showerroralert(xhr.responseText);
        }
    });
}

function ValidateOTP() {
    hideotpallalerts();
    var textotp = $("#textotp");
    var spanotphelper = $("#spanotphelper");
    $(spanotphelper).html('');

    var isvalid = true;

    if (validatetextbox(textotp) == false) {
        isvalid = false;
        $(spanotphelper).html('Invalid OTP');
    }

    if (isvalid) {
        showloading();

        var logindetails = [];
        logindetails = {
            LoginId: loginid,
            OTP: textotp.val().trim()
        };

        $.ajax({
            type: "POST",
            data: (logindetails),
            url: apiurl + "api/account/validateloginotp",
            dataType: "json",
            success: function (data) {
                if (data.StatusId == 1) {
                    $('#closemodalotp').click();
                    SaveLoginDetails();
                }
                else {
                    showotpwarningalert(data.StatusMessage);
                }
                hideloading();
            },
            error: function (xhr) {
                hideloading();
                showotperroralert(xhr.responseText);
            }
        });
    }
    else {
        return false;
    }
}


function showotpwarningalert(message) {
    $('#divOTPErrorAlert').hide();
    $('#divOTPWarningAlert').show();
    $('#spanOTPWarningAlert').html(message);
}

function showotperroralert(message) {
    $('#divOTPWarningAlert').hide();
    $('#divOTPErrorAlert').show();
    $('#spanOTPErrorAlert').html(message);
}

function hideotpallalerts() {
    $('#divOTPWarningAlert').hide();
    $('#divOTPErrorAlert').hide();
}
