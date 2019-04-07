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
                    $.ajax({
                        type: "POST",
                        data: (data),
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
