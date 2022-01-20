window.ShowToast = (type,message) => {
    if (type === "success") {
        toastr.success(message, "Success!");
    } else if (type === "error") {
        toastr.error(message, "Operation failed!");
    }
}

function showDeleteConfirm() {
    $("#deleteConfirmationModal").modal('show')
}
function hideDeleteConfirm() {
    $("#deleteConfirmationModal").modal('hide')
}

function showSignoutConfirm() {
    $("#confirmSignOut").appendTo("body").modal('show')
}
function hideSignoutConfirm() {
    $("#confirmSignOut").modal('hide')
}