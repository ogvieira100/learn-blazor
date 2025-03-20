window.hideLoading = () => {
    let loading = document.getElementById("loadingIndicator");
    if (loading) {
        loading.style.display = "none";
    }
};

window.messageToast = (message) => {

    alert(message)    

}

window.toastrNotify = (type, message, title) => {
    if (type === "success") {
        toastr.success(message, title);
    } else if (type === "error") {
        toastr.error(message, title);
    } else if (type === "info") {
        toastr.info(message, title);
    } else if (type === "warning") {
        toastr.warning(message, title);
    }
};

