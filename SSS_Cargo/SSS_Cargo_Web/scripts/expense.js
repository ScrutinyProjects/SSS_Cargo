function getExpenseTypes() {
   
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if (loginid != "" && counterid) {
        showloading();
        $.ajax({
            type: "GET",
            url: apiurl + "api/expense/getexpensetypes",
            dataType: "json",
            success: function (data) {
                if (data && data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        var option = '<option value="' + data[i].ExpenseTypeId + '">' + data[i].ExpenseType + '</option>';
                            $('#ddlExpenseType').append(option);
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
function applyDatePicker()
{
    $('#txtDateOfExpense').datepicker({
        changeMonth: true,
        changeYear: true,
        format: "dd/mm/yyyy",
        language: "tr"
    });
}
function getExpenses(dateOfExpense)
{
   
    var loginid = 0, counterid = 0;
    if ($("#hiddenloginid"))
         loginid = $("#hiddenloginid").val();
    if ($("#hiddencounterid"))
        counterid = $("#hiddencounterid").val();
    if (loginid && counterid) {
        showloading();
        var url = apiurl;
        if (dateOfExpense)
            url = apiurl + "api/expense/" + loginid + "/" + counterid + "/getexpenses?dateOfExpense=" + dateOfExpense;
        else
            url = apiurl + "api/expense/" + loginid + "/" + counterid + "/getexpenses";
        $.ajax({
            type: "GET",
            url: url,
            dataType: "json",
            success: function (data) {
                if (data && data.length > 0) {
                    $("#lblGridMessage").css("display", "none");
                    $("#divExpenses").css({ display: "" });
                    $("#gridExpenses").kendoGrid({
                        dataSource: { data: data, pageSize: 5 },
                        height: 250,
                        sortable: true,
                        pageable: {
                            refresh: true,
                            pageSizes: true,
                            buttonCount: 5
                        },
                        columns:GetColumns(),
                    });

                }
                else {
                    $("#lblGridMessage").css("display", "");
                    if ($("#gridExpenses").data('kendoGrid'))
                        $("#gridExpenses").data('kendoGrid').dataSource.data([]);
                    $("#divExpenses").css({ display: "none" });
                }
                hideloading();
            },
            error: function (xhr) {
                hideloading();
                showerroralert(xhr.responseText);
            }
        });

        function GetColumns()
        {
            var editable = $("#hiddeniseditable").val();
            var deletable = $("#hiddenisdeletable").val();
            var columns = [];

            columns = [{
                field: "ExpenseType",
                title: "Expense Type",
                width: 150,
                media: "(min-width: 650px)"
            }, {
                field: "Amount",
                title: "Amount",
                width: 100,
                media: "(min-width: 450px)"
            }, {
                field: "DateOfExpense",
                title: "Date Of Expense",
                type: 'date',
                format: '{0:MM/dd/yyyy}',
                width: 150,
                media: "(min-width: 450px)"
            }, {
                field: "CreatedDate",
                title: "Transaction Date",
                type: "date",
                format: "{0:MM/dd/yyyy}",
                width: 150,
                media: "(min-width: 750px)"
            }, {
                field: "Remarks",
                title: "Remarks",
                width: 150,
                media: "(min-width: 650px)"
            }, {
                field: "EditRemarks",
                title: "Edit Remarks",
                width: 150,
                media: "(min-width: 450px)"
            }];

            if (editable || deletable) {
                var commands = [];
                if (editable)
                    commands.push({
                        name: "Edit", click: function (e) {
                            var grid = $("#gridExpenses").data("kendoGrid");
                            var rowData = grid.dataItem($(e.target).parent().closest("tr"));

                            $('#ddlExpenseType').val(rowData.ExpenseTypeId);
                            $('#txtAmount').val(rowData.Amount);
                            $('#txtExpenseTo').val(rowData.ExpenseTo);
                            $('#txtContactNumber').val(rowData.ContactNumber);
                            $('#txtDateOfExpense').val($.datepicker.formatDate('mm/dd/yy', new Date(rowData.DateOfExpense)));
                            $('#txtRemarks').val(rowData.EditRemarks);
                            $('#hiddenExpenseId').val(rowData.ExpenseId);
                            $("#btnUpdate").css({ display: '' });
                            $("#btnSave").css({ display: 'none' });


                            $('#ddlExpenseType').prop("disabled", true);
                            $('#txtExpenseTo').prop("disabled", true);
                            $('#txtContactNumber').prop("disabled", true);
                            $('#txtDateOfExpense').prop("disabled", true);
                            //$("#editModal").modal('show');
                        }
                    });
                if (deletable)
                    commands.push({
                        name: "Delete", click: function (e) {
                            if (confirm("Are you sure do you want to delete?")) {
                                var grid = $("#gridExpenses").data("kendoGrid");
                                var rowData = grid.dataItem($(e.target).parent().closest("tr"));

                                $.ajax({
                                    type: "GET",
                                    url: apiurl + "api/expense/" + rowData.ExpenseId + "/deleteexpense",
                                    dataType: "json",
                                    success: function (data) {
                                        if (data) {
                                            if (data.StatusId == 1) {
                                                showsuccessalert(data.StatusMessage);
                                                getExpenses();
                                            }
                                            else if (data.StatusId == 2)
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

                        }
                    });
                columns.push({
                    title: "Actions",
                    command: commands,
                })
            }

            return columns;

        }
    }
}
function GetExpensesByDate()
{
    getExpenses($("#txtDateOfExpense").val());
}
function LockExpense()
{
    var loginid = $("#hiddenloginid").val();
    var counterid = $("#hiddencounterid").val();
    if ($("#txtDateOfExpense").val() && loginid && counterid) {
        $.ajax({
            type: "GET",
            url: apiurl + "api/expense/" + loginid + "/" + counterid + "/lockexpenses?dateOfExpense=" + $("#txtDateOfExpense").val(),
            dataType: "json",
            success: function (data) {
                if (data) {
                    if (data.StatusId == 1) {
                        showsuccessalert(data.StatusMessage);
                    }
                    else if (data.StatusId == 2)
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
    else
        return false;

}
function validatetextbox(value, span, message) {
    if (value == "") {
        $(span).html(message);
        return false;
    }
}

function validatedropdown(value, span, message) {
    if (value == "0" || value == "") {
        $(span).html(message);
        return false;
    }
}

function clearallfields() {
    $('#ddlExpenseType').val('0');
    $('#txtAmount').val('');
    $('#txtExpenseTo').val('');
    $('#txtContactNumber').val('');
    $('#txtDateOfExpense').val('');
    $('#txtRemarks').val('');

    $('#spnExpenseType').html("");
    $('#spnAmount').html("");
    $('#spnExpenseTo').html("");
    $('#spnContactNumber').html("");
    $('#spnDateOfExpense').html("");
    $('#spnRemarks').html('');
    $('#hiddenExpenseId').val(0);
    $("#btnUpdate").css({ display: 'none' });
    $("#btnSave").css({ display: '' });
    $('#ddlExpenseType').prop("disabled", false);
    $('#txtExpenseTo').prop("disabled", false);
    $('#txtContactNumber').prop("disabled", false);
    $('#txtDateOfExpense').prop("disabled", false);
}

function SaveExpense(isVerified) {
    var isvalid = true;

    var loginid = $('#hiddenloginid').val().trim();
    var counterid = $('#hiddencounterid').val().trim();
    
    var expenseType = $('#ddlExpenseType').val();
    var amount = $('#txtAmount').val();
    var expenseTo = $('#txtExpenseTo').val().trim();
    var contactNumber = $('#txtContactNumber').val().trim();
    var dateOfExpense = $('#txtDateOfExpense').val().trim();
    var remarks = $('#txtRemarks').val().trim();
    var expenseId = $('#hiddenExpenseId').val();

    if (validatedropdown(expenseType, $('#spnExpenseType'), 'Please select Expense Type') == false) {
        isvalid = false;
    }
    if (validatetextbox(amount, $('#spnAmount'), 'Please enter Amount') == false) {
        isvalid = false;
    }
    if (validatetextbox(expenseTo, $('#spnExpenseTo'), 'Please enter Expense To') == false) {
        isvalid = false;
    }
    if (validatetextbox(contactNumber, $('#spnContactNumber'), 'Please enter Contact Number') == false) {
        isvalid = false;
    }
    if (validatetextbox(dateOfExpense, $('#spnDateOfExpense'), 'Please enter Date of Expense') == false) {
        isvalid = false;
    }
    if (validatetextbox(remarks, $('#spnRemarks'), 'Please enter remarks') == false) {
        isvalid = false;
    }

    if (isvalid) {
        showloading();
        if (!isVerified)
            isVerified = false;
        var input = {};
        input = {
            ExpenseId: expenseId,
            ExpenseTypeId: expenseType,
            Amount: amount,
            ExpenseTo: expenseTo,
            ContactNumber: contactNumber,
            DateOfExpense: dateOfExpense,
            Remarks: remarks,
            CreatedBy: loginid,
            CounterId: counterid,
            EditRemarks : remarks
        };

        $.ajax({
            type: "POST",
            data: (input),
            url: apiurl + "api/expense/saveexpense?isVerified="+ isVerified,
            dataType: "json",
            success: function (data) {
                if (data.objSaveResponse && data.objSaveResponse.StatusId == 1) {
                    showsuccessalert(data.objSaveResponse.StatusMessage);
                    getExpenses();
                    clearallfields();
                    $('#hiddenExpenseId').val(0);
                    if (isVerified) {
                        $("#saveModal").modal('hide');
                    }
                    $("#btnUpdate").css({ display: 'none' });
                    $("#btnSave").css({ display: '' });
                }
                else if (data.objSaveResponse.StatusId == 3) {
                    showwarningalert(data.objSaveResponse.StatusMessage);
                }
                else if (data.objSaveResponse.StatusId == 2 && data.objExpenseRequest) {
                    $("#lblExpenseType").text(data.objExpenseRequest.ExpenseType);
                    $("#lblAmount").text(data.objExpenseRequest.Amount);
                    $("#lblRemarks").text(data.objExpenseRequest.Remarks);
                    $("#saveModal").modal('show');
                    $("#btnCloseSaveModal, #btnExpenseCancel").unbind().click(function () {
                        $("#saveModal").modal('hide');
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
}
