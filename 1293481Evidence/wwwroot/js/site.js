function readUrl(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            // Update this selector to match your HTML ID
            $('#createImagePreview').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

// 2. Dynamic Row Addition
function AddItem() {
    var tbody = $("#SaleItemsTable tbody");
    var index = tbody.find("tr").length;

    var newRow = `
            <tr>
                <td><input type="text" name="SaleItems[${index}].MedicineName" class="form-control" required /></td>
                <td><input type="number" name="SaleItems[${index}].Quantity" class="form-control" required /></td>
                <td><input type="number" step="0.01" name="SaleItems[${index}].UnitPrice" class="form-control" required /></td>
                <td><button type="button" class="btn btn-danger" onclick="DeleteItem(this)">Remove</button></td>
            </tr>`;

    tbody.append(newRow);
    $("#hdnLastIndex").val(index + 1);
}

// 3. Row Deletion & Re-indexing
function DeleteItem(btn) {
    $(btn).closest('tr').remove();
    reIndexItems();
}

function reIndexItems() {
    $("#SaleItemsTable tbody tr").each(function (index, row) {
        $(row).find("input").each(function () {
            var name = $(this).attr("name");
            if (name) {
                // Correctly replaces the index part: SaleItems[0].Name -> SaleItems[1].Name
                $(this).attr("name", name.replace(/\[\d+\]/, "[" + index + "]"));
            }
        });
    });
    $("#hdnLastIndex").val($("#SaleItemsTable tbody tr").length);
}

// 4. AJAX Submission
function CreateNewInvoice() {
    var $form = $("#createForm");

    // Ensure client-side validation passes
    if (!$form.valid()) {
        alert("Please fill all required fields.");
        return false;
    }

    var formData = new FormData($form[0]);

    $.ajax({
        url: $form.attr("action"),
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            alert("Invoice saved successfully!");
            window.location.href = "/Invoices/Index";
        },
        error: function (xhr) {
            console.error(xhr.responseText);
            alert("Error saving data. Check console for details.");
        }
    });
}

function UpdateInvoice() {
    var $form = $("#editForm");

    if ($.isFunction($form.valid) && !$form.valid()) {
        return false;
    }

    var formData = new FormData($form[0]);

    $.ajax({
        url: $form.attr("action"),
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            alert("Invoice updated successfully!");
            window.location.href = "/Invoices/Index";
        },
        error: function () {
            alert("Error updating data.");
        }
    });
}