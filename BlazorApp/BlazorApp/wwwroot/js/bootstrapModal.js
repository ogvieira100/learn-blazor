window.bootstrapModal = {
    show: (id) => {
        var myModal = new bootstrap.Modal(document.getElementById(id));
        myModal.show();
    },
    hide: (id) => {
        var modalElement = document.getElementById(id);
        var modal = bootstrap.Modal.getInstance(modalElement);
        if (modal) {
            modal.hide();
        }
    }
};